namespace TODOApp.Api.Models;

public class Subtask {
    public int ID { get; set; }
    public required string Title { get; set; }
    public bool Completed { get; set; } = false;
}