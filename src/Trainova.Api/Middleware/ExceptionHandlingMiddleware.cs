using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Trainova.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message, errors) = exception switch
        {
            ValidationException ve => (
                HttpStatusCode.BadRequest,
                "Validation failed",
                ve.Errors.Select(e => e.ErrorMessage).ToArray()),

            // Todo: Add Domain Exception

            KeyNotFoundException => (
                HttpStatusCode.NotFound,
                exception.Message,
                Array.Empty<string>()),

            UnauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                "Unauthorised",
                Array.Empty<string>()),

            _ => (
                HttpStatusCode.InternalServerError,
                "Something went wrong",
                Array.Empty<string>())
        };

        // Unhandle exceptions
        if (statusCode == HttpStatusCode.InternalServerError)
            _logger.LogError(exception, "Unhandled exception occurred");

        var response = new
        {
            type = $"https://httpstatuses.io/{(int)statusCode}",
            title = message,
            status = (int)statusCode,
            errors
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));

    }
}
