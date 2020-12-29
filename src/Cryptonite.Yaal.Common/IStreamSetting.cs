using System;

namespace Cryptonite.Yaal.Common
{
    public interface IStreamSetting
    {
        String Name { get; }

        String AbbreviatedName { get; }

        String DisplayValue { get; }

        Type UnderlyingType { get; }

        void Parse(String value);
    }
}
