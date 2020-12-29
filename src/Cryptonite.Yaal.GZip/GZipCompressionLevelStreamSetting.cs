using System;
using System.IO.Compression;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.GZip
{
    public class GZipCompressionLevelStreamSetting : EnumStreamSetting<CompressionLevel>
    {
        public GZipCompressionLevelStreamSetting() : this(CompressionLevel.Optimal)
        {
        }
        public GZipCompressionLevelStreamSetting(CompressionLevel value) : base(value)
        {
        }

        public override String Name => "Compression Level";

        public override String AbbreviatedName => "compressionLevel";

        public override string DisplayValue => Value.ToString();
    }
}
