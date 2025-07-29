using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace TestTask.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred");
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = validationException.Message });
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new { error = notFoundException.Message ?? "Resource not found" });
                    break;
                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonSerializer.Serialize(new { error = "Unauthorized access" });
                    break;
                case ArgumentException argumentException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = argumentException.Message });
                    break;
                case DbUpdateException:
                    code = HttpStatusCode.Conflict;
                    result = JsonSerializer.Serialize(new { error = "Database update error" });
                    break;
                default:
                    result = JsonSerializer.Serialize(new { error = "An unexpected error occurred" });
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}