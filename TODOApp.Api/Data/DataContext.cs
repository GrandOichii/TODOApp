using Microsoft.EntityFrameworkCore;

namespace TODOApp.Api.Data;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {
        
    }

    public DbSet<UserTask> UserTasks => Set<UserTask>();
    public DbSet<Subtask> Subtasks => Set<Subtask>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .Entity<UserTask>()
            .HasMany(t => t.Subtasks)
            .WithOne(s => s.OwnerTask)
            .OnDelete(DeleteBehavior.Cascade);
    }
}