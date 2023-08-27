using Bogus;
using Domain;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Scripts;
using Infrastructure.DataAccess.Dtos;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.DataAccess.Scripts;
using Infrastructure.DataAccess.SqlServer.Context;
using Moq;
using Xunit;

namespace UnitTests.Infrastructure.Repositories
{
    public class LoginRepositoryTests
    {
        private readonly Mock<IDbConnectionWrapper> _dbConnectionWrapperMock;
        private readonly ILoginScripts _scripts;
        private readonly ILoginRepository _repository;
        private readonly Faker _faker;

        public LoginRepositoryTests()
        {
            _dbConnectionWrapperMock = new Mock<IDbConnectionWrapper>();
            _scripts = new LoginScripts();
            _repository = new LoginRepository(_dbConnectionWrapperMock.Object, _scripts);
            _faker = new Faker();
        }

        [Fact]
        public async Task ShouldInsertUserSuccessfully()
        {
            // Given
            var user = new LoginUser(
                _faker.Random.String2(10),
                _faker.Random.String2(10),
                _faker.Random.String2(10));

            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _repository.InsertUserAsync(user, cancellationToken);

            // Assert
            _dbConnectionWrapperMock.Verify(x => x.QuerySingleAsync<int>(
                _scripts.InsertUserAsync, user, cancellationToken, null), Times.Once);
        }

        [Fact]
        public async Task ShouldGetUserByIdSuccessfully()
        {
            // Given
            var id = _faker.Random.Number(10);
            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _repository.GetUserByIdAsync(id, cancellationToken);

            // Assert
            _dbConnectionWrapperMock.Verify(x => x.QuerySingleOrDefaultAsync<LoginDto>(
                _scripts.GetUserByIdAsync, It.IsAny<object>(), cancellationToken, null), Times.Once);
        }

        [Fact]
        public async Task ShouldChangeUserPermissionSuccessfully()
        {
            // Given
            var id = _faker.Random.Number(10);
            var roles = _faker.Random.String2(10);
            var cancellationToken = CancellationToken.None;

            // Act
            await _repository.ChangeUserPermissionAsync(id, roles, cancellationToken);

            // Assert
            _dbConnectionWrapperMock.Verify(x => x.ExecuteAsync(
                _scripts.ChangeUserPermissionAsync, It.IsAny<object>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task ShouldGetUserByNameSuccessfully()
        {
            // Given
            var name = _faker.Random.String2(10);
            var cancellationToken = CancellationToken.None;

            // Act
            var output = await _repository.GetUserByNameAsync(name, cancellationToken);

            // Assert
            _dbConnectionWrapperMock.Verify(x => x.QuerySingleOrDefaultAsync<LoginDto>(
                _scripts.GetUserByNameAsync, It.IsAny<object>(), cancellationToken, null), Times.Once);
        }

        [Fact]
        public async Task ShouldMapUserSuccessfully()
        {
            // Given
            var id = _faker.Random.Number(10);
            var cancellationToken = CancellationToken.None;
            var fakeDto = CreateFakeLoginDto();

            _dbConnectionWrapperMock
                .Setup(x => x.QuerySingleOrDefaultAsync<LoginDto>(_scripts.GetUserByIdAsync, It.IsAny<object>(), cancellationToken, null))
                .ReturnsAsync(fakeDto);

            // Act
            var output = await _repository.GetUserByIdAsync(id, cancellationToken);

            // Assert
            output.Name.Equals(fakeDto.Name);
            output.Id.Equals(fakeDto.Id);
            output.Roles.Equals(fakeDto.Roles);
            output.Password.Equals(fakeDto.Password);
        }

        private LoginDto CreateFakeLoginDto() => new LoginDto
        {
            Id = _faker.Random.Number(10),
            Name = _faker.Random.String2(10),
            Password = _faker.Random.String2(10),
            Roles = _faker.Random.String2(10)
        };
    }
}