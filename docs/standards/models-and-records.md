# Models and Records

Prefer `record` types with `{ get; init; }` for:
- Domain models (`Book`)
- Data entities (`BookEntity`)
- API contracts (`BookContract`)
- Error types (`Error` in TryResult)

Use regular classes only for stateful services.

Nullable reference types are enabled project-wide. Do not use `#nullable disable`. Always handle nullability properly.
