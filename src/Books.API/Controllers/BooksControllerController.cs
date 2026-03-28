using Books.Domain.Books.Services;
using BookShelf.API.Contracts.Books;
using BookShelf.API.Mappers.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace BookShelf.API.Controllers;

[ApiController]
[Route("books")]
public class BooksController(IBookService bookService, IBookMapper mapper, IFeatureManager featureManager) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookContract>>> GetBooks()
    {
        if (!await featureManager.IsEnabledAsync("EnableBooksList"))
            return NotFound();

        var result = await bookService.GetBooksAsync();

        return !result.IsSuccess
            ? HandleErrorResponse(result.Error)
            : Ok(mapper.MapBooks(result.Value));
    }

    [HttpGet("{bookId:int}")]
    public async Task<ActionResult<BookContract>> GetBook([FromRoute]int bookId)
    {
        var bookResult = await bookService.GetBookAsync(bookId);

        return !bookResult.IsSuccess
            ? HandleErrorResponse(bookResult.Error)
            : Ok(mapper.MapBook(bookResult.Value));
    }
}