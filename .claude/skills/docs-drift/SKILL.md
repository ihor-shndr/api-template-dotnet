---
name: docs-drift
description: 'Check whether the docs still match the code and fix what drifted. Works from any diff — uncommitted changes, a branch, a PR, or recent commits. Use when docs may be stale or when asked "are the docs still accurate?".'
---

# Documentation Drift Check

Find what changed, then fix any doc that no longer matches.

## 1. Get the diff

Use whatever the user pointed at. If they named nothing, take the first of these that returns something:

| Source | How |
|---|---|
| A PR | `gh pr diff <number>` |
| A branch or range | `git diff <base>...<head>` |
| Uncommitted work | `git diff HEAD` plus `git status --short` for new files |
| Recent commits | `git log --oneline -10` and `git diff HEAD~10..HEAD` |

Say which one you used.

## 2. Compare against the docs

Read `AGENTS.md`, `README.md`, `docs/standards/*`, and the docs under `.claude/`. Most rules live in `docs/standards/` — `AGENTS.md` is mostly links to them.

A doc is drifted only if the code disagrees with it. Verify before editing — check the actual file, endpoint, or command instead of assuming. Paths, ports, and shell commands drift most often. If the same fact is documented in two places, fix both and say so.

## 3. Fix and report

Edit only what is wrong. Do not create new docs, rewrite accurate sections, or make cosmetic changes. Report what you changed and why — or say the docs are current and stop.
