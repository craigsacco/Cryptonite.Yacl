using Cryptonite.Yacl.Common;
using System;

namespace Cryptonite.Yacl.BZip2
{
    public class BZip2WorkFactorSetting : Int32Setting
    {
        public const Int32 DefaultValue = 30;

        public BZip2WorkFactorSetting() : this(DefaultValue)
        {
        }

        public BZip2WorkFactorSetting(Int32 value) : base(value, 1, 250)
        {
        }

        public override String Name => "Work Factor";

        public override String AbbreviatedName => "workFactor";
    }
}
