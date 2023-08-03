using System;
using System.Numerics;
using System.Text.RegularExpressions;

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

            if (!needFix)
            {
                throw new NotImplementedException();
            }

            var sign = 1;
            if (value < 0)
            {
                sign = -1;
                value = -value;
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (value == Math.Floor(value))
            {
                Value = new BigInteger(value) * sign;
                return;
            }

            var valueStringify = value.ToString("G17");
            var valueStringifyLength = valueStringify.Length;

            int addExp = 0;
            {
                var rParse1 = new Regex("^9\\.9+[0-9]{1,2}E\\-(\\d+)$");
                var m = rParse1.Match(valueStringify);
                if (m.Success)
                {
                    var exp = int.Parse(m.Groups[1].Value);
                    Value = sign;
                    Offset = exp - 1;

                    return;
                }

                var rParse2 = new Regex("^1E\\-(\\d+)$");
                m = rParse2.Match(valueStringify);
                if (m.Success)
                {
                    var exp = int.Parse(m.Groups[1].Value);
                    Value = sign;
                    Offset = exp;

                    return;
                }

                if (valueStringify.Contains("E"))
                {
                    var rParse3 = new Regex("E\\-(\\d+)$");
                    m = rParse3.Match(valueStringify);
                    if (!m.Success)
                    {
                        throw new NotImplementedException($"Not implemented style '{value}'");
                    }

                    addExp = int.Parse(m.Groups[1].Value);
                    value *= Math.Pow(10d, addExp);
                    valueStringify = value.ToString("G17");
                    valueStringifyLength = valueStringify.Length;
                }
            }

            var bio = Parse(valueStringify);
            if (valueStringifyLength + addExp >= 18)
            {
                if (bio.Value % 1000 == 1)
                {
                    bio.Value--;
                }
                else if (bio.Value % 1000 >= 995)
                {
                    bio.Value += 1000 - (bio.Value % 1000);
                }
            }

            Value = bio.Value * sign;
            Offset = bio._offset + addExp;
            NormalizeOffset();
        }
    }
}