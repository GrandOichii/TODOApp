namespace TODOApp.Api.Services;

public interface ISubtasksService {

    public Task<GetUserTask> AddToTask(CreateSubtask newSubtask, string username);
    public Task<GetUserTask> SetCompleted(CompleteSubtask subtask, string username);

}