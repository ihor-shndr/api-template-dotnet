---
name: ci-explorer
description: 'Debug failed GitHub Actions runs by fetching and analysing logs with gh CLI.'
---

# CI Explorer

Investigate a failed GitHub Actions run and explain what went wrong.

## Steps

1. Find the failed run — `gh run list --status failure --limit 5` or use a run ID if provided.
2. Fetch logs — `gh run view <run-id> --log-failed`.
3. Read the error output and identify the root cause.
4. Suggest a fix or next steps.

## Rules

- Use `gh` CLI only. Do not open a browser.
- Do not modify any files. Diagnose only.
- Keep the summary short — error, cause, suggested fix.
