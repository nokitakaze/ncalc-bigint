using System;
using System.Numerics;

namespace NCalc.BigIntOffset
{
    public partial class BigIntegerOffset
    {
        public override bool Equals(object obj)
        {
            if (!(obj is BigIntegerOffset other))
            {
                return false;
            }

            return (this == other);
        }

        #region operator type casting

        public static explicit operator BigInteger(BigIntegerOffset item)
        {
            return item.Value / item.OffsetPower;
        }

        public static explicit operator int(BigIntegerOffset item)
        {
            return (int)(BigInteger)item;
        }

        public static explicit operator long(BigIntegerOffset item)
        {
            return (long)(BigInteger)item;
        }

        public static explicit operator decimal(BigIntegerOffset item)
        {
            return decimal.Parse(item.ToStringDouble());
        }

        public static explicit operator double(BigIntegerOffset item)
        {
            return double.Parse(item.ToStringDouble());
        }

        #endregion

        #region operator ==

        public static bool operator ==(BigIntegerOffset a, BigIntegerOffset b)
        {
            a.NormalizeOffset();
            b.NormalizeOffset();
            return (a._offset == b._offset) && (a.Value == b.Value);
        }

        public static bool operator !=(BigIntegerOffset a, BigIntegerOffset b)
        {
            return !(a == b);
        }

        public static bool operator ==(BigIntegerOffset a, BigInteger b)
        {
            a.NormalizeOffset();
            return (a._offset == 0) && (a.Value == b);
        }

        public static bool operator !=(BigIntegerOffset a, BigInteger b)
        {
            return !(a == b);
        }

        #endregion

        #region operator +-

        /// <summary>
        /// Unary minus
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static BigIntegerOffset operator -(BigIntegerOffset a)
        {
            a.NormalizeOffset();
            var newValue = new BigIntegerOffset(a);
            newValue.Value = -newValue.Value;

            return newValue;
        }

        public static BigIntegerOffset operator +(BigIntegerOffset a, BigIntegerOffset b)
        {
            var maxOffset = Math.Max(a._offset, b._offset);

            BigInteger valueA = a.Value;
            if (a._offset < maxOffset)
            {
                var p = BigInteger.Pow(BigInteger10, maxOffset - a._offset);
                valueA *= p;
            }

            BigInteger valueB = b.Value;
            if (b._offset < maxOffset)
            {
                var p = BigInteger.Pow(BigInteger10, maxOffset - b._offset);
                valueB *= p;
            }

            var newValue = new BigIntegerOffset(a)
            {
                Value = valueA + valueB,
                Offset = maxOffset,
            };
            newValue.NormalizeOffset();

            return newValue;
        }

        public static BigIntegerOffset operator -(BigIntegerOffset a, BigIntegerOffset b)
        {
            return a + (-b);
        }

        public static BigIntegerOffset operator +(BigIntegerOffset a, BigInteger b)
        {
            var newValue = new BigIntegerOffset(a);
            newValue.Value += b * newValue.OffsetPower;
            newValue.NormalizeOffset();

            return newValue;
        }

        public static BigIntegerOffset operator +(BigInteger b, BigIntegerOffset a)
        {
            return a + b;
        }

        public static BigIntegerOffset operator -(BigIntegerOffset a, BigInteger b)
        {
            return a + (-b);
        }

        public static BigIntegerOffset operator -(BigInteger b, BigIntegerOffset a)
        {
            return b + (-a);
        }

        public static BigIntegerOffset operator +(BigIntegerOffset a, long b)
        {
            return a + new BigInteger(b);
        }

        public static BigIntegerOffset operator +(long b, BigIntegerOffset a)
        {
            return a + new BigInteger(b);
        }

        public static BigIntegerOffset operator -(BigIntegerOffset a, long b)
        {
            return a + new BigInteger(-b);
        }

        public static BigIntegerOffset operator -(long b, BigIntegerOffset a)
        {
            return -a + new BigInteger(b);
        }

        public static BigIntegerOffset operator +(BigIntegerOffset a, ulong b)
        {
            return a + new BigInteger(b);
        }

        public static BigIntegerOffset operator +(ulong b, BigIntegerOffset a)
        {
            return a + new BigInteger(b);
        }

        public static BigIntegerOffset operator -(BigIntegerOffset a, ulong b)
        {
            return a - new BigInteger(b);
        }

        public static BigIntegerOffset operator -(ulong b, BigIntegerOffset a)
        {
            return -a + new BigInteger(b);
        }

        public static BigIntegerOffset operator +(BigIntegerOffset a, decimal b)
        {
            return a + new BigIntegerOffset(b);
        }

        public static BigIntegerOffset operator +(decimal b, BigIntegerOffset a)
        {
            return a + new BigIntegerOffset(b);
        }

        public static BigIntegerOffset operator -(BigIntegerOffset a, decimal b)
        {
            return a + new BigIntegerOffset(-b);
        }

        public static BigIntegerOffset operator -(decimal b, BigIntegerOffset a)
        {
            return new BigIntegerOffset(b) - a;
        }

        public static BigIntegerOffset operator +(BigIntegerOffset a, double b)
        {
            return a + new BigIntegerOffset(b);
        }

        public static BigIntegerOffset operator +(double b, BigIntegerOffset a)
        {
            return a + new BigIntegerOffset(b);
        }

        public static BigIntegerOffset operator -(BigIntegerOffset a, double b)
        {
            return a + new BigIntegerOffset(-b);
        }

        public static BigIntegerOffset operator -(double b, BigIntegerOffset a)
        {
            return new BigIntegerOffset(b) - a;
        }

        #endregion

        #region operator *

        public static BigIntegerOffset operator *(BigIntegerOffset a, BigIntegerOffset b)
        {
            var newValue = new BigIntegerOffset(a);
            newValue.Offset += b.Offset;
            newValue.Value *= b.Value;
            newValue.NormalizeOffset();

            return newValue;
        }

        public static BigIntegerOffset operator *(BigIntegerOffset a, BigInteger b)
        {
            var newValue = new BigIntegerOffset(a);
            newValue.Value *= b;
            newValue.NormalizeOffset();

            return newValue;
        }

        public static BigIntegerOffset operator *(BigInteger b, BigIntegerOffset a)
        {
            return a * b;
        }

        public static BigIntegerOffset operator *(BigIntegerOffset a, long b)
        {
            return a * new BigInteger(b);
        }

        public static BigIntegerOffset operator *(long b, BigIntegerOffset a)
        {
            return a * new BigInteger(b);
        }

        public static BigIntegerOffset operator *(BigIntegerOffset a, ulong b)
        {
            return a * new BigInteger(b);
        }

        public static BigIntegerOffset operator *(ulong b, BigIntegerOffset a)
        {
            return a * new BigInteger(b);
        }

        public static BigIntegerOffset operator *(decimal b, BigIntegerOffset a)
        {
            return a * new BigIntegerOffset(b);
        }

        public static BigIntegerOffset operator *(double b, BigIntegerOffset a)
        {
            return a * new BigIntegerOffset(b);
        }

        public static BigIntegerOffset operator *(BigIntegerOffset a, double b)
        {
            return a * new BigIntegerOffset(b);
        }

        #endregion
    }
}