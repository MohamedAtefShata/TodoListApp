using System.ComponentModel.DataAnnotations;
using TodoList.Core.DTOs.Auth;
using TodoList.Core.DTOs.User;
using TodoList.Core.Services.Auth.Helpers.JwtTokenAuth;
using TodoList.Core.Services.Auth.Helpers.PasswordHasing;
using TodoList.Domain;
using TodoList.Infrastructure.Repositories.UserRepository;

namespace TodoList.Core.Services.Auth;

public class AuthService(
    IUserRepository userRepository,
    IJwtGenerator jwtGenerator,
    IPasswordHasher passwordHasher)
    : IAuthService
{
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(request.Password))
            throw new ValidationException("Password is required");
            
        if (string.IsNullOrWhiteSpace(request.UserName))
            throw new ValidationException("Username is required");

        // Create new user
        var user = new User
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = passwordHasher.HashPassword(request.Password)
        };

        await userRepository.CreateAsync(user);

        // Generate token and return response
        var token = jwtGenerator.GenerateToken(user);
        return new AuthResponseDto
        {
            Token = token,
            User = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            }
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await userRepository.GetByUsernameAsync(request.UserName);
        
        if (!passwordHasher.VerifyPassword(request.Password, user.Password))
            throw new ValidationException("Invalid username or password");

        var token = jwtGenerator.GenerateToken(user);
        return new AuthResponseDto
        {
            Token = token,
            User = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            }
        };
    }
}