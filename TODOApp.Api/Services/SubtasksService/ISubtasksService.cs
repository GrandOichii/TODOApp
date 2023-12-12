namespace TODOApp.Api.Services;

public interface ISubtasksService {

    public Task<GetUserTask> AddToTask(CreateSubtask newSubtask, int taskId);

}