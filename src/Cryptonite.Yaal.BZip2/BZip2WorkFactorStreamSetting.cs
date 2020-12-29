using System;
using Cryptonite.Yaal.Common;

namespace Cryptonite.Yaal.BZip2
{
    public class BZip2WorkFactorStreamSetting : Int32StreamSetting
    {
        public BZip2WorkFactorStreamSetting() : this(30)
        {
        }
        public BZip2WorkFactorStreamSetting(Int32 value) : base(value, 1, 250)
        {
        }

        public override String Name => "Work Factor";

        public override String AbbreviatedName => "workFactor";

        public override string DisplayValue => Value.ToString();
    }
}
