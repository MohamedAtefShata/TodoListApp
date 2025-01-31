using TodoList.Domain;

namespace TodoList.Infrastructure.Repositories.UserRepository;

public class UserRepository:IUserRepository
{
    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}