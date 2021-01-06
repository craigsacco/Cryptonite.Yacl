using Cryptonite.Yacl.Common;
using System;

namespace Cryptonite.Yacl.XZ
{
    public class LZMACompressionPresetSetting : UInt32Setting
    {
        public const UInt32 DefaultValue = 6;

        public LZMACompressionPresetSetting() : this(DefaultValue)
        {
        }

        public LZMACompressionPresetSetting(UInt32 value) : base(value, 0, 9)
        {
        }

        public override String Name => "Compression Preset";

        public override String AbbreviatedName => "compressionPreset";
    }
}
