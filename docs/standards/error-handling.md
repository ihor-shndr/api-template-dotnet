# Error Handling: TryResult Pattern

All service and DAO methods return `TryResult<T>` — a custom result monad defined in `Books.Common`. Never throw exceptions for expected failures (not found, validation errors). Only propagate exceptions for unexpected infrastructure failures.

```csharp
// DAO returns a TryResult
public async Task<TryResult<Book>> GetBookAsync(int id)
{
    var entity = await ...;
    if (entity is null)
        return new Error(BookErrorCodes.BookNotFound, "Book was not found.");
    return MapToDomain(entity); // implicit conversion to TryResult<Book>
}

// Service checks result before proceeding
var result = await _bookDao.GetBookAsync(id);
if (!result.IsSuccess)
    return result.Error!; // propagate upward

return result.Value!;
```

**In the controller**, `ApiControllerBase.HandleErrorResponse()` translates domain `Error` objects to `ProblemDetails` HTTP responses. The mapping from error code to status code lives in the controller — not in the domain.

**Adding a new error code:**
1. Add a string constant to the relevant `*ErrorCodes` class in `Books.Domain`
2. Add a `case` for it in the appropriate controller's `HandleErrorResponse` override

See also: [Naming Conventions](naming-conventions.md) for error code naming, [Controllers](controllers.md) for error translation.
