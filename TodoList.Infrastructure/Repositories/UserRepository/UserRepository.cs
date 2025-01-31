using Microsoft.EntityFrameworkCore;
using TodoList.Domain;
using TodoList.Infrastructure.Exceptions;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Repositories.UserRepository;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext dbContext = context;

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await dbContext.User
            .Include(u => u.TodoItems)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            throw new EntityNotFoundException(nameof(User), id);

        return user;
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be null or empty.", nameof(username));

        var user = await dbContext.User
            .Include(u => u.TodoItems)
            .FirstOrDefaultAsync(u => u.UserName == username);

        if (user == null)
            throw new EntityNotFoundException(nameof(User), username);

        return user;
    }

    public async Task<User> CreateAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (string.IsNullOrWhiteSpace(user.UserName))
            throw new ArgumentException("Username cannot be null or empty.", nameof(user.UserName));

        var exists = await dbContext.User.AnyAsync(u => u.UserName == user.UserName);
        if (exists)
            throw new InvalidOperationException($"Username '{user.UserName}' is already taken.");

        dbContext.User.Add(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var existingUser = await dbContext.User.FindAsync(user.Id);
        if (existingUser == null)
            throw new EntityNotFoundException(nameof(User), user.Id);

        dbContext.Entry(existingUser).CurrentValues.SetValues(user);
        // Prevent password hash from being modified if it's not intended
        dbContext.Entry(existingUser).Property(x => x.Password).IsModified = false;
        
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await dbContext.User.FindAsync(id);
        if (user == null)
            throw new EntityNotFoundException(nameof(User), id);

        dbContext.User.Remove(user);
        await dbContext.SaveChangesAsync();
    }
}