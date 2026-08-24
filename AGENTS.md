# Agents Guide

This file provides AI agents with everything needed to work effectively in this codebase.

---

## Project Overview

**Books API** is a Clean Architecture template for .NET 10 REST APIs. Its purpose is to demonstrate patterns and conventions that should be followed when building new APIs at this org. It is intentionally minimal — a single domain (Books) with one GET endpoint — so the structure and conventions are easy to follow without business logic noise.

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
  Books.Web/                  # Vite + React frontend
tests/
  Books.UnitTests/            # xUnit v3 unit tests (Microsoft Testing Platform), NSubstitute for mocking
```

**Dependency direction (strict):** `API → Domain → Common`, `Data → Domain → Common`, `API → Data` only via DI module wiring. Never introduce a reference that goes against this flow. Full detail: [docs/standards/architecture.md](docs/standards/architecture.md).

---

## Coding Standards

Detailed conventions live in `docs/standards/` — read the relevant doc before touching that part of the codebase:

| Doc | Covers |
|---|---|
| [architecture.md](docs/standards/architecture.md) | Layer responsibilities, dependency direction, checklist for adding a new domain area |
| [error-handling.md](docs/standards/error-handling.md) | The `TryResult` pattern, adding error codes |
| [naming-conventions.md](docs/standards/naming-conventions.md) | Naming for models, entities, contracts, services, DAOs, modules |
| [dependency-injection.md](docs/standards/dependency-injection.md) | DI module pattern, service lifetimes |
| [controllers.md](docs/standards/controllers.md) | Controller structure and error translation |
| [data-access.md](docs/standards/data-access.md) | DAO/entity/seed-data layout, current in-memory persistence |
| [testing.md](docs/standards/testing.md) | xUnit v3 + NSubstitute setup and conventions |
| [models-and-records.md](docs/standards/models-and-records.md) | Record vs. class usage, nullable reference types |
| [what-to-avoid.md](docs/standards/what-to-avoid.md) | Quick checklist of common mistakes |

These standards apply regardless of which feature you're implementing. For the technical design of a specific in-development feature, check `docs/tech-designs/` first.

---

## Commit Conventions

See `.github/git-commit-instructions.md` for the full commit message rules. In short: [Conventional Commits](https://www.conventionalcommits.org/) format, imperative mood, one logical change per commit. Build and tests must pass before committing.

---

## Local Development

```bash
dotnet run --project src/Books.API    # Start the API
```

The health check endpoint is `GET /api/health`.

**API documentation** uses the built-in ASP.NET Core OpenAPI stack (`Microsoft.AspNetCore.OpenApi`) with [Scalar](https://scalar.com/) as the UI. Swashbuckle / Swagger UI has been removed — do not reintroduce it.

`Program.cs` registers `builder.Services.AddOpenApi()`, then `app.MapOpenApi()` and `app.MapScalarApiReference()` inside the `IsDevelopment()` block, so docs are **not** served outside Development. Because `app.UsePathBase(new PathString("/api"))` applies to the whole app, both live under `/api`:

- OpenAPI document: `http://localhost:5265/api/openapi/v1.json`
- Scalar UI: `http://localhost:5265/api/scalar`

Note that controllers declare routes *without* the `api/` prefix (e.g. `[Route("books")]`) — the prefix comes from `UsePathBase`, not from the route template. Keep it that way.

Environment variables follow the ASP.NET Core double-underscore convention for nested config:
- `Database__ConnectionString` → `DatabaseConfig.ConnectionString`

When adding new configuration sections, create a typed config class in `Books.API/Configuration/` and bind it in `Program.cs` using `services.Configure<T>()`.
