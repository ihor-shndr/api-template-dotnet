---
name: docs-drift
description: 'Analyse the last 10 commits on main and update AGENTS.md and README.md if they have drifted from the code.'
---

# Documentation Drift Check

Review recent changes and fix any documentation that has fallen out of sync.

## Steps

1. **Gather changes** — Run `git log --oneline -10 main` and `git diff HEAD~10..HEAD --stat` to understand what changed recently.
2. **Read docs** — Read `AGENTS.md` and `README.md` in full.
3. **Compare** — For each commit, check whether the change affects anything documented:
   - Solution layout or project structure
   - Architecture layers or dependency direction
   - Naming conventions
   - Error handling patterns
   - DI registration patterns
   - Local dev setup
   - Testing conventions
   - Available skills or agents
4. **Update** — Edit only the sections that are wrong or missing. Do not rewrite sections that are still accurate.
5. **Report** — List what you changed and why, or confirm that docs are up to date.

## Rules

- Only update `AGENTS.md` and `README.md`. Do not create new documentation files.
- Keep edits minimal — fix what drifted, nothing more.
- If a commit added a new domain area, verify the "Adding a New Domain Area" checklist in AGENTS.md still applies.
- If no drift is found, say so and stop. Do not make cosmetic changes.
