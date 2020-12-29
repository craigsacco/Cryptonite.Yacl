using System;

namespace Cryptonite.Yaal.Common
{
    public abstract class EnumStreamSetting<T> : StreamSetting<T>
    {
        protected EnumStreamSetting(T value)
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
