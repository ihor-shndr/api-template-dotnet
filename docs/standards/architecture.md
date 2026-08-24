# Architecture: Clean Architecture

## Solution Layout

```
Books.slnx                    # Solution file (modern .slnx format — do not convert to .sln)
src/
  Books.API/                  # HTTP layer: controllers, contracts (DTOs), mappers, DI modules
  Books.Domain/               # Business logic: services, interfaces, domain models, error codes
  Books.Data/                 # Data access: repositories (DAOs), entities, seed data
  Books.Common/               # Shared primitives: TryResult error monad
tests/
  Books.UnitTests/            # xUnit v3 unit tests (Microsoft Testing Platform), NSubstitute for mocking
```

**Dependency direction (strict):**
```
API → Domain → Common
Data → Domain → Common
API → Data (only via DI module wiring; no direct source references)
```

Never introduce a reference that goes against this flow.

## Layer Responsibilities

Each layer has a clearly defined responsibility. When adding a feature:

| Layer | What lives here | What does NOT live here |
|---|---|---|
| `Books.API` | Controllers, DTOs (contracts), mappers, HTTP error translation | Business logic, DB calls, domain rules |
| `Books.Domain` | Service interfaces + implementations, domain models, error codes, DAO interfaces | HTTP concerns, EF entities, connection strings |
| `Books.Data` | DAO implementations, EF entities, migrations, seed data | Domain models, business rules, HTTP types |
| `Books.Common` | Reusable cross-cutting primitives (TryResult) | Domain or app-specific logic |

When you are unsure which layer a class belongs in, ask: _does this concept exist without HTTP? Without a database?_ Place it at the lowest layer where it still makes sense.

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

See also: [Error Handling](error-handling.md), [Naming Conventions](naming-conventions.md), [Dependency Injection](dependency-injection.md), [Controllers](controllers.md), [Data Access](data-access.md), [Testing](testing.md).
