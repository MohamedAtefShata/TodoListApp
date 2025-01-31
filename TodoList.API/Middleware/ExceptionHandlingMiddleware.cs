using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using TodoList.Infrastructure.Exceptions;

namespace TodoList.API.Middleware;

public class ErrorResponse
{
    public string Message { get; set; }
    public string TraceId { get; set; } = Activity.Current?.Id ?? Guid.NewGuid().ToString();
}

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = new ErrorResponse();

        switch (exception)
        {
            case EntityNotFoundException notFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = notFoundException.Message;
                break;

            case ValidationException validationException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = validationException.Message;
                break;

            case ArgumentException argumentException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = argumentException.Message;
                break;

            case UnauthorizedAccessException:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Message = "Unauthorized access";
                break;
            
            case InvalidOperationException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "This is invalid operation";
                break;
            
            case AggregateException:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Message = "AggregateException";
                break;
            
            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "An error occurred while processing your request.";
                break;
        }

        await context.Response.WriteAsJsonAsync(response);
    }
}
