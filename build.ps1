dotnet --info

echo The installed .NET Core SDKs are:
dir $env:ProgramFiles"\dotnet\sdk" | findstr /l "."

dotnet restore ./src/CacheManager.Serialization.Hyperion.sln

dotnet build ./src/Tests/CacheManager.Serialization.Hyperion.Tests/CacheManager.Serialization.Hyperion.Tests.csproj -c Release

echo --------------------
echo Running NET452 Tests
echo --------------------

dotnet test -c Release ./src/Tests/CacheManager.Serialization.Hyperion.Tests -c Release -f net452

echo --------------------
echo Running NETCORE2 Tests
echo --------------------

dotnet test -c Release ./src/Tests/CacheManager.Serialization.Hyperion.Tests -c Release -f netcoreapp2.0

echo --------------------
echo Running NETCORE1 Tests
echo --------------------

dotnet test -c Release ./src/Tests/CacheManager.Serialization.Hyperion.Tests -c Release -f netcoreapp1.1