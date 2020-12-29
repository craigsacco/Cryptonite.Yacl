using System;

namespace Cryptonite.Yaal.Common
{
    public abstract class BoolStreamSetting : StreamSetting<Boolean>
    {
        protected BoolStreamSetting(Boolean value)
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
