using System.Net;
using Books.API.Configuration;
using Books.API.Controllers;
using Books.API.Filters;
using Books.API.Mappers.Books;
using Books.Common.TryResult;
using Books.Domain.Books.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Books.UnitTests.Books;

public class BookDeletionGuardTests
{
    private readonly IBookService _service = Substitute.For<IBookService>();

    private async Task<IActionResult?> RunAsync(string environment, bool enabled, IPAddress? peer)
    {
        var env = Substitute.For<IHostEnvironment>();
        env.EnvironmentName.Returns(environment);
        var filter = new BookDeletionGuardFilter(env, Options.Create(new BookDeletionConfig { Enabled = enabled }));

        var httpContext = new DefaultHttpContext();
        httpContext.Connection.RemoteIpAddress = peer;
        var controller = new BooksController(_service, Substitute.For<IBookMapper>());
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        var executing = new ActionExecutingContext(actionContext, [], new Dictionary<string, object?>(), controller);

        await filter.OnActionExecutionAsync(executing, async () =>
        {
            executing.Result = await controller.DeleteBook(1);
            return new ActionExecutedContext(actionContext, [], controller) { Result = executing.Result };
        });

        return executing.Result;
    }

    [Fact]
    public async Task Delete_WhenPeerIsNotLoopback_ReturnsNotFoundWithoutCallingService()
    {
        var result = await RunAsync(Environments.Development, true, IPAddress.Parse("203.0.113.10"));

        Assert.IsType<NotFoundResult>(result);
        await _service.DidNotReceiveWithAnyArgs().DeleteBookAsync(default);
    }

    [Fact]
    public async Task Delete_WhenPeerIsMissing_ReturnsNotFoundWithoutCallingService()
    {
        var result = await RunAsync(Environments.Development, true, null);

        Assert.IsType<NotFoundResult>(result);
        await _service.DidNotReceiveWithAnyArgs().DeleteBookAsync(default);
    }

    [Fact]
    public async Task Delete_WhenNotEnabled_ReturnsNotFoundWithoutCallingService()
    {
        var result = await RunAsync(Environments.Development, false, IPAddress.Loopback);

        Assert.IsType<NotFoundResult>(result);
        await _service.DidNotReceiveWithAnyArgs().DeleteBookAsync(default);
    }

    [Fact]
    public async Task Delete_WhenNotDevelopment_ReturnsNotFoundWithoutCallingService()
    {
        var result = await RunAsync(Environments.Production, true, IPAddress.Loopback);

        Assert.IsType<NotFoundResult>(result);
        await _service.DidNotReceiveWithAnyArgs().DeleteBookAsync(default);
    }

    [Fact]
    public async Task Delete_WhenEnabledDevelopmentLoopback_CallsServiceAndReturnsNoContent()
    {
        _service.DeleteBookAsync(1).Returns(TryResult.Success());

        var result = await RunAsync(Environments.Development, true, IPAddress.IPv6Loopback);

        Assert.IsType<NoContentResult>(result);
        await _service.Received(1).DeleteBookAsync(1);
    }
}
