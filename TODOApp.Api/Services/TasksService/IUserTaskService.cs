namespace TODOApp.Api.Services;

public interface IUserTasksService {
    public Task<IEnumerable<GetUserTask>> All();
    public Task<IEnumerable<GetUserTask>> Create(CreateUserTask newUserTask);
}