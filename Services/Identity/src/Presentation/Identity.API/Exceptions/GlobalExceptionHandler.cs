using Identity.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Text.Json;

namespace Identity.API.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = CreateProblemDetais(exception);

            httpContext.Response.StatusCode = (int)problemDetails.Status!;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
        private ProblemDetails CreateProblemDetais(Exception exception)
        {
            var code = StatusCodes.Status500InternalServerError;
            string result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = StatusCodes.Status400BadRequest;
                    result = JsonSerializer.Serialize(validationException);
                    break;
                case SecurityException securityException:
                    code = StatusCodes.Status401Unauthorized;
                    result = JsonSerializer.Serialize(securityException);
                    break;
                case NotFoundExceptions notFoundExceptions:
                    code = StatusCodes.Status404NotFound;
                    result = JsonSerializer.Serialize(notFoundExceptions);
                    break;
                case AgregareExceptions agregareExceptions:
                    code = StatusCodes.Status400BadRequest;
                    result = JsonSerializer.Serialize(agregareExceptions);
                    break;
            }

            result = result == string.Empty
                        ? JsonSerializer.Serialize(new { error = exception.Message })
                        : result;
            return new ProblemDetails()
            {
                Type = "http://www.rfc-editor.org/info/rfc7231#section-6.6.1",
                Title = "One or more errors occurs",
                Status = code,
                Extensions = new Dictionary<string, object?>()
                {
                     { "isSuccess", false},
                    { "errors", result }
                }
            };
        }
    }
}
