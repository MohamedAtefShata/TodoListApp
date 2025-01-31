namespace TodoList.Core.DTOs.TodoItem;

public class TodoListDto
{
    public List<TodoItemDto> CompletedTodos { get; set; } = new();
    public List<TodoItemDto> IncompleteTodos { get; set; } = new();
}