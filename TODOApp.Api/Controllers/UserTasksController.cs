using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TODOApp.Api.Controllers;


[ApiController]
[Route("/api/tasks")]
public class UserTasksController : ControllerBase
{
    readonly IUserTasksService _userTasksService;
    public UserTasksController(IUserTasksService userTasksService)
    {
        _userTasksService = userTasksService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value;
        return Ok(await _userTasksService.All(username));
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateUserTask newUserTask) {
        var username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value;

        return Ok(await _userTasksService.Create(newUserTask, username));
    }
}
