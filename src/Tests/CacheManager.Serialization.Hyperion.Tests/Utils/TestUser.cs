using System;

namespace CacheManager.Serialization.Hyperion.Tests.Utils
{
    public class TestUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public override string ToString() => $"{Id} - {Name} - {Age}";
    }
}
