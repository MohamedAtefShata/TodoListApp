namespace TodoList.Infrastructure.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string name, object key)
        : base($"Entity '{name}' with key '{key}' was not found.")
    {
    }
}