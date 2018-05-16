using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheManager.Serialization.Hyperion.Settings;
using Xunit;

namespace CacheManager.Serialization.Hyperion.Tests
{
    public class HyperionSerializerSettingsTests
    {
        [Fact]
        public void Should_Initialize_With_Given_Parameters()
        {
            var preserveObjectReferences = true;
            var versionTolerance = false;
            var ignoreISerializable = true;

            HyperionSerializerSettings hyperionSerializerSettings = new HyperionSerializerSettings(preserveObjectReferences, versionTolerance, ignoreISerializable);

            Assert.Equal(preserveObjectReferences, hyperionSerializerSettings.PreserveObjectReferences);
            Assert.Equal(versionTolerance, hyperionSerializerSettings.VersionTolerance);
            Assert.Equal(ignoreISerializable, hyperionSerializerSettings.IgnoreISerializable);
        }

        [Fact]
        public void Should_Initialize_Defualt_Values()
        {
            HyperionSerializerSettings hyperionSerializerSettings = HyperionSerializerSettings.Default;

            Assert.True(hyperionSerializerSettings.PreserveObjectReferences);
            Assert.True(hyperionSerializerSettings.VersionTolerance);
            Assert.True(hyperionSerializerSettings.IgnoreISerializable);
        }
    }
}
