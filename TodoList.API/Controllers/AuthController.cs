using Microsoft.AspNetCore.Mvc;
using TodoList.Core.DTOs.Auth;
using TodoList.Core.DTOs.User;
using TodoList.Core.Services.Auth;

namespace TodoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody]RegisterRequestDto userDto)
    {
        var user = await authService.RegisterAsync(userDto);
        return Ok(user);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        AuthResponseDto user = await authService.LoginAsync(loginRequestDto);
        return Ok(user);
    }
}