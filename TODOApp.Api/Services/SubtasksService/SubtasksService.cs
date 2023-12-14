
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

    public async Task<GetUserTask> AddToTask(CreateSubtask newSubtask, string username)
    {
        var taskId = newSubtask.TaskID;
        var user = _ctx.Users.Include(u => u.Tasks).ThenInclude(st => st.Subtasks).First(u => u.Username == username);
        var task = user.Tasks.FirstOrDefault(task => task.ID == taskId)
            ?? throw new TODOAPPApiBaseException("User " + username + " doesn't own the task with ID " + taskId);

        var subtask = new Subtask {
            Title = newSubtask.Title,
            OwnerTask = task
        };

        task.Subtasks.Add(subtask);
        await _ctx.SaveChangesAsync();

        return _mapper.Map<GetUserTask>(task);
    }

    public async Task<GetSubtask> SetCompleted(CompleteSubtask subtask, string username)
    {
        var taskId = subtask.TaskID;
        var user = _ctx.Users.Include(u => u.Tasks).ThenInclude(task => task.Subtasks).First(u => u.Username == username);
        var task = user.Tasks.FirstOrDefault(task => task.ID == taskId)
            ?? throw new TODOAPPApiBaseException("User " + username + " doesn't own the task with ID " + taskId);

        var sub = task.Subtasks.FirstOrDefault(s => s.ID == subtask.SubtaskID) 
            ?? throw new TODOAPPApiBaseException("No subtask with ID " + subtask.SubtaskID);

        sub.Completed = subtask.Completed;
        await _ctx.SaveChangesAsync();

        return _mapper.Map<GetSubtask>(sub);
    }
}