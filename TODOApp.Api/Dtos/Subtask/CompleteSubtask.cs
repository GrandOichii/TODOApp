namespace TODOApp.Api.Dtos;

public class CompleteSubtask {
    public int SubtaskID { get; set; }
    public int TaskID { get; set; }
    public bool Completed { get; set; }
}