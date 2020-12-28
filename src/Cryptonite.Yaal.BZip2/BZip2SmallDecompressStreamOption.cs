using System;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2SmallDecompressStreamOption : BoolStreamOption
    {
        public BZip2SmallDecompressStreamOption() : this(false)
        {
        }
        public BZip2SmallDecompressStreamOption(Boolean value) : base(value)
        {
        }

        public override String Name => "Small Decompress Mode";
    }
}
