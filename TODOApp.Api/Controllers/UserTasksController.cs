using Microsoft.AspNetCore.Mvc;

namespace TODOApp.Api.Controllers;


[ApiController]
[Route("/api/tasks")]
public class UserTasksController : ControllerBase
{
    IUserTasksService _userTasksService;
    public UserTasksController(IUserTasksService userTasksService)
    {
        _userTasksService = userTasksService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _userTasksService.All());
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateUserTask newUserTask) {
        // TODO invlove user
        return Ok(await _userTasksService.Create(newUserTask));
    }
}
