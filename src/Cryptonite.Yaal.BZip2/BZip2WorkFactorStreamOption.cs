using System;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2WorkFactorStreamOption : Int32StreamOption
    {
        public BZip2WorkFactorStreamOption() : this(30)
        {
        }
        public BZip2WorkFactorStreamOption(Int32 value) : base(value, 1, 250)
        {
        }

        public override String Name => "Work Factor";
    }
}
