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
            _ctx.Users.Include(u => u.Tasks).FirstAsync(u => u.Username == username)
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
}