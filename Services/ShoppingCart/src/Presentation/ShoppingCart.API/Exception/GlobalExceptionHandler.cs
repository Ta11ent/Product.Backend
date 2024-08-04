namespace ShoppingCart.API.Exception
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = CreateProblemDetails(exception);

            httpContext.Response.StatusCode = (int)problemDetails.Status;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private ProblemDetails CreateProblemDetails(System.Exception exception)
        {
            var code = StatusCodes.Status500InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = StatusCodes.Status400BadRequest;
                    result = JsonSerializer.Serialize(validationException);
                    break;
                case NotFoundException:
                    code = StatusCodes.Status400BadRequest;
                    break;
            }

            result = result == string.Empty ? JsonSerializer.Serialize(new { error = exception.Message }) : result;

            return new ProblemDetails()
            {
                Type = "http://www.rfc-editor.org/info/rfc7231#section-6.6.1",
                Title = "One or more errors occurs",
                Status = code,
                Extensions = new Dictionary<string, object?>
                {
                    { "isSuccess", false},
                    { "errors", result }
                }
            };
        }
    }
}
