using TodoList.Core.DTOs.User;

namespace TodoList.Core.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; }
    public UserDto User { get; set; }
}