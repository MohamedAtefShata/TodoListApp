using TodoList.Domain;

namespace TodoList.Infrastructure.Repositories.UserRepository;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByUsernameAsync(string username);
    Task<User> CreateAsync(User? user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}