# Data Access

- DAO interfaces live in `Books.Domain/{Domain}/DataAccess/`
- DAO implementations live in `Books.Data/{Domain}/DataAccess/`
- Entities live in `Books.Data/{Domain}/Entities/`
- Seed data lives in `Books.Data/{Domain}/DataAccess/` as internal static classes

The project currently uses in-memory seed data (`BookSeedData`). There is **no Entity Framework and no migrations** in the repo yet — when adding EF Core, add it to `Books.Data` only and register it in `DbModule`.

## Configuration

A database connection is configured but not yet opened. `DbModule` binds the `Database` section to the typed `DatabaseConfig` class in `Books.API/Configuration/`:

```json
// src/Books.API/appsettings.Development.json
{
  "Database": {
    "ConnectionString": "..."
  }
}
```

The same value is supplied to containers as the environment variable `Database__ConnectionString` (`docker-compose.yml`). The double underscore is ASP.NET Core's separator for nested configuration keys — `Database__ConnectionString` binds to `DatabaseConfig.ConnectionString`.

To add a new configuration section: create a typed class in `Books.API/Configuration/` with a `public const string SectionName`, then bind it with `services.Configure<T>()` inside the relevant module.
