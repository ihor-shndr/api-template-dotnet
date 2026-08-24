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

Namespaces follow the folder structure exactly. Do not flatten or skip folders.
