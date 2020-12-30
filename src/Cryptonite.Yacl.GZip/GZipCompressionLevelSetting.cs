using System;
using System.IO.Compression;
using Cryptonite.Yacl.Common;

namespace Cryptonite.Yacl.GZip
{
    public class GZipCompressionLevelSetting : EnumSetting<CompressionLevel>
    {
        public const CompressionLevel DefaultValue = CompressionLevel.Optimal;

        public GZipCompressionLevelSetting() : this(DefaultValue)
        {
        }

        public GZipCompressionLevelSetting(CompressionLevel value) : base(value)
        {
        }

        public override String Name => "Compression Level";

        public override String AbbreviatedName => "compressionLevel";
    }
}
