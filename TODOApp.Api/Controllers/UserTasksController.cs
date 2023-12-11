using Microsoft.AspNetCore.Mvc;

namespace TODOApp.Api.Controllers;


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
