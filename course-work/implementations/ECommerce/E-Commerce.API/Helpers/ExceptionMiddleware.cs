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

            // having all the statuses
            var statusCode = exception switch
            {
                UnauthorizedAccessException => HttpStatusCode.Unauthorized, //401
                KeyNotFoundException => HttpStatusCode.NotFound, //404
                ArgumentException => HttpStatusCode.BadRequest, //400
                _ => HttpStatusCode.InternalServerError //500
            };

            context.Response.StatusCode = (int)statusCode;

            // Create object by standard RFC 7807
            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = statusCode.ToString(),
                Detail = _env.IsDevelopment() ? exception.Message : "An error occured!!.",
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
