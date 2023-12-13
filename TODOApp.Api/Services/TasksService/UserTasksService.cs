using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TODOApp.Api.Services;

public class UserTasksService : IUserTasksService {
    IMapper _mapper;
    DataContext _ctx;
    public UserTasksService(IMapper mapper, DataContext ctx) {
        _mapper = mapper;
        _ctx = ctx;
    }

    public async Task<IEnumerable<GetUserTask>> All(string username) {
        var user = await (
            _ctx.Users
                .Include(u => u.Tasks)
                .ThenInclude(task => task.Subtasks)
                .FirstAsync(u => u.Username == username)
        );
        return user.Tasks.Select(task => _mapper.Map<GetUserTask>(task));
    }

    public async Task<IEnumerable<GetUserTask>> Create(CreateUserTask newUserTask, string username)
    {
        var user = await _ctx.Users.FirstAsync(u => u.Username == username);

        var newTask = _mapper.Map<UserTask>(newUserTask);
        user.Tasks.Add(newTask);

        await _ctx.SaveChangesAsync();
        return await All(username);
    }

    public async Task<IEnumerable<GetUserTask>> Remove(int id, string username)
    {
        var user = await _ctx.Users.Include(user => user.Tasks).FirstAsync(u => u.Username == username);

        var task = user.Tasks.FirstOrDefault(t => t.ID == id)
            ?? throw new TODOAPPApiBaseException("User " + username + " has no task with ID " + id);

        foreach (var st in task.Subtasks) {
            _ctx.Subtasks.Remove(st);
        }
        await _ctx.SaveChangesAsync();
        _ctx.UserTasks.Remove(task);
        await _ctx.SaveChangesAsync();

        return await All(username);
    }
}