namespace TODOApp.Api.Dtos;

public class CreateSubtask {
    public int TaskID { get; set; }
    public required string Title { get; set; }
}