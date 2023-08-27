using Application.UseCases.InsertCar;
using Application.UseCases.InsertCar.Commands;
using Application.UseCases.InsertCar.Validation;
using Bogus;
using Domain;
using Domain.Interfaces.Entity;
using Domain.Interfaces.Repositories;
using Domain.ValueObjects;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCases
{
    public class InsertCarUseCaseTests
    {
        private readonly Mock<ILogger<InsertCarUseCase>> _loggerMock;
        private readonly Mock<ICarRepository> _repositoryMock;
        private readonly IValidator<InsertCarCommand> _validator;
        private readonly Mock<IEntityFactory> _entityFactoryMock;
        private readonly IInsertCarUseCase _useCase;

        private readonly Faker _faker;

        public InsertCarUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<InsertCarUseCase>>();
            _repositoryMock = new Mock<ICarRepository>();
            _validator = new InsertCarCommandValidator();
            _entityFactoryMock = new Mock<IEntityFactory>();

            _useCase = new InsertCarUseCase(
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
            var command = new InsertCarCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf($"'Model' must not be empty.");
            output.ErrorMessages.Should().ContainEquivalentOf($"'Brand' must not be empty.");
            output.ErrorMessages.Should().ContainEquivalentOf($"'Year' must be greater than '1500'.");
        }

        [Fact]
        public async Task ShouldInsertSuccessfullyIfCommandIsValid()
        {
            // Given
            var command = CreateCommand();
            var cancellationToken = CancellationToken.None;
            var id = _faker.Random.Number();
            var correlationId = Guid.NewGuid();
            var user = new User(command.UserId, command.UserName);

            _repositoryMock
                .Setup(x => x.InsertCarAsync(It.IsAny<ICar>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            _entityFactoryMock
                .Setup(x => x.NewCar(command.Brand, command.Model, command.Year, user, user))
                .Returns(new Car(correlationId, command.Brand, command.Model, command.Year, user, user));

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeTrue();
            output.Result.Should().Be($"Car inserted; Cod: {id}; CorrelationId: {correlationId}");
        }

        [Fact]
        public async Task ShouldLogErrorWhenThrowException()
        {
            // Given
            var command = CreateCommand();
            var cancellationToken = CancellationToken.None;
            var id = _faker.Random.Number();

            _repositoryMock
                .Setup(x => x.InsertCarAsync(It.IsAny<ICar>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("ex"));

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf($"An unexpected error occurred while inserting the car.");
        }

        private InsertCarCommand CreateCommand() => new InsertCarCommand
        {
            Brand = _faker.Random.String2(2),
            Model = _faker.Random.String2(2),
            Year = _faker.Random.Number(2000, 2020),
            UserName = _faker.Random.String2(2),
            UserId = _faker.Random.Number()
        };
    }
}
