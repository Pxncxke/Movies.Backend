using Microsoft.AspNetCore.Diagnostics;
using Movies.Api.Exceptions;

namespace Movies.Api.Middleware;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        this._logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (int statusCode, string errorMessage) = exception switch
        {
            BadRequestException validationException => (400, validationException.Message),
            NotFoundException notFoundException => (404, notFoundException.Message),
            _ => (500, "An error occurred")
        };

        _logger.LogError(exception, errorMessage);
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(new { errorMessage });
        return true;
    }
}
