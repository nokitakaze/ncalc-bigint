﻿using System;
using System.Numerics;

namespace NCalc.BigIntegerOffset
{
    public partial class BigIntegerOffset
    {
        public const int MaxDefaultPrecision = 18;
        public int MaxPrecision = MaxDefaultPrecision;
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

        protected BigInteger OffsetPower = BigInteger.Zero;

        public void NormalizeOffset()
        {
            if (Value.IsZero)
            {
                Offset = 0;
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

            var sign = (Value > 0) ? "" : "-";
            var _value = (Value > 0) ? Value : -Value;

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

            throw new NotImplementedException();
        }
    }
}