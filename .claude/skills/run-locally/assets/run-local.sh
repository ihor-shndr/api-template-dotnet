#!/usr/bin/env bash
set -euo pipefail
dotnet run --project src/Books.API --launch-profile http
