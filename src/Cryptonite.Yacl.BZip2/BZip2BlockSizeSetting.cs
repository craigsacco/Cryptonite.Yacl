using System;
using Cryptonite.Yacl.Common;

namespace Cryptonite.Yacl.BZip2
{
    public class BZip2BlockSizeSetting : Int32Setting
    {
        public const Int32 DefaultValue = 100;

        public BZip2BlockSizeSetting() : this(DefaultValue)
        {
        }

        public BZip2BlockSizeSetting(Int32 value) : base(value, 100, 900, 100)
        {
        }

        public override String Name => "Block Size";

        public override String AbbreviatedName => "blockSize";

        public override string DisplayValue => String.Format("{0} kB", Value);
    }
}
