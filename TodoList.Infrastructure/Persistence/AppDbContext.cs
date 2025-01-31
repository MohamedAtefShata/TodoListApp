using Microsoft.EntityFrameworkCore;
using TodoList.Domain;

namespace TodoList.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User?> User { get; set; }
    public DbSet<TodoItem> TodoItem { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}