using TodoList.Core.DTOs.TodoItem;

namespace TodoList.Core.Services.TodoItem;

public interface ITodoService
{
    Task<TodoItemDto> CreateTodoAsync(CreateTodoItemDto dto, int userId);
    Task DeleteTodoAsync(int todoId, int userId);
    Task<TodoListDto> GetUserTodoListAsync(int userId);
    Task<TodoItemDto> ToggleTodoCompletionAsync(int todoId, int userId);
}