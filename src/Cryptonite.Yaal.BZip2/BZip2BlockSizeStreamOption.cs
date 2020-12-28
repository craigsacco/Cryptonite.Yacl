using System;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2BlockSizeStreamOption : Int32StreamOption
    {
        public BZip2BlockSizeStreamOption() : this(100)
        {
        }
        public BZip2BlockSizeStreamOption(Int32 value) : base(value, 100, 900, 100)
        {
        }

        public override String Name => "Block Size";
    }
}
