using Microsoft.AspNetCore.Mvc;
using TodoList.Core.DTOs.TodoItem;
using TodoList.Core.Services.TodoItem;
using TodoList.Domain;

namespace TodoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController(ITodoService todoService) : ControllerBase
{
    [HttpPost("{userId}")]
    public async Task<ActionResult<TodoItemDto>> CreateTodo(int userId, [FromBody] CreateTodoItemDto createTodoItemDto)
    {
        var newTodo = await todoService.CreateTodoAsync(createTodoItemDto, userId);
        return Ok(newTodo);
    }
    
    [HttpGet("{userId}")]
    public async Task<ActionResult<TodoListDto>> GetUserTodos(int userId)
    {
        return Ok(await todoService.GetUserTodoListAsync(userId));
    }
    
    [HttpPut("{userId}/{todoId}")]
    public async Task<ActionResult<TodoItem>> ChangeTodoState(int userId, int todoId)
    {
        return Ok(await todoService.ToggleTodoCompletionAsync(userId, todoId));
    }
    
    [HttpDelete("{userId}/{todoId}")]
    public async Task<ActionResult> DeleteTodo(int userId, int todoId)
    {
        await todoService.DeleteTodoAsync(userId, todoId);
        return NoContent();
    }
}