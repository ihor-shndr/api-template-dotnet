# Dependency Injection

Services are registered in static extension methods (modules), not inline in `Program.cs`.

```csharp
// src/Books.API/Modules/BooksModule.cs
public static class BooksModule
{
    public static void AddBooksModule(this IServiceCollection services)
    {
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IBookMapper, BookMapper>();
    }
}
```

All services are registered as **Transient** unless there is an explicit, documented reason to choose a different lifetime. Never use Singleton for anything that touches user data or request state.

When adding a new domain area, create a new `{Domain}Module.cs` in `Books.API/Modules/` and call it from `Program.cs`.
