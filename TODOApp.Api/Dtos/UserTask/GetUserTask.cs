namespace TODOApp.Api.Dtos;

public class GetUserTask {
    public int ID { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required List<GetSubtask> Subtasks { get; set; }
}