using TodoList.Domain;

namespace TodoList.Infrastructure.Repositories.TodoItemRepository;

public class TodoItemRepository:ITodoItemRepository
{
    public Task<IEnumerable<TodoItem>> GetAllByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem> CreateAsync(TodoItem todoItem)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TodoItem todoItem)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}