# Controllers

- Inherit from `ApiControllerBase`
- Route prefix: `/api/{resource}` — route set at controller level with `[Route("api/[controller]")]`
- Return `ActionResult<T>` (strongly typed)
- No business logic — delegate immediately to a service
- Translate errors via `HandleErrorResponse()` from the base class

```csharp
[HttpGet("{bookId:int}")]
public async Task<ActionResult<BookContract>> GetBook(int bookId)
{
    var result = await _bookService.GetBookAsync(bookId);
    if (!result.IsSuccess)
        return HandleErrorResponse(result.Error!);

    return Ok(_bookMapper.MapBook(result.Value!));
}
```

See also: [Error Handling](error-handling.md) for how `HandleErrorResponse()` maps domain errors to HTTP responses.
