namespace TODOApp.Api.Services;


public class Global {
    private Global() {}
    public static Global Instance { get; } = new();

    public List<UserTask> Tasks { get; } = new() {
        new() {
            Title = "Task1",
            Description = "task description"
        },
    };
}

public class UserTasksService : IUserTasksService {
    public async Task<IEnumerable<UserTask>> All() {
        return Global.Instance.Tasks.Select(task => task);
    }
}