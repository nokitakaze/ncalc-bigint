using System;
using System.Numerics;

namespace NCalc.BigIntOffset
{
    public partial class BigIntegerOffset
    {
        public const int MaxDefaultPrecision = 18;
        public readonly int MaxPrecision = MaxDefaultPrecision;
        public static readonly BigIntegerOffset One = new BigIntegerOffset(1);
        public static readonly BigIntegerOffset Zero = new BigIntegerOffset(0);

        protected BigInteger Value = BigInteger.Zero;
        protected int _offset;
        protected static readonly BigInteger BigInteger10 = new BigInteger(10);

        protected int Offset
        {
            get => _offset;
            set
            {
                if (value < 0)
                {
                    throw new BigIntegerOffsetException($"Offset ('{value}') < 0");
                }

                if (_offset == value)
                {
                    return;
                }

                _offset = value;
                OffsetPower = BigInteger.Pow(BigInteger10, _offset);
            }
        }

        protected BigInteger OffsetPower = BigInteger.One;

        public void NormalizeOffset()
        {
            if (Value.IsZero)
            {
                Offset = 0;
                return;
            }

            if (_offset == 0)
            {
                return;
            }

            var value = (Value > 0) ? Value : -Value;
            var u = false;
            while ((_offset > 0) && (value % BigInteger10 == BigInteger.Zero))
            {
                _offset--;
                value /= 10;
                Value /= 10;
                u = true;
            }

            if (u)
            {
                OffsetPower = BigInteger.Pow(BigInteger10, _offset);
            }
        }

        public string ToStringDouble()
        {
            if (_offset == 0)
            {
                return Value.ToString();
            }

            var sign = string.Empty;
            var _value = Value;
            if (Value < 0)
            {
                sign = "-";
                _value = -_value;
            }

            var vTail = _value % OffsetPower;
            var vEntier = (_value - vTail) / OffsetPower;

            var vTailString = string.Empty;
            for (var i = 0; i < _offset; i++)
            {
                var mode = (int)(_value % BigInteger10);
                vTailString = mode + vTailString;
                _value /= BigInteger10;
            }

            return $"{sign}{vEntier}.{vTailString}";
        }

        public override string ToString()
        {
            return ToStringDouble();
        }

        public string ToString(IFormatProvider provider)
        {
            var a = provider.ToString();
            throw new NotImplementedException();
        }

        public static BigIntegerOffset Parse(string value)
        {
            if (value == "0")
            {
                return Zero;
            }

            value = value
                .Replace(" ", string.Empty)
                .Replace("_", string.Empty);

            var sign = 1;
            if (value.StartsWith("-"))
            {
                sign = -1;
                value = value.Substring(1);
            }

            var chunks = value.Split('.');
            if (chunks.Length > 2)
            {
                throw new BigIntegerOffsetException($"Value '{value}' malformed");
            }

            if (chunks.Length == 1)
            {
                var valBI = BigInteger.Parse(value) * sign;
                return new BigIntegerOffset(valBI);
            }

            var chunks1 = chunks[1].TrimEnd('0');
            if (chunks1 == string.Empty)
            {
                var valBI = BigInteger.Parse(chunks[0]) * sign;
                return new BigIntegerOffset(valBI);
            }

            {
                var tail = BigInteger.Zero;
                for (var i = 0; i < chunks1.Length; i++)
                {
                    tail *= BigInteger10;
                    var c1 = chunks1.Substring(i, 1);
                    tail += int.Parse(c1);
                }

                // ReSharper disable once UseObjectOrCollectionInitializer
                var valBI = new BigIntegerOffset(tail);
                valBI.Offset = chunks1.Length;
                valBI.Value += valBI.OffsetPower * BigInteger.Parse(chunks[0]);
                valBI.Value *= sign;

                return valBI;
            }
        }
    }
}