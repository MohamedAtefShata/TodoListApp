namespace TodoList.API.Middleware;

public class ExceptionHandlingMiddlewareFactory(ILogger<ExceptionHandlingMiddleware> logger)
{
    public ExceptionHandlingMiddleware Create(RequestDelegate next)
    {
        return new ExceptionHandlingMiddleware(next, logger);
    }
}