using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TODOApp.Api.Dtos;

namespace TODOApp.Api.Tests.Endpoints;

public class UsersEndpointTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly WebApplicationFactory<Program> _factory;

    public UsersEndpointTests(WebApplicationFactory<Program> factory) {
        _factory = factory.WithWebHostBuilder(builder => {
            builder.ConfigureServices(services => {
                // TODO Add services here
                services.AddSingleton<IUsersService, UsersServiceMock>();
            });
        });
    }

    [Fact]
    public async Task User_Register_ReturnsSuccess() {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var result = await client.PostAsync("/api/Users/register", JsonContent.Create(new CreateUser {
            Username = "new user",
            Password = "new password"
        }));

        // Assert
        result.Should().BeSuccessful();    
    }

    [Fact]
    public async Task User_Login_ReturnsSuccess() {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        await client.PostAsync("/api/Users/register", JsonContent.Create(new CreateUser {
            Username = "new user",
            Password = "new password"
        }));

        var result = await client.PostAsync("/api/users/login", JsonContent.Create(new CreateUser {
            Username = "new user",
            Password = "new password"
        }));

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public async Task User_Register_UsernameTaken() {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        await client.PostAsync("/api/users/register", JsonContent.Create(new CreateUser {
            Username = "user1",
            Password = "password"
        }));
        var result = await client.PostAsync("/api/users/register", JsonContent.Create(new CreateUser {
            Username = "user1",
            Password = "password"
        }));

        // Assert
        result.Should().HaveClientError();
    }

    // TODO move this to service tests

    // [Theory]
    // [InlineData("user", ""), InlineData("", "pass")]
    // public async Task User_Register_InvalidUsernameOrPassword(string username, string password) {
    //     // Arrange
    //     var client = _factory.CreateClient();

    //     // Act
    //     var result = await client.PostAsync("/api/Users/register", JsonContent.Create(new CreateUser {
    //         Username = username,
    //         Password = password
    //     }));

    //     // Assert
    //     result.Should().HaveClientError();    

    // }

    
}