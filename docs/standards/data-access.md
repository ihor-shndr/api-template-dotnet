# Data Access

- DAO interfaces live in `Books.Domain/{Domain}/DataAccess/`
- DAO implementations live in `Books.Data/{Domain}/DataAccess/`
- Entities live in `Books.Data/{Domain}/Entities/`
- Seed data lives in `Books.Data/{Domain}/DataAccess/` as internal static classes

The project currently uses in-memory seed data (`BookSeedData`). A real database connection is wired but not implemented — `Database__ConnectionString` in `appsettings.Development.json` points to SQL Server. When adding EF Core, add it to `Books.Data` only; register it in `DbModule`.
