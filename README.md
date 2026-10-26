# 📚 Books API

A **.NET 10 Clean Architecture** REST API template. The app is intentionally minimal — one domain, a few endpoints, in-memory data — so the structure and conventions are easy to follow.

Needs the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0), and [Node.js](https://nodejs.org/) 20+ for the demo frontend.

## Layout

```
src/
  Books.API/      ← controllers, contracts, mappers, DI modules
  Books.Domain/   ← services, interfaces, domain models, error codes
  Books.Data/     ← DAOs, entities, in-memory seed data
  Books.Common/   ← TryResult error monad
  Books.Web/      ← Vite + React frontend (not part of Books.slnx)
tests/
  Books.UnitTests/
```

Conventions live in [AGENTS.md](AGENTS.md) and [docs/standards/](docs/standards/).

## Build and test

```bash
dotnet build Books.slnx
dotnet test tests/Books.UnitTests/Books.UnitTests.csproj
```

## Run

```bash
dotnet run --project src/Books.API --launch-profile http
npm install --prefix src/Books.Web && npm run dev --prefix src/Books.Web
```

The API listens on `http://localhost:5265/api/v1` (health check: `GET /api/v1/health`); the UI on `http://localhost:5173`.

## What's in `.claude/`

```
skills/
  run-locally/   ← /run-locally — start the API and frontend
  docs-drift/    ← /docs-drift — check docs against any diff
  ci-explorer/   ← /ci-explorer — debug a red CI run
launch.json      ← run configurations (API http/https/docker, frontend)
```
