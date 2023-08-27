using Bogus;
using Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WebApi.Models;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class CarControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly Faker _faker;

        public CarControllerTests(WebApplicationFactory<Program> factory)
        {
            var appFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Development");
            });

            _httpClient = appFactory.CreateClient();
            _faker = new Faker();
        }

        [Fact]
        public async Task ShouldReturnsUnauthorizedWhenTryingInsertCar()
        {
            // Given
            var input = new InsertCarInput();

            var data = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync($"api/car", data);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task ShouldCreateCarSuccessfully()
        {
            // Given
            var adminAccessToken = await GetAdminAccessToken();
            var input = new InsertCarInput
            {
                Brand = _faker.Random.String2(5),
                Model = _faker.Random.String2(5),
                Year = _faker.Random.Number(2000, 2023)
            };

            var data = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", adminAccessToken);

            // Act
            var response = await _httpClient.PostAsync($"api/car", data);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldSearchCarSuccessfully()
        {
            // Given
            var adminAccessToken = await GetAdminAccessToken();

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", adminAccessToken);

            // Act
            var response = await _httpClient.GetAsync($"api/car/civic");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            content.Should().Contain("Civic");
        }

        [Fact]
        public async Task ShouldDeleteCarSuccessfully()
        {
            // Given
            var adminAccessToken = await GetAdminAccessToken();
            var input = new InsertCarInput
            {
                Brand = _faker.Random.String2(5),
                Model = _faker.Random.String2(5),
                Year = _faker.Random.Number(2000, 2023)
            };

            var data = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", adminAccessToken);

            // Act
            var responseCreate = await _httpClient.PostAsync($"api/car", data);
            var responseSearch = await _httpClient.GetAsync($"api/car/{input.Model}");
            var searchContent = await responseSearch.Content.ReadAsStringAsync();
            var desserializedItems = JsonSerializer.Deserialize<List<Car>>(searchContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            var responseDelete = await _httpClient.DeleteAsync($"api/car/{desserializedItems?.First().Id}");

            // Assert
            responseDelete.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task ShouldUpdateCarDataSuccessfully()
        {
            // Given
            var carId = 1;
            var adminAccessToken = await GetAdminAccessToken();
            var input = new UpdateCarInput { Year = 2023 };

            var data = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", adminAccessToken);

            // Act
            var response = await _httpClient.PatchAsync($"api/car/{carId}", data);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        private async Task<string> GetAdminAccessToken()
        {
            var input = new LoginUserInput
            {
                UserName = "admin",
                Password = "123456"
            };

            var data = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/login", data);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
