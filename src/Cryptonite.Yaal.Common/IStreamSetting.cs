using System;

namespace Cryptonite.Yaal.Common
{
    public interface IStreamSetting
    {
        String Name { get; }

        String DisplayValue { get; }

        Type UnderlyingType { get; }
    }
}
