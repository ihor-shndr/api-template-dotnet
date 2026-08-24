using Books.Common.TryResult;
using Books.Domain.Books.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.API.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    private static int MapErrorToStatusCode(string code) => code switch
    {
        // 404 Not Found
        BookErrorCodes.BookNotFound => StatusCodes.Status404NotFound,

        // 401 Unauthorized

        // 500 Internal Server Error

        _ => StatusCodes.Status500InternalServerError
    };

    protected static ObjectResult HandleErrorResponse(Error error)
    {
        var statusCode = MapErrorToStatusCode(error.Code);

        var problem = new ProblemDetails
        {
            Title = error.Message,
            Status = statusCode,
            Type = error.Code
        };

        return new ObjectResult(problem)
        {
            StatusCode = statusCode
        };
    }
}