using System;
using System.IO;
using CacheManager.Core.Internal;
using CacheManager.Serialization.Hyperion.Settings;
using Hyperion;

namespace CacheManager.Serialization.Hyperion
{
    public class HyperionSerializer : CacheSerializer
    {
        private static readonly Type OpenGenericItemType = typeof(HyperionCacheItem<>);

        private readonly Serializer _serializer;

        public HyperionSerializer() 
            : this(HyperionSerializerSettings.Default)
        {          
        }

        public HyperionSerializer(HyperionSerializerSettings hyperionSerializerSettings)
        {
            var settings = hyperionSerializerSettings ?? HyperionSerializerSettings.Default;

            _serializer = new Serializer(new SerializerOptions(
                preserveObjectReferences: settings.PreserveObjectReferences,
                versionTolerance: settings.VersionTolerance,
                ignoreISerializable: settings.IgnoreISerializable));
        }

        public override byte[] Serialize<T>(T value)
        {
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(value, stream);

                return stream.ToArray();
            }            
        }

        public override object Deserialize(byte[] data, Type target)
        {
            using (Stream stream = new MemoryStream(data))
            {
                var res = _serializer.Deserialize<object>(stream);
                return res;
            }
        }

        /// <inheritdoc/>
        protected override object CreateNewItem<TCacheValue>(ICacheItemProperties properties, object value)
        {
            return new HyperionCacheItem<TCacheValue>(properties, value);
        }

        /// <inheritdoc/>
        protected override Type GetOpenGeneric()
        {
            return OpenGenericItemType;
        }
    }
}
