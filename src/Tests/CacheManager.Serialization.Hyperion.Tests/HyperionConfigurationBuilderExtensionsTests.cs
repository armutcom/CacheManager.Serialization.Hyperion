using System;
using CacheManager.Core;
using CacheManager.Serialization.Hyperion.Settings;
using Moq;
using Xunit;

namespace CacheManager.Serialization.Hyperion.Tests
{
    public class HyperionConfigurationBuilderExtensionsTests
    {
        [Fact]
        public void
            WithHyperionSerializer_Should_Throw_ArgumentNullException_If_Given_ConfigurationBuilderCachePart_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => HyperionConfigurationBuilderExtensions.WithHyperionSerializer(null));
            Assert.Throws<ArgumentNullException>(() => HyperionConfigurationBuilderExtensions.WithHyperionSerializer(null, HyperionSerializerSettings.Default));
        }
    }
}
