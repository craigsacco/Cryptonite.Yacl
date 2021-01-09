/*
 * MIT License
 *
 * Copyright (c) 2020-2021 Craig Sacco
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;

namespace Cryptonite.Yacl.Common
{
    public abstract class UInt32Setting : Setting<UInt32>
    {
        public UInt32 Minimum { get; private set; }

        public UInt32 Maximum { get; private set; }

        public UInt32 Step { get; private set; }

        protected UInt32Setting(UInt32 value) : this(value, UInt32.MinValue, UInt32.MaxValue)
        {
        }

        protected UInt32Setting(UInt32 value, UInt32 minimum, UInt32 maximum) : this(value, minimum, maximum, 1)
        {
        }

        protected UInt32Setting(UInt32 value, UInt32 minimum, UInt32 maximum, UInt32 step)
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

        private UInt32 m_value;
        public override UInt32 Value
        {
            get
            {
                return m_value;
            }
            set
            {
                if ((Minimum != UInt32.MinValue && value < Minimum) ||
                    (Maximum != UInt32.MaxValue && value > Maximum))
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
            Value = UInt32.Parse(value);
        }
    }
}
