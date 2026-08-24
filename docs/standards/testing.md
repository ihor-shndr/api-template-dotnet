# Testing

**Framework:** xUnit v3 running on [Microsoft Testing Platform (MTP)](https://learn.microsoft.com/dotnet/core/testing/microsoft-testing-platform-intro), with NSubstitute for test doubles. No NUnit, no Moq — do not reintroduce them. (`xunit.runner.visualstudio` is still referenced so IDE test explorers work; tests run under MTP, not VSTest.)

MTP is opted into by `global.json` at the repo root:

```json
{ "test": { "runner": "Microsoft.Testing.Platform" } }
```

The test project must also set both of these in its `.csproj`:

```xml
<OutputType>Exe</OutputType>
<UseMicrosoftTestingPlatformRunner>true</UseMicrosoftTestingPlatformRunner>
```

xUnit v3 test projects are executables and the build fails without `OutputType`.

Run tests with `dotnet test tests/Books.UnitTests/Books.UnitTests.csproj`. For coverage, add `--coverage` (provided by `Microsoft.Testing.Extensions.CodeCoverage`). The VSTest-era `--collect:"XPlat Code Coverage"` / coverlet collector flow does **not** work under MTP.

**Location:** `tests/Books.UnitTests/{Domain}/`

**Conventions:**
- Class name: `{ClassUnderTest}Tests`
- Method name: `{MethodName}_{Condition}_{ExpectedOutcome}` (e.g., `GetBookAsync_WhenBookExists_ReturnsBook`)
- Mark tests with `[Fact]` (or `[Theory]` + `[InlineData]` for parameterised cases)
- Initialize substitutes and the system under test in the **constructor** — xUnit creates a new instance per test, so there is no `[SetUp]`. Use `IDisposable`/`IAsyncLifetime` for teardown if ever needed.
- Substitute all dependencies via `Substitute.For<T>()` (not `Mock<T>`), inject via constructor, and store them in `private readonly` fields
- Unit tests target the domain service. The DAO is in-memory seed data and the controller is a thin delegation, so neither is worth unit testing today.

**NSubstitute cheat sheet:**

```csharp
private readonly IBookDao _bookDao = Substitute.For<IBookDao>();

// stub a return value (works directly for Task<T>-returning methods)
_bookDao.GetBookAsync(bookId).Returns(TryResult.Success(book));

// assert a call happened exactly once
await _bookDao.Received(1).GetBookAsync(bookId);

// assert a call never happened
await _bookDao.DidNotReceive().GetBookAsync(Arg.Any<int>());
```

Assertions are xUnit's, and **expected comes first**: `Assert.Equal(expected, actual)`, `Assert.True(x)`, `Assert.Null(x)`. There is no `Assert.Multiple` — write sequential asserts.

**What to test:**
- All service methods with at least one success path and one failure path
- Error code propagation (verify the exact `BookErrorCodes.*` string is returned on failure)
- Mappers may be tested in isolation (`BookMapperTests` does), but do not add mapper tests for mappings that are a straight field-for-field copy unless you are pinning a contract

When adding a new service, add a corresponding test class before or alongside the implementation.
