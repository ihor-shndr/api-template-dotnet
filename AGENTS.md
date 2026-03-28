# Agents Guide

This file provides AI agents with everything needed to work effectively in this codebase.

---

## Project Overview

**Books API** is a Clean Architecture template for .NET 8 REST APIs. Its purpose is to demonstrate patterns and conventions that should be followed when building new APIs at this org. It is intentionally minimal — a single domain (Books) with one GET endpoint — so the structure and conventions are easy to follow without business logic noise.

Do not mistake simplicity for incompleteness. The architecture, error handling, naming, and DI patterns are intentional and should be preserved when adding features.

---

## Solution Layout

```
Books.slnx                    # Solution file (modern .slnx format — do not convert to .sln)
src/
  Books.API/                  # HTTP layer: controllers, contracts (DTOs), mappers, DI modules
  Books.Domain/               # Business logic: services, interfaces, domain models, error codes
  Books.Data/                 # Data access: repositories (DAOs), entities, seed data
  Books.Common/               # Shared primitives: TryResult error monad
tests/
  Books.UnitTests/            # NUnit unit tests, Moq for mocking
```

**Dependency direction (strict):**
```
API → Domain → Common
Data → Domain → Common
API → Data (only via DI module wiring; no direct source references)
```

Never introduce a reference that goes against this flow.

---

## Architecture: Clean Architecture

Each layer has a clearly defined responsibility. When adding a feature:

| Layer | What lives here | What does NOT live here |
|---|---|---|
| `Books.API` | Controllers, DTOs (contracts), mappers, HTTP error translation | Business logic, DB calls, domain rules |
| `Books.Domain` | Service interfaces + implementations, domain models, error codes, DAO interfaces | HTTP concerns, EF entities, connection strings |
| `Books.Data` | DAO implementations, EF entities, migrations, seed data | Domain models, business rules, HTTP types |
| `Books.Common` | Reusable cross-cutting primitives (TryResult) | Domain or app-specific logic |

When you are unsure which layer a class belongs in, ask: _does this concept exist without HTTP? Without a database?_ Place it at the lowest layer where it still makes sense.

---

## Error Handling: TryResult Pattern

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

---

## Naming Conventions

| Concept | Convention | Example |
|---|---|---|
| Domain model | Plain noun | `Book` |
| Data entity | `{Domain}Entity` | `BookEntity` |
| DTO / API contract | `{Domain}Contract` | `BookContract` |
| Service interface | `I{Domain}Service` | `IBookService` |
| DAO interface | `I{Domain}Dao` | `IBookDao` |
| Mapper interface | `I{Domain}Mapper` | `IBookMapper` |
| DI module class | `{Domain}Module` | `BooksModule` |
| Error codes class | `{Domain}ErrorCodes` | `BookErrorCodes` |
| Error code value | `{Domain}.{PascalCase}` | `"Books.NotFound"` |

Namespaces follow the folder structure exactly. Do not flatten or skip folders.

---

## Dependency Injection

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

---

## Controllers

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

---

## Data Access

- DAO interfaces live in `Books.Domain/{Domain}/DataAccess/`
- DAO implementations live in `Books.Data/{Domain}/DataAccess/`
- Entities live in `Books.Data/{Domain}/Entities/`
- Seed data lives in `Books.Data/{Domain}/DataAccess/` as internal static classes

The project currently uses in-memory seed data (`BookSeedData`). A real database connection is wired but not implemented — `Database__ConnectionString` in `appsettings.Development.json` points to SQL Server. When adding EF Core, add it to `Books.Data` only; register it in `DbModule`.

---

## Testing

**Framework:** NUnit 3 with Moq.

**Location:** `tests/Books.UnitTests/{Domain}/`

**Conventions:**
- Class name: `{ClassUnderTest}Tests`
- Method name: `{MethodName}_{Condition}_{ExpectedOutcome}` (e.g., `GetBookAsync_WhenBookExists_ReturnsBook`)
- Use `[SetUp]` to initialize mocks and the system under test
- Test the domain service, not the DAO or controller in unit tests
- Mock all dependencies via `Mock<T>`, inject via constructor

**What to test:**
- All service methods with at least one success path and one failure path
- Error code propagation (verify the exact `BookErrorCodes.*` string is returned on failure)
- Do not test mappers in isolation unless the mapping is non-trivial

When adding a new service, add a corresponding test class before or alongside the implementation.

---

## Models and Records

Prefer `record` types with `{ get; init; }` for:
- Domain models (`Book`)
- Data entities (`BookEntity`)
- API contracts (`BookContract`)
- Error types (`Error` in TryResult)

Use regular classes only for stateful services.

Nullable reference types are enabled project-wide. Do not use `#nullable disable`. Always handle nullability properly.

---

## Adding a New Domain Area

Follow this checklist in order:

1. **Domain layer** (`Books.Domain/{NewDomain}/`)
   - `Models/{NewEntity}.cs` — domain model (record)
   - `Models/{NewEntity}ErrorCodes.cs` — error code constants
   - `DataAccess/I{NewEntity}Dao.cs` — DAO interface
   - `Services/I{NewEntity}Service.cs` — service interface
   - `Services/{NewEntity}Service.cs` — service implementation

2. **Data layer** (`Books.Data/{NewDomain}/`)
   - `Entities/{NewEntity}Entity.cs` — data entity
   - `DataAccess/{NewEntity}Dao.cs` — DAO implementation

3. **API layer** (`Books.API/`)
   - `Contracts/{NewDomain}/{NewEntity}Contract.cs` — response DTO
   - `Mappers/I{NewEntity}Mapper.cs` + `{NewEntity}Mapper.cs`
   - `Controllers/{NewEntity}Controller.cs`
   - `Modules/{NewDomain}Module.cs` — register new services

4. **Tests** (`tests/Books.UnitTests/{NewDomain}/`)
   - `{NewEntity}ServiceTests.cs`

5. **Wire up** in `Program.cs`: `builder.Services.Add{NewDomain}Module();`

---

## Commit Conventions

See `.github/git-commit-instructions.md` for the full commit message rules. In short: [Conventional Commits](https://www.conventionalcommits.org/) format, imperative mood, one logical change per commit. Build and tests must pass before committing.

---

## Local Development

```bash
dotnet run --project src/Books.API    # Start the API
```

The health check endpoint is `GET /api/health`.

Environment variables follow the ASP.NET Core double-underscore convention for nested config:
- `Database__ConnectionString` → `DatabaseConfig.ConnectionString`

When adding new configuration sections, create a typed config class in `Books.API/Configuration/` and bind it in `Program.cs` using `services.Configure<T>()`.

---

## What to Avoid

- **Do not** add logic to controllers — delegate to services
- **Do not** reference `Books.Data` from `Books.Domain` (wrong direction)
- **Do not** throw exceptions for expected failure cases — use `TryResult` and `Error`
- **Do not** register services as `Singleton` without a clear reason
- **Do not** add inline service registration to `Program.cs` — use modules
- **Do not** expose EF entities or `BookEntity` through the API — always map to contracts
- **Do not** put seed data or SQL queries in the domain layer
- **Do not** skip the `TryResult` return type on DAOs or services in favor of nullable returns
- **Do not** convert the `.slnx` solution file back to `.sln`
