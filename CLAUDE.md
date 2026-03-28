@import AGENTS.md

## Workflow

For all implementation tasks (new features, bug fixes, refactors), use the `coordinator` subagent. Do not implement directly.

Example: if the user says *"Add a GET /api/books endpoint with a feature flag"*, delegate to `coordinator` — it will handle planning, branching, implementation, review, and PR creation.