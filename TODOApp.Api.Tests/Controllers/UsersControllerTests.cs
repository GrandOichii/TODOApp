using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TODOApp.Api.Exceptions;

namespace TODOApp.Api.Tests.Controllers;

public class UsersControllerTests {
    readonly IUsersService _service;
    readonly UsersController _controller;

    public UsersControllerTests() {
        _service = A.Fake<IUsersService>();

        _controller = new UsersController(_service);
    }

    [Fact]
    public async void UsersController_Register_ReturnsSuccess() {
        // Arrange
        var user = A.Fake<CreateUser>();
        var resultUser = A.Fake<GetUser>();
        
        A.CallTo(() => _service.Register(user)).Returns(resultUser);

        // Act
        var result = await _controller.Register(user);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async void UsersController_Register_UsernameTaken() {
        // Arrange
        var user = A.Fake<CreateUser>();
        
        A.CallTo(() => _service.Register(user))
            .Returns(A.Fake<GetUser>())
            .Once()
            .Then
            .Throws<TODOAPPApiBaseException>();

        // Act
        await _controller.Register(user);
        var result = await _controller.Register(user);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async void UsersController_Login_ReturnsSuccess() {
        // Arrange
        var user = A.Fake<CreateUser>();

        A.CallTo(() => _service.Login(user))
            .Returns("jwt token");

        // Act
        var result = await _controller.Login(user);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }
    
}