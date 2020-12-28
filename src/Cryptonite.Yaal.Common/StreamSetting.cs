using System;

namespace Cryptonite.Yaal.Common
{
    public abstract class StreamSetting<T> : IStreamSetting
    {
        public abstract String Name { get; }

        public abstract T Value { get; set; }

        public abstract String DisplayValue { get; }

        public abstract Type UnderlyingType { get; }
    }
}
