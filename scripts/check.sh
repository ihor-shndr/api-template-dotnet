#!/usr/bin/env bash
set -euo pipefail

cd "$(dirname "${BASH_SOURCE[0]}")/.."
dotnet restore Books.slnx
dotnet build Books.slnx --configuration Release --no-restore
dotnet format Books.slnx --verify-no-changes
dotnet test tests/Books.UnitTests/Books.UnitTests.csproj --configuration Release --no-restore
