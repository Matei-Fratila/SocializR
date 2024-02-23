using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace SocializR.SPA.Server.ExceptionHandlers;

public class DefaultExceptionHandler(ILogger<DefaultExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unexpected error occured and has been handled by the {DefaultExceptionHandler} handler", nameof(DefaultExceptionHandler));

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Type = exception.GetType().Name,
            Title = "An unexpected error occurred",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        }, cancellationToken);

        return true;
    }
}
