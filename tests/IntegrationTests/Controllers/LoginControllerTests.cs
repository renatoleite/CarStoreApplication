using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;
using WebApi.Models;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class LoginControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly Faker _faker;

        public LoginControllerTests(WebApplicationFactory<Program> factory)
        {
            var appFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Development");
            });

            _httpClient = appFactory.CreateClient();
            _faker = new Faker();
        }

        [Fact]
        public async Task ShouldPerformLoginSuccessfully()
        {
            // Given
            var input = new LoginUserInput
            {
                UserName = "admin",
                Password = "123456"
            };

            var data = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync($"api/login", data);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldCreateUserSuccessfully()
        {
            // Given
            var input = new InsertUserInput
            {
                UserName = _faker.Random.String2(5),
                Password = _faker.Random.String2(5),
                Roles = "create;read;update;delete"
            };

            var data = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync($"api/login/create", data);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldUpdateUserAdminPermissionsSuccessfully()
        {
            // Given
            var adminUserId = 1;

            var input = new UpdatePermissionInput
            {
                Roles = "create;read;update;delete;test"
            };

            var data = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PatchAsync($"api/login/{adminUserId}", data);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
