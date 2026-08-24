# What to Avoid

- **Do not** add logic to controllers — delegate to services
- **Do not** reference `Books.Data` from `Books.Domain` (wrong direction)
- **Do not** throw exceptions for expected failure cases — use `TryResult` and `Error`
- **Do not** register services as `Singleton` without a clear reason
- **Do not** add inline service registration to `Program.cs` — use modules
- **Do not** expose EF entities or `BookEntity` through the API — always map to contracts
- **Do not** put seed data or SQL queries in the domain layer
- **Do not** skip the `TryResult` return type on DAOs or services in favor of nullable returns
- **Do not** convert the `.slnx` solution file back to `.sln`
