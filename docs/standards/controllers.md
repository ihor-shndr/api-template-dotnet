# Controllers

- Inherit from `ApiControllerBase`
- Route set at controller level with `[Route("{resource}")]` — **without** an `api/` segment. The `/api/v1` prefix comes from `app.UsePathBase("/api/v1")` in `Program.cs`, not from the route template. Keep it that way, or endpoints end up at `/api/v1/api/books`.
- Annotate the class with `[ApiController]`
- Take dependencies as primary-constructor parameters
- Return `ActionResult<T>` (strongly typed)
- Bind route values with `[FromRoute]`
- No business logic — delegate immediately to a service
- Translate errors via `HandleErrorResponse()` from the base class

```csharp
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
```

No null-forgiving `!` is needed on `.Error` or `.Value` — `TryResult.IsSuccess` is annotated with `[MemberNotNullWhen]`, so the compiler narrows them for you.

The example above resolves to `GET /api/v1/books/{bookId}`.

See also: [Error Handling](error-handling.md) for how `HandleErrorResponse()` maps domain errors to HTTP responses.
