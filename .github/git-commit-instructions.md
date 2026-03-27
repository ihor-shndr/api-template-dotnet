# Git Commit Instructions

## Goal
Write commits that clearly explain what changed and why.

## Commit Format
Use Conventional Commits:

`<type>(<optional-scope>): <short summary>`

Examples:
- `feat(api): add endpoint for book search`
- `fix(data): handle null author in mapper`
- `chore(solution): migrate solution from .sln to .slnx`

## Allowed Types
- `feat`: new behavior or capability
- `fix`: bug fix
- `refactor`: code change without behavior change
- `perf`: performance improvement
- `test`: new or updated tests
- `docs`: documentation only
- `build`: build tooling or dependencies
- `ci`: CI/CD changes
- `chore`: maintenance tasks
- `revert`: revert a previous commit

## Subject Line Rules
- Use imperative mood (`add`, `fix`, `remove`), not past tense.
- Keep it specific; avoid vague text like `update` or `small changes`.
- Keep it under 72 characters when possible.
- Do not end with a period.

## Body Rules
Add a body when the change is non-trivial.

Include:
1. What changed (key files/components).
2. Why it changed (problem or objective).
3. Any side effects, migration notes, or risks.

## Footer Rules
- Reference issues/tickets when applicable, for example: `Refs: #123`.
- For breaking changes, add: `BREAKING CHANGE: <description>`.

## Quality Gates Before Commit
- Commit only one logical change per commit.
- Ensure `dotnet build` passes.
- Ensure tests pass for affected areas.
- Do not commit generated noise or unrelated file changes.
