using TodoList.Core.DTOs.TodoItem;

namespace TodoList.Core.Services.TodoItem;

public class TodoService:ITodoService
{
    public Task<TodoItemDto> CreateTodoAsync(CreateTodoItemDto dto, int userId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTodoAsync(int todoId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<TodoListDto> GetUserTodoListAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItemDto> ToggleTodoCompletionAsync(int todoId, int userId)
    {
        throw new NotImplementedException();
    }
}