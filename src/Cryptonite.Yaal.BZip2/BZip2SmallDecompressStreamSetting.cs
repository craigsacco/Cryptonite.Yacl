using System;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2SmallDecompressStreamSetting : BoolStreamSetting
    {
        public BZip2SmallDecompressStreamSetting() : this(false)
        {
        }
        public BZip2SmallDecompressStreamSetting(Boolean value) : base(value)
        {
        }

        public override String Name => "Small Decompress Mode";
    }
}
