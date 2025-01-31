using TodoList.Domain;

namespace TodoList.Core.Services.Auth.Helpers.JwtTokenAuth;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}