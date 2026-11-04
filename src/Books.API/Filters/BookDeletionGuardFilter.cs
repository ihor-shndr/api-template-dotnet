using System.Net;
using Books.API.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace Books.API.Filters;

/// <summary>
/// Allows book deletion only in Development, when explicitly enabled, for a loopback connection peer.
/// Forwarded headers are deliberately ignored. Denied requests look like a missing route (404).
/// </summary>
public class BookDeletionGuardFilter(IHostEnvironment environment, IOptions<BookDeletionConfig> options)
    : IAsyncActionFilter
{
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var peer = context.HttpContext.Connection.RemoteIpAddress;
        var allowed = environment.IsDevelopment()
                      && options.Value.Enabled
                      && peer is not null
                      && IPAddress.IsLoopback(peer);

        if (!allowed)
        {
            context.Result = new NotFoundResult();
            return Task.CompletedTask;
        }

        return next();
    }
}
