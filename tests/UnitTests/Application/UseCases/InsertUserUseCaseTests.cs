using Application.UseCases.InsertUser;
using Application.UseCases.InsertUser.Commands;
using Application.UseCases.InsertUser.Validation;
using Bogus;
using Domain;
using Domain.Interfaces.Entity;
using Domain.Interfaces.Repositories;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCases
{
    public class InsertUserUseCaseTests
    {
        private readonly Mock<ILogger<InsertUserUseCase>> _loggerMock;
        private readonly Mock<ILoginRepository> _repositoryMock;
        private readonly IValidator<InsertUserCommand> _validator;
        private readonly Mock<IEntityFactory> _entityFactoryMock;
        private readonly IInsertUserUseCase _useCase;

        private readonly Faker _faker;

        public InsertUserUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<InsertUserUseCase>>();
            _repositoryMock = new Mock<ILoginRepository>();
            _validator = new InsertUserCommandValidator();
            _entityFactoryMock = new Mock<IEntityFactory>();

            _useCase = new InsertUserUseCase(
                _loggerMock.Object,
                _repositoryMock.Object,
                _validator,
                _entityFactoryMock.Object);

            _faker = new Faker();
        }

        [Fact]
        public async Task ShouldReturnErrorValidationIfCommandIsEmpty()
        {
            // Given
            var command = new InsertUserCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("'Name' must not be empty.");
            output.ErrorMessages.Should().ContainEquivalentOf("'Password' must not be empty.");
        }

        [Fact]
        public async Task ShouldInsertSuccessfullyIfCommandIsValid()
        {
            // Given
            var command = CreateCommand();
            var cancellationToken = CancellationToken.None;
            var id = _faker.Random.Number();

            _repositoryMock
                .Setup(x => x.InsertUserAsync(It.IsAny<ILoginUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            _entityFactoryMock
                .Setup(x => x.NewLoginUser(command.Name, command.Password, command.Roles))
                .Returns(new LoginUser(command.Name, command.Password, command.Roles));

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeTrue();
            output.Result.Should().Be($"User inserted; Cod: {id}; Name: {command.Name}");
        }

        [Fact]
        public async Task ShouldLogErrorWhenThrowException()
        {
            // Given
            var command = CreateCommand();
            var cancellationToken = CancellationToken.None;
            var id = _faker.Random.Number();

            _repositoryMock
                .Setup(x => x.InsertUserAsync(It.IsAny<ILoginUser>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("ex"));

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("An unexpected error occurred while inserting the user");
        }

        private InsertUserCommand CreateCommand() => new InsertUserCommand
        {
            Name = _faker.Random.String2(10),
            Password = _faker.Random.String2(10),
            Roles = _faker.Random.String2(10)
        };
    }
}
