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

Every project's root namespace is its own name (`Books.API.*`, `Books.Domain.*`, `Books.Data.*`, `Books.Common.*`), and namespaces then follow the folder structure. No project sets `<RootNamespace>` — MSBuild defaults it to the project name, which is what we want; adding an override that disagrees with the assembly name is how namespaces and folders drift apart.

There is one deliberate exception:

- **DI modules declare `namespace Microsoft.Extensions.DependencyInjection;`** so `Program.cs` picks up the extension methods without an extra `using`. See [Dependency Injection](dependency-injection.md).

Outside that one case, do not flatten or skip folders. Note that interface and implementation are split: the interface sits in the folder (`Services/IBookService.cs`), the implementation one level down in `Implementation/` (`Services/Implementation/BookService.cs`), and the namespace follows.
