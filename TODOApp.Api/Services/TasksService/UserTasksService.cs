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

    public async Task<IEnumerable<GetUserTask>> All() {
        var userTasks = await _ctx.UserTasks.ToListAsync();
        return userTasks.Select(task => _mapper.Map<GetUserTask>(task));
    }

    public async Task<IEnumerable<GetUserTask>> Create(CreateUserTask newUserTask)
    {
        var newTask = _mapper.Map<UserTask>(newUserTask);
        await _ctx.UserTasks.AddAsync(newTask);
        await _ctx.SaveChangesAsync();
        return await All();
    }
}