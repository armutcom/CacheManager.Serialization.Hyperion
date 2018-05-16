using System;
using CacheManager.Core;
using CacheManager.Serialization.Hyperion.Settings;

namespace CacheManager.Serialization.Hyperion
{
    /// <summary>
    /// Configuration builder extensions for the <c>Hyperion</c> based <see cref="CacheManager.Core.Internal.ICacheSerializer"/>.
    /// </summary>
    public static class HyperionConfigurationBuilderExtensions
    {
        /// <summary>
        /// Configures the cache manager to use the <code>Hyperion</code> based cache serializer.
        /// </summary>
        /// <param name="part">The configuration part.</param>
        /// <returns>The builder instance.</returns>
        public static ConfigurationBuilderCachePart WithHyperionSerializer(this ConfigurationBuilderCachePart part)
        {
            if (part == null)
            {
                throw new ArgumentNullException(nameof(part));
            }

            return part.WithSerializer(typeof(HyperionSerializer));
        }

        /// <summary>
        /// Configures the cache manager to use the <code>Hyperion</code> based cache serializer.
        /// </summary>
        /// <param name="part">The configuration part.</param>
        /// <param name="serializerSettings">Hyperion serialization settings</param>
        /// <returns>The builder instance.</returns>
        public static ConfigurationBuilderCachePart WithHyperionSerializer(this ConfigurationBuilderCachePart part, HyperionSerializerSettings serializerSettings)
        {
            if (part == null)
            {
                throw new ArgumentNullException(nameof(part));
            }

            return part.WithSerializer(typeof(HyperionSerializer), serializerSettings);
        }
    }
}
