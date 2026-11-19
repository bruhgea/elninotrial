using Microsoft.EntityFrameworkCore;

namespace todoapp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskModel> TodoItems { get; set; }
}