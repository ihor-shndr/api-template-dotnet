# Naming Conventions

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

One public type per file, and the file is named after the type it contains.

## Namespaces

Namespaces follow the folder structure, with two deliberate exceptions:

- **`Books.API` uses the root namespace `BookShelf.API`**, set via `<RootNamespace>` in `Books.API.csproj`. Every other project uses its own name as the root (`Books.Domain.*`, `Books.Data.*`, `Books.Common.*`). This is a leftover from an earlier rename; match the existing files rather than mixing both.
- **DI modules declare `namespace Microsoft.Extensions.DependencyInjection;`** so `Program.cs` picks up the extension methods without an extra `using`. See [Dependency Injection](dependency-injection.md).

Outside those two cases, do not flatten or skip folders. Note that interface and implementation are split: the interface sits in the folder (`Services/IBookService.cs`), the implementation one level down in `Implementation/` (`Services/Implementation/BookService.cs`), and the namespace follows.
