using Bogus;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using Application.UseCases.SearchCar;
using Application.UseCases.SearchCar.Commands;
using Application.UseCases.SearchCar.Validation;
using Domain.ValueObjects;
using Domain;
using Xunit;
using FluentAssertions;

namespace UnitTests.Application.UseCases
{
    public class SearchCarUseCaseTests
    {
        private readonly Mock<ILogger<SearchCarUseCase>> _loggerMock;
        private readonly Mock<ICarRepository> _repositoryMock;
        private readonly IValidator<SearchCarCommand> _validator;
        private readonly ISearchCarUseCase _useCase;

        private readonly Faker _faker;

        public SearchCarUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<SearchCarUseCase>>();
            _repositoryMock = new Mock<ICarRepository>();
            _validator = new SearchCarCommandValidator();

            _useCase = new SearchCarUseCase(
                _loggerMock.Object,
                _repositoryMock.Object,
                _validator);

            _faker = new Faker();
        }

        [Fact]
        public async Task ShouldReturnErrorValidationIfCommandIsEmpty()
        {
            // Given
            var command = new SearchCarCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("'Term' must not be empty.");
        }

        [Fact]
        public async Task ShouldSearchCarSuccessfully()
        {
            // Given
            var command = new SearchCarCommand() { Term = "civic" };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.SearchCarAsync(command.Term, cancellationToken))
                .ReturnsAsync(new List<ICar>() { CreateFakeCar() });

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldLogErrorWhenTrownException()
        {
            // Given
            var command = new SearchCarCommand() { Term = "civic" };
            var cancellationToken = CancellationToken.None;

            _repositoryMock
                .Setup(x => x.SearchCarAsync(command.Term, cancellationToken))
                .Throws(new Exception("ex"));

            // Act
            var output = await _useCase.ExecuteAsync(command, cancellationToken);

            // Assert
            output.IsValid.Should().BeFalse();
            output.ErrorMessages.Should().ContainEquivalentOf("An unexpected error occurred while searching the car");
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
