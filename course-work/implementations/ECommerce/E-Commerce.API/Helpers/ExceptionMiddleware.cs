using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace E_Commerce.API.Helpers
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";

            // Определяме статус кода според вида на грешката
            var statusCode = exception switch
            {
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                ArgumentException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            // Създаваме обекта по стандарт RFC 7807
            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = statusCode.ToString(),
                Detail = _env.IsDevelopment() ? exception.Message : "Възникна неочаквана грешка. Моля, свържете се с поддръжката.",
                Instance = context.Request.Path
            };

            // Ако сме в Development режим, добавяме StackTrace за лесно дебъгване
            if (_env.IsDevelopment())
            {
                problem.Extensions.Add("stackTrace", exception.StackTrace);
            }

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(problem, options);

            await context.Response.WriteAsync(json);
        }
    }
}
