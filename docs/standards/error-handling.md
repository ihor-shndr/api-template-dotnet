# Error Handling: TryResult Pattern

All service and DAO methods return `TryResult<T>` — a custom result monad defined in `Books.Common`. Never throw exceptions for expected failures (not found, validation errors). Only propagate exceptions for unexpected infrastructure failures.

```csharp
// DAO returns a TryResult
public async Task<TryResult<Book>> GetBookAsync(int id)
{
    var book = BookSeedData.Books.FirstOrDefault(b => b.Id == id);

    if (book is null)
        return new Error(BookErrorCodes.BookNotFound, $"Book with ID {id} not found");

    return await Task.FromResult(new Book
    {
        Title = book.Title,
        Author = book.Author,
        CreatedDate = book.CreatedDate
    }); // implicit conversion to TryResult<Book>
}

// Service checks result before proceeding
var result = await bookDao.GetBookAsync(id);
if (!result.IsSuccess)
    return result.Error; // propagate upward

return result.Value;
```

`IsSuccess` carries `[MemberNotNullWhen]` annotations, so once you have checked it the compiler knows `Error` and `Value` are non-null. Do not add `!`.

**In the controller**, `ApiControllerBase.HandleErrorResponse()` translates domain `Error` objects into `ProblemDetails` HTTP responses. Error-code-to-status mapping lives in the API layer, never in the domain.

**Adding a new error code:**
1. Add a string constant to the relevant `*ErrorCodes` class in `Books.Domain`
2. Add a `case` for it to the `MapErrorToStatusCode` switch in `src/Books.API/Controllers/ApiControllerBase.cs`

Note that `HandleErrorResponse` and `MapErrorToStatusCode` are `static` on the shared base class — they are not virtual and cannot be overridden per controller. Every controller therefore shares one switch. That is fine at the current size; if a second domain area lands and the switch starts mixing unrelated error codes, move the mapping to a per-domain strategy rather than growing the switch indefinitely.

See also: [Naming Conventions](naming-conventions.md) for error code naming, [Controllers](controllers.md) for error translation.
