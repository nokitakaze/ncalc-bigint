using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace NCalc
{
    public class Numbers
    {
        private static object ConvertIfString(object s, IFormatProvider cultureInfo)
        {
            switch (s)
            {
                case string sValue:
                    return decimal.Parse(sValue, cultureInfo);
                case char charValue:
                    return decimal.Parse(charValue.ToString(), cultureInfo);
                default:
                    return s;
            }
        }

        #region Add

        public static object Add(object a, object b)
        {
            return Add(a, b, CultureInfo.CurrentCulture);
        }

        [SuppressMessage("ReSharper", "RedundantCast")]
        public static object Add(object a, object b, CultureInfo cultureInfo)
        {
            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);
            switch (a)
            {
                case byte aByte:
                    switch (b)
                    {
                        case byte bByte:
                            return aByte + bByte;
                        case sbyte bSbyte:
                            return aByte + bSbyte;
                        case short bShort:
                            return aByte + bShort;
                        case ushort bUshort:
                            return aByte + bUshort;
                        case int bInt:
                            return aByte + bInt;
                        case uint bUint:
                            return aByte + bUint;
                        case long bLong:
                            return aByte + bLong;
                        case ulong bUlong:
                            return aByte + bUlong;
                        case float bFloat:
                            return aByte + bFloat;
                        case double bDouble:
                            return aByte + bDouble;
                        case decimal bDecimal:
                            return (decimal)aByte + bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aByte) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'byte' and '{b.GetType()}'");
                    }
                case sbyte aSbyte:
                    switch (b)
                    {
                        case byte bByte:
                            return aSbyte + bByte;
                        case sbyte bSbyte:
                            return aSbyte + bSbyte;
                        case short bShort:
                            return aSbyte + bShort;
                        case ushort bUshort:
                            return aSbyte + bUshort;
                        case int bInt:
                            return aSbyte + bInt;
                        case uint bUint:
                            return aSbyte + bUint;
                        case long bLong:
                            return aSbyte + bLong;
                        case ulong bUlong:
                            return aSbyte + (long)bUlong;
                        case float bFloat:
                            return aSbyte + bFloat;
                        case double bDouble:
                            return aSbyte + bDouble;
                        case decimal bDecimal:
                            return (decimal)aSbyte + bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aSbyte) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'sbyte' and '{b.GetType()}'");
                    }
                case short aShort:
                    switch (b)
                    {
                        case byte bByte:
                            return aShort + bByte;
                        case sbyte bSbyte:
                            return aShort + bSbyte;
                        case short bShort:
                            return aShort + bShort;
                        case ushort bUshort:
                            return aShort + bUshort;
                        case int bInt:
                            return aShort + bInt;
                        case uint bUint:
                            return aShort + bUint;
                        case long bLong:
                            return aShort + bLong;
                        case ulong bUlong:
                            return aShort + (long)bUlong;
                        case float bFloat:
                            return aShort + bFloat;
                        case double bDouble:
                            return aShort + bDouble;
                        case decimal bDecimal:
                            return (decimal)aShort + bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aShort) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'short' and '{b.GetType()}'");
                    }
                case ushort aUshort:
                    switch (b)
                    {
                        case byte bByte:
                            return aUshort + bByte;
                        case sbyte bSbyte:
                            return aUshort + bSbyte;
                        case short bShort:
                            return aUshort + bShort;
                        case ushort bUshort:
                            return aUshort + bUshort;
                        case int bInt:
                            return aUshort + bInt;
                        case uint bUint:
                            return aUshort + bUint;
                        case long bLong:
                            return aUshort + bLong;
                        case ulong bUlong:
                            return aUshort + bUlong;
                        case float bFloat:
                            return aUshort + bFloat;
                        case double bDouble:
                            return aUshort + bDouble;
                        case decimal bDecimal:
                            return (decimal)aUshort + bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUshort) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'ushort' and '{b.GetType()}'");
                    }
                case int aInt:
                    switch (b)
                    {
                        case byte bByte:
                            return aInt + bByte;
                        case sbyte bSbyte:
                            return aInt + bSbyte;
                        case short bShort:
                            return aInt + bShort;
                        case ushort bUshort:
                            return aInt + bUshort;
                        case int bInt:
                            return aInt + bInt;
                        case uint bUint:
                            return aInt + bUint;
                        case long bLong:
                            return aInt + bLong;
                        case ulong bUlong:
                            return aInt + (long)bUlong;
                        case float bFloat:
                            return aInt + bFloat;
                        case double bDouble:
                            return aInt + bDouble;
                        case decimal bDecimal:
                            return (decimal)aInt + bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aInt) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'int' and '{b.GetType()}'");
                    }
                case uint aUint:
                    switch (b)
                    {
                        case byte bByte:
                            return aUint + bByte;
                        case sbyte bSbyte:
                            return aUint + bSbyte;
                        case short bShort:
                            return aUint + bShort;
                        case ushort bUshort:
                            return aUint + bUshort;
                        case int bInt:
                            return aUint + bInt;
                        case uint bUint:
                            return aUint + bUint;
                        case long bLong:
                            return aUint + bLong;
                        case ulong bUlong:
                            return aUint + bUlong;
                        case float bFloat:
                            return aUint + bFloat;
                        case double bDouble:
                            return aUint + bDouble;
                        case decimal bDecimal:
                            return (decimal)aUint + bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUint) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'uint' and '{b.GetType()}'");
                    }
                case long aLong:
                    switch (b)
                    {
                        case byte bByte:
                            return aLong + bByte;
                        case sbyte bSbyte:
                            return aLong + bSbyte;
                        case short bShort:
                            return aLong + bShort;
                        case ushort bUshort:
                            return aLong + bUshort;
                        case int bInt:
                            return aLong + bInt;
                        case uint bUint:
                            return aLong + bUint;
                        case long bLong:
                            return aLong + bLong;
                        case ulong bUlong:
                            return aLong + (long)bUlong;
                        case float bFloat:
                            return aLong + bFloat;
                        case double bDouble:
                            return aLong + bDouble;
                        case decimal bDecimal:
                            return (decimal)aLong + bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aLong) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'long' and '{b.GetType()}'");
                    }
                case ulong aUlong:
                    switch (b)
                    {
                        case byte bByte:
                            return aUlong + bByte;
                        case sbyte bSbyte:
                            return (long)aUlong + bSbyte;
                        case short bShort:
                            return (long)aUlong + bShort;
                        case ushort bUshort:
                            return aUlong + bUshort;
                        case int bInt:
                            return (long)aUlong + bInt;
                        case uint bUint:
                            return aUlong + bUint;
                        case long bLong:
                            return (long)aUlong + bLong;
                        case ulong bUlong:
                            return aUlong + bUlong;
                        case float bFloat:
                            return aUlong + bFloat;
                        case double bDouble:
                            return aUlong + bDouble;
                        case decimal bDecimal:
                            return (decimal)aUlong + bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUlong) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'ulong' and '{b.GetType()}'");
                    }
                case float aFloat:
                    switch (b)
                    {
                        case byte bByte:
                            return aFloat + bByte;
                        case sbyte bSbyte:
                            return aFloat + bSbyte;
                        case short bShort:
                            return aFloat + bShort;
                        case ushort bUshort:
                            return aFloat + bUshort;
                        case int bInt:
                            return aFloat + bInt;
                        case uint bUint:
                            return aFloat + bUint;
                        case long bLong:
                            return aFloat + bLong;
                        case ulong bUlong:
                            return aFloat + bUlong;
                        case float bFloat:
                            return aFloat + bFloat;
                        case double bDouble:
                            return aFloat + bDouble;
                        case decimal bDecimal:
                            return (decimal)aFloat + bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aFloat) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'float' and '{b.GetType()}'");
                    }
                case double aDouble:
                    switch (b)
                    {
                        case byte bByte:
                            return aDouble + bByte;
                        case sbyte bSbyte:
                            return aDouble + bSbyte;
                        case short bShort:
                            return aDouble + bShort;
                        case ushort bUshort:
                            return aDouble + bUshort;
                        case int bInt:
                            return aDouble + bInt;
                        case uint bUint:
                            return aDouble + bUint;
                        case long bLong:
                            return aDouble + bLong;
                        case ulong bUlong:
                            return aDouble + bUlong;
                        case float bFloat:
                            return aDouble + bFloat;
                        case double bDouble:
                            return aDouble + bDouble;
                        case decimal bDecimal:
                            return (decimal)aDouble + bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aDouble) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'double' and '{b.GetType()}'");
                    }
                case decimal aDecimal:
                    switch (b)
                    {
                        case byte bByte:
                            return aDecimal + (decimal)bByte;
                        case sbyte bSbyte:
                            return aDecimal + (decimal)bSbyte;
                        case short bShort:
                            return aDecimal + (decimal)bShort;
                        case ushort bUshort:
                            return aDecimal + (decimal)bUshort;
                        case int bInt:
                            return aDecimal + (decimal)bInt;
                        case uint bUint:
                            return aDecimal + (decimal)bUint;
                        case long bLong:
                            return aDecimal + (decimal)bLong;
                        case ulong bUlong:
                            return aDecimal + (decimal)bUlong;
                        case float bFloat:
                            return aDecimal + (decimal)bFloat;
                        case double bDouble:
                            return aDecimal + (decimal)bDouble;
                        case decimal bDecimal:
                            return aDecimal + (decimal)bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aDecimal) + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'decimal' and '{b.GetType()}'");
                    }
                case BigInteger aBigInteger:
                    switch (b)
                    {
                        case byte bByte:
                            return aBigInteger + new BigInteger(bByte);
                        case sbyte bSbyte:
                            return aBigInteger + new BigInteger(bSbyte);
                        case short bShort:
                            return aBigInteger + new BigInteger(bShort);
                        case ushort bUshort:
                            return aBigInteger + new BigInteger(bUshort);
                        case int bInt:
                            return aBigInteger + new BigInteger(bInt);
                        case uint bUint:
                            return aBigInteger + new BigInteger(bUint);
                        case long bLong:
                            return aBigInteger + new BigInteger(bLong);
                        case ulong bUlong:
                            return aBigInteger + new BigInteger(bUlong);
                        case float bFloat:
                            return aBigInteger + new BigInteger(bFloat);
                        case double bDouble:
                            return aBigInteger + new BigInteger(bDouble);
                        case decimal bDecimal:
                            return aBigInteger + new BigInteger(bDecimal);
                        case BigInteger bBigInteger:
                            return aBigInteger + bBigInteger;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'BigInteger' and '{b.GetType()}'");
                    }

                default:
                    throw new InvalidOperationException(
                        $"Operator '+' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }
        }

        #endregion

        public static object Subtract(object a, object b)
        {
            return Subtract(a, b, CultureInfo.CurrentCulture);
        }

        public static object Subtract(object a, object b, CultureInfo cultureInfo)
        {
            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);

            TypeCode typeCodeA = Type.GetTypeCode(a.GetType());
            TypeCode typeCodeB = Type.GetTypeCode(b.GetType());

            switch (typeCodeA)
            {
                case TypeCode.Boolean: 
                    throw new InvalidOperationException($"Operator '-' can't be applied to operands of types 'bool' and {typeCodeB}");
                case TypeCode.Byte:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'byte' and 'bool'");
                        case TypeCode.Byte: return (Byte)a - (Byte)b;
                        case TypeCode.SByte: return (Byte)a - (SByte)b;
                        case TypeCode.Int16: return (Byte)a - (Int16)b;
                        case TypeCode.UInt16: return (Byte)a - (UInt16)b;
                        case TypeCode.Int32: return (Byte)a - (Int32)b;
                        case TypeCode.UInt32: return (Byte)a - (UInt32)b;
                        case TypeCode.Int64: return (Byte)a - (Int64)b;
                        case TypeCode.UInt64: return (Byte)a - (UInt64)b;
                        case TypeCode.Single: return (Byte)a - (Single)b;
                        case TypeCode.Double: return (Byte)a - (Double)b;
                        case TypeCode.Decimal: return (Byte)a - (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'byte' and {typeCodeB}");
                    }
                case TypeCode.SByte:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'sbyte' and 'bool'");
                        case TypeCode.Byte: return (SByte)a - (Byte)b;
                        case TypeCode.SByte: return (SByte)a - (SByte)b;
                        case TypeCode.Int16: return (SByte)a - (Int16)b;
                        case TypeCode.UInt16: return (SByte)a - (UInt16)b;
                        case TypeCode.Int32: return (SByte)a - (Int32)b;
                        case TypeCode.UInt32: return (SByte)a - (UInt32)b;
                        case TypeCode.Int64: return (SByte)a - (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'sbyte' and 'ulong'");
                        case TypeCode.Single: return (SByte)a - (Single)b;
                        case TypeCode.Double: return (SByte)a - (Double)b;
                        case TypeCode.Decimal: return (SByte)a - (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'byte' and {typeCodeB}");
                    }
                case TypeCode.Int16:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'short' and 'bool'");
                        case TypeCode.Byte: return (Int16)a - (Byte)b;
                        case TypeCode.SByte: return (Int16)a - (SByte)b;
                        case TypeCode.Int16: return (Int16)a - (Int16)b;
                        case TypeCode.UInt16: return (Int16)a - (UInt16)b;
                        case TypeCode.Int32: return (Int16)a - (Int32)b;
                        case TypeCode.UInt32: return (Int16)a - (UInt32)b;
                        case TypeCode.Int64: return (Int16)a - (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'short' and 'ulong'");
                        case TypeCode.Single: return (Int16)a - (Single)b;
                        case TypeCode.Double: return (Int16)a - (Double)b;
                        case TypeCode.Decimal: return (Int16)a - (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'short' and {typeCodeB}");
                    }
                case TypeCode.UInt16:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ushort' and 'bool'");
                        case TypeCode.Byte: return (UInt16)a - (Byte)b;
                        case TypeCode.SByte: return (UInt16)a - (SByte)b;
                        case TypeCode.Int16: return (UInt16)a - (Int16)b;
                        case TypeCode.UInt16: return (UInt16)a - (UInt16)b;
                        case TypeCode.Int32: return (UInt16)a - (Int32)b;
                        case TypeCode.UInt32: return (UInt16)a - (UInt32)b;
                        case TypeCode.Int64: return (UInt16)a - (Int64)b;
                        case TypeCode.UInt64: return (UInt16)a - (UInt64)b;
                        case TypeCode.Single: return (UInt16)a - (Single)b;
                        case TypeCode.Double: return (UInt16)a - (Double)b;
                        case TypeCode.Decimal: return (UInt16)a - (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'ushort' and {typeCodeB}");
                    }
                case TypeCode.Int32:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'int' and 'bool'");
                        case TypeCode.Byte: return (Int32)a - (Byte)b;
                        case TypeCode.SByte: return (Int32)a - (SByte)b;
                        case TypeCode.Int16: return (Int32)a - (Int16)b;
                        case TypeCode.UInt16: return (Int32)a - (UInt16)b;
                        case TypeCode.Int32: return (Int32)a - (Int32)b;
                        case TypeCode.UInt32: return (Int32)a - (UInt32)b;
                        case TypeCode.Int64: return (Int32)a - (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'int' and 'ulong'");
                        case TypeCode.Single: return (Int32)a - (Single)b;
                        case TypeCode.Double: return (Int32)a - (Double)b;
                        case TypeCode.Decimal: return (Int32)a - (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'int' and {typeCodeB}");
                    }
                case TypeCode.UInt32:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'uint' and 'bool'");
                        case TypeCode.Byte: return (UInt32)a - (Byte)b;
                        case TypeCode.SByte: return (UInt32)a - (SByte)b;
                        case TypeCode.Int16: return (UInt32)a - (Int16)b;
                        case TypeCode.UInt16: return (UInt32)a - (UInt16)b;
                        case TypeCode.Int32: return (UInt32)a - (Int32)b;
                        case TypeCode.UInt32: return (UInt32)a - (UInt32)b;
                        case TypeCode.Int64: return (UInt32)a - (Int64)b;
                        case TypeCode.UInt64: return (UInt32)a - (UInt64)b;
                        case TypeCode.Single: return (UInt32)a - (Single)b;
                        case TypeCode.Double: return (UInt32)a - (Double)b;
                        case TypeCode.Decimal: return (UInt32)a - (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'uint' and {typeCodeB}");
                    }
                case TypeCode.Int64:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'long' and 'bool'");
                        case TypeCode.Byte: return (Int64)a - (Byte)b;
                        case TypeCode.SByte: return (Int64)a - (SByte)b;
                        case TypeCode.Int16: return (Int64)a - (Int16)b;
                        case TypeCode.UInt16: return (Int64)a - (UInt16)b;
                        case TypeCode.Int32: return (Int64)a - (Int32)b;
                        case TypeCode.UInt32: return (Int64)a - (UInt32)b;
                        case TypeCode.Int64: return (Int64)a - (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'long' and 'ulong'");
                        case TypeCode.Single: return (Int64)a - (Single)b;
                        case TypeCode.Double: return (Int64)a - (Double)b;
                        case TypeCode.Decimal: return (Int64)a - (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'long' and {typeCodeB}");
                    }
                case TypeCode.UInt64:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'bool'");
                        case TypeCode.Byte: return (UInt64)a - (byte)b;
                        case TypeCode.SByte: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'sbyte'");
                        case TypeCode.Int16: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'short'");
                        case TypeCode.UInt16: return (UInt64)a - (UInt16)b;
                        case TypeCode.Int32: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'int'");
                        case TypeCode.UInt32: return (UInt64)a - (UInt32)b;
                        case TypeCode.Int64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'long'");
                        case TypeCode.UInt64: return (UInt64)a - (UInt64)b;
                        case TypeCode.Single: return (UInt64)a - (Single)b;
                        case TypeCode.Double: return (UInt64)a - (Double)b;
                        case TypeCode.Decimal: return (UInt64)a - (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'ulong' and {typeCodeB}");
                    }

                case TypeCode.Single:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'float' and 'bool'");
                        case TypeCode.Byte: return (Single)a - (Byte)b;
                        case TypeCode.SByte: return (Single)a - (SByte)b;
                        case TypeCode.Int16: return (Single)a - (Int16)b;
                        case TypeCode.UInt16: return (Single)a - (UInt16)b;
                        case TypeCode.Int32: return (Single)a - (Int32)b;
                        case TypeCode.UInt32: return (Single)a - (UInt32)b;
                        case TypeCode.Int64: return (Single)a - (Int64)b;
                        case TypeCode.UInt64: return (Single)a - (UInt64)b;
                        case TypeCode.Single: return (Single)a - (Single)b;
                        case TypeCode.Double: return (Single)a - (Double)b;
                        case TypeCode.Decimal: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'float' and 'decimal'");
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'float' and {typeCodeB}");
                    }
                case TypeCode.Double:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'double' and 'bool'");
                        case TypeCode.Byte: return (Double)a - (Byte)b;
                        case TypeCode.SByte: return (Double)a - (SByte)b;
                        case TypeCode.Int16: return (Double)a - (Int16)b;
                        case TypeCode.UInt16: return (Double)a - (UInt16)b;
                        case TypeCode.Int32: return (Double)a - (Int32)b;
                        case TypeCode.UInt32: return (Double)a - (UInt32)b;
                        case TypeCode.Int64: return (Double)a - (Int64)b;
                        case TypeCode.UInt64: return (Double)a - (UInt64)b;
                        case TypeCode.Single: return (Double)a - (Single)b;
                        case TypeCode.Double: return (Double)a - (Double)b;
                        case TypeCode.Decimal: return Convert.ToDecimal(a) - (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'double' and {typeCodeB}");
                    }
                case TypeCode.Decimal:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'decimal' and 'bool'");
                        case TypeCode.Byte: return (Decimal)a - (Byte)b;
                        case TypeCode.SByte: return (Decimal)a - (SByte)b;
                        case TypeCode.Int16: return (Decimal)a - (Int16)b;
                        case TypeCode.UInt16: return (Decimal)a - (UInt16)b;
                        case TypeCode.Int32: return (Decimal)a - (Int32)b;
                        case TypeCode.UInt32: return (Decimal)a - (UInt32)b;
                        case TypeCode.Int64: return (Decimal)a - (Int64)b;
                        case TypeCode.UInt64: return (Decimal)a - (UInt64)b;
                        case TypeCode.Single: return (Decimal)a - Convert.ToDecimal(b);
                        case TypeCode.Double: return (Decimal)a - Convert.ToDecimal(b);
                        case TypeCode.Decimal: return (Decimal)a - (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types 'decimal' and {typeCodeB}");
                    }
                default: throw new InvalidOperationException($"Operator '-' not implemented for operands of types {typeCodeA} and {typeCodeB}");
            }
        }

        public static object Multiply(object a, object b)
        {
            return Multiply(a, b, CultureInfo.CurrentCulture);
        }

        public static object Multiply(object a, object b, CultureInfo cultureInfo)
        {
            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);

            TypeCode typeCodeA = Type.GetTypeCode(a.GetType());
            TypeCode typeCodeB = Type.GetTypeCode(b.GetType());

            switch (typeCodeA)
            {
                case TypeCode.Boolean:
                    throw new InvalidOperationException(
                        $"Operator '*' can't be applied to operands of types 'bool' and {typeCodeB}");
                case TypeCode.Byte:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'byte' and 'bool'");
                        case TypeCode.Byte: return (Byte)a * (Byte)b;
                        case TypeCode.SByte: return (Byte)a * (SByte)b;
                        case TypeCode.Int16: return (Byte)a * (Int16)b;
                        case TypeCode.UInt16: return (Byte)a * (UInt16)b;
                        case TypeCode.Int32: return (Byte)a * (Int32)b;
                        case TypeCode.UInt32: return (Byte)a * (UInt32)b;
                        case TypeCode.Int64: return (Byte)a * (Int64)b;
                        case TypeCode.UInt64: return (Byte)a * (UInt64)b;
                        case TypeCode.Single: return (Byte)a * (Single)b;
                        case TypeCode.Double: return (Byte)a * (Double)b;
                        case TypeCode.Decimal: return (Byte)a * (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'byte' and {typeCodeB}");
                    }
                case TypeCode.SByte:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'sbyte' and 'bool'");
                        case TypeCode.Byte: return (SByte)a * (Byte)b;
                        case TypeCode.SByte: return (SByte)a * (SByte)b;
                        case TypeCode.Int16: return (SByte)a * (Int16)b;
                        case TypeCode.UInt16: return (SByte)a * (UInt16)b;
                        case TypeCode.Int32: return (SByte)a * (Int32)b;
                        case TypeCode.UInt32: return (SByte)a * (UInt32)b;
                        case TypeCode.Int64: return (SByte)a * (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'sbyte' and 'ulong'");
                        case TypeCode.Single: return (SByte)a * (Single)b;
                        case TypeCode.Double: return (SByte)a * (Double)b;
                        case TypeCode.Decimal: return (SByte)a * (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'sbyte' and {typeCodeB}");
                    }
                case TypeCode.Int16:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'short' and 'bool'");
                        case TypeCode.Byte: return (Int16)a * (Byte)b;
                        case TypeCode.SByte: return (Int16)a * (SByte)b;
                        case TypeCode.Int16: return (Int16)a * (Int16)b;
                        case TypeCode.UInt16: return (Int16)a * (UInt16)b;
                        case TypeCode.Int32: return (Int16)a * (Int32)b;
                        case TypeCode.UInt32: return (Int16)a * (UInt32)b;
                        case TypeCode.Int64: return (Int16)a * (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'short' and 'ulong'");
                        case TypeCode.Single: return (Int16)a * (Single)b;
                        case TypeCode.Double: return (Int16)a * (Double)b;
                        case TypeCode.Decimal: return (Int16)a * (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'short' and {typeCodeB}");
                    }
                case TypeCode.UInt16:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ushort' and 'bool'");
                        case TypeCode.Byte: return (UInt16)a * (Byte)b;
                        case TypeCode.SByte: return (UInt16)a * (SByte)b;
                        case TypeCode.Int16: return (UInt16)a * (Int16)b;
                        case TypeCode.UInt16: return (UInt16)a * (UInt16)b;
                        case TypeCode.Int32: return (UInt16)a * (Int32)b;
                        case TypeCode.UInt32: return (UInt16)a * (UInt32)b;
                        case TypeCode.Int64: return (UInt16)a * (Int64)b;
                        case TypeCode.UInt64: return (UInt16)a * (UInt64)b;
                        case TypeCode.Single: return (UInt16)a * (Single)b;
                        case TypeCode.Double: return (UInt16)a * (Double)b;
                        case TypeCode.Decimal: return (UInt16)a * (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'ushort' and {typeCodeB}");
                    }
                case TypeCode.Int32:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'int' and 'bool'");
                        case TypeCode.Byte: return (Int32)a * (Byte)b;
                        case TypeCode.SByte: return (Int32)a * (SByte)b;
                        case TypeCode.Int16: return (Int32)a * (Int16)b;
                        case TypeCode.UInt16: return (Int32)a * (UInt16)b;
                        case TypeCode.Int32: return (Int32)a * (Int32)b;
                        case TypeCode.UInt32: return (Int32)a * (UInt32)b;
                        case TypeCode.Int64: return (Int32)a * (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'int' and 'ulong'");
                        case TypeCode.Single: return (Int32)a * (Single)b;
                        case TypeCode.Double: return (Int32)a * (Double)b;
                        case TypeCode.Decimal: return (Int32)a * (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'int' and {typeCodeB}");
                    }
                case TypeCode.UInt32:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'uint' and 'bool'");
                        case TypeCode.Byte: return (UInt32)a * (Byte)b;
                        case TypeCode.SByte: return (UInt32)a * (SByte)b;
                        case TypeCode.Int16: return (UInt32)a * (Int16)b;
                        case TypeCode.UInt16: return (UInt32)a * (UInt16)b;
                        case TypeCode.Int32: return (UInt32)a * (Int32)b;
                        case TypeCode.UInt32: return (UInt32)a * (UInt32)b;
                        case TypeCode.Int64: return (UInt32)a * (Int64)b;
                        case TypeCode.UInt64: return (UInt32)a * (UInt64)b;
                        case TypeCode.Single: return (UInt32)a * (Single)b;
                        case TypeCode.Double: return (UInt32)a * (Double)b;
                        case TypeCode.Decimal: return (UInt32)a * (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'int' and {typeCodeB}");
                    }
                case TypeCode.Int64:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'long' and 'bool'");
                        case TypeCode.Byte: return (Int64)a * (Byte)b;
                        case TypeCode.SByte: return (Int64)a * (SByte)b;
                        case TypeCode.Int16: return (Int64)a * (Int16)b;
                        case TypeCode.UInt16: return (Int64)a * (UInt16)b;
                        case TypeCode.Int32: return (Int64)a * (Int32)b;
                        case TypeCode.UInt32: return (Int64)a * (UInt32)b;
                        case TypeCode.Int64: return (Int64)a * (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'long' and 'ulong'");
                        case TypeCode.Single: return (Int64)a * (Single)b;
                        case TypeCode.Double: return (Int64)a * (Double)b;
                        case TypeCode.Decimal: return (Int64)a * (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'int' and {typeCodeB}");
                    }
                case TypeCode.UInt64:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'bool'");
                        case TypeCode.Byte: return (UInt64)a * (Byte)b;
                        case TypeCode.SByte: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'sbyte'");
                        case TypeCode.Int16: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'short'");
                        case TypeCode.UInt16: return (UInt64)a * (UInt16)b;
                        case TypeCode.Int32: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'int'");
                        case TypeCode.UInt32: return (UInt64)a * (UInt32)b;
                        case TypeCode.Int64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'long'");
                        case TypeCode.UInt64: return (UInt64)a * (UInt64)b;
                        case TypeCode.Single: return (UInt64)a * (Single)b;
                        case TypeCode.Double: return (UInt64)a * (Double)b;
                        case TypeCode.Decimal: return (UInt64)a * (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'ulong' and {typeCodeB}");
                    }

                case TypeCode.Single:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'float' and 'bool'");
                        case TypeCode.Byte: return (Single)a * (Byte)b;
                        case TypeCode.SByte: return (Single)a * (SByte)b;
                        case TypeCode.Int16: return (Single)a * (Int16)b;
                        case TypeCode.UInt16: return (Single)a * (UInt16)b;
                        case TypeCode.Int32: return (Single)a * (Int32)b;
                        case TypeCode.UInt32: return (Single)a * (UInt32)b;
                        case TypeCode.Int64: return (Single)a * (Int64)b;
                        case TypeCode.UInt64: return (Single)a * (UInt64)b;
                        case TypeCode.Single: return (Single)a * (Single)b;
                        case TypeCode.Double: return (Single)a * (Double)b;
                        case TypeCode.Decimal: return Convert.ToDecimal(a) * (Decimal) b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'float' and {typeCodeB}");
                    }
 
                case TypeCode.Double:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'double' and 'bool'");
                        case TypeCode.Byte: return (Double)a * (Byte)b;
                        case TypeCode.SByte: return (Double)a * (SByte)b;
                        case TypeCode.Int16: return (Double)a * (Int16)b;
                        case TypeCode.UInt16: return (Double)a * (UInt16)b;
                        case TypeCode.Int32: return (Double)a * (Int32)b;
                        case TypeCode.UInt32: return (Double)a * (UInt32)b;
                        case TypeCode.Int64: return (Double)a * (Int64)b;
                        case TypeCode.UInt64: return (Double)a * (UInt64)b;
                        case TypeCode.Single: return (Double)a * (Single)b;
                        case TypeCode.Double: return (Double)a * (Double)b;
                        case TypeCode.Decimal: return Convert.ToDecimal(a) * (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'double' and {typeCodeB}");
                    }
                case TypeCode.Decimal:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'decimal' and 'bool'");
                        case TypeCode.Byte: return (Decimal)a * (Byte)b;
                        case TypeCode.SByte: return (Decimal)a * (SByte)b;
                        case TypeCode.Int16: return (Decimal)a * (Int16)b;
                        case TypeCode.UInt16: return (Decimal)a * (UInt16)b;
                        case TypeCode.Int32: return (Decimal)a * (Int32)b;
                        case TypeCode.UInt32: return (Decimal)a * (UInt32)b;
                        case TypeCode.Int64: return (Decimal)a * (Int64)b;
                        case TypeCode.UInt64: return (Decimal)a * (UInt64)b;
                        case TypeCode.Single: return (Decimal) a * Convert.ToDecimal(b);
                        case TypeCode.Double: return (Decimal)a * Convert.ToDecimal(b);
                        case TypeCode.Decimal: return (Decimal)a * (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types 'decimal' and {typeCodeB}");
                    }
                default: throw new InvalidOperationException($"Operator '*' not implemented for operands of types {typeCodeA} and {typeCodeB}");
            }
        }
        
        public static object Divide(object a, object b)
        {
            return Divide(a, b, CultureInfo.CurrentCulture);
        }

        public static object Divide(object a, object b, CultureInfo cultureInfo)
        {
            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);

            TypeCode typeCodeA = Type.GetTypeCode(a.GetType());
            TypeCode typeCodeB = Type.GetTypeCode(b.GetType());

            switch (typeCodeA)
            {
                case TypeCode.Boolean:
                    throw new InvalidOperationException(
                        $"Operator '/' can't be applied to operands of types 'bool' and {typeCodeB}");
                case TypeCode.Byte:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'byte' and 'bool'");
                        case TypeCode.Byte: return (Byte)a / (Byte)b;
                        case TypeCode.SByte: return (Byte)a / (SByte)b;
                        case TypeCode.Int16: return (Byte)a / (Int16)b;
                        case TypeCode.UInt16: return (Byte)a / (UInt16)b;
                        case TypeCode.Int32: return (Byte)a / (Int32)b;
                        case TypeCode.UInt32: return (Byte)a / (UInt32)b;
                        case TypeCode.Int64: return (Byte)a / (Int64)b;
                        case TypeCode.UInt64: return (Byte)a / (UInt64)b;
                        case TypeCode.Single: return (Byte)a / (Single)b;
                        case TypeCode.Double: return (Byte)a / (Double)b;
                        case TypeCode.Decimal: return (Byte)a / (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'decimal' and {typeCodeB}");
                    }
                case TypeCode.SByte:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'sbyte' and 'bool'");
                        case TypeCode.Byte: return (SByte)a / (Byte)b;
                        case TypeCode.SByte: return (SByte)a / (SByte)b;
                        case TypeCode.Int16: return (SByte)a / (Int16)b;
                        case TypeCode.UInt16: return (SByte)a / (UInt16)b;
                        case TypeCode.Int32: return (SByte)a / (Int32)b;
                        case TypeCode.UInt32: return (SByte)a / (UInt32)b;
                        case TypeCode.Int64: return (SByte)a / (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'sbyte' and 'ulong'");
                        case TypeCode.Single: return (SByte)a / (Single)b;
                        case TypeCode.Double: return (SByte)a / (Double)b;
                        case TypeCode.Decimal: return (SByte)a / (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'decimal' and {typeCodeB}");
                    }

                case TypeCode.Int16:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'short' and 'bool'");
                        case TypeCode.Byte: return (Int16)a / (Byte)b;
                        case TypeCode.SByte: return (Int16)a / (SByte)b;
                        case TypeCode.Int16: return (Int16)a / (Int16)b;
                        case TypeCode.UInt16: return (Int16)a / (UInt16)b;
                        case TypeCode.Int32: return (Int16)a / (Int32)b;
                        case TypeCode.UInt32: return (Int16)a / (UInt32)b;
                        case TypeCode.Int64: return (Int16)a / (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'short' and 'ulong'");
                        case TypeCode.Single: return (Int16)a / (Single)b;
                        case TypeCode.Double: return (Int16)a / (Double)b;
                        case TypeCode.Decimal: return (Int16)a / (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'decimal' and {typeCodeB}");
                    }
                case TypeCode.UInt16:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ushort' and 'bool'");
                        case TypeCode.Byte: return (UInt16)a / (Byte)b;
                        case TypeCode.SByte: return (UInt16)a / (SByte)b;
                        case TypeCode.Int16: return (UInt16)a / (Int16)b;
                        case TypeCode.UInt16: return (UInt16)a / (UInt16)b;
                        case TypeCode.Int32: return (UInt16)a / (Int32)b;
                        case TypeCode.UInt32: return (UInt16)a / (UInt32)b;
                        case TypeCode.Int64: return (UInt16)a / (Int64)b;
                        case TypeCode.UInt64: return (UInt16)a / (UInt64)b;
                        case TypeCode.Single: return (UInt16)a / (Single)b;
                        case TypeCode.Double: return (UInt16)a / (Double)b;
                        case TypeCode.Decimal: return (UInt16)a / (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'ushort' and {typeCodeB}");
                    }
                case TypeCode.Int32:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'int' and 'bool'");
                        case TypeCode.Byte: return (Int32)a / (Byte)b;
                        case TypeCode.SByte: return (Int32)a / (SByte)b;
                        case TypeCode.Int16: return (Int32)a / (Int16)b;
                        case TypeCode.UInt16: return (Int32)a / (UInt16)b;
                        case TypeCode.Int32: return (Int32)a / (Int32)b;
                        case TypeCode.UInt32: return (Int32)a / (UInt32)b;
                        case TypeCode.Int64: return (Int32)a / (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'int' and 'ulong'");
                        case TypeCode.Single: return (Int32)a / (Single)b;
                        case TypeCode.Double: return (Int32)a / (Double)b;
                        case TypeCode.Decimal: return (Int32)a / (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'int' and {typeCodeB}");
                    }
                case TypeCode.UInt32:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'uint' and 'bool'");
                        case TypeCode.Byte: return (UInt32)a / (Byte)b;
                        case TypeCode.SByte: return (UInt32)a / (SByte)b;
                        case TypeCode.Int16: return (UInt32)a / (Int16)b;
                        case TypeCode.UInt16: return (UInt32)a / (UInt16)b;
                        case TypeCode.Int32: return (UInt32)a / (Int32)b;
                        case TypeCode.UInt32: return (UInt32)a / (UInt32)b;
                        case TypeCode.Int64: return (UInt32)a / (Int64)b;
                        case TypeCode.UInt64: return (UInt32)a / (UInt64)b;
                        case TypeCode.Single: return (UInt32)a / (Single)b;
                        case TypeCode.Double: return (UInt32)a / (Double)b;
                        case TypeCode.Decimal: return (UInt32)a / (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'uint' and {typeCodeB}");
                    }
                case TypeCode.Int64:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'long' and 'bool'");
                        case TypeCode.Byte: return (Int64)a / (Byte)b;
                        case TypeCode.SByte: return (Int64)a / (SByte)b;
                        case TypeCode.Int16: return (Int64)a / (Int16)b;
                        case TypeCode.UInt16: return (Int64)a / (UInt16)b;
                        case TypeCode.Int32: return (Int64)a / (Int32)b;
                        case TypeCode.UInt32: return (Int64)a / (UInt32)b;
                        case TypeCode.Int64: return (Int64)a / (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'long' and 'ulong'");
                        case TypeCode.Single: return (Int64)a / (Single)b;
                        case TypeCode.Double: return (Int64)a / (Double)b;
                        case TypeCode.Decimal: return (Int64)a / (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'long' and {typeCodeB}");
                    }
                case TypeCode.UInt64:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'bool'");
                        case TypeCode.Byte: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ulong' and 'byte'");
                        case TypeCode.SByte: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ulong' and 'sbyte'");
                        case TypeCode.Int16: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ulong' and 'short'");
                        case TypeCode.UInt16: return (UInt64)a / (UInt16)b;
                        case TypeCode.Int32: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ulong' and 'int'");
                        case TypeCode.UInt32: return (UInt64)a / (UInt32)b;
                        case TypeCode.Int64: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ulong' and 'long'");
                        case TypeCode.UInt64: return (UInt64)a / (UInt64)b;
                        case TypeCode.Single: return (UInt64)a / (Single)b;
                        case TypeCode.Double: return (UInt64)a / (Double)b;
                        case TypeCode.Decimal: return (UInt64)a / (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'ulong' and {typeCodeB}");
                    }
                case TypeCode.Single:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'float' and 'bool'");
                        case TypeCode.Byte: return (Single)a / (Byte)b;
                        case TypeCode.SByte: return (Single)a / (SByte)b;
                        case TypeCode.Int16: return (Single)a / (Int16)b;
                        case TypeCode.UInt16: return (Single)a / (UInt16)b;
                        case TypeCode.Int32: return (Single)a / (Int32)b;
                        case TypeCode.UInt32: return (Single)a / (UInt32)b;
                        case TypeCode.Int64: return (Single)a / (Int64)b;
                        case TypeCode.UInt64: return (Single)a / (UInt64)b;
                        case TypeCode.Single: return (Single)a / (Single)b;
                        case TypeCode.Double: return (Single)a / (Double)b;
                        case TypeCode.Decimal: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'float' and 'decimal'");
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'ulong' and {typeCodeB}");
                    }
                case TypeCode.Double:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'double' and 'bool'");
                        case TypeCode.Byte: return (Double)a / (Byte)b;
                        case TypeCode.SByte: return (Double)a / (SByte)b;
                        case TypeCode.Int16: return (Double)a / (Int16)b;
                        case TypeCode.UInt16: return (Double)a / (UInt16)b;
                        case TypeCode.Int32: return (Double)a / (Int32)b;
                        case TypeCode.UInt32: return (Double)a / (UInt32)b;
                        case TypeCode.Int64: return (Double)a / (Int64)b;
                        case TypeCode.UInt64: return (Double)a / (UInt64)b;
                        case TypeCode.Single: return (Double)a / (Single)b;
                        case TypeCode.Double: return (Double)a / (Double)b;
                        case TypeCode.Decimal: return Convert.ToDecimal(a) / (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'double' and {typeCodeB}");
                    }
                case TypeCode.Decimal:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'decimal' and 'bool'");
                        case TypeCode.Byte: return (Decimal)a / (SByte)b;
                        case TypeCode.SByte: return (Decimal)a / (SByte)b;
                        case TypeCode.Int16: return (Decimal)a / (Int16)b;
                        case TypeCode.UInt16: return (Decimal)a / (UInt16)b;
                        case TypeCode.Int32: return (Decimal)a / (Int32)b;
                        case TypeCode.UInt32: return (Decimal)a / (UInt32)b;
                        case TypeCode.Int64: return (Decimal)a / (Int64)b;
                        case TypeCode.UInt64: return (Decimal)a / (UInt64)b;
                        case TypeCode.Single: return (Decimal)a / Convert.ToDecimal(b);
                        case TypeCode.Double: return (Decimal)a / Convert.ToDecimal(b);
                        case TypeCode.Decimal: return (Decimal)a / (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'decimal' and {typeCodeB}");
                    }
                default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types {typeCodeA} and {typeCodeB}");
            }
        }

        public static object Modulo(object a, object b)
        {
            return Modulo(a, b, CultureInfo.CurrentCulture);
        }

        public static object Modulo(object a, object b, CultureInfo cultureInfo)
        {
            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);

            TypeCode typeCodeA = Type.GetTypeCode(a.GetType());
            TypeCode typeCodeB = Type.GetTypeCode(b.GetType());

            switch (typeCodeA)
            {
                case TypeCode.Boolean:
                    throw new InvalidOperationException(
                        $"Operator '%' can't be applied to operands of types 'bool' and {typeCodeB}");
                case TypeCode.Byte:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'byte' and 'bool'");
                        case TypeCode.Byte: return (Byte)a % (Byte)b;
                        case TypeCode.SByte: return (Byte)a % (SByte)b;
                        case TypeCode.Int16: return (Byte)a % (Int16)b;
                        case TypeCode.UInt16: return (Byte)a % (UInt16)b;
                        case TypeCode.Int32: return (Byte)a % (Int32)b;
                        case TypeCode.UInt32: return (Byte)a % (UInt32)b;
                        case TypeCode.Int64: return (Byte)a % (Int64)b;
                        case TypeCode.UInt64: return (Byte)a % (UInt64)b;
                        case TypeCode.Single: return (Byte)a % (Single)b;
                        case TypeCode.Double: return (Byte)a % (Double)b;
                        case TypeCode.Decimal: return (Byte)a % (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'byte' and {typeCodeB}");
                    }
                case TypeCode.SByte:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'sbyte' and 'bool'");
                        case TypeCode.Byte: return (SByte)a % (Byte)b;
                        case TypeCode.SByte: return (SByte)a % (SByte)b;
                        case TypeCode.Int16: return (SByte)a % (Int16)b;
                        case TypeCode.UInt16: return (SByte)a % (UInt16)b;
                        case TypeCode.Int32: return (SByte)a % (Int32)b;
                        case TypeCode.UInt32: return (SByte)a % (UInt32)b;
                        case TypeCode.Int64: return (SByte)a % (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'sbyte' and 'ulong'");
                        case TypeCode.Single: return (SByte)a % (Single)b;
                        case TypeCode.Double: return (SByte)a % (Double)b;
                        case TypeCode.Decimal: return (SByte)a % (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '/' not implemented for operands of types 'sbyte' and {typeCodeB}");
                    }
                case TypeCode.Int16:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'short' and 'bool'");
                        case TypeCode.Byte: return (Int16)a % (Byte)b;
                        case TypeCode.SByte: return (Int16)a % (SByte)b;
                        case TypeCode.Int16: return (Int16)a % (Int16)b;
                        case TypeCode.UInt16: return (Int16)a % (UInt16)b;
                        case TypeCode.Int32: return (Int16)a % (Int32)b;
                        case TypeCode.UInt32: return (Int16)a % (UInt32)b;
                        case TypeCode.Int64: return (Int16)a % (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'short' and 'ulong'");
                        case TypeCode.Single: return (Int16)a % (Single)b;
                        case TypeCode.Double: return (Int16)a % (Double)b;
                        case TypeCode.Decimal: return (Int16)a % (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '%' not implemented for operands of types 'short' and {typeCodeB}");
                    }
                case TypeCode.UInt16:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ushort' and 'bool'");
                        case TypeCode.Byte: return (UInt16)a % (Byte)b;
                        case TypeCode.SByte: return (UInt16)a % (SByte)b;
                        case TypeCode.Int16: return (UInt16)a % (Int16)b;
                        case TypeCode.UInt16: return (UInt16)a % (UInt16)b;
                        case TypeCode.Int32: return (UInt16)a % (Int32)b;
                        case TypeCode.UInt32: return (UInt16)a % (UInt32)b;
                        case TypeCode.Int64: return (UInt16)a % (Int64)b;
                        case TypeCode.UInt64: return (UInt16)a % (UInt64)b;
                        case TypeCode.Single: return (UInt16)a % (Single)b;
                        case TypeCode.Double: return (UInt16)a % (Double)b;
                        case TypeCode.Decimal: return (UInt16)a % (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '%' not implemented for operands of types 'ushort' and {typeCodeB}");
                    }
                case TypeCode.Int32:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'int' and 'bool'");
                        case TypeCode.Byte: return (Int32)a % (Byte)b;
                        case TypeCode.SByte: return (Int32)a % (SByte)b;
                        case TypeCode.Int16: return (Int32)a % (Int16)b;
                        case TypeCode.UInt16: return (Int32)a % (UInt16)b;
                        case TypeCode.Int32: return (Int32)a % (Int32)b;
                        case TypeCode.UInt32: return (Int32)a % (UInt32)b;
                        case TypeCode.Int64: return (Int32)a % (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'int' and 'ulong'");
                        case TypeCode.Single: return (Int32)a % (Single)b;
                        case TypeCode.Double: return (Int32)a % (Double)b;
                        case TypeCode.Decimal: return (Int32)a % (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '%' not implemented for operands of types 'int' and {typeCodeB}");
                    }
                case TypeCode.UInt32:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'uint' and 'bool'");
                        case TypeCode.Byte: return (UInt32)a % (Byte)b;
                        case TypeCode.SByte: return (UInt32)a % (SByte)b;
                        case TypeCode.Int16: return (UInt32)a % (Int16)b;
                        case TypeCode.UInt16: return (UInt32)a % (UInt16)b;
                        case TypeCode.Int32: return (UInt32)a % (Int32)b;
                        case TypeCode.UInt32: return (UInt32)a % (UInt32)b;
                        case TypeCode.Int64: return (UInt32)a % (Int64)b;
                        case TypeCode.UInt64: return (UInt32)a % (UInt64)b;
                        case TypeCode.Single: return (UInt32)a % (Single)b;
                        case TypeCode.Double: return (UInt32)a % (Double)b;
                        case TypeCode.Decimal: return (UInt32)a % (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '%' not implemented for operands of types 'uint' and {typeCodeB}");
                    }
                case TypeCode.Int64:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'long' and 'bool'");
                        case TypeCode.Byte: return (Int64)a % (Byte)b;
                        case TypeCode.SByte: return (Int64)a % (SByte)b;
                        case TypeCode.Int16: return (Int64)a % (Int16)b;
                        case TypeCode.UInt16: return (Int64)a % (UInt16)b;
                        case TypeCode.Int32: return (Int64)a % (Int32)b;
                        case TypeCode.UInt32: return (Int64)a % (UInt32)b;
                        case TypeCode.Int64: return (Int64)a % (Int64)b;
                        case TypeCode.UInt64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'long' and 'ulong'");
                        case TypeCode.Single: return (Int64)a % (Single)b;
                        case TypeCode.Double: return (Int64)a % (Double)b;
                        case TypeCode.Decimal: return (Int64)a % (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '%' not implemented for operands of types 'long' and {typeCodeB}");
                    }
                case TypeCode.UInt64:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ulong' and 'bool'");
                        case TypeCode.Byte: return (UInt64)a % (Byte)b;
                        case TypeCode.SByte: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ulong' and 'sbyte'");
                        case TypeCode.Int16: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ulong' and 'short'");
                        case TypeCode.UInt16: return (UInt64)a % (UInt16)b;
                        case TypeCode.Int32: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ulong' and 'int'");
                        case TypeCode.UInt32: return (UInt64)a % (UInt32)b;
                        case TypeCode.Int64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ulong' and 'long'");
                        case TypeCode.UInt64: return (UInt64)a % (UInt64)b;
                        case TypeCode.Single: return (UInt64)a % (Single)b;
                        case TypeCode.Double: return (UInt64)a % (Double)b;
                        case TypeCode.Decimal: return (UInt64)a % (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '%' not implemented for operands of types 'ulong' and {typeCodeB}");
                    }
                case TypeCode.Single:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'float' and 'bool'");
                        case TypeCode.Byte: return (Single)a % (Byte)b;
                        case TypeCode.SByte: return (Single)a % (SByte)b;
                        case TypeCode.Int16: return (Single)a % (Int16)b;
                        case TypeCode.UInt16: return (Single)a % (UInt16)b;
                        case TypeCode.Int32: return (Single)a % (Int32)b;
                        case TypeCode.UInt32: return (Single)a % (UInt32)b;
                        case TypeCode.Int64: return (Single)a % (Int64)b;
                        case TypeCode.UInt64: return (Single)a % (UInt64)b;
                        case TypeCode.Single: return (Single)a % (Single)b;
                        case TypeCode.Double: return (Single)a % (Double)b;
                        case TypeCode.Decimal: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'float' and 'decimal'");
                        default: throw new InvalidOperationException($"Operator '%' not implemented for operands of types 'long' and {typeCodeB}");
                    }
                case TypeCode.Double:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'double' and 'bool'");
                        case TypeCode.Byte: return (Double)a % (Byte)b;
                        case TypeCode.SByte: return (Double)a % (SByte)b;
                        case TypeCode.Int16: return (Double)a % (Int16)b;
                        case TypeCode.UInt16: return (Double)a % (UInt16)b;
                        case TypeCode.Int32: return (Double)a % (Int32)b;
                        case TypeCode.UInt32: return (Double)a % (UInt32)b;
                        case TypeCode.Int64: return (Double)a % (Int64)b;
                        case TypeCode.UInt64: return (Double)a % (UInt64)b;
                        case TypeCode.Single: return (Double)a % (Single)b;
                        case TypeCode.Double: return (Double)a % (Double)b;
                        case TypeCode.Decimal: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'double' and 'decimal'");
                        default: throw new InvalidOperationException($"Operator '%' not implemented for operands of types 'double' and {typeCodeB}");
                    }
                case TypeCode.Decimal:
                    switch (typeCodeB)
                    {
                        case TypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'decimal' and 'bool'");
                        case TypeCode.Byte: return (Decimal)a % (Byte)b;
                        case TypeCode.SByte: return (Decimal)a % (SByte)b;
                        case TypeCode.Int16: return (Decimal)a % (Int16)b;
                        case TypeCode.UInt16: return (Decimal)a % (UInt16)b;
                        case TypeCode.Int32: return (Decimal)a % (Int32)b;
                        case TypeCode.UInt32: return (Decimal)a % (UInt32)b;
                        case TypeCode.Int64: return (Decimal)a % (Int64)b;
                        case TypeCode.UInt64: return (Decimal)a % (UInt64)b;
                        case TypeCode.Single: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'decimal' and 'float'");
                        case TypeCode.Double: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'decimal' and 'decimal'");
                        case TypeCode.Decimal: return (Decimal)a % (Decimal)b;
                        default: throw new InvalidOperationException($"Operator '%' not implemented for operands of types 'decimal' and {typeCodeB}");
                    }
                default: throw new InvalidOperationException($"Operator '+' not implemented for operands of types {typeCodeA} and {typeCodeB}");
            }
        }

        public static object Max(object a, object b)
        {
            return Max(a, b, CultureInfo.CurrentCulture);
        }

        public static object Max(object a, object b, CultureInfo cultureInfo)
        {
            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);

            if (a == null && b == null)
            {
                return null;
            }

            if (a == null)
            {
                return b;
            }

            if (b == null)
            {
                return a;
            }

            TypeCode typeCodeA = Type.GetTypeCode(a.GetType());
            TypeCode typeCodeB = Type.GetTypeCode(b.GetType());

            switch (typeCodeA)
            {
                case TypeCode.Byte:
                    return Math.Max((byte)a, Convert.ToByte(b));
                case TypeCode.SByte:
                    return Math.Max((sbyte)a, Convert.ToSByte(b));
                case TypeCode.Int16:
                    return Math.Max((short)a, Convert.ToInt16(b));
                case TypeCode.UInt16:
                    return Math.Max((ushort)a, Convert.ToUInt16(b));
                case TypeCode.Int32:
                    return Math.Max((int)a, Convert.ToInt32(b));
                case TypeCode.UInt32:
                    return Math.Max((uint)a, Convert.ToUInt32(b));
                case TypeCode.Int64:
                    return Math.Max((long)a, Convert.ToInt64(b));
                case TypeCode.UInt64:
                    return Math.Max((ulong)a, Convert.ToUInt64(b));
                case TypeCode.Single:
                    return Math.Max((float)a, Convert.ToSingle(b));
                case TypeCode.Double:
                    return Math.Max((double)a, Convert.ToDouble(b));
                case TypeCode.Decimal:
                    return Math.Max((decimal)a, Convert.ToDecimal(b));
                default: throw new InvalidOperationException($"Max not implemented for parameters of {typeCodeA} and {typeCodeB}");
            }
        }

        public static object Min(object a, object b)
        {
            return Min(a, b, CultureInfo.CurrentCulture);
        }

        public static object Min(object a, object b, CultureInfo cultureInfo)
        {
            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);

            if (a == null && b == null)
            {
                return null;
            }

            if (a == null)
            {
                return b;
            }

            if (b == null)
            {
                return a;
            }

            TypeCode typeCodeA = Type.GetTypeCode(a.GetType());
            TypeCode typeCodeB = Type.GetTypeCode(b.GetType());

            switch (typeCodeA)
            {
                case TypeCode.Byte:
                    return Math.Min((Byte)a, Convert.ToByte(b));
                case TypeCode.SByte:
                    return Math.Min((SByte)a, Convert.ToSByte(b));
                case TypeCode.Int16:
                    return Math.Min((Int16)a, Convert.ToInt16(b));
                case TypeCode.UInt16:
                    return Math.Min((UInt16)a, Convert.ToUInt16(b));
                case TypeCode.Int32:
                    return Math.Min((Int32)a, Convert.ToInt32(b));
                case TypeCode.UInt32:
                    return Math.Min((UInt32)a, Convert.ToUInt32(b));
                case TypeCode.Int64:
                    return Math.Min((Int64)a, Convert.ToInt64(b));
                case TypeCode.UInt64:
                    return Math.Min((UInt64)a, Convert.ToUInt64(b));
                case TypeCode.Single:
                    return Math.Min((Single)a, Convert.ToSingle(b));
                case TypeCode.Double:
                    return Math.Min((Double)a, Convert.ToDouble(b));
                case TypeCode.Decimal:
                    return Math.Min((Decimal)a, Convert.ToDecimal(b));
                default: throw new InvalidOperationException($"Max not implemented for parameters of {typeCodeA} and {typeCodeB}");

            }
        }
    }
}
