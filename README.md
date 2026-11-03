# 📚 Books API

A **.NET 10 Clean Architecture** REST API template. The app is intentionally minimal — one domain, one endpoint — so the structure and conventions are easy to follow.

Needs the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0), and [`gh`](https://cli.github.com/) for PRs and CI. The demo React frontend lives in its own repository, [api-template-dotnet-web](https://github.com/ihor-shndr/api-template-dotnet-web).

## 📖 Where to start

- [AGENTS.md](AGENTS.md) — project overview, solution layout, standards index
- [docs/standards/](docs/standards/) — coding conventions
- [docs/tech-designs/](docs/tech-designs/) — designs for features in development

## ▶️ Run it

```bash
dotnet run --project src/Books.API --launch-profile http
```

The API answers at `http://localhost:5265/api/v1`, with a health check at `/health` and, in Development, the Scalar API reference at `/scalar`.

## 📁 What's in `.claude/`

```
skills/
  run-locally/   ← ⚡ /run-locally — start the API
  docs-drift/    ← ⚡ /docs-drift — check docs against any diff
  ci-explorer/   ← ⚡ /ci-explorer — debug a red CI run
launch.json      ← ▶️ run configurations (API http/https/docker)
```

[`.mcp.json`](.mcp.json) declares the task tracker's MCP server.

## 🎯 Demo tasks

The **ADLC Demo** project in the tracker holds real tasks for this repo, each linked to its Confluence requirements:

- *Implement Delete Book*
- *Implement Edit Book*
- *Implement Create Book*

List Books and Book Details are already done.
