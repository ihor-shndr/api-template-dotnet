---
name: run-locally
description: 'Start the Books app locally — the .NET API, the Vite + React frontend, or both.'
---

# Run the App Locally

Start whichever part you need. The frontend calls the API, so if you are running the UI, run the API too.

## API (.NET)

Run `.claude/skills/run-locally/assets/run-local.sh`, or directly:

```bash
dotnet run --project src/Books.API --launch-profile http
```

- Base URL: `http://localhost:5265/api/v1` (the app sets a `/api/v1` path base)
- Health check: `GET http://localhost:5265/api/v1/health`
- Scalar API reference (Development only): `http://localhost:5265/api/v1/scalar`

## Frontend (Vite + React)

```bash
npm install --prefix src/Books.Web && npm run dev --prefix src/Books.Web
```

- UI: `http://localhost:5173`
- `npm install` is only needed the first time or after dependency changes.
- It reads the API base from `VITE_API_BASE_URL`, defaulting to `http://localhost:5265/api/v1`.
- Dev-only CORS for `http://localhost:5173` is already configured in `Program.cs`.

## Both at once

Each server runs in the foreground, so start them as two separate long-running processes — do not chain them with `&&`. In Claude Code, prefer the `launch.json` configurations (`Books API - HTTP`, `Books Web`) over raw shell commands so the servers are managed and their logs stay readable.

## Rules

- Never block the session waiting on a server — start it in the background or via a launch configuration.
- **The ports are fixed, not incidental.** The API's dev CORS policy allows `http://localhost:5173` only, and the frontend defaults to `http://localhost:5265/api/v1`. Both launch entries set `"autoPort": false` and Vite uses `strictPort`, so a busy port fails loudly rather than binding elsewhere and breaking every request.
- If a port is already in use, the app is probably already running — including from another session. Check the health endpoint and reuse it instead of starting a second instance.
- Verify the API is up before testing the UI; a UI error is often just a missing API.
