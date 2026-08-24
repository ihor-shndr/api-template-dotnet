# Architecture: Clean Architecture

## Solution Layout

```
Books.slnx                    # Solution file (modern .slnx format — do not convert to .sln)
src/
  Books.API/                  # HTTP layer: controllers, contracts (DTOs), mappers, DI modules, config
  Books.Domain/               # Business logic: services, interfaces, domain models, error codes
  Books.Data/                 # Data access: DAOs, entities, seed data
  Books.Common/               # Shared primitives: TryResult error monad
  Books.Web/                  # Vite + React frontend — NOT part of Books.slnx
tests/
  Books.UnitTests/            # xUnit v3 unit tests (Microsoft Testing Platform), NSubstitute for mocking
```

`Books.Web` is deliberately outside the solution, so `dotnet build` / `dotnet format Books.slnx` never see it. Check it with `npm run lint` and `npm run build` instead.

**Dependency direction (strict):**
```
API → Domain → Common
API → Data          (project reference exists; source references confined to DI wiring)
Data → Domain → Common
UnitTests → API, Domain
```

`Books.API` does hold a `ProjectReference` to `Books.Data` — it has to, in order to name `BookDao`. The rule is about *source* coupling: `Books.Data` may be referenced from `Modules/*.cs` only. Everywhere else in the API layer, depend on the `Books.Domain` interface. Never introduce a reference that goes against this flow.

## Layer Responsibilities

Each layer has a clearly defined responsibility. When adding a feature:

| Layer | What lives here | What does NOT live here |
|---|---|---|
| `Books.API` | Controllers, DTOs (contracts), mappers, HTTP error translation, typed configuration | Business logic, DB calls, domain rules |
| `Books.Domain` | Service interfaces + implementations, domain models, error codes, DAO interfaces, logging | HTTP concerns, data entities, connection strings |
| `Books.Data` | DAO implementations, data entities, seed data | Domain models, business rules, HTTP types |
| `Books.Common` | Reusable cross-cutting primitives (TryResult) | Domain or app-specific logic |

Domain services may take `ILogger<T>` (via `Microsoft.Extensions.Logging.Abstractions`) — logging is not an HTTP concern and belongs at the layer where the decision is made.

When you are unsure which layer a class belongs in, ask: _does this concept exist without HTTP? Without a database?_ Place it at the lowest layer where it still makes sense.

## Adding a New Domain Area

Follow this checklist in order:

1. **Domain layer** (`Books.Domain/{NewDomain}/`)
   - `Models/{NewEntity}.cs` — domain model
   - `Models/{NewEntity}ErrorCodes.cs` — error code constants
   - `DataAccess/I{NewEntity}Dao.cs` — DAO interface
   - `Services/I{NewEntity}Service.cs` — service interface
   - `Services/Implementation/{NewEntity}Service.cs` — service implementation

2. **Data layer** (`Books.Data/{NewDomain}/`)
   - `Entities/{NewEntity}Entity.cs` — data entity
   - `DataAccess/{NewEntity}Dao.cs` — DAO implementation

3. **API layer** (`Books.API/`)
   - `Contracts/{NewDomain}/{NewEntity}Contract.cs` — response DTO
   - `Mappers/{NewDomain}/I{NewEntity}Mapper.cs` + `Mappers/{NewDomain}/Implementation/{NewEntity}Mapper.cs`
   - `Controllers/{NewEntity}Controller.cs`
   - `Modules/{NewDomain}Module.cs` — register the service, the mapper, **and the DAO**

4. **Tests** (`tests/Books.UnitTests/{NewDomain}/`)
   - `{NewEntity}ServiceTests.cs`

5. **Wire up** in `Program.cs`: `builder.Services.Add{NewDomain}Module();`

Domain models are plain classes with `required` init-only properties, except where a value genuinely mutates. Note that the `Book` domain model has **no `Id`** — identity currently lives only on `BookEntity`, and the frontend fills it in from the requested route value. If you add a list or create endpoint, expect to put `Id` on the domain model first.

See also: [Error Handling](error-handling.md), [Naming Conventions](naming-conventions.md), [Dependency Injection](dependency-injection.md), [Controllers](controllers.md), [Data Access](data-access.md), [Testing](testing.md).
