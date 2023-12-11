namespace TODOApp.Api.Services;

public interface IUserTasksService {
    public Task<IEnumerable<UserTask>> All();
}