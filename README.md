# CacheManager.Serialization.Hyperion
[CacheManager](https://github.com/MichaCo/CacheManager) extension package providing polymorphic (de)serialization support using [Hyperion](https://github.com/akkadotnet/Hyperion) for distributed caches. CacheManager is an open source caching abstraction layer for .NET written in C#. It supports various cache providers and implements many advanced features, the Hyperion serializer can be used in place of the default Binary Serializer

## Builds status
|       | Linux | Windows |
|-------|-------|----------|
| Build | [![Build Status](https://travis-ci.org/armutcom/CacheManager.Serialization.Hyperion.svg?branch=master)](https://travis-ci.org/armutcom/CacheManager.Serialization.Hyperion)      | [![Build status](https://ci.appveyor.com/api/projects/status/o6ni2cs82at7t1e5/branch/master?svg=true)](https://ci.appveyor.com/project/Blind-Striker/cachemanager-serialization-hyperion/branch/master)

## Installation
[![NuGet](https://img.shields.io/nuget/v/CacheManager.Serialization.Hyperion.svg)](https://www.nuget.org/packages/CacheManager.Serialization.Hyperion)

To install CacheManager.Serialization.Hyperion, run the following command in the Package Manager Console

```
Install-Package CacheManager.Serialization.Hyperion
```

## Usage

`WithHyperionSerializer` extension method can be used as below

```csharp
var cache = CacheFactory.Build("armutCache",
    settings =>
    {
       settings.WithHyperionSerializer().WithMicrosoftMemoryCacheHandle("memoryCache");
    });
```

Or `HyperionSerializerSettings` can be customize as below

```csharp
var cache = CacheFactory.Build("armutCache",
    settings =>
    {
       settings
        .WithHyperionSerializer(new HyperionSerializerSettings(preserveObjectReferences:true, versionTolerance:true, ignoreISerializable:true))
        .WithMicrosoftMemoryCacheHandle("memoryCache");
    });
```

## License
Licensed under MIT, see [LICENSE](LICENSE) for the full text.
