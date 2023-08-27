using Bogus;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Application.UseCases.ChangeUserPermission;
using Application.UseCases.ChangeUserPermission.Commands;
using Application.UseCases.ChangeUserPermission.Validation;
using Domain;
using Xunit;
using FluentAssertions;

namespace UnitTests.Application.UseCases
{
    public class ChangeUserPermissionUseCaseTests
    {
        private readonly Mock<ILogger<ChangeUserPermissionUseCase>> _loggerMock;
        private readonly Mock<ILoginRepository> _repositoryMock;
        private readonly IValidator<UpdatePermissionCommand> _validator;
        private readonly IChangeUserPermissionUseCase _useCase;

        private readonly Faker _faker;

        public ChangeUserPermissionUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<ChangeUserPermissionUseCase>>();
            _repositoryMock = new Mock<ILoginRepository>();
            _validator = new UpdatePermissionCommandValidator();

            _useCase = new ChangeUserPermissionUseCase(
                _loggerMock.Object,
                _repositoryMock.Object,
                _validator);

            _faker = new Faker();
        }

        [Fact]
        public async Task ShouldReturnErrorValidationIfCommandIsEmpty()
        {
            // Given
            var command = new UpdatePermissionCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("'Id' must be greater than '0'.");
        }

        [Fact]
        public async Task ShouldLogErrorWhenUserNotExists()
        {
            // Given
            var command = new UpdatePermissionCommand() { Id = 1 };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.GetUserByIdAsync(command.Id, cancellationToken))
                .ReturnsAsync((LoginUser)null);

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("User does not exist");
        }

        [Fact]
        public async Task ShouldChangePermissionSuccessfully()
        {
            // Given
            var command = new UpdatePermissionCommand() { Id = 1 };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.GetUserByIdAsync(command.Id, cancellationToken))
                .ReturnsAsync(CreateFakeUser());

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeTrue();
            output.Result.Should().Be("Login permission updated successfully");
        }

        [Fact]
        public async Task ShouldLogErrorWhenTrownException()
        {
            // Given
            var command = new UpdatePermissionCommand() { Id = 1 };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.GetUserByIdAsync(command.Id, cancellationToken))
                .Throws(new Exception("ex"));

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("An unexpected error occurred while updating the user permission");
        }

        private LoginUser CreateFakeUser() => new LoginUser(
            _faker.Random.String2(2),
            _faker.Random.String2(2),
            _faker.Random.String2(2));
    }
}
