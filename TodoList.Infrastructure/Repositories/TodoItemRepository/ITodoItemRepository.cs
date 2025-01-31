using TodoList.Domain;

namespace TodoList.Infrastructure.Repositories.TodoItemRepository;

public interface ITodoItemRepository
{
    Task<IEnumerable<TodoItem>> GetAllByUserIdAsync(int userId);
    Task<TodoItem> GetByIdAsync(int id);
    Task<TodoItem> CreateAsync(TodoItem todoItem);
    Task UpdateAsync(TodoItem todoItem);
    Task DeleteAsync(int id);
}