#!/usr/bin/env bash

#exit if any command fails
set -e

export FrameworkPathOverride=$(dirname $(which mono))/../lib/mono/4.5.2-api/

dotnet restore ./src/CacheManager.Serialization.Hyperion.sln

# Linux/Darwin
OSNAME=$(uname -s)
echo "OSNAME: $OSNAME"

dotnet build ./src/Tests/CacheManager.Serialization.Hyperion.Tests/CacheManager.Serialization.Hyperion.Tests.csproj /p:Configuration=Release || exit 1

sudo nuget install xunit.runner.console -OutputDirectory testrunner

echo --------------------
echo Running NET452 Tests
echo --------------------

mono ./testrunner/xunit.runner.console.*/tools/net452/xunit.console.exe ./src/Tests/CacheManager.Serialization.Hyperion.Tests/bin/Release/net452/CacheManager.Serialization.Hyperion.Tests.dll

echo --------------------
echo Running NETCORE2 Tests
echo --------------------

dotnet test -c Release ./src/Tests/CacheManager.Serialization.Hyperion.Tests -f netcoreapp2.0

echo --------------------
echo Running NETCORE1 Tests
echo --------------------

dotnet test -c Release ./src/Tests/CacheManager.Serialization.Hyperion.Tests -f netcoreapp1.1
