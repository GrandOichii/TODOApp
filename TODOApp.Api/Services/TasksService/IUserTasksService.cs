namespace TODOApp.Api.Services;

public interface IUserTasksService {
    public Task<IEnumerable<GetUserTask>> All(string username);
    public Task<IEnumerable<GetUserTask>> Create(CreateUserTask newUserTask, string username);
    public Task<IEnumerable<GetUserTask>> Remove(int id, string username);
}