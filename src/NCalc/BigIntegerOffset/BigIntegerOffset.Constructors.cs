using System.Numerics;

namespace NCalc.BigIntegerOffset
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

        public BigIntegerOffset(decimal value) : this(new BigInteger(value))
        {
        }

        // TODO Для дабла должен быть более сложный конструктор
        public BigIntegerOffset(double value) : this(new BigInteger(value))
        {
        }
    }
}