using Bogus;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Application.UseCases.PerformLogin;
using Application.UseCases.PerformLogin.Commands;
using Application.UseCases.PerformLogin.Validation;
using Application.UseCases.PerformLogin.Services;
using Application.Shared.Extensions;
using Xunit;
using FluentAssertions;
using Domain;

namespace UnitTests.Application.UseCases
{
    public class PerformLoginUseCaseTests
    {
        private readonly Mock<ILogger<PerformLoginUseCase>> _loggerMock;
        private readonly Mock<ILoginRepository> _repositoryMock;
        private readonly IValidator<UserLoginCommand> _validator;
        private readonly Mock<IAuthenticationService> _authenticationServiceMock;
        private readonly IPerformLoginUseCase _useCase;

        private readonly Faker _faker;

        public PerformLoginUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<PerformLoginUseCase>>();
            _repositoryMock = new Mock<ILoginRepository>();
            _authenticationServiceMock = new Mock<IAuthenticationService>();
            _validator = new UserLoginCommandValidator();

            _useCase = new PerformLoginUseCase(
                _loggerMock.Object,
                _repositoryMock.Object,
                _validator,
                _authenticationServiceMock.Object);

            _faker = new Faker();
        }

        [Fact]
        public async Task ShouldReturnErrorValidationIfCommandIsEmpty()
        {
            // Given
            var command = new UserLoginCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("'Name' must not be empty.");
            output.ErrorMessages.Should().ContainEquivalentOf("'Password' must not be empty.");
        }

        [Fact]
        public async Task ShouldLogErrorWhenUserNotExists()
        {
            // Given
            var command = new UserLoginCommand() { Name = "xpto", Password = "123456" };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.GetUserByNameAsync(command.Name, cancellationToken))
                .ReturnsAsync((LoginUser)null);

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("User does not exist");
        }

        [Fact]
        public async Task ShouldLogErrorWhenPasswordIsWrong()
        {
            // Given
            var command = new UserLoginCommand() { Name = "xpto", Password = "123456" };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.GetUserByNameAsync(command.Name, cancellationToken))
                .ReturnsAsync(CreateFakeUser());

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("User does not exist");
        }

        [Fact]
        public async Task ShouldCreateTokenSuccessfully()
        {
            // Given
            var command = new UserLoginCommand() { Name = "xpto", Password = "123456" };
            var cancellationToken = CancellationToken.None;
            var expectedToken = _faker.Random.String2(50);

            _repositoryMock
                .Setup(x => x.GetUserByNameAsync(command.Name, cancellationToken))
                .ReturnsAsync(CreateFakeUser(command.Name, command.Password.CreateSHA256Hash()));

            _authenticationServiceMock
                .Setup(x => x.CreateToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(expectedToken);

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeTrue();
            output.Result.Should().Be(expectedToken);
        }

        [Fact]
        public async Task ShouldLogErrorWhenThrownAnException()
        {
            // Given
            var command = new UserLoginCommand() { Name = "xpto", Password = "123456" };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.GetUserByNameAsync(command.Name, cancellationToken))
                .Throws(new Exception("ex"));

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("An unexpected error has occurred");
        }

        private LoginUser CreateFakeUser(string? name = null, string? password = null) => new LoginUser(
            name ?? _faker.Random.String2(2),
            password ?? _faker.Random.String2(2),
            _faker.Random.String2(2));
    }
}
