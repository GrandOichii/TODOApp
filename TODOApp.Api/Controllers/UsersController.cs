using Microsoft.AspNetCore.Mvc;

namespace TODOApp.Api.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : ControllerBase
{
    IUsersService _usersService;
    public UsersController(IUsersService userTasksService)
    {
        _usersService = userTasksService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUser newUser) {
        try {
            var user = await _usersService.Register(newUser);
            return Ok(user);
        } catch (TODOAPPApiBaseException e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CreateUser user) {
        try {
            var token = await _usersService.Login(user);
            return Ok(token);
        } catch (TODOAPPApiBaseException e) {
            return Unauthorized(e.Message);
        }
    }
}
