namespace TODOApp.Api.Dtos;

public class GetSubtask {
    public int ID { get; set; }
    public required string Title { get; set; }
    public required bool Completed { get; set; }
}