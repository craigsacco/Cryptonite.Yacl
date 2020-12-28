using System;

namespace Cryptonite.Yaal.Common
{
    public abstract class BoolStreamOption : StreamOption<Boolean>
    {
        protected BoolStreamOption(Boolean value)
        {
            Value = value;
        }

        public override Boolean Value { get; set; }
        
        public override Type UnderlyingType => typeof(Boolean);
    }
}
