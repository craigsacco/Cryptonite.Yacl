using System;

namespace Cryptonite.Yaal.Common
{
    public abstract class BoolStreamSetting : StreamSetting<Boolean>
    {
        protected BoolStreamSetting(Boolean value)
        {
            Value = value;
        }

        public override Boolean Value { get; set; }
        
        public override Type UnderlyingType => typeof(Boolean);
    }
}
