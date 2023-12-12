using System.ComponentModel.DataAnnotations;

namespace TODOApp.Api.Models;

public class User {
    [Key]
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    // TODO add email - as hash?

    public List<UserTask> Tasks { get; set; } = new();
}