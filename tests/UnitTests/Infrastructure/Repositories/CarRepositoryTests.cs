using Bogus;
using Domain;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Scripts;
using Domain.ValueObjects;
using Infrastructure.DataAccess.Dtos;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.DataAccess.Scripts;
using Infrastructure.DataAccess.SqlServer.Context;
using Moq;
using Xunit;

namespace UnitTests.Infrastructure.Repositories
{
    public class CarRepositoryTests
    {
        private readonly Mock<IDbConnectionWrapper> _dbConnectionWrapperMock;
        private readonly ICarScripts _scripts;
        private readonly ICarRepository _repository;
        private readonly Faker _faker;

        public CarRepositoryTests()
        {
            _dbConnectionWrapperMock = new Mock<IDbConnectionWrapper>();
            _scripts = new CarScripts();
            _repository = new CarRepository(_dbConnectionWrapperMock.Object, _scripts);
            _faker = new Faker();
        }

        [Fact]
        public async Task ShouldInsertCarAsyncSuccessfully()
        {
            // Given
            var user = new User(_faker.Random.Number(), _faker.Random.String2(10));
            var car = new Car(
                _faker.Random.Guid(),
                _faker.Random.String2(10),
                _faker.Random.String2(10),
                _faker.Random.Number(),
                user, user);

            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _repository.InsertCarAsync(car, cancellationToken);

            // Assert
            _dbConnectionWrapperMock.Verify(x => x.QuerySingleAsync<int>(
                _scripts.InsertCarAsync, It.IsAny<object>(), cancellationToken, null), Times.Once);
        }

        [Fact]
        public async Task ShouldSearchCarSuccessfully()
        {
            // Given
            var term = _faker.Random.String2(10);
            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _repository.SearchCarAsync(term, cancellationToken);

            // Assert
            _dbConnectionWrapperMock.Verify(x => x.QueryAsync<CarDto>(
                _scripts.SearchCarAsync, It.IsAny<object>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task ShouldGetCarByIdSuccessfully()
        {
            // Given
            var id = _faker.Random.Number(10);
            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _repository.GetCarByIdAsync(id, cancellationToken);

            // Assert
            _dbConnectionWrapperMock.Verify(x => x.QuerySingleOrDefaultAsync<CarDto>(
                _scripts.GetCarByIdAsync, It.IsAny<object>(), cancellationToken, null), Times.Once);
        }

        [Fact]
        public async Task ShouldDeleteCarSuccessfully()
        {
            // Given
            var id = _faker.Random.Number(10);
            var cancellationToken = CancellationToken.None;

            // Act
            await _repository.DeleteCarAsync(id, cancellationToken);

            // Assert
            _dbConnectionWrapperMock.Verify(x => x.ExecuteAsync(
                _scripts.DeleteCarAsync, It.IsAny<object>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdateCarSuccessfully()
        {
            // Given
            var id = _faker.Random.Number(10);
            var model = _faker.Random.String2(10);
            var brand = _faker.Random.String2(10);
            var year = _faker.Random.Number(10);
            var codUser = _faker.Random.Number(10);
            var cancellationToken = CancellationToken.None;

            // Act
            await _repository.UpdateCarAsync(id, model, brand, year, codUser, cancellationToken);

            // Assert
            _dbConnectionWrapperMock.Verify(x => x.ExecuteAsync(
                _scripts.UpdateCarAsync, It.IsAny<object>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task ShouldMapCarCollectionSuccessfully()
        {
            // Given
            var term = _faker.Random.String2(10);
            var cancellationToken = CancellationToken.None;
            var fakeDto = CreateFakeCarDto();
            var fakeDtoList = new List<CarDto>() { fakeDto };

            _dbConnectionWrapperMock
                .Setup(x => x.QueryAsync<CarDto>(_scripts.SearchCarAsync, It.IsAny<object>(), cancellationToken))
                .ReturnsAsync(fakeDtoList);

            // Act
            var output = await _repository.SearchCarAsync(term, cancellationToken);

            // Assert
            var outputFirst = output.First();

            outputFirst.Id.Equals(fakeDto.Id);
            outputFirst.Model.Equals(fakeDto.Model);
            outputFirst.Brand.Equals(fakeDto.Brand);
            outputFirst.CorrelationId.Equals(fakeDto.CorrelationId);
            outputFirst.CreatedBy.Id.Equals(fakeDto.CreatedUserId);
            outputFirst.CreatedBy.Name.Equals(fakeDto.CreatedUserName);
            outputFirst.UpdatedBy.Id.Equals(fakeDto.UpdatedUserId);
            outputFirst.UpdatedBy.Name.Equals(fakeDto.UpdatedUserName);
            outputFirst.Year.Equals(fakeDto.Year);
        }

        [Fact]
        public async Task ShouldMapCarSuccessfully()
        {
            // Given
            var id = _faker.Random.Number(10);
            var cancellationToken = CancellationToken.None;
            var fakeDto = CreateFakeCarDto();

            _dbConnectionWrapperMock
                .Setup(x => x.QuerySingleOrDefaultAsync<CarDto>(_scripts.GetCarByIdAsync, It.IsAny<object>(), cancellationToken, null))
                .ReturnsAsync(fakeDto);

            // Act
            var output = await _repository.GetCarByIdAsync(id, cancellationToken);

            // Assert
            output.Id.Equals(fakeDto.Id);
            output.Model.Equals(fakeDto.Model);
            output.Brand.Equals(fakeDto.Brand);
            output.CorrelationId.Equals(fakeDto.CorrelationId);
            output.CreatedBy.Id.Equals(fakeDto.CreatedUserId);
            output.CreatedBy.Name.Equals(fakeDto.CreatedUserName);
            output.UpdatedBy.Id.Equals(fakeDto.UpdatedUserId);
            output.UpdatedBy.Name.Equals(fakeDto.UpdatedUserName);
            output.Year.Equals(fakeDto.Year);
        }

        private CarDto CreateFakeCarDto() => new CarDto
        {
            Id = _faker.Random.Number(10),
            Brand = _faker.Random.String2(10),
            Model = _faker.Random.String2(10),
            Year = _faker.Random.Number(10),
            CorrelationId = _faker.Random.Guid(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CreatedUserName = _faker.Random.String2(10),
            UpdatedUserName = _faker.Random.String2(10),
            CreatedUserId = _faker.Random.Number(10),
            UpdatedUserId = _faker.Random.Number(10)            
        };
    }
}