using Microsoft.AspNetCore.Mvc;

namespace TODOApp.Api.Controllers;

public class Global {
    private Global() {}
    public static Global Instance { get; } = new();

    public List<UserTask> Tasks { get; } = new() {
        new() {
            Title = "Task1",
            Description = "task description"
        },
    };
}

[ApiController]
[Route("/api/tasks")]
public class UserTasksController : ControllerBase
{
    public UserTasksController()
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(Global.Instance.Tasks);
    }
}
