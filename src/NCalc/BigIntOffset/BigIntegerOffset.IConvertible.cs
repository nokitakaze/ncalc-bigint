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
            if (conversionType == typeof(ulong))
            {
                return this.ToUInt64(provider);
            }
            else if (conversionType == typeof(long))
            {
                return this.ToInt64(provider);
            }
            else if (conversionType == typeof(uint))
            {
                return this.ToUInt32(provider);
            }
            else if (conversionType == typeof(int))
            {
                return this.ToInt32(provider);
            }
            else if (conversionType == typeof(ushort))
            {
                return this.ToUInt16(provider);
            }
            else if (conversionType == typeof(short))
            {
                return this.ToInt16(provider);
            }
            else if (conversionType == typeof(byte))
            {
                return this.ToByte(provider);
            }
            else if (conversionType == typeof(sbyte))
            {
                return this.ToSByte(provider);
            }
            else if (conversionType == typeof(decimal))
            {
                return this.ToDecimal(provider);
            }
            else if (conversionType == typeof(double))
            {
                return this.ToDouble(provider);
            }
            else
            {
                throw new NotImplementedException();
            }
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