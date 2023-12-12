using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TODOApp.Api.Controllers;


[ApiController]
[Route("/api/tasks")]
public class UserTasksController : ControllerBase
{
    readonly IUserTasksService _userTasksService;
    readonly ISubtasksService _subtasksService;
    public UserTasksController(IUserTasksService userTasksService, ISubtasksService subtasksService)
    {
        _userTasksService = userTasksService;
        _subtasksService = subtasksService;
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

    [HttpPost("subtask/add")]
    [Authorize]
    public async Task<IActionResult> AddSubtask([FromBody] CreateSubtask newSubtask) {
        var username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value;
        
        try {
            var result = await _subtasksService.AddToTask(newSubtask, username);
            return Ok(result);
        } catch (TODOAPPApiBaseException e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> SetCompleted([FromBody] CompleteSubtask subtask) {
        var username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value;

        try {
            var result = await _subtasksService.SetCompleted(subtask, username);
            return Ok(result);
        } catch (TODOAPPApiBaseException e) {
            return BadRequest(e.Message);
        }
    }
}
