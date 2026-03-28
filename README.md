# 📚 Books API — Multi-Agent AI Demo

A **.NET 8 Clean Architecture** REST API template with a **multi-agent AI setup** baked in. The app itself is intentionally minimal (one domain, one endpoint) so you can focus on how the agents work together rather than business logic.

## 📋 Prerequisites

**AI tools** (pick one or both):

- [Claude Code](https://docs.anthropic.com/en/docs/claude-code) (CLI or VS Code extension) — Anthropic's coding agent
- [GitHub Copilot](https://github.com/features/copilot) (VS Code / JetBrains) — GitHub's AI coding assistant

Both tools read agent definitions from `.agents/`. Claude Code uses a `.claude/` symlink; Copilot picks up agents natively from `.agents/`.

**💡 Key concepts you'll see here:**

- 🤖 **Agent** — an AI sub-process with a focused role, its own system prompt, and constraints on what it can/cannot do. Defined in `.agents/agents/*.agent.md`.
- ⚡ **Skill** — a reusable slash command (like `/run-locally`) that triggers a predefined workflow. Defined in `.agents/skills/*/SKILL.md`.
- 🔌 **MCP (Model Context Protocol)** — a standard that lets AI tools call external services (GitHub, Jira, browsers, etc.) via plugins. The `coordinator` uses GitHub MCP to create draft PRs. The `manual-qa` agent can use browser MCP to test the UI.
- 📖 **AGENTS.md** — a shared context file that all AI tools load automatically. Contains architecture rules, naming conventions, and patterns the agents must follow.

**Other tools:**

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) — to build and run the app
- [Node.js](https://nodejs.org/) — Playwright MCP runs via npx
- [GitHub CLI (`gh`)](https://cli.github.com/) — used by `/ci-explorer` for CI logs

## 🤔 Why multi-agent?

A single AI chat can write code, but it tries to do everything at once — plan, implement, review, test. That leads to shortcuts, missed issues, and a bloated context window that degrades quality as the conversation grows.

Sub-agents solve both problems. Each one runs in **its own context window** — it gets only the information it needs, does its job, and returns a short result to the coordinator. This keeps every agent focused and prevents the context from filling up with irrelevant history. It also mirrors how a real team works:

| Role | Human team | AI agent |
|------|-----------|----------|
| 🧑‍💼 Tech lead | Plans the approach, delegates, reviews scope | `coordinator` |
| 👨‍💻 Developer | Writes code and tests | `implement` |
| 🔍 Code reviewer | Catches bugs, checks standards | `code-review` |
| 🧪 QA engineer | Verifies fixes in a running app | `manual-qa` |

Each agent has **one job**, a **focused prompt**, and **clear boundaries** (e.g. the reviewer cannot edit files, the implementer cannot commit). This separation prevents the "do everything" drift you get with a single prompt.

## ⚙️ How it works

```
You: "Add a GET /api/books endpoint with a feature flag"
 |
 v
+--------------+
| Coordinator  |  1. Reads codebase, creates a plan, asks for your OK
+--------------+
 |
 v
+--------------+
| Implement    |  2. Writes tests first, then code. Runs dotnet test & format
+--------------+
 |
 v
+--------------+
| Code Review  |  3. Checks against AGENTS.md rules
+--------------+
 |
 |-- NEEDS_REVISION --> back to Implement (max 2 rounds)
 |
 |-- APPROVED
 v (optional, for bug fixes)
+--------------+
| Manual QA    |  4. Hits the running API with curl/browser to verify
+--------------+
 |
 v
+--------------+
| Coordinator  |  5. Commits + creates draft PR + tags @codex
+--------------+
```

The coordinator never writes code. The implementer never commits. The reviewer never edits files. These constraints are what make the system reliable.

## 📁 Project structure

```
.agents/
  agents/
    coordinator.agent.md   ← 🧑‍💼 orchestrator (the only one you invoke directly)
    implement.agent.md     ← 👨‍💻 writes code and tests
    code-review.agent.md   ← 🔍 reviews against AGENTS.md rules
    manual-qa.agent.md     ← 🧪 tests the running app
  skills/
    run-locally/           ← ⚡ /run-locally — start the API via dotnet run
    docs-drift/            ← ⚡ /docs-drift — check if docs match recent commits
    ci-explorer/           ← ⚡ /ci-explorer — debug failed GitHub Actions runs
  settings.local.json      ← 🔒 tool permissions

.mcp.json                  ← 🔌 MCP servers for Claude Code
.vscode/mcp.json           ← 🔌 MCP servers for GitHub Copilot
.github/git-commit-instructions.md ← 📝 commit message rules (Conventional Commits)

AGENTS.md                  ← 📖 shared codebase context (architecture, conventions, patterns)
CLAUDE.md                  ← 📎 imports AGENTS.md for Claude Code
```

### 📌 What goes where

| File | Purpose | Who reads it |
|------|---------|-------------|
| `AGENTS.md` | Architecture rules, naming conventions, error patterns, what to avoid | All agents + Copilot |
| `*.agent.md` | Single agent's role, workflow steps, output format, constraints | That specific agent |
| `SKILL.md` | Reusable slash command (like a script with AI reasoning) | The tool running it |

## 🔌 MCP servers

Agents can call external services via [MCP (Model Context Protocol)](https://modelcontextprotocol.io/) servers:

| MCP Server | Used by | Purpose |
|------------|---------|---------|
| GitHub | `coordinator` | Create draft PRs, add comments, tag reviewers |
| Playwright | `manual-qa`, any agent | Browser automation, UI testing, page snapshots |

MCP configs are included in the repo and ready to use:

| File | Tool | Notes |
|------|------|-------|
| `.mcp.json` | Claude Code | Set `GITHUB_PERSONAL_ACCESS_TOKEN` env var (remote API, no Docker) |
| `.vscode/mcp.json` | GitHub Copilot | GitHub MCP uses Copilot OAuth (auto), no token needed |

Both files configure:
- **GitHub** — for creating draft PRs and comments (`coordinator`)
- **Playwright** — for browser-level testing (`manual-qa`)

The agents will work without MCP — the `coordinator` will just skip PR creation and the `manual-qa` agent will fall back to `curl`.

## 🔗 Shared config across tools

All AI configuration lives in `.agents/` and is shared via symlinks:

```
.claude/ → .agents/    (Claude Code)
```

Copilot reads `.agents/` directly — no symlink needed. This means the same agents, skills, and rules work across tools without duplication.

## 🚀 Running the app

```bash
dotnet run --project src/Books.API
```

API at `http://localhost:5265`. Health check: `GET /api/health`.

## 🎮 Try it

### Claude Code (CLI or VS Code)

`CLAUDE.md` tells Claude to route all implementation tasks through the `coordinator` automatically. Just describe what you want:

```
> Add a GET /api/books endpoint that returns all books, guarded by a .NET feature flag EnableBooksList using Microsoft.FeatureManagement
```

Claude will pick up the `coordinator` agent, which will plan → branch → implement → review → commit → draft PR — without you invoking anything manually.

### GitHub Copilot

In Copilot Chat, select the `coordinator` agent from the dropdown, then type your request.

### Example 1: Feature implementation

Ask for a feature — the coordinator handles the full cycle:

- *"Add a GET /api/books endpoint with a .NET feature flag `EnableBooksList`"*
- *"Add a GET endpoint that returns a book by ID"*
- *"The health check returns 500 when the DB is unreachable — fix it"*

### Example 2: Debug a failed CI run

Use the `/ci-explorer` skill to investigate GitHub Actions failures:

- *"CI is red, what happened?"*
- *"/ci-explorer"*

It will fetch the failed run logs with `gh`, find the error, and suggest a fix.

The agents are simple markdown files. Read them, tweak them, break them — that's the point of a demo project. 🛠️
