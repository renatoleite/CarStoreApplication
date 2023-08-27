using Bogus;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FluentAssertions;
using Domain;
using Domain.ValueObjects;
using Application.UseCases.UpdateCar.Commands;
using Application.UseCases.UpdateCar;
using Application.UseCases.UpdateCar.Validation;

namespace UnitTests.Application.UseCases
{
    public class UpdateCarUseCaseTests
    {
        private readonly Mock<ILogger<UpdateCarUseCase>> _loggerMock;
        private readonly Mock<ICarRepository> _repositoryMock;
        private readonly IValidator<UpdateCarCommand> _validator;
        private readonly IUpdateCarUseCase _useCase;

        private readonly Faker _faker;

        public UpdateCarUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<UpdateCarUseCase>>();
            _repositoryMock = new Mock<ICarRepository>();
            _validator = new UpdateCarCommandValidator();

            _useCase = new UpdateCarUseCase(
                _loggerMock.Object,
                _repositoryMock.Object,
                _validator);

            _faker = new Faker();
        }

        [Fact]
        public async Task ShouldReturnErrorValidationIfCommandIsEmpty()
        {
            // Given
            var command = new UpdateCarCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("'Id' must be greater than '0'.");
        }

        [Fact]
        public async Task ShouldLogErrorWhenCarNotExists()
        {
            // Given
            var command = new UpdateCarCommand() { Id = 1 };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.GetCarByIdAsync(command.Id, cancellationToken))
                .ReturnsAsync((Car)null);

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("Car does not exist");
        }

        [Fact]
        public async Task ShouldUpdateCarSuccessfully()
        {
            // Given
            var command = new UpdateCarCommand() { Id = 1, Year = 2000 };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.GetCarByIdAsync(command.Id, cancellationToken))
                .ReturnsAsync(CreateFakeCar());

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeTrue();
            output.Result.Should().Be("Car updated successfully");
        }

        [Fact]
        public async Task ShouldLogErrorWhenTrownException()
        {
            // Given
            var command = new UpdateCarCommand() { Id = 1 };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.GetCarByIdAsync(command.Id, cancellationToken))
                .Throws(new Exception("ex"));

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("An unexpected error occurred while updating the car");
        }

        private Car CreateFakeCar()
        {
            var user = new User(_faker.Random.Number(), _faker.Random.String2(10));
            return new Car(
                Guid.NewGuid(),
                _faker.Random.String2(10),
                _faker.Random.String2(10),
                _faker.Random.Number(),
                user, user);
        }
    }
}
