# Dependency Injection

Services are registered in static extension methods (modules), not inline in `Program.cs`.

Modules deliberately declare `namespace Microsoft.Extensions.DependencyInjection;` so they are in scope in `Program.cs` without an extra `using`. This is the one place where the namespace does not follow the folder path — keep it, or `Program.cs` will not compile.

```csharp
// src/Books.API/Modules/BooksModule.cs
using Books.Data.Books.DataAccess;
using Books.Domain.Books.DataAccess;
using Books.Domain.Books.Services;
using Books.Domain.Books.Services.Implementation;
using Books.API.Mappers.Books;
using Books.API.Mappers.Books.Implementation;

namespace Microsoft.Extensions.DependencyInjection;

public static class BooksModule
{
    public static void AddBooksModule(this IServiceCollection services)
    {
        services.AddTransient<IBookDao, BookDao>();
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IBookMapper, BookMapper>();
    }
}
```

The DAO registration is the **only** place the API layer names a `Books.Data` type. That single line is what keeps the API → Data dependency confined to wiring — see [Architecture](architecture.md).

All services are registered as **Transient** unless there is an explicit, documented reason to choose a different lifetime. Never use Singleton for anything that touches user data or request state.

## Two module shapes

| Extends | Use when | Called as |
|---|---|---|
| `IServiceCollection` | Registering services only (the common case) | `builder.Services.AddBooksModule();` |
| `IHostApplicationBuilder` | The module also needs `Configuration` | `builder.AddDbModule();` |

`DbModule` uses the second shape because it binds `DatabaseConfig` from configuration.

When adding a new domain area, create a new `{Domain}Module.cs` in `Books.API/Modules/` and call it from `Program.cs`.
