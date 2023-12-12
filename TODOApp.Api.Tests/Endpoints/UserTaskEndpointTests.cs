using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TODOApp.Api.Dtos;

namespace TODOApp.Api.Tests;

public class UserTaskEndpointTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly WebApplicationFactory<Program> _factory;

    public UserTaskEndpointTests(WebApplicationFactory<Program> factory) {
        _factory = factory.WithWebHostBuilder(builder => {
            builder.ConfigureServices(services => {
                // TODO Add services here
            });
        });
    }

    [Fact]
    public async Task UserTask_All_ReturnsSuccess() {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var polls = await client.GetFromJsonAsync<IEnumerable<GetUserTask>>("/api/tasks");

        // Assert
        polls.Should().NotBeNull();
    }
}