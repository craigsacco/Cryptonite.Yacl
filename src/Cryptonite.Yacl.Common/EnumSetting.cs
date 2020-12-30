using System;

namespace Cryptonite.Yacl.Common
{
    public abstract class EnumSetting<T> : Setting<T>
    {
        protected EnumSetting(T value)
        {
            Value = value;
        }

        private T m_value;
        public override T Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                OnPropertyChanged();
                OnPropertyChanged("DisplayValue");
            }
        }

        public override Type UnderlyingType => typeof(T);

        public override void Parse(String value)
        {
            Value = (T)Enum.Parse(typeof(T), value);
        }
    }
}
