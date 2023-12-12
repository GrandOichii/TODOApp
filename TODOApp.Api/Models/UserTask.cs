namespace TODOApp.Api.Models;

public class UserTask {
    public int ID { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<Subtask> Subtasks { get; set; } = new();
}