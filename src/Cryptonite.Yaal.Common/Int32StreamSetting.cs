using System;

namespace Cryptonite.Yaal.Common
{
    public abstract class Int32StreamSetting : StreamSetting<Int32>
    {
        public Int32 Minimum { get; private set; }

        public Int32 Maximum { get; private set; }

        public Int32 Step { get; private set; }

        protected Int32StreamSetting(Int32 value) : this(value, Int32.MinValue, Int32.MaxValue)
        {
        }

        protected Int32StreamSetting(Int32 value, Int32 minimum, Int32 maximum) : this(value, minimum, maximum, 1)
        {
        }

        protected Int32StreamSetting(Int32 value, Int32 minimum, Int32 maximum,  Int32 step)
        {
            if (minimum >= maximum)
            {
                throw new ArgumentException("Minimum value must be less thab maximum value");
            }

            if (step < 1)
            {
                throw new ArgumentException("Step argument must be a positive value");
            }

            Minimum = minimum;
            Maximum = maximum;
            Step = step;
            Value = value;
        }

        private Int32 m_value;
        public override Int32 Value
        {
            get
            {
                return m_value;
            }
            set
            {
                if ((Minimum != Int32.MinValue && value < Minimum) ||
                    (Maximum != Int32.MaxValue && value > Maximum))
                {
                    throw new ArgumentOutOfRangeException("Value is not within the range for this type");
                }

                if ((value - Minimum) % Step != 0)
                {
                    throw new ArgumentException("Value does not meet the step requirement");
                }

                m_value = value;
                OnPropertyChanged();
                OnPropertyChanged("DisplayValue");
            }
        }

        public override Type UnderlyingType => typeof(Int32);

        public override void Parse(String value)
        {
            Value = Int32.Parse(value);
        }
    }
}
