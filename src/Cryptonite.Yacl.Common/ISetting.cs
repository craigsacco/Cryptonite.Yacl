using System;

namespace Cryptonite.Yacl.Common
{
    public interface ISetting
    {
        String Name { get; }

        String AbbreviatedName { get; }

        String DisplayValue { get; }

        Type UnderlyingType { get; }

        void Parse(String value);
    }
}
