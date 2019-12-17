var target = Argument("target", "Default");

using System;
using System.Diagnostics;

// Variables
var configuration = "Release";
var fullFrameworkTarget = "net45";
var netCoreTarget30 = "netcoreapp3.0";

var projects = GetFiles("**/*.csproj");
string testProjectPath = "../tests/CacheManager.Serialization.Hyperion.Tests/CacheManager.Serialization.Hyperion.Tests.csproj";

Task("Default")
    .IsDependentOn("Test");

Task("Clean")
    .Does(() =>
    {
        foreach(var project in projects)
        {
            DotNetCoreClean(project.ToString());
        }
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        foreach(var project in projects)
        {
            DotNetCoreRestore(project.ToString());
        }
    });

Task("Build")
    .IsDependentOn("Restore")
    .Description("Builds all the projects in the solution")
    .Does(() =>
    {
        DotNetCoreBuildSettings settings = new DotNetCoreBuildSettings
        {
            NoRestore = true,
            Configuration = configuration
        };

        foreach(var project in projects)
        {
            DotNetCoreBuild(project.ToString(), settings);
        } 
    });

Task("NetCore3.0Tests")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetCoreTestSettings settings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
            Framework = netCoreTarget30
        };
        DotNetCoreTest(testProjectPath, settings);
    });

Task("NetFramework4.5Tests")
    .IsDependentOn("Build")
    .Does(() => {
        bool isRunningOnUnix = IsRunningOnUnix();

        if(!isRunningOnUnix) // Windows
        {
            DotNetCoreTestSettings settings = new DotNetCoreTestSettings
            {
                Configuration = configuration,
                Framework = fullFrameworkTarget
            };

            DotNetCoreTest(testProjectPath, settings);
        }
        else
        {
            var nugetInstallSettings = new NuGetInstallSettings
            {
                OutputDirectory = "testrunner",
                WorkingDirectory = "."
            };

            NuGetInstall("NUnit.ConsoleRunner", nugetInstallSettings);

            StartProcess("mono", new ProcessSettings {
                Arguments = $"./testrunner/NUnit.ConsoleRunner.*/tools/nunit3-console.exe/ ./tests/CacheManager.Serialization.Hyperion.Tests/bin/Release/{fullFrameworkTarget}/CacheManager.Serialization.Hyperion.Tests.dll"
            });
        }
    });

Task("Test")
    .IsDependentOn("NetCore3.0Tests")
    .IsDependentOn("NetFramework4.5Tests");

Task("Nuget-Pack")
    .Description("Publish to nuget")
    .Does(() =>
    {
        var settings = new DotNetCorePackSettings
        {
            Configuration = "Release",
            WorkingDirectory = "../src/CacheManager.Serialization.Hyperion",
            OutputDirectory = "../artifacts"
        };

        DotNetCorePack("CacheManager.Serialization.Hyperion.csproj", settings);
    });

RunTarget(target);