using System.Net.Http.Json;
using FluentAssertions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TODOApp.Api.Dtos;

namespace TODOApp.Api.Tests.Endpoints;

public class UserTaskssEndpointTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly WebApplicationFactory<Program> _factory;

    public UserTaskssEndpointTests(WebApplicationFactory<Program> factory) {
        _factory = factory.WithWebHostBuilder(builder => {
            builder.ConfigureServices(services => {

                services.AddSingleton<IUsersService, UsersServiceMock>();
                services.AddSingleton<IUserTasksService, UserTasksServiceMock>();

            });
        });
    }

    private static async Task<string> GetJwtToken(HttpClient client, string username, string password) {
        var result = await client.PostAsync("/api/users/login", JsonContent.Create(new CreateUser {
            Username = username,
            Password = password
        }));
        return await result.Content.ReadAsStringAsync();
    }

    [Fact]
    public async Task UserTasks_GetAll_ReturnsSuccess() {
        // Arrange
        var client = _factory.CreateClient();
        var token = await GetJwtToken(client, "testuser", "pass");
        client.SetBearerToken(token);

        // Act
        var result = await client.GetAsync("/api/tasks");

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public async Task UserTasks_GetAll_Unauthorized() {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var result = await client.GetAsync("/api/tasks");

        // Assert
        result.Should().HaveClientError();
    }

    [Fact]
    public async Task UserTasks_Create_ReturnsSuccess() {
        // Arrange
        var client = _factory.CreateClient();
        var token = await GetJwtToken(client, "testuser", "pass");
        client.SetBearerToken(token);

        // Act
        var result = await client.PostAsync("/api/tasks/create", JsonContent.Create(new CreateUserTask {
            Title = "new title",
            Description = "new description"
        }));

        // Assert
        // (await result.Content.ReadAsStringAsync()).Should().Be("1");
        result.Should().BeSuccessful();
    }
}