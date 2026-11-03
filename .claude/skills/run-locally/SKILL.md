---
name: run-locally
description: 'Start the Books API locally.'
---

# Run the API Locally

Run `.claude/skills/run-locally/assets/run-local.sh`, or directly:

```bash
dotnet run --project src/Books.API --launch-profile http
```

- Base URL: `http://localhost:5265/api/v1` (the app sets a `/api/v1` path base)
- Health check: `GET http://localhost:5265/api/v1/health`
- Scalar API reference (Development only): `http://localhost:5265/api/v1/scalar`

The React frontend lives in its own repository, [api-template-dotnet-web](https://github.com/ihor-shndr/api-template-dotnet-web). Dev-only CORS for its origin, `http://localhost:5173`, stays configured in `Program.cs`.

## Rules

- Never block the session waiting on a server — start it in the background or via the `Books API - HTTP` launch configuration.
- **The port is fixed, not incidental.** The frontend defaults to `http://localhost:5265/api/v1`, and the launch entry sets `"autoPort": false`, so a busy port fails loudly rather than binding elsewhere.
- If the port is already in use, the API is probably already running — including from another session. Check the health endpoint and reuse it instead of starting a second instance.
