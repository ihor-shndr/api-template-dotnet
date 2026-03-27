#!/usr/bin/env bash
# Usage: ./run-local.sh [--down]
set -euo pipefail

if [[ "${1:-}" == "--down" ]]; then
  docker compose down
  exit 0
fi

docker compose up -d --wait
echo "✅ App is running: http://localhost:8080"
