---
name: run-locally
description: 'Start or stop the Books API locally with Docker Compose. Use when: starting the app, tearing down containers.'
argument-hint: '[--down] to stop'
---

# Run the App Locally

## Usage
```bash
# Start
.github/skills/run-locally/assets/run-local.sh

# Stop
.github/skills/run-locally/assets/run-local.sh --down
```

The script runs `docker compose up -d --wait` which waits for health checks to pass.

Access the app at `http://localhost:8080`.
