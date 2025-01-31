using System.ComponentModel.DataAnnotations;
using TodoList.Core.DTOs.TodoItem;
using TodoList.Infrastructure.Repositories.TodoItemRepository;

namespace TodoList.Core.Services.TodoItem;

public class TodoService(ITodoItemRepository todoRepository) : ITodoService
{
    public async Task<TodoItemDto> CreateTodoAsync(CreateTodoItemDto dto, int userId)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new ValidationException("Title is required");

        var todoItem = new Domain.TodoItem
        {
            Title = dto.Title.Trim(),
            IsCompleted = false,
            UserId = userId
        };

        var created = await todoRepository.CreateAsync(todoItem);
        return new TodoItemDto
        {
            IsCompleted = created.IsCompleted,
            Title = created.Title,
            Id = created.Id
        };
    }

    public async Task DeleteTodoAsync(int todoId, int userId)
    {
        var todo = await todoRepository.GetByIdAsync(todoId);

        if (todo.UserId != userId)
            throw new UnauthorizedAccessException("You can only delete your own todos");

        await todoRepository.DeleteAsync(todoId);
    }
    public async Task<TodoListDto> GetUserTodoListAsync(int userId)
    {
        var allTodos = await todoRepository.GetAllByUserIdAsync(userId);

        return new TodoListDto
        {
            CompletedTodos = allTodos
                .Where(t => t.IsCompleted)
                .Select(t => new TodoItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsCompleted = t.IsCompleted
                })
                .ToList(),
            IncompleteTodos = allTodos
                .Where(t => t.IsCompleted)
                .Select(t => new TodoItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsCompleted = t.IsCompleted
                })
                .ToList()
        };
    }

    public async Task<TodoItemDto> ToggleTodoCompletionAsync(int todoId, int userId)
    {
        var todo = await todoRepository.GetByIdAsync(todoId);

        if (todo.UserId != userId)
            throw new UnauthorizedAccessException("You can only update your own todos");

        todo.IsCompleted = !todo.IsCompleted;

        await todoRepository.UpdateAsync(todo);
        return new TodoItemDto()
        {
            Id = todo.Id,
            Title = todo.Title,
            IsCompleted = todo.IsCompleted
        };
    }
}