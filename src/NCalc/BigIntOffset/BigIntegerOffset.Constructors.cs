using System;
using System.Numerics;

namespace NCalc.BigIntOffset
{
    public partial class BigIntegerOffset
    {
        public BigIntegerOffset(BigIntegerOffset value)
        {
            Value = value.Value;
            Offset = value.Offset;
        }

        public BigIntegerOffset(BigInteger value)
        {
            Value = value;
        }

        public BigIntegerOffset(long value) : this(new BigInteger(value))
        {
        }

        public BigIntegerOffset(ulong value) : this(new BigInteger(value))
        {
        }

        public BigIntegerOffset(decimal value)
        {
            var sign = 1;
            if (value < 0)
            {
                value = -value;
                sign = -1;
            }

            var decEntier = Math.Floor(value);
            var decTail = value - decEntier;

            if (decTail == 0)
            {
                var bi = new BigInteger(decEntier);
                Value = bi * sign;
                return;
            }

            var tailResult = BigInteger.Zero;
            const int OffsetStep = 9;
            var modifier = BigInteger.Pow(BigInteger10, OffsetStep);
            const decimal modifierDecimal = 1000_000_000m;
            while (decTail > 0)
            {
                Offset += OffsetStep;
                tailResult *= modifier;
                decTail *= modifierDecimal;

                var a = (long)Math.Floor(decTail);
                tailResult += a;
                decTail -= a;
            }

            Value = tailResult + (new BigInteger(decEntier)) * OffsetPower;
            Value *= sign;
            this.NormalizeOffset();
        }

        // TODO Для дабла должен быть более сложный конструктор
        public BigIntegerOffset(double value, bool needFix = true)
        {
            if (value == 0)
            {
                Value = Zero.Value;
                Offset = Zero._offset;
                return;
            }

            var sign = 1;
            if (value < 0)
            {
                sign = -1;
                value = -value;
            }

            if (value == Math.Floor(value))
            {
                Value = new BigInteger(value) * sign;
                return;
            }

            throw new NotImplementedException();
        }
    }
}