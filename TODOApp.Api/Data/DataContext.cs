using Microsoft.EntityFrameworkCore;

namespace TODOApp.Api.Data;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {
        
    }

    public DbSet<UserTask> UserTasks => Set<UserTask>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
    }
}