using CacheManager.Core;
using CacheManager.Core.Internal;

namespace CacheManager.Serialization.Hyperion
{
    public class HyperionCacheItem<T> : SerializerCacheItem<T>
    {
        public HyperionCacheItem()
        {
            
        }

        public HyperionCacheItem(ICacheItemProperties properties, object value) 
            : base(properties, value)
        {
            
        }

        public override long CreatedUtc { get; set; }

        public override ExpirationMode ExpirationMode { get; set; }

        public override double ExpirationTimeout { get; set; }

        public override string Key { get; set; }

        public override long LastAccessedUtc { get; set; }

        public override string Region { get; set; }

        public override bool UsesExpirationDefaults { get; set; }

        public override string ValueType { get; set; }

        public override T Value { get; set; }
    }
}
