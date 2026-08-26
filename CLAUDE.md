@AGENTS.md

## Workflow

All implementation work (new features, bug fixes, refactors) goes through the `coordinator` skill, which only the user can start.

Do not implement directly, and do not reproduce the coordinator's steps by hand. If a request looks like an implementation task and the coordinator is not already running, say so and ask the user to run `/coordinator`.
