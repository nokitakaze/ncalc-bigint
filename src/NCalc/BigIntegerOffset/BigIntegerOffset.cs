using System;
using System.Numerics;

namespace NCalc.BigIntegerOffset
{
    public partial class BigIntegerOffset
    {
        public const int MaxDefaultPrecision = 18;
        public int MaxPrecision = MaxDefaultPrecision;

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

                _offset = value;
                OffsetPower = BigInteger.Pow(BigInteger10, _offset);
            }
        }

        protected BigInteger OffsetPower = BigInteger.Zero;

        public void NormalizeOffset()
        {
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
    }
}