using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using NCalc.BigIntOffset;

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
            if ((a is bool) || (b is bool))
            {
                throw new InvalidOperationException(
                    $"Operator '+' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }

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
                        case BigIntegerOffset bBigIntegerOffset:
                            return aByte + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return aSbyte + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return (long)aShort + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUshort + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return aInt + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUint + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return aLong + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUlong + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return aFloat + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return aDouble + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return new BigIntegerOffset(aDecimal) + bBigIntegerOffset;
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
                        case BigIntegerOffset bBigIntegerOffset:
                            return aBigInteger + bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'BigInteger' and '{b.GetType()}'");
                    }
                case BigIntegerOffset aBigIntegerOffset:
                    switch (b)
                    {
                        case byte bByte:
                            return aBigIntegerOffset + bByte;
                        case sbyte bSbyte:
                            return aBigIntegerOffset + bSbyte;
                        case short bShort:
                            return aBigIntegerOffset + bShort;
                        case ushort bUshort:
                            return aBigIntegerOffset + bUshort;
                        case int bInt:
                            return aBigIntegerOffset + bInt;
                        case uint bUint:
                            return aBigIntegerOffset + bUint;
                        case long bLong:
                            return aBigIntegerOffset + bLong;
                        case ulong bUlong:
                            return aBigIntegerOffset + bUlong;
                        case float bFloat:
                            return aBigIntegerOffset + bFloat;
                        case double bDouble:
                            return aBigIntegerOffset + bDouble;
                        case decimal bDecimal:
                            return aBigIntegerOffset + new BigIntegerOffset(bDecimal);
                        case BigInteger bBigInteger:
                            return aBigIntegerOffset + bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aBigIntegerOffset + bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '+' can't be applied to operands of types 'BigIntegerOffset' and '{b.GetType()}'");
                    }

                default:
                    throw new InvalidOperationException(
                        $"Operator '+' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }
        }

        #endregion

        #region Subtract

        public static object Subtract(object a, object b)
        {
            return Subtract(a, b, CultureInfo.CurrentCulture);
        }

        [SuppressMessage("ReSharper", "RedundantCast")]
        public static object Subtract(object a, object b, CultureInfo cultureInfo)
        {
            if ((a is bool) || (b is bool))
            {
                throw new InvalidOperationException(
                    $"Operator '+' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }

            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);
            switch (a)
            {
                case byte aByte:
                    switch (b)
                    {
                        case byte bByte:
                            return aByte - bByte;
                        case sbyte bSbyte:
                            return aByte - bSbyte;
                        case short bShort:
                            return aByte - bShort;
                        case ushort bUshort:
                            return aByte - bUshort;
                        case int bInt:
                            return aByte - bInt;
                        case uint bUint:
                            return aByte - bUint;
                        case long bLong:
                            return aByte - bLong;
                        case ulong bUlong:
                            return aByte - bUlong;
                        case float bFloat:
                            return aByte - bFloat;
                        case double bDouble:
                            return aByte - bDouble;
                        case decimal bDecimal:
                            return (decimal)aByte - bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aByte) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aByte - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'byte' and '{b.GetType()}'");
                    }
                case sbyte aSbyte:
                    switch (b)
                    {
                        case byte bByte:
                            return aSbyte - bByte;
                        case sbyte bSbyte:
                            return aSbyte - bSbyte;
                        case short bShort:
                            return aSbyte - bShort;
                        case ushort bUshort:
                            return aSbyte - bUshort;
                        case int bInt:
                            return aSbyte - bInt;
                        case uint bUint:
                            return aSbyte - bUint;
                        case long bLong:
                            return aSbyte - bLong;
                        case ulong bUlong:
                            return aSbyte - (long)bUlong;
                        case float bFloat:
                            return aSbyte - bFloat;
                        case double bDouble:
                            return aSbyte - bDouble;
                        case decimal bDecimal:
                            return (decimal)aSbyte - bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aSbyte) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aSbyte - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'sbyte' and '{b.GetType()}'");
                    }
                case short aShort:
                    switch (b)
                    {
                        case byte bByte:
                            return aShort - bByte;
                        case sbyte bSbyte:
                            return aShort - bSbyte;
                        case short bShort:
                            return aShort - bShort;
                        case ushort bUshort:
                            return aShort - bUshort;
                        case int bInt:
                            return aShort - bInt;
                        case uint bUint:
                            return aShort - bUint;
                        case long bLong:
                            return aShort - bLong;
                        case ulong bUlong:
                            return aShort - (long)bUlong;
                        case float bFloat:
                            return aShort - bFloat;
                        case double bDouble:
                            return aShort - bDouble;
                        case decimal bDecimal:
                            return (decimal)aShort - bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aShort) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aShort - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'short' and '{b.GetType()}'");
                    }
                case ushort aUshort:
                    switch (b)
                    {
                        case byte bByte:
                            return aUshort - bByte;
                        case sbyte bSbyte:
                            return aUshort - bSbyte;
                        case short bShort:
                            return aUshort - bShort;
                        case ushort bUshort:
                            return aUshort - bUshort;
                        case int bInt:
                            return aUshort - bInt;
                        case uint bUint:
                            return aUshort - bUint;
                        case long bLong:
                            return aUshort - bLong;
                        case ulong bUlong:
                            return aUshort - bUlong;
                        case float bFloat:
                            return aUshort - bFloat;
                        case double bDouble:
                            return aUshort - bDouble;
                        case decimal bDecimal:
                            return (decimal)aUshort - bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUshort) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUshort - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'ushort' and '{b.GetType()}'");
                    }
                case int aInt:
                    switch (b)
                    {
                        case byte bByte:
                            return aInt - bByte;
                        case sbyte bSbyte:
                            return aInt - bSbyte;
                        case short bShort:
                            return aInt - bShort;
                        case ushort bUshort:
                            return aInt - bUshort;
                        case int bInt:
                            return aInt - bInt;
                        case uint bUint:
                            return aInt - bUint;
                        case long bLong:
                            return aInt - bLong;
                        case ulong bUlong:
                            return aInt - (long)bUlong;
                        case float bFloat:
                            return aInt - bFloat;
                        case double bDouble:
                            return aInt - bDouble;
                        case decimal bDecimal:
                            return (decimal)aInt - bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aInt) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aInt - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'int' and '{b.GetType()}'");
                    }
                case uint aUint:
                    switch (b)
                    {
                        case byte bByte:
                            return aUint - bByte;
                        case sbyte bSbyte:
                            return aUint - bSbyte;
                        case short bShort:
                            return aUint - bShort;
                        case ushort bUshort:
                            return aUint - bUshort;
                        case int bInt:
                            return aUint - bInt;
                        case uint bUint:
                            return aUint - bUint;
                        case long bLong:
                            return aUint - bLong;
                        case ulong bUlong:
                            return aUint - bUlong;
                        case float bFloat:
                            return aUint - bFloat;
                        case double bDouble:
                            return aUint - bDouble;
                        case decimal bDecimal:
                            return (decimal)aUint - bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUint) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUint - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'uint' and '{b.GetType()}'");
                    }
                case long aLong:
                    switch (b)
                    {
                        case byte bByte:
                            return aLong - bByte;
                        case sbyte bSbyte:
                            return aLong - bSbyte;
                        case short bShort:
                            return aLong - bShort;
                        case ushort bUshort:
                            return aLong - bUshort;
                        case int bInt:
                            return aLong - bInt;
                        case uint bUint:
                            return aLong - bUint;
                        case long bLong:
                            return aLong - bLong;
                        case ulong bUlong:
                            return aLong - (long)bUlong;
                        case float bFloat:
                            return aLong - bFloat;
                        case double bDouble:
                            return aLong - bDouble;
                        case decimal bDecimal:
                            return (decimal)aLong - bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aLong) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aLong - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'long' and '{b.GetType()}'");
                    }
                case ulong aUlong:
                    switch (b)
                    {
                        case byte bByte:
                            return aUlong - bByte;
                        case sbyte bSbyte:
                            return (long)aUlong - bSbyte;
                        case short bShort:
                            return (long)aUlong - bShort;
                        case ushort bUshort:
                            return aUlong - bUshort;
                        case int bInt:
                            return (long)aUlong - bInt;
                        case uint bUint:
                            return aUlong - bUint;
                        case long bLong:
                            return (long)aUlong - bLong;
                        case ulong bUlong:
                            return aUlong - bUlong;
                        case float bFloat:
                            return aUlong - bFloat;
                        case double bDouble:
                            return aUlong - bDouble;
                        case decimal bDecimal:
                            return (decimal)aUlong - bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUlong) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUlong - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'ulong' and '{b.GetType()}'");
                    }
                case float aFloat:
                    switch (b)
                    {
                        case byte bByte:
                            return aFloat - bByte;
                        case sbyte bSbyte:
                            return aFloat - bSbyte;
                        case short bShort:
                            return aFloat - bShort;
                        case ushort bUshort:
                            return aFloat - bUshort;
                        case int bInt:
                            return aFloat - bInt;
                        case uint bUint:
                            return aFloat - bUint;
                        case long bLong:
                            return aFloat - bLong;
                        case ulong bUlong:
                            return aFloat - bUlong;
                        case float bFloat:
                            return aFloat - bFloat;
                        case double bDouble:
                            return aFloat - bDouble;
                        case decimal bDecimal:
                            return (decimal)aFloat - bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aFloat) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aFloat - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'float' and '{b.GetType()}'");
                    }
                case double aDouble:
                    switch (b)
                    {
                        case byte bByte:
                            return aDouble - bByte;
                        case sbyte bSbyte:
                            return aDouble - bSbyte;
                        case short bShort:
                            return aDouble - bShort;
                        case ushort bUshort:
                            return aDouble - bUshort;
                        case int bInt:
                            return aDouble - bInt;
                        case uint bUint:
                            return aDouble - bUint;
                        case long bLong:
                            return aDouble - bLong;
                        case ulong bUlong:
                            return aDouble - bUlong;
                        case float bFloat:
                            return aDouble - bFloat;
                        case double bDouble:
                            return aDouble - bDouble;
                        case decimal bDecimal:
                            return (decimal)aDouble - bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aDouble) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aDouble - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'double' and '{b.GetType()}'");
                    }
                case decimal aDecimal:
                    switch (b)
                    {
                        case byte bByte:
                            return aDecimal - (decimal)bByte;
                        case sbyte bSbyte:
                            return aDecimal - (decimal)bSbyte;
                        case short bShort:
                            return aDecimal - (decimal)bShort;
                        case ushort bUshort:
                            return aDecimal - (decimal)bUshort;
                        case int bInt:
                            return aDecimal - (decimal)bInt;
                        case uint bUint:
                            return aDecimal - (decimal)bUint;
                        case long bLong:
                            return aDecimal - (decimal)bLong;
                        case ulong bUlong:
                            return aDecimal - (decimal)bUlong;
                        case float bFloat:
                            return aDecimal - (decimal)bFloat;
                        case double bDouble:
                            return aDecimal - (decimal)bDouble;
                        case decimal bDecimal:
                            return aDecimal - (decimal)bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aDecimal) - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return new BigIntegerOffset(aDecimal) - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'decimal' and '{b.GetType()}'");
                    }
                case BigInteger aBigInteger:
                    switch (b)
                    {
                        case byte bByte:
                            return aBigInteger - new BigInteger(bByte);
                        case sbyte bSbyte:
                            return aBigInteger - new BigInteger(bSbyte);
                        case short bShort:
                            return aBigInteger - new BigInteger(bShort);
                        case ushort bUshort:
                            return aBigInteger - new BigInteger(bUshort);
                        case int bInt:
                            return aBigInteger - new BigInteger(bInt);
                        case uint bUint:
                            return aBigInteger - new BigInteger(bUint);
                        case long bLong:
                            return aBigInteger - new BigInteger(bLong);
                        case ulong bUlong:
                            return aBigInteger - new BigInteger(bUlong);
                        case float bFloat:
                            return aBigInteger - new BigInteger(bFloat);
                        case double bDouble:
                            return aBigInteger - new BigInteger(bDouble);
                        case decimal bDecimal:
                            return aBigInteger - new BigInteger(bDecimal);
                        case BigInteger bBigInteger:
                            return aBigInteger - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aBigInteger - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'BigInteger' and '{b.GetType()}'");
                    }
                case BigIntegerOffset aBigIntegerOffset:
                    switch (b)
                    {
                        case byte bByte:
                            return aBigIntegerOffset - bByte;
                        case sbyte bSbyte:
                            return aBigIntegerOffset - bSbyte;
                        case short bShort:
                            return aBigIntegerOffset - bShort;
                        case ushort bUshort:
                            return aBigIntegerOffset - bUshort;
                        case int bInt:
                            return aBigIntegerOffset - bInt;
                        case uint bUint:
                            return aBigIntegerOffset - bUint;
                        case long bLong:
                            return aBigIntegerOffset - bLong;
                        case ulong bUlong:
                            return aBigIntegerOffset - bUlong;
                        case float bFloat:
                            return aBigIntegerOffset - bFloat;
                        case double bDouble:
                            return aBigIntegerOffset - bDouble;
                        case decimal bDecimal:
                            return aBigIntegerOffset - bDecimal;
                        case BigInteger bBigInteger:
                            return aBigIntegerOffset - bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aBigIntegerOffset - bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '-' can't be applied to operands of types 'BigIntegerOffset' and '{b.GetType()}'");
                    }

                default:
                    throw new InvalidOperationException(
                        $"Operator '-' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }
        }

        #endregion

        #region Multiply

        public static object Multiply(object a, object b)
        {
            return Multiply(a, b, CultureInfo.CurrentCulture);
        }

        [SuppressMessage("ReSharper", "RedundantCast")]
        public static object Multiply(object a, object b, CultureInfo cultureInfo)
        {
            if ((a is bool) || (b is bool))
            {
                throw new InvalidOperationException(
                    $"Operator '+' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }

            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);
            switch (a)
            {
                case byte aByte:
                    switch (b)
                    {
                        case byte bByte:
                            return aByte * bByte;
                        case sbyte bSbyte:
                            return aByte * bSbyte;
                        case short bShort:
                            return aByte * bShort;
                        case ushort bUshort:
                            return aByte * bUshort;
                        case int bInt:
                            return aByte * bInt;
                        case uint bUint:
                            return aByte * bUint;
                        case long bLong:
                            return aByte * bLong;
                        case ulong bUlong:
                            return aByte * bUlong;
                        case float bFloat:
                            return aByte * bFloat;
                        case double bDouble:
                            return aByte * bDouble;
                        case decimal bDecimal:
                            return (decimal)aByte * bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aByte) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aByte * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'byte' and '{b.GetType()}'");
                    }
                case sbyte aSbyte:
                    switch (b)
                    {
                        case byte bByte:
                            return aSbyte * bByte;
                        case sbyte bSbyte:
                            return aSbyte * bSbyte;
                        case short bShort:
                            return aSbyte * bShort;
                        case ushort bUshort:
                            return aSbyte * bUshort;
                        case int bInt:
                            return aSbyte * bInt;
                        case uint bUint:
                            return aSbyte * bUint;
                        case long bLong:
                            return aSbyte * bLong;
                        case ulong bUlong:
                            return aSbyte * (long)bUlong;
                        case float bFloat:
                            return aSbyte * bFloat;
                        case double bDouble:
                            return aSbyte * bDouble;
                        case decimal bDecimal:
                            return (decimal)aSbyte * bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aSbyte) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aSbyte * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'sbyte' and '{b.GetType()}'");
                    }
                case short aShort:
                    switch (b)
                    {
                        case byte bByte:
                            return aShort * bByte;
                        case sbyte bSbyte:
                            return aShort * bSbyte;
                        case short bShort:
                            return aShort * bShort;
                        case ushort bUshort:
                            return aShort * bUshort;
                        case int bInt:
                            return aShort * bInt;
                        case uint bUint:
                            return aShort * bUint;
                        case long bLong:
                            return aShort * bLong;
                        case ulong bUlong:
                            return aShort * (long)bUlong;
                        case float bFloat:
                            return aShort * bFloat;
                        case double bDouble:
                            return aShort * bDouble;
                        case decimal bDecimal:
                            return (decimal)aShort * bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aShort) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aShort * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'short' and '{b.GetType()}'");
                    }
                case ushort aUshort:
                    switch (b)
                    {
                        case byte bByte:
                            return aUshort * bByte;
                        case sbyte bSbyte:
                            return aUshort * bSbyte;
                        case short bShort:
                            return aUshort * bShort;
                        case ushort bUshort:
                            return aUshort * bUshort;
                        case int bInt:
                            return aUshort * bInt;
                        case uint bUint:
                            return aUshort * bUint;
                        case long bLong:
                            return aUshort * bLong;
                        case ulong bUlong:
                            return aUshort * bUlong;
                        case float bFloat:
                            return aUshort * bFloat;
                        case double bDouble:
                            return aUshort * bDouble;
                        case decimal bDecimal:
                            return (decimal)aUshort * bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUshort) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUshort * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'ushort' and '{b.GetType()}'");
                    }
                case int aInt:
                    switch (b)
                    {
                        case byte bByte:
                            return aInt * bByte;
                        case sbyte bSbyte:
                            return aInt * bSbyte;
                        case short bShort:
                            return aInt * bShort;
                        case ushort bUshort:
                            return aInt * bUshort;
                        case int bInt:
                            return aInt * bInt;
                        case uint bUint:
                            return aInt * bUint;
                        case long bLong:
                            return aInt * bLong;
                        case ulong bUlong:
                            return aInt * (long)bUlong;
                        case float bFloat:
                            return aInt * bFloat;
                        case double bDouble:
                            return aInt * bDouble;
                        case decimal bDecimal:
                            return (decimal)aInt * bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aInt) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aInt * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'int' and '{b.GetType()}'");
                    }
                case uint aUint:
                    switch (b)
                    {
                        case byte bByte:
                            return aUint * bByte;
                        case sbyte bSbyte:
                            return aUint * bSbyte;
                        case short bShort:
                            return aUint * bShort;
                        case ushort bUshort:
                            return aUint * bUshort;
                        case int bInt:
                            return aUint * bInt;
                        case uint bUint:
                            return aUint * bUint;
                        case long bLong:
                            return aUint * bLong;
                        case ulong bUlong:
                            return aUint * bUlong;
                        case float bFloat:
                            return aUint * bFloat;
                        case double bDouble:
                            return aUint * bDouble;
                        case decimal bDecimal:
                            return (decimal)aUint * bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUint) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUint * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'uint' and '{b.GetType()}'");
                    }
                case long aLong:
                    switch (b)
                    {
                        case byte bByte:
                            return aLong * bByte;
                        case sbyte bSbyte:
                            return aLong * bSbyte;
                        case short bShort:
                            return aLong * bShort;
                        case ushort bUshort:
                            return aLong * bUshort;
                        case int bInt:
                            return aLong * bInt;
                        case uint bUint:
                            return aLong * bUint;
                        case long bLong:
                            return aLong * bLong;
                        case ulong bUlong:
                            return aLong * (long)bUlong;
                        case float bFloat:
                            return aLong * bFloat;
                        case double bDouble:
                            return aLong * bDouble;
                        case decimal bDecimal:
                            return (decimal)aLong * bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aLong) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aLong * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'long' and '{b.GetType()}'");
                    }
                case ulong aUlong:
                    switch (b)
                    {
                        case byte bByte:
                            return aUlong * bByte;
                        case sbyte bSbyte:
                            return (long)aUlong * bSbyte;
                        case short bShort:
                            return (long)aUlong * bShort;
                        case ushort bUshort:
                            return aUlong * bUshort;
                        case int bInt:
                            return (long)aUlong * bInt;
                        case uint bUint:
                            return aUlong * bUint;
                        case long bLong:
                            return (long)aUlong * bLong;
                        case ulong bUlong:
                            return aUlong * bUlong;
                        case float bFloat:
                            return aUlong * bFloat;
                        case double bDouble:
                            return aUlong * bDouble;
                        case decimal bDecimal:
                            return (decimal)aUlong * bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUlong) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUlong * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'ulong' and '{b.GetType()}'");
                    }
                case float aFloat:
                    switch (b)
                    {
                        case byte bByte:
                            return aFloat * bByte;
                        case sbyte bSbyte:
                            return aFloat * bSbyte;
                        case short bShort:
                            return aFloat * bShort;
                        case ushort bUshort:
                            return aFloat * bUshort;
                        case int bInt:
                            return aFloat * bInt;
                        case uint bUint:
                            return aFloat * bUint;
                        case long bLong:
                            return aFloat * bLong;
                        case ulong bUlong:
                            return aFloat * bUlong;
                        case float bFloat:
                            return aFloat * bFloat;
                        case double bDouble:
                            return aFloat * bDouble;
                        case decimal bDecimal:
                            return (decimal)aFloat * bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aFloat) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aFloat * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'float' and '{b.GetType()}'");
                    }
                case double aDouble:
                    switch (b)
                    {
                        case byte bByte:
                            return aDouble * bByte;
                        case sbyte bSbyte:
                            return aDouble * bSbyte;
                        case short bShort:
                            return aDouble * bShort;
                        case ushort bUshort:
                            return aDouble * bUshort;
                        case int bInt:
                            return aDouble * bInt;
                        case uint bUint:
                            return aDouble * bUint;
                        case long bLong:
                            return aDouble * bLong;
                        case ulong bUlong:
                            return aDouble * bUlong;
                        case float bFloat:
                            return aDouble * bFloat;
                        case double bDouble:
                            return aDouble * bDouble;
                        case decimal bDecimal:
                            return (decimal)aDouble * bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aDouble) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aDouble * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'double' and '{b.GetType()}'");
                    }
                case decimal aDecimal:
                    switch (b)
                    {
                        case byte bByte:
                            return aDecimal * (decimal)bByte;
                        case sbyte bSbyte:
                            return aDecimal * (decimal)bSbyte;
                        case short bShort:
                            return aDecimal * (decimal)bShort;
                        case ushort bUshort:
                            return aDecimal * (decimal)bUshort;
                        case int bInt:
                            return aDecimal * (decimal)bInt;
                        case uint bUint:
                            return aDecimal * (decimal)bUint;
                        case long bLong:
                            return aDecimal * (decimal)bLong;
                        case ulong bUlong:
                            return aDecimal * (decimal)bUlong;
                        case float bFloat:
                            return aDecimal * (decimal)bFloat;
                        case double bDouble:
                            return aDecimal * (decimal)bDouble;
                        case decimal bDecimal:
                            return aDecimal * (decimal)bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aDecimal) * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aDecimal * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'decimal' and '{b.GetType()}'");
                    }
                case BigInteger aBigInteger:
                    switch (b)
                    {
                        case byte bByte:
                            return aBigInteger * new BigInteger(bByte);
                        case sbyte bSbyte:
                            return aBigInteger * new BigInteger(bSbyte);
                        case short bShort:
                            return aBigInteger * new BigInteger(bShort);
                        case ushort bUshort:
                            return aBigInteger * new BigInteger(bUshort);
                        case int bInt:
                            return aBigInteger * new BigInteger(bInt);
                        case uint bUint:
                            return aBigInteger * new BigInteger(bUint);
                        case long bLong:
                            return aBigInteger * new BigInteger(bLong);
                        case ulong bUlong:
                            return aBigInteger * new BigInteger(bUlong);
                        case float bFloat:
                            return aBigInteger * new BigInteger(bFloat);
                        case double bDouble:
                            return aBigInteger * new BigInteger(bDouble);
                        case decimal bDecimal:
                            return aBigInteger * new BigInteger(bDecimal);
                        case BigInteger bBigInteger:
                            return aBigInteger * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aBigInteger * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'BigInteger' and '{b.GetType()}'");
                    }
                case BigIntegerOffset aBigIntegerOffset:
                    switch (b)
                    {
                        case byte bByte:
                            return aBigIntegerOffset * bByte;
                        case sbyte bSbyte:
                            return aBigIntegerOffset * bSbyte;
                        case short bShort:
                            return aBigIntegerOffset * bShort;
                        case ushort bUshort:
                            return aBigIntegerOffset * bUshort;
                        case int bInt:
                            return aBigIntegerOffset * bInt;
                        case uint bUint:
                            return aBigIntegerOffset * bUint;
                        case long bLong:
                            return aBigIntegerOffset * bLong;
                        case ulong bUlong:
                            return aBigIntegerOffset * bUlong;
                        case float bFloat:
                            return aBigIntegerOffset * bFloat;
                        case double bDouble:
                            return aBigIntegerOffset * bDouble;
                        case decimal bDecimal:
                            return aBigIntegerOffset * new BigIntegerOffset(bDecimal);
                        case BigInteger bBigInteger:
                            return aBigIntegerOffset * bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aBigIntegerOffset * bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '*' can't be applied to operands of types 'BigIntegerOffset' and '{b.GetType()}'");
                    }

                default:
                    throw new InvalidOperationException(
                        $"Operator '*' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }
        }

        #endregion

        #region Divide

        public static object Divide(object a, object b)
        {
            return Divide(a, b, CultureInfo.CurrentCulture);
        }

        [SuppressMessage("ReSharper", "RedundantCast")]
        public static object Divide(object a, object b, CultureInfo cultureInfo)
        {
            if ((a is bool) || (b is bool))
            {
                throw new InvalidOperationException(
                    $"Operator '+' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }

            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);
            switch (a)
            {
                case byte aByte:
                    switch (b)
                    {
                        case byte bByte:
                            return aByte / bByte;
                        case sbyte bSbyte:
                            return aByte / bSbyte;
                        case short bShort:
                            return aByte / bShort;
                        case ushort bUshort:
                            return aByte / bUshort;
                        case int bInt:
                            return aByte / bInt;
                        case uint bUint:
                            return aByte / bUint;
                        case long bLong:
                            return aByte / bLong;
                        case ulong bUlong:
                            return aByte / bUlong;
                        case float bFloat:
                            return aByte / bFloat;
                        case double bDouble:
                            return aByte / bDouble;
                        case decimal bDecimal:
                            return (decimal)aByte / bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aByte) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aByte / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'byte' and '{b.GetType()}'");
                    }
                case sbyte aSbyte:
                    switch (b)
                    {
                        case byte bByte:
                            return aSbyte / bByte;
                        case sbyte bSbyte:
                            return aSbyte / bSbyte;
                        case short bShort:
                            return aSbyte / bShort;
                        case ushort bUshort:
                            return aSbyte / bUshort;
                        case int bInt:
                            return aSbyte / bInt;
                        case uint bUint:
                            return aSbyte / bUint;
                        case long bLong:
                            return aSbyte / bLong;
                        case ulong bUlong:
                            return aSbyte / (long)bUlong;
                        case float bFloat:
                            return aSbyte / bFloat;
                        case double bDouble:
                            return aSbyte / bDouble;
                        case decimal bDecimal:
                            return (decimal)aSbyte / bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aSbyte) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aSbyte / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'sbyte' and '{b.GetType()}'");
                    }
                case short aShort:
                    switch (b)
                    {
                        case byte bByte:
                            return aShort / bByte;
                        case sbyte bSbyte:
                            return aShort / bSbyte;
                        case short bShort:
                            return aShort / bShort;
                        case ushort bUshort:
                            return aShort / bUshort;
                        case int bInt:
                            return aShort / bInt;
                        case uint bUint:
                            return aShort / bUint;
                        case long bLong:
                            return aShort / bLong;
                        case ulong bUlong:
                            return aShort / (long)bUlong;
                        case float bFloat:
                            return aShort / bFloat;
                        case double bDouble:
                            return aShort / bDouble;
                        case decimal bDecimal:
                            return (decimal)aShort / bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aShort) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aShort / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'short' and '{b.GetType()}'");
                    }
                case ushort aUshort:
                    switch (b)
                    {
                        case byte bByte:
                            return aUshort / bByte;
                        case sbyte bSbyte:
                            return aUshort / bSbyte;
                        case short bShort:
                            return aUshort / bShort;
                        case ushort bUshort:
                            return aUshort / bUshort;
                        case int bInt:
                            return aUshort / bInt;
                        case uint bUint:
                            return aUshort / bUint;
                        case long bLong:
                            return aUshort / bLong;
                        case ulong bUlong:
                            return aUshort / bUlong;
                        case float bFloat:
                            return aUshort / bFloat;
                        case double bDouble:
                            return aUshort / bDouble;
                        case decimal bDecimal:
                            return (decimal)aUshort / bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUshort) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUshort / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'ushort' and '{b.GetType()}'");
                    }
                case int aInt:
                    switch (b)
                    {
                        case byte bByte:
                            return aInt / bByte;
                        case sbyte bSbyte:
                            return aInt / bSbyte;
                        case short bShort:
                            return aInt / bShort;
                        case ushort bUshort:
                            return aInt / bUshort;
                        case int bInt:
                            return aInt / bInt;
                        case uint bUint:
                            return aInt / bUint;
                        case long bLong:
                            return aInt / bLong;
                        case ulong bUlong:
                            return aInt / (long)bUlong;
                        case float bFloat:
                            return aInt / bFloat;
                        case double bDouble:
                            return aInt / bDouble;
                        case decimal bDecimal:
                            return (decimal)aInt / bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aInt) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aInt / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'int' and '{b.GetType()}'");
                    }
                case uint aUint:
                    switch (b)
                    {
                        case byte bByte:
                            return aUint / bByte;
                        case sbyte bSbyte:
                            return aUint / bSbyte;
                        case short bShort:
                            return aUint / bShort;
                        case ushort bUshort:
                            return aUint / bUshort;
                        case int bInt:
                            return aUint / bInt;
                        case uint bUint:
                            return aUint / bUint;
                        case long bLong:
                            return aUint / bLong;
                        case ulong bUlong:
                            return aUint / bUlong;
                        case float bFloat:
                            return aUint / bFloat;
                        case double bDouble:
                            return aUint / bDouble;
                        case decimal bDecimal:
                            return (decimal)aUint / bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUint) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUint / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'uint' and '{b.GetType()}'");
                    }
                case long aLong:
                    switch (b)
                    {
                        case byte bByte:
                            return aLong / bByte;
                        case sbyte bSbyte:
                            return aLong / bSbyte;
                        case short bShort:
                            return aLong / bShort;
                        case ushort bUshort:
                            return aLong / bUshort;
                        case int bInt:
                            return aLong / bInt;
                        case uint bUint:
                            return aLong / bUint;
                        case long bLong:
                            return aLong / bLong;
                        case ulong bUlong:
                            return aLong / (long)bUlong;
                        case float bFloat:
                            return aLong / bFloat;
                        case double bDouble:
                            return aLong / bDouble;
                        case decimal bDecimal:
                            return (decimal)aLong / bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aLong) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aLong / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'long' and '{b.GetType()}'");
                    }
                case ulong aUlong:
                    switch (b)
                    {
                        case byte bByte:
                            return aUlong / bByte;
                        case sbyte bSbyte:
                            return (long)aUlong / bSbyte;
                        case short bShort:
                            return (long)aUlong / bShort;
                        case ushort bUshort:
                            return aUlong / bUshort;
                        case int bInt:
                            return (long)aUlong / bInt;
                        case uint bUint:
                            return aUlong / bUint;
                        case long bLong:
                            return (long)aUlong / bLong;
                        case ulong bUlong:
                            return aUlong / bUlong;
                        case float bFloat:
                            return aUlong / bFloat;
                        case double bDouble:
                            return aUlong / bDouble;
                        case decimal bDecimal:
                            return (decimal)aUlong / bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUlong) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUlong / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'ulong' and '{b.GetType()}'");
                    }
                case float aFloat:
                    switch (b)
                    {
                        case byte bByte:
                            return aFloat / bByte;
                        case sbyte bSbyte:
                            return aFloat / bSbyte;
                        case short bShort:
                            return aFloat / bShort;
                        case ushort bUshort:
                            return aFloat / bUshort;
                        case int bInt:
                            return aFloat / bInt;
                        case uint bUint:
                            return aFloat / bUint;
                        case long bLong:
                            return aFloat / bLong;
                        case ulong bUlong:
                            return aFloat / bUlong;
                        case float bFloat:
                            return aFloat / bFloat;
                        case double bDouble:
                            return aFloat / bDouble;
                        case decimal bDecimal:
                            return (decimal)aFloat / bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aFloat) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aFloat / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'float' and '{b.GetType()}'");
                    }
                case double aDouble:
                    switch (b)
                    {
                        case byte bByte:
                            return aDouble / bByte;
                        case sbyte bSbyte:
                            return aDouble / bSbyte;
                        case short bShort:
                            return aDouble / bShort;
                        case ushort bUshort:
                            return aDouble / bUshort;
                        case int bInt:
                            return aDouble / bInt;
                        case uint bUint:
                            return aDouble / bUint;
                        case long bLong:
                            return aDouble / bLong;
                        case ulong bUlong:
                            return aDouble / bUlong;
                        case float bFloat:
                            return aDouble / bFloat;
                        case double bDouble:
                            return aDouble / bDouble;
                        case decimal bDecimal:
                            return (decimal)aDouble / bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aDouble) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aDouble / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'double' and '{b.GetType()}'");
                    }
                case decimal aDecimal:
                    switch (b)
                    {
                        case byte bByte:
                            return aDecimal / (decimal)bByte;
                        case sbyte bSbyte:
                            return aDecimal / (decimal)bSbyte;
                        case short bShort:
                            return aDecimal / (decimal)bShort;
                        case ushort bUshort:
                            return aDecimal / (decimal)bUshort;
                        case int bInt:
                            return aDecimal / (decimal)bInt;
                        case uint bUint:
                            return aDecimal / (decimal)bUint;
                        case long bLong:
                            return aDecimal / (decimal)bLong;
                        case ulong bUlong:
                            return aDecimal / (decimal)bUlong;
                        case float bFloat:
                            return aDecimal / (decimal)bFloat;
                        case double bDouble:
                            return aDecimal / (decimal)bDouble;
                        case decimal bDecimal:
                            return aDecimal / (decimal)bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aDecimal) / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aDecimal / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'decimal' and '{b.GetType()}'");
                    }
                case BigInteger aBigInteger:
                    switch (b)
                    {
                        case byte bByte:
                            return aBigInteger / new BigInteger(bByte);
                        case sbyte bSbyte:
                            return aBigInteger / new BigInteger(bSbyte);
                        case short bShort:
                            return aBigInteger / new BigInteger(bShort);
                        case ushort bUshort:
                            return aBigInteger / new BigInteger(bUshort);
                        case int bInt:
                            return aBigInteger / new BigInteger(bInt);
                        case uint bUint:
                            return aBigInteger / new BigInteger(bUint);
                        case long bLong:
                            return aBigInteger / new BigInteger(bLong);
                        case ulong bUlong:
                            return aBigInteger / new BigInteger(bUlong);
                        case float bFloat:
                            return aBigInteger / new BigInteger(bFloat);
                        case double bDouble:
                            return aBigInteger / new BigInteger(bDouble);
                        case decimal bDecimal:
                            return aBigInteger / new BigInteger(bDecimal);
                        case BigInteger bBigInteger:
                            return aBigInteger / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aBigInteger / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'BigInteger' and '{b.GetType()}'");
                    }
                case BigIntegerOffset aBigIntegerOffset:
                    switch (b)
                    {
                        case byte bByte:
                            return aBigIntegerOffset / bByte;
                        case sbyte bSbyte:
                            return aBigIntegerOffset / bSbyte;
                        case short bShort:
                            return aBigIntegerOffset / bShort;
                        case ushort bUshort:
                            return aBigIntegerOffset / bUshort;
                        case int bInt:
                            return aBigIntegerOffset / bInt;
                        case uint bUint:
                            return aBigIntegerOffset / bUint;
                        case long bLong:
                            return aBigIntegerOffset / bLong;
                        case ulong bUlong:
                            return aBigIntegerOffset / bUlong;
                        case float bFloat:
                            return aBigIntegerOffset / bFloat;
                        case double bDouble:
                            return aBigIntegerOffset / bDouble;
                        case decimal bDecimal:
                            return aBigIntegerOffset / bDecimal;
                        case BigInteger bBigInteger:
                            return aBigIntegerOffset / bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aBigIntegerOffset / bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '/' can't be applied to operands of types 'BigIntegerOffset' and '{b.GetType()}'");
                    }

                default:
                    throw new InvalidOperationException(
                        $"Operator '/' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }
        }

        #endregion

        #region Modulo

        public static object Modulo(object a, object b)
        {
            return Modulo(a, b, CultureInfo.CurrentCulture);
        }

        [SuppressMessage("ReSharper", "RedundantCast")]
        public static object Modulo(object a, object b, CultureInfo cultureInfo)
        {
            if ((a is bool) || (b is bool))
            {
                throw new InvalidOperationException(
                    $"Operator '+' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }

            a = ConvertIfString(a, cultureInfo);
            b = ConvertIfString(b, cultureInfo);
            switch (a)
            {
                case byte aByte:
                    switch (b)
                    {
                        case byte bByte:
                            return aByte % bByte;
                        case sbyte bSbyte:
                            return aByte % bSbyte;
                        case short bShort:
                            return aByte % bShort;
                        case ushort bUshort:
                            return aByte % bUshort;
                        case int bInt:
                            return aByte % bInt;
                        case uint bUint:
                            return aByte % bUint;
                        case long bLong:
                            return aByte % bLong;
                        case ulong bUlong:
                            return aByte % bUlong;
                        case float bFloat:
                            return aByte % bFloat;
                        case double bDouble:
                            return aByte % bDouble;
                        case decimal bDecimal:
                            return (decimal)aByte % bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aByte) % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aByte % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'byte' and '{b.GetType()}'");
                    }
                case sbyte aSbyte:
                    switch (b)
                    {
                        case byte bByte:
                            return aSbyte % bByte;
                        case sbyte bSbyte:
                            return aSbyte % bSbyte;
                        case short bShort:
                            return aSbyte % bShort;
                        case ushort bUshort:
                            return aSbyte % bUshort;
                        case int bInt:
                            return aSbyte % bInt;
                        case uint bUint:
                            return aSbyte % bUint;
                        case long bLong:
                            return aSbyte % bLong;
                        case ulong bUlong:
                            return aSbyte % (long)bUlong;
                        case float bFloat:
                            return aSbyte % bFloat;
                        case double bDouble:
                            return aSbyte % bDouble;
                        case decimal bDecimal:
                            return (decimal)aSbyte % bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aSbyte) % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aSbyte % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'sbyte' and '{b.GetType()}'");
                    }
                case short aShort:
                    switch (b)
                    {
                        case byte bByte:
                            return aShort % bByte;
                        case sbyte bSbyte:
                            return aShort % bSbyte;
                        case short bShort:
                            return aShort % bShort;
                        case ushort bUshort:
                            return aShort % bUshort;
                        case int bInt:
                            return aShort % bInt;
                        case uint bUint:
                            return aShort % bUint;
                        case long bLong:
                            return aShort % bLong;
                        case ulong bUlong:
                            return aShort % (long)bUlong;
                        case float bFloat:
                            return aShort % bFloat;
                        case double bDouble:
                            return aShort % bDouble;
                        case decimal bDecimal:
                            return (decimal)aShort % bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aShort) % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aShort % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'short' and '{b.GetType()}'");
                    }
                case ushort aUshort:
                    switch (b)
                    {
                        case byte bByte:
                            return aUshort % bByte;
                        case sbyte bSbyte:
                            return aUshort % bSbyte;
                        case short bShort:
                            return aUshort % bShort;
                        case ushort bUshort:
                            return aUshort % bUshort;
                        case int bInt:
                            return aUshort % bInt;
                        case uint bUint:
                            return aUshort % bUint;
                        case long bLong:
                            return aUshort % bLong;
                        case ulong bUlong:
                            return aUshort % bUlong;
                        case float bFloat:
                            return aUshort % bFloat;
                        case double bDouble:
                            return aUshort % bDouble;
                        case decimal bDecimal:
                            return (decimal)aUshort % bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUshort) % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUshort % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'ushort' and '{b.GetType()}'");
                    }
                case int aInt:
                    switch (b)
                    {
                        case byte bByte:
                            return aInt % bByte;
                        case sbyte bSbyte:
                            return aInt % bSbyte;
                        case short bShort:
                            return aInt % bShort;
                        case ushort bUshort:
                            return aInt % bUshort;
                        case int bInt:
                            return aInt % bInt;
                        case uint bUint:
                            return aInt % bUint;
                        case long bLong:
                            return aInt % bLong;
                        case ulong bUlong:
                            return aInt % (long)bUlong;
                        case float bFloat:
                            return aInt % bFloat;
                        case double bDouble:
                            return aInt % bDouble;
                        case decimal bDecimal:
                            return (decimal)aInt % bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aInt) % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aInt % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'int' and '{b.GetType()}'");
                    }
                case uint aUint:
                    switch (b)
                    {
                        case byte bByte:
                            return aUint % bByte;
                        case sbyte bSbyte:
                            return aUint % bSbyte;
                        case short bShort:
                            return aUint % bShort;
                        case ushort bUshort:
                            return aUint % bUshort;
                        case int bInt:
                            return aUint % bInt;
                        case uint bUint:
                            return aUint % bUint;
                        case long bLong:
                            return aUint % bLong;
                        case ulong bUlong:
                            return aUint % bUlong;
                        case float bFloat:
                            return aUint % bFloat;
                        case double bDouble:
                            return aUint % bDouble;
                        case decimal bDecimal:
                            return (decimal)aUint % bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUint) % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUint % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'uint' and '{b.GetType()}'");
                    }
                case long aLong:
                    switch (b)
                    {
                        case byte bByte:
                            return aLong % bByte;
                        case sbyte bSbyte:
                            return aLong % bSbyte;
                        case short bShort:
                            return aLong % bShort;
                        case ushort bUshort:
                            return aLong % bUshort;
                        case int bInt:
                            return aLong % bInt;
                        case uint bUint:
                            return aLong % bUint;
                        case long bLong:
                            return aLong % bLong;
                        case ulong bUlong:
                            return aLong % (long)bUlong;
                        case float bFloat:
                            return aLong % bFloat;
                        case double bDouble:
                            return aLong % bDouble;
                        case decimal bDecimal:
                            return (decimal)aLong % bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aLong) % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aLong % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'long' and '{b.GetType()}'");
                    }
                case ulong aUlong:
                    switch (b)
                    {
                        case byte bByte:
                            return aUlong % bByte;
                        case sbyte bSbyte:
                            return (long)aUlong % bSbyte;
                        case short bShort:
                            return (long)aUlong % bShort;
                        case ushort bUshort:
                            return aUlong % bUshort;
                        case int bInt:
                            return (long)aUlong % bInt;
                        case uint bUint:
                            return aUlong % bUint;
                        case long bLong:
                            return (long)aUlong % bLong;
                        case ulong bUlong:
                            return aUlong % bUlong;
                        case float bFloat:
                            return aUlong % bFloat;
                        case double bDouble:
                            return aUlong % bDouble;
                        case decimal bDecimal:
                            return (decimal)aUlong % bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aUlong) % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aUlong % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'ulong' and '{b.GetType()}'");
                    }
                case float aFloat:
                    switch (b)
                    {
                        case byte bByte:
                            return aFloat % bByte;
                        case sbyte bSbyte:
                            return aFloat % bSbyte;
                        case short bShort:
                            return aFloat % bShort;
                        case ushort bUshort:
                            return aFloat % bUshort;
                        case int bInt:
                            return aFloat % bInt;
                        case uint bUint:
                            return aFloat % bUint;
                        case long bLong:
                            return aFloat % bLong;
                        case ulong bUlong:
                            return aFloat % bUlong;
                        case float bFloat:
                            return aFloat % bFloat;
                        case double bDouble:
                            return aFloat % bDouble;
                        case decimal bDecimal:
                            return (decimal)aFloat % bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aFloat) % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aFloat % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'float' and '{b.GetType()}'");
                    }
                case double aDouble:
                    switch (b)
                    {
                        case byte bByte:
                            return aDouble % bByte;
                        case sbyte bSbyte:
                            return aDouble % bSbyte;
                        case short bShort:
                            return aDouble % bShort;
                        case ushort bUshort:
                            return aDouble % bUshort;
                        case int bInt:
                            return aDouble % bInt;
                        case uint bUint:
                            return aDouble % bUint;
                        case long bLong:
                            return aDouble % bLong;
                        case ulong bUlong:
                            return aDouble % bUlong;
                        case float bFloat:
                            return aDouble % bFloat;
                        case double bDouble:
                            return aDouble % bDouble;
                        case decimal bDecimal:
                            return (decimal)aDouble % bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aDouble) % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aDouble % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'double' and '{b.GetType()}'");
                    }
                case decimal aDecimal:
                    switch (b)
                    {
                        case byte bByte:
                            return aDecimal % (decimal)bByte;
                        case sbyte bSbyte:
                            return aDecimal % (decimal)bSbyte;
                        case short bShort:
                            return aDecimal % (decimal)bShort;
                        case ushort bUshort:
                            return aDecimal % (decimal)bUshort;
                        case int bInt:
                            return aDecimal % (decimal)bInt;
                        case uint bUint:
                            return aDecimal % (decimal)bUint;
                        case long bLong:
                            return aDecimal % (decimal)bLong;
                        case ulong bUlong:
                            return aDecimal % (decimal)bUlong;
                        case float bFloat:
                            return aDecimal % (decimal)bFloat;
                        case double bDouble:
                            return aDecimal % (decimal)bDouble;
                        case decimal bDecimal:
                            return aDecimal % (decimal)bDecimal;
                        case BigInteger bBigInteger:
                            return new BigInteger(aDecimal) % bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aDecimal % (decimal)bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'decimal' and '{b.GetType()}'");
                    }
                case BigInteger aBigInteger:
                    switch (b)
                    {
                        case byte bByte:
                            return aBigInteger % new BigInteger(bByte);
                        case sbyte bSbyte:
                            return aBigInteger % new BigInteger(bSbyte);
                        case short bShort:
                            return aBigInteger % new BigInteger(bShort);
                        case ushort bUshort:
                            return aBigInteger % new BigInteger(bUshort);
                        case int bInt:
                            return aBigInteger % new BigInteger(bInt);
                        case uint bUint:
                            return aBigInteger % new BigInteger(bUint);
                        case long bLong:
                            return aBigInteger % new BigInteger(bLong);
                        case ulong bUlong:
                            return aBigInteger % new BigInteger(bUlong);
                        case float bFloat:
                            return aBigInteger % new BigInteger(bFloat);
                        case double bDouble:
                            return aBigInteger % new BigInteger(bDouble);
                        case decimal bDecimal:
                            return aBigInteger % new BigInteger(bDecimal);
                        case BigInteger bBigInteger:
                            return aBigInteger % bBigInteger;
                        /*
                        case BigIntegerOffset bBigIntegerOffset:
                            return aBigInteger % bBigIntegerOffset;
                        */
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'BigInteger' and '{b.GetType()}'");
                    }
                    /*
                case BigIntegerOffset aBigIntegerOffset:
                    switch (b)
                    {
                        case byte bByte:
                            return aBigIntegerOffset % bByte;
                        case sbyte bSbyte:
                            return aBigIntegerOffset % bSbyte;
                        case short bShort:
                            return aBigIntegerOffset % bShort;
                        case ushort bUshort:
                            return aBigIntegerOffset % bUshort;
                        case int bInt:
                            return aBigIntegerOffset % bInt;
                        case uint bUint:
                            return aBigIntegerOffset % bUint;
                        case long bLong:
                            return aBigIntegerOffset % bLong;
                        case ulong bUlong:
                            return aBigIntegerOffset % bUlong;
                        case float bFloat:
                            return aBigIntegerOffset % bFloat;
                        case double bDouble:
                            return aBigIntegerOffset % bDouble;
                        case decimal bDecimal:
                            return (decimal)aBigIntegerOffset % bDecimal;
                        case BigInteger bBigInteger:
                            return aBigIntegerOffset % bBigInteger;
                        case BigIntegerOffset bBigIntegerOffset:
                            return aBigIntegerOffset % bBigIntegerOffset;
                        default:
                            throw new InvalidOperationException(
                                $"Operator '%' can't be applied to operands of types 'BigIntegerOffset' and '{b.GetType()}'");
                    }
                    */

                default:
                    throw new InvalidOperationException(
                        $"Operator '%' can't be applied to operands of types '{a.GetType()}' and '{b.GetType()}'");
            }
        }

        #endregion

        #region Max

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
                default:
                    throw new InvalidOperationException(
                        $"Max not implemented for parameters of {typeCodeA} and {typeCodeB}");
            }
        }

        #endregion

        #region Min

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
                default:
                    throw new InvalidOperationException(
                        $"Max not implemented for parameters of {typeCodeA} and {typeCodeB}");
            }
        }

        #endregion
    }
}