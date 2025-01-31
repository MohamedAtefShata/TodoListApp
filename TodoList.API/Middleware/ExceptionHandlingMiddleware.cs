﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using TodoList.Infrastructure.Exceptions;

namespace TodoList.API.Middleware;

public class ErrorResponse
{
    public string Message { get; set; }
    public string TraceId { get; set; } = Activity.Current?.Id ?? Guid.NewGuid().ToString();
}

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
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

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "An error occurred while processing your request.";
                break;
        }

        await context.Response.WriteAsJsonAsync(response);
    }
}
