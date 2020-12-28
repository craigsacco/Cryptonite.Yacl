using System;

namespace Cryptonite.Yaal.Common
{
    public interface IStreamOption
    {
        String Name { get; }

        String PrintableValue { get; }

        Type UnderlyingType { get; }
    }
}
