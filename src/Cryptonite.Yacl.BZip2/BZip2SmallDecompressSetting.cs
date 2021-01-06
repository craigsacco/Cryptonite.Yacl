using Cryptonite.Yacl.Common;
using System;

namespace Cryptonite.Yacl.BZip2
{
    public class BZip2SmallDecompressSetting : BoolSetting
    {
        public const Boolean DefaultValue = false;

        public BZip2SmallDecompressSetting() : this(DefaultValue)
        {
        }

        public BZip2SmallDecompressSetting(Boolean value) : base(value)
        {
        }

        public override String Name => "Small Decompress Mode?";

        public override String AbbreviatedName => "smallDecompress";

        public override string DisplayValue => Value ? "Yes" : "No";
    }
}
