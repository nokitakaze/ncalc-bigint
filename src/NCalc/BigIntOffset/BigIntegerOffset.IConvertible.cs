using System;

namespace NCalc.BigIntOffset
{
    public partial class BigIntegerOffset : System.IConvertible
    {
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return (this > Zero);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return (byte)(ulong)this;
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new BigIntegerOffsetException("Can't type cast BigIntegerOffset to char");
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new BigIntegerOffsetException("Can't type cast BigIntegerOffset to DateTime");
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return (decimal)this;
        }

        public double ToDouble(IFormatProvider provider)
        {
            return (double)this;
        }

        public short ToInt16(IFormatProvider provider)
        {
            return (short)(long)this;
        }

        public int ToInt32(IFormatProvider provider)
        {
            return (int)this;
        }

        public long ToInt64(IFormatProvider provider)
        {
            return (long)this;
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return (sbyte)(int)this;
        }

        public float ToSingle(IFormatProvider provider)
        {
            return (float)(double)this;
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return (ushort)(ulong)this;
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return (uint)(ulong)this;
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return (ulong)this;
        }
    }
}