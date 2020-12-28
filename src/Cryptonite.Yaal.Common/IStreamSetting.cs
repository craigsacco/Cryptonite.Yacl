using System;

namespace Cryptonite.Yaal.Common
{
    public interface IStreamSetting
    {
        String Name { get; }

        String PrintableValue { get; }

        Type UnderlyingType { get; }
    }
}
