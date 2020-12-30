using System;
using Cryptonite.Yacl.Common;

namespace Cryptonite.Yacl.XZ
{
    public class LZMAExtremeCompressionSetting : BoolSetting
    {
        public const Boolean DefaultValue = false;

        public LZMAExtremeCompressionSetting() : this(DefaultValue)
        {
        }

        public LZMAExtremeCompressionSetting(Boolean value) : base(value)
        {
        }

        public override String Name => "Extreme Compression Mode?";

        public override String AbbreviatedName => "extremeCompression";

        public override string DisplayValue => Value ? "Yes" : "No";
    }
}
