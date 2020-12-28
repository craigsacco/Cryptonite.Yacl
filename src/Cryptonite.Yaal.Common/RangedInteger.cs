using System;
using System.IO;
using System.Linq;

namespace Cryptonite.Yaal.Common
{
    public class RangedInteger<T> where T : IComparable<T>
    {
        public T Minimum { get; private set; }

        public T Maximum { get; private set; }

        public T Value { get; private set; }

        public RangedInteger(T minimum, T maximum, T defaultValue)
        {
            if (minimum >= maximum)
            {
                throw new ArgumentException("Minimum value must be less thab maximum value");
            }
            if (defaultValue < minimum || defaultValue > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(defaultValue), "Default value is not within the range for this type");
            }

            Minimum = minimum;
            Maximum = maximum;
            Value = defaultValue;
        }

        public static RangedInteger<T> operator +(RangedInteger<T> a) => a;

        public static RangedInteger<T> operator -(RangedInteger<T> a)
            => new RangedInteger<T>(a.Minimum, a.Maximum, 0 - a.Value);

        public static RangedInteger<T> operator +(RangedInteger<T> a, RangedInteger<T> b)
            => new RangedInteger<T>(a.Minimum, a.Maximum, a.Value + b.Value);

        public static RangedInteger<T> operator -(RangedInteger<T> a, RangedInteger<T> b)
            => new RangedInteger<T>(a.Minimum, a.Maximum, a.Value - b.Value);

        public static RangedInteger<T> operator *(RangedInteger<T> a, RangedInteger<T> b)
            => new RangedInteger<T>(a.Minimum, a.Maximum, a.Value * b.Value);

        public static RangedInteger<T> operator /(RangedInteger<T> a, RangedInteger<T> b)
            => new RangedInteger<T>(a.Minimum, a.Maximum, a.Value / b.Value);
    }
}
