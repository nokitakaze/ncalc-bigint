using System.Numerics;

namespace NCalc.BigIntegerOffset
{
    public partial class BigIntegerOffset
    {
        public static BigIntegerOffset operator -(BigIntegerOffset a)
        {
            a.NormalizeOffset();
            var newValue = new BigIntegerOffset(a);
            newValue.Value = -newValue.Value;

            return newValue;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BigIntegerOffset other))
            {
                return false;
            }

            return (this == other);
        }

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

        public static BigIntegerOffset operator *(BigIntegerOffset a, BigInteger b)
        {
            var newValue = new BigIntegerOffset(a);
            newValue.Value *= b;
            newValue.NormalizeOffset();

            return newValue;
        }
    }
}