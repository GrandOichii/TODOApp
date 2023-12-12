
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TODOApp.Api.Services;

public class SubtasksService : ISubtasksService
{
    readonly DataContext _ctx;
    readonly IMapper _mapper;
    public SubtasksService(DataContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public async Task<GetUserTask> AddToTask(CreateSubtask newSubtask, int taskId)
    {
        var task = await _ctx.UserTasks.FirstOrDefaultAsync(task => task.ID == taskId)
            ?? throw new TODOAPPApiBaseException("Task with ID " + taskId + " doesn't exist");

        var subtask = new Subtask {
            Title = newSubtask.Title
        };

        task.Subtasks.Add(subtask);
        await _ctx.SaveChangesAsync();

        return _mapper.Map<GetUserTask>(task);
    }
}