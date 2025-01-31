using Microsoft.EntityFrameworkCore;
using TodoList.Domain;
using TodoList.Infrastructure.Exceptions;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Repositories.TodoItemRepository;

public class TodoItemRepository(AppDbContext context) : ITodoItemRepository
{
    private readonly AppDbContext dbContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<TodoItem>> GetAllByUserIdAsync(int userId)
    {
        // Check if user exists first
        var userExists = await dbContext.User.AnyAsync(u => u.Id == userId);
        if (!userExists)
            throw new EntityNotFoundException(nameof(User), userId);

        return await dbContext.TodoItem
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<TodoItem> GetByIdAsync(int id)
    {
        var todoItem = await dbContext.TodoItem
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (todoItem == null)
            throw new EntityNotFoundException(nameof(TodoItem), id);

        return todoItem;
    }

    public async Task<TodoItem> CreateAsync(TodoItem todoItem)
    {
        if (todoItem == null)
            throw new ArgumentNullException(nameof(todoItem));

        if (string.IsNullOrWhiteSpace(todoItem.Title))
            throw new ArgumentException("Title cannot be null or empty.", nameof(todoItem.Title));

        // Verify user exists
        var userExists = await dbContext.User.AnyAsync(u => u.Id == todoItem.UserId);
        if (!userExists)
            throw new EntityNotFoundException(nameof(User), todoItem.UserId);

        dbContext.TodoItem.Add(todoItem);
        await dbContext.SaveChangesAsync();
        return todoItem;
    }

    public async Task UpdateAsync(TodoItem todoItem)
    {
        if (todoItem == null)
            throw new ArgumentNullException(nameof(todoItem));

        var existingItem = await dbContext.TodoItem.FindAsync(todoItem.Id);
        if (existingItem == null)
            throw new EntityNotFoundException(nameof(TodoItem), todoItem.Id);

        dbContext.Entry(existingItem).CurrentValues.SetValues(todoItem);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var todoItem = await dbContext.TodoItem.FindAsync(id);
        if (todoItem == null)
            throw new EntityNotFoundException(nameof(TodoItem), id);

        dbContext.TodoItem.Remove(todoItem);
        await dbContext.SaveChangesAsync();
    }
}