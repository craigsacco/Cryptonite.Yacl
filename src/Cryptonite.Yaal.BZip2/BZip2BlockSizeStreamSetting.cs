using System;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2BlockSizeStreamSetting : Int32StreamSetting
    {
        public BZip2BlockSizeStreamSetting() : this(100)
        {
        }
        public BZip2BlockSizeStreamSetting(Int32 value) : base(value, 100, 900, 100)
        {
        }

        public override String Name => "Block Size";
    }
}
