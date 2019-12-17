using System;
using System.IO;
using System.Reflection;
using CacheManager.Serialization.Hyperion.Settings;
using CacheManager.Serialization.Hyperion.Tests.Utils;
using Hyperion;
using Xunit;

namespace CacheManager.Serialization.Hyperion.Tests
{
    public class HyperionSerializerTests
    {
        [Fact]
        public void GetOpenGeneric_Should_Return_Type_Of_HyperionCacheItem()
        {
            HyperionSerializer hyperionSerializer = new HyperionSerializer();

            MethodInfo getOpenGeneric = hyperionSerializer.GetType().GetMethod("GetOpenGeneric", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.NotNull(getOpenGeneric);

            object openGenericItemType = getOpenGeneric.Invoke(hyperionSerializer, null);

            Assert.Equal(typeof(HyperionCacheItem<>), openGenericItemType);
        }

        [Fact]
        public void Serialize_Shoud_Serialize_Given_Object()
        {
            var serializer = new Serializer(new SerializerOptions(
                preserveObjectReferences: HyperionSerializerSettings.Default.PreserveObjectReferences,
                versionTolerance: HyperionSerializerSettings.Default.VersionTolerance,
                ignoreISerializable: HyperionSerializerSettings.Default.IgnoreISerializable));

            HyperionSerializer hyperionSerializer = new HyperionSerializer();

            var user = new TestUser
            {
                Age = 31,
                Id = Guid.NewGuid(),
                Name = "Deniz"
            };

            byte[] serializedUser = hyperionSerializer.Serialize(user);

            using (var bodyStream = new MemoryStream(serializedUser))
            {
                var deserializedUser = serializer.Deserialize(bodyStream);

                var testUser = deserializedUser as TestUser;

                Assert.True(testUser != null);

                Assert.Equal(testUser.Name, user.Name);
                Assert.Equal(testUser.Age, user.Age);
                Assert.Equal(testUser.Id, user.Id);
            }
        }

        [Fact]
        public void Deserialize_Should_Deserialize_Given_Object()
        {
            var serializer = new Serializer(new SerializerOptions(
                preserveObjectReferences: HyperionSerializerSettings.Default.PreserveObjectReferences,
                versionTolerance: HyperionSerializerSettings.Default.VersionTolerance,
                ignoreISerializable: HyperionSerializerSettings.Default.IgnoreISerializable));

            HyperionSerializer hyperionSerializer = new HyperionSerializer();

            var user = new TestUser
            {
                Age = 31,
                Id = Guid.NewGuid(),
                Name = "Deniz"
            };

            using (var bodyStream = new MemoryStream())
            {
                serializer.Serialize(user, bodyStream);

                byte[] array = bodyStream.ToArray();

                object deserializedUser = hyperionSerializer.Deserialize(array, typeof(TestUser));

                var testUser = deserializedUser as TestUser;

                Assert.True(testUser != null);

                Assert.Equal(testUser.Name, user.Name);
                Assert.Equal(testUser.Age, user.Age);
                Assert.Equal(testUser.Id, user.Id);
            }
        }
    }
}
