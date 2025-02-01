using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Core.DTOs.TodoItem;
using TodoList.Core.Services.TodoItem;
using TodoList.Domain;

namespace TodoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TodoController(ITodoService todoService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> CreateTodo([FromBody] CreateTodoItemDto createTodoItemDto)
    {
        return Ok(await todoService.CreateTodoAsync(createTodoItemDto, GetCurrentUserId()));
    }
    
    [HttpGet]
    public async Task<ActionResult<TodoListDto>> GetUserTodos()
    {
        return Ok(await todoService.GetUserTodoListAsync(GetCurrentUserId()));
    }
    
    [HttpPut("{todoId}")]
    public async Task<ActionResult<TodoItem>> ChangeTodoState(int todoId)
    {
        return Ok(await todoService.ToggleTodoCompletionAsync(todoId, GetCurrentUserId()));
    }
    
    [HttpDelete("{todoId}")]
    public async Task<ActionResult> DeleteTodo(int todoId)
    {
        await todoService.DeleteTodoAsync(todoId, GetCurrentUserId());
        return NoContent();
    }
    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException());
    }
}