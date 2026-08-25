using Books.Domain.Books.Services;
using Books.API.Contracts.Books;
using Books.API.Mappers.Books;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers;

[ApiController]
[Route("books")]
public class BooksController(IBookService bookService, IBookMapper mapper) : ApiControllerBase
{
    [HttpGet("{bookId:int}")]
    public async Task<ActionResult<BookContract>> GetBook([FromRoute] int bookId)
    {
        var bookResult = await bookService.GetBookAsync(bookId);

        return !bookResult.IsSuccess
            ? HandleErrorResponse(bookResult.Error)
            : Ok(mapper.MapBook(bookResult.Value));
    }
}