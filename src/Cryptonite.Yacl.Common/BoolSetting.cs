using System;

namespace Cryptonite.Yacl.Common
{
    public abstract class BoolSetting : Setting<Boolean>
    {
        protected BoolSetting(Boolean value)
        {
            Value = value;
        }

        private Boolean m_value;
        public override Boolean Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = Value;
                OnPropertyChanged();
                OnPropertyChanged("DisplayValue");
            }
        }
        
        public override Type UnderlyingType => typeof(Boolean);

        public override void Parse(String value)
        {
            Value = Boolean.Parse(value);
        }
    }
}
