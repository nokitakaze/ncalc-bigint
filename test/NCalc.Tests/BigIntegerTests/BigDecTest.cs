﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using InfiniteDecimal;
using Xunit;

namespace NCalc.Tests.BigIntegerTests;

public class BigDecTest
{
    #region Equality

    [Fact]
    public void TestEquality()
    {
        var rawValues = new decimal[]
        {
            0m,
            1m,
            2m,
            10m,
            1.0001m,
            1.00001m,
            1.00000000000001m,
            0.0001m,
            0.00001m,
            0.00000000000001m,
        };
        rawValues = rawValues
            .Concat(rawValues.Select(t => -t))
            .Distinct()
            .OrderBy(t => t)
            .ToArray();

        for (var i = 0; i < rawValues.Length; i++)
        {
            var operand1 = new BigDec(rawValues[i]);

            for (var j = 0; j < rawValues.Length; j++)
            {
                var operand2 = new BigDec(rawValues[j]);

                if (i == j)
                {
                    Assert.Equal(operand1, operand2);
                    Assert.Equal(operand2, operand1);
                    Assert.True(operand1 == operand2);
                    Assert.False(operand1 != operand2);
                    Assert.True(operand2 == operand1);
                    Assert.False(operand2 != operand1);
                }
                else
                {
                    Assert.NotEqual(operand1, operand2);
                    Assert.NotEqual(operand2, operand1);
                    Assert.False(operand1 == operand2);
                    Assert.True(operand1 != operand2);
                    Assert.False(operand2 == operand1);
                    Assert.True(operand2 != operand1);
                }
            }
        }
    }

    #endregion

    #region Division

    public static object[][] TestDivisionData()
    {
        var operands1 = new decimal[]
        {
            0.0001m,
            0.00001m,
            1m,
            2m,
            10m,
            120m,
            0.1m,
            14.88m,
            0.100_000_01m,
            0.100_000_001m,
            1.000_000_001m,
            1.100_000_001m,
            1.000_000_000_001m,
            1.100_000_000_001m,
        };
        operands1 = operands1
            .Concat(operands1.Select(t => -t))
            .Distinct()
            .OrderBy(Math.Abs)
            .ToArray();

        var operands2 = new decimal[]
        {
            1m,
            2m,
            3m,
            4m,
            8m,
            16m,
            5m,
            50m,
            0.1m,
            0.0001m,
            0.00001m,
        };
        operands2 = operands2
            .Concat(operands2.Select(t => -t))
            .Distinct()
            .OrderBy(Math.Abs)
            .ToArray();

        var result = operands1
            .SelectMany(operand1 => operands2
                .Select(operand2 => (operand1, operand2)))
            .Where(x =>
            {
                var exp1 = ExpUp(x.operand1);
                var exp2 = ExpUp(x.operand2);

                return (exp1 * 1_000_000m % exp2 == 0);
            })
            .Select(t => new object[]
            {
                new BigDec(t.operand1),
                new BigDec(t.operand2),
                t.operand1 / t.operand2,
            })
            .ToArray();

        return result;
    }

    private static decimal ExpUp(decimal rawValue)
    {
        while (rawValue != Math.Floor(rawValue))
        {
            rawValue *= 10m;
        }

        return rawValue;
    }

    [Theory]
    [MemberData(nameof(TestDivisionData))]
    public void TestDivision(BigDec a, BigDec b, decimal expected)
    {
        var actual = a / b;

        Assert.Equal(new BigDec(expected), actual);
        Assert.True(actual == expected);
        Assert.True(expected == actual);
        Assert.False(actual != expected);
        Assert.False(expected != actual);
    }

    #endregion

    [Fact]
    public void CheckLongTail()
    {
        var bi10 = new BigInteger(10);

        var valueField = typeof(BigDec)
            .GetField("Value", BindingFlags.NonPublic | BindingFlags.Instance);
        if (valueField is null)
        {
            throw new Exception();
        }

        var offsetField = typeof(BigDec)
            .GetField("_offset", BindingFlags.NonPublic | BindingFlags.Instance);
        if (offsetField is null)
        {
            throw new Exception();
        }

        for (var exp1 = -30; exp1 <= 30; exp1++)
        {
            BigDec operand1 = new BigDec(1);
            if (exp1 >= 0)
            {
                operand1 *= BigInteger.Pow(bi10, exp1);
            }
            else
            {
                operand1 /= BigInteger.Pow(bi10, -exp1);
                if (operand1 == BigDec.Zero)
                {
                    continue;
                }
            }

            for (var exp2 = -30; exp2 <= 30; exp2++)
            {
                BigDec operand2 = new BigDec(1);
                if (exp2 >= 0)
                {
                    operand2 *= BigInteger.Pow(bi10, exp2);
                }
                else
                {
                    operand2 /= BigInteger.Pow(bi10, -exp2);
                    if (operand2 == BigDec.Zero)
                    {
                        continue;
                    }
                }

                var result = operand1 / operand2;
                var expected = exp1 - exp2;
                if (-expected > result.MaxPrecision)
                {
                    Assert.Equal(BigDec.Zero, result);
                    continue;
                }

                var actual = 0;

                var _value = (BigInteger)valueField.GetValue(result)!;
                while (_value >= 10)
                {
                    result /= 10;
                    _value = (BigInteger)valueField.GetValue(result)!;
                    actual++;
                }

                Assert.Equal(BigInteger.One, _value);
                actual -= (int)offsetField.GetValue(result)!;

                Assert.Equal(expected, actual);
            }
        }
    }

    [Fact]
    public void CheckLongTailAdd()
    {
        var a = new BigDec(1m, 5);
        var b = new BigDec(1m, 30);
        b /= BigInteger.Pow(new BigInteger(10), 20);

        var c = a / b;
        var actual = 0;
        while (c >= new BigDec(10))
        {
            actual++;
            c /= new BigDec(10);
        }

        Assert.Equal(BigDec.One, c);
        Assert.Equal(20, actual);
    }

    #region inequality

    public static object[][] CheckInequalityData()
    {
        var rawValues = new decimal[]
        {
            0m,
            1m,
            2m,
            10m,
            1.0001m,
            1.00001m,
            1.00000000000001m,
            0.0001m,
            0.00001m,
            0.00000000000001m,
        };
        rawValues = rawValues
            .Concat(rawValues.Select(t => -t))
            .Distinct()
            .OrderBy(t => t)
            .ToArray();

        var result = new List<object[]>();
        foreach (var operand1 in rawValues)
        {
            var a = new BigDec(operand1);

            foreach (var operand2 in rawValues)
            {
                var b = new BigDec(operand2);

                int expected;
                if (operand1 == operand2)
                {
                    expected = 0;
                }
                else if (operand1 > operand2)
                {
                    expected = 1;
                }
                else
                {
                    expected = -1;
                }

                result.Add(new object[] { a, b, expected });
            }
        }

        return result.ToArray();
    }

    [Theory]
    [MemberData(nameof(CheckInequalityData))]
    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    public void CheckInequality(
        BigDec operand1,
        BigDec operand2,
        int expectedEquality
    )
    {
        if (expectedEquality == 0)
        {
            Assert.Equal(operand1, operand2);
            Assert.Equal(operand2, operand1);
            Assert.True(operand1 == operand2);
            Assert.True(operand2 == operand1);
            Assert.False(operand1 != operand2);
            Assert.False(operand2 != operand1);

            Assert.True(operand1 >= operand2);
            Assert.True(operand1 <= operand2);
            Assert.True(operand2 >= operand1);
            Assert.True(operand2 <= operand1);

            Assert.False(operand1 > operand2);
            Assert.False(operand1 < operand2);
            Assert.False(operand2 > operand1);
            Assert.False(operand2 < operand1);

            return;
        }

        if (expectedEquality == -1)
        {
            (operand2, operand1) = (operand1, operand2);
            expectedEquality = 1;
        }

        // ReSharper disable once InvertIf
        if (expectedEquality == 1)
        {
            Assert.True(operand1 > operand2);
            Assert.True(operand1 >= operand2);

            Assert.False(operand1 < operand2);
            Assert.False(operand1 <= operand2);
            return;
        }

        throw new ArgumentOutOfRangeException(nameof(expectedEquality));
    }

    #endregion

    #region max precision

    public static object[][] CheckPrecisionData()
    {
        var rawValues = new decimal[]
        {
            0m,
            1m,
            2m,
            10m,
            1.0001m,
            1.00001m,
            1.00000000000001m,
            0.0001m,
            0.00001m,
            0.00000000000001m,
        };
        rawValues = rawValues
            .Concat(rawValues.Select(t => -t))
            .Distinct()
            .OrderBy(t => t)
            .ToArray();

        return rawValues
            .SelectMany(operand1 => Enumerable
                .Range(10, 20)
                .Where(x => x % 3 == 0)
                .Where(x => x != 18)
                .SelectMany(operand1Prec => rawValues
                    .Select(operand2 => new object[]
                    {
                        new BigDec(operand1, operand1Prec),
                        new BigDec(operand2),
                        operand1Prec,
                    })
                ))
            .ToArray();
    }

    [Theory]
    [MemberData(nameof(CheckPrecisionData))]
    public void CheckPrecision(
        BigDec operand1,
        BigDec operand2,
        int _
    )
    {
        var minPrecision = Math.Max(operand1.MaxPrecision, operand2.MaxPrecision);
        var result = operand1 - operand2;
        Assert.InRange(result.MaxPrecision, minPrecision, int.MaxValue);

        result = operand1 + operand2;
        Assert.InRange(result.MaxPrecision, minPrecision, int.MaxValue);

        result = operand2 - operand1;
        Assert.InRange(result.MaxPrecision, minPrecision, int.MaxValue);

        result = -operand1;
        Assert.InRange(result.MaxPrecision, operand1.MaxPrecision, int.MaxValue);

        result = operand1 * operand2;
        Assert.InRange(result.MaxPrecision, minPrecision, int.MaxValue);

        result = operand2 * operand1;
        Assert.InRange(result.MaxPrecision, minPrecision, int.MaxValue);

        if (operand2 > BigDec.Zero)
        {
            result = operand1 / operand2;
            if ((result != BigDec.One) && (result != BigDec.Zero))
            {
                Assert.InRange(result.MaxPrecision, minPrecision, int.MaxValue);
            }
        }

        // ReSharper disable once InvertIf
        if (operand1 > BigDec.Zero)
        {
            result = operand2 / operand1;
            if ((result != BigDec.One) && (result != BigDec.Zero))
            {
                Assert.InRange(result.MaxPrecision, minPrecision, int.MaxValue);
            }
        }
    }

    #endregion

    #region exceptions

    [Fact]
    public void ParseMultiDotString()
    {
        Assert.Throws<InfiniteDecimalException>(() => { BigDec.Parse("123.321.11"); });
    }

    [Fact]
    public void DivisionByZero()
    {
        Assert.Throws<InfiniteDecimalException>(() =>
        {
            var _ = BigDec.One / BigDec.Zero;
        });
    }

    [Fact]
    public void ToChar()
    {
        Assert.Throws<InfiniteDecimalException>(() =>
        {
            var _ = BigDec.One.ToChar(null);
        });
    }

    [Fact]
    public void ToDateTime()
    {
        Assert.Throws<InfiniteDecimalException>(() =>
        {
            var _ = BigDec.One.ToDateTime(null);
        });
    }

    [Fact]
    public void NegativeOffset()
    {
        var offsetField = typeof(BigDec)
            .GetProperty("Offset", BindingFlags.NonPublic | BindingFlags.Instance);
        if (offsetField is null)
        {
            throw new Exception("'Offset' property does not exist");
        }

        var bio = new BigDec(-2);

        try
        {
            offsetField.SetValue(bio, -2);
        }
        catch (System.Reflection.TargetInvocationException e)
        {
            Assert.NotNull(e.InnerException);
            Assert.Equal(typeof(InfiniteDecimalException), e.InnerException.GetType());
        }
    }

    [Fact]
    public void UnreachableTypeForCasting()
    {
        Assert.Throws<InfiniteDecimalException>(() =>
        {
            var _ = BigDec.One.ToType(typeof(Task), null);
        });
    }

    #endregion

    #region different small tests

    [Fact]
    public void EqualWithNullViaOperator()
    {
        Assert.False(BigDec.One == null);
        Assert.True(BigDec.One != null);
        Assert.False(null == BigDec.One);
        Assert.True(null != BigDec.One);
    }

    [Fact]
    public void EqualWithNull()
    {
        Assert.False(BigDec.One.Equals(null));
    }

    [Fact]
    public void EqualWithBigInt()
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        Assert.False(BigDec.One.Equals(BigInteger.One));
    }

    [Fact]
    public void IsTypeCodeAnObject()
    {
        Assert.Equal(TypeCode.Object, BigDec.One.GetTypeCode());
    }

    #endregion

    #region conversion long/ulong

    [SuppressMessage("ReSharper", "RedundantCast")]
    public static object[][] TestConversionUlongData()
    {
        var values = new ulong[]
        {
            0,
            (ulong)sbyte.MaxValue - 1,
            (ulong)sbyte.MaxValue,
            (ulong)sbyte.MaxValue + 1,
            byte.MaxValue - (ulong)1,
            byte.MaxValue,
            byte.MaxValue + (ulong)1,
            (ulong)short.MaxValue - 1,
            (ulong)short.MaxValue,
            (ulong)short.MaxValue + 1,
            ushort.MaxValue - (ulong)1,
            ushort.MaxValue,
            ushort.MaxValue + (ulong)1,
            (ulong)int.MaxValue - 1,
            (ulong)int.MaxValue,
            (ulong)int.MaxValue + 1,
            uint.MaxValue - 1,
            uint.MaxValue,
            uint.MaxValue + (ulong)1,
            (ulong)long.MaxValue - 1,
            (ulong)long.MaxValue,
            (ulong)long.MaxValue + 1,
            ulong.MaxValue - 1,
            ulong.MaxValue,
        };

        return values.Select(t => new object[] { t }).ToArray();
    }

    [Theory]
    [MemberData(nameof(TestConversionUlongData))]
    [SuppressMessage("ReSharper", "InvertIf")]
    public void TestConversionUlong(ulong value)
    {
        var bio = new BigDec(value);
        Assert.True(value == bio);
        Assert.False(value != bio);

        var reUlong = bio.ToUInt64(null);
        Assert.Equal(value, reUlong);
        reUlong = (ulong)bio.ToType(typeof(ulong), null);
        Assert.Equal(value, reUlong);
        reUlong = (ulong)bio;
        Assert.Equal(value, reUlong);

        if (value <= uint.MaxValue)
        {
            uint valueCasted = (uint)value;
            uint reUint = bio.ToUInt32(null);
            Assert.Equal(valueCasted, reUint);
            reUint = (uint)bio.ToType(typeof(uint), null);
            Assert.Equal(valueCasted, reUint);
        }

        if (value <= ushort.MaxValue)
        {
            ushort valueCasted = (ushort)value;
            ushort reUshort = bio.ToUInt16(null);
            Assert.Equal(valueCasted, reUshort);
            reUshort = (ushort)bio.ToType(typeof(ushort), null);
            Assert.Equal(valueCasted, reUshort);
        }

        if (value <= byte.MaxValue)
        {
            byte valueCasted = (byte)value;
            byte reByte = bio.ToByte(null);
            Assert.Equal(valueCasted, reByte);
            reByte = (byte)bio.ToType(typeof(byte), null);
            Assert.Equal(valueCasted, reByte);
        }
    }

    public static object[][] TestConversionLongData()
    {
        var values = new long[]
        {
            0,
            sbyte.MaxValue - (long)1,
            sbyte.MaxValue,
            sbyte.MaxValue + (long)1,
            short.MaxValue - (long)1,
            short.MaxValue,
            short.MaxValue + (long)1,
            int.MaxValue - 1,
            int.MaxValue,
            int.MaxValue + (long)1,
            long.MaxValue - 1,
            long.MaxValue,
        };

        return values.Select(t => new object[] { t }).ToArray();
    }

    [Theory]
    [MemberData(nameof(TestConversionLongData))]
    [SuppressMessage("ReSharper", "InvertIf")]
    public void TestConversionLong(long value)
    {
        var bio = new BigDec(value);
        Assert.True(value == bio);
        Assert.False(value != bio);

        var reLong = bio.ToInt64(null);
        Assert.Equal(value, reLong);
        reLong = (long)bio.ToType(typeof(long), null);
        Assert.Equal(value, reLong);
        reLong = (long)bio;
        Assert.Equal(value, reLong);

        if (value is >= int.MinValue and <= int.MaxValue)
        {
            int valueCasted = (int)value;
            int reInt = bio.ToInt32(null);
            Assert.Equal(valueCasted, reInt);
            reInt = (int)bio.ToType(typeof(int), null);
            Assert.Equal(valueCasted, reInt);
        }

        if (value is >= short.MinValue and <= short.MaxValue)
        {
            short valueCasted = (short)value;
            short reShort = bio.ToInt16(null);
            Assert.Equal(valueCasted, reShort);
            reShort = (short)bio.ToType(typeof(short), null);
            Assert.Equal(valueCasted, reShort);
        }

        if (value is >= sbyte.MinValue and <= sbyte.MaxValue)
        {
            sbyte valueCasted = (sbyte)value;
            sbyte reSbyte = bio.ToSByte(null);
            Assert.Equal(valueCasted, reSbyte);
            reSbyte = (sbyte)bio.ToType(typeof(sbyte), null);
            Assert.Equal(valueCasted, reSbyte);
        }
    }

    [Fact]
    public void ToBooleanTest()
    {
        var values = new decimal[]
        {
            -10m,
            -1m,
            -0.5m,
            0,
            0.5m,
            1m,
            10m,
        };

        foreach (var value in values)
        {
            var u = (value > 0m);
            var bio = new BigDec(value);
            Assert.Equal(u, bio.ToBoolean(null));
        }
    }

    #endregion

    #region Convert decimal, double, float

    public static object[][] TestConvertDecimalData()
    {
        var values = new decimal[]
        {
            0m,
            2m,
            1_234_567_890m,
            1000.0001m,
            0.1m,
            0.000_000_1m,
            0.000_000_000_000_1m,
            0.100_1m,
            0.100_000_1m,
            0.100_000_001m,
            0.100_000_000_001m,
            0.100_000_000_000_001m,
            0.100_000_000_000_000_001m,
            0.100_2m,
            0.100_000_2m,
            0.100_000_002m,
            0.100_000_000_002m,
            0.100_000_000_000_002m,
            0.100_000_000_000_000_002m,
            0.3m,
            0.03m,
            0.003m,
            0.000_3m,
            0.000_03m,
            0.000_003m,
            0.000_000_3m,
            0.000_000_03m,
            0.000_000_003m,
            0.000_000_000_3m,
            1_234_567_890.000_000_000_000_1m,
            3.7m,
        };

        return values
            .Concat(values.Where(x => x > 0).Select(t => -t))
            .Select(t => new object[] { t })
            .ToArray();
    }

    public static object[][] TestConvertDoubleData()
    {
        return TestConvertDecimalData()
            .Select(t =>
            {
                var value = (decimal)t[0];
                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (Math.Abs(value).ToString(CultureInfo.InvariantCulture).Length > 18)
                {
                    return null;
                }

                return t;
            })
            .Where(x => x is not null)
            .ToArray();
    }

    [Theory]
    [MemberData(nameof(TestConvertDecimalData))]
    public void StringifyDecimal(decimal rawValue)
    {
        var valueBio1 = new BigDec(rawValue);
        var stringify = valueBio1.ToStringDouble();
        var valueBio2 = BigDec.Parse(stringify);
        Assert.Equal(valueBio1, valueBio2);
    }

    [Theory]
    [MemberData(nameof(TestConvertDecimalData))]
    public void TestConvertDecimal(decimal rawValue)
    {
        var valueBio = new BigDec(rawValue);
        var valueString = valueBio.ToString(CultureInfo.InvariantCulture);
        var decimalRevert1 = (decimal)valueBio;
        var decimalRevert2 = decimal.Parse(valueString);
        var decimalRevert3 = (decimal)valueBio.ToType(typeof(decimal), null);

        Assert.Equal(rawValue, decimalRevert1);
        Assert.Equal(rawValue, decimalRevert2);
        Assert.Equal(rawValue, decimalRevert3);

        var valueBio2 = BigDec.Parse(valueString);
        Assert.Equal(valueBio, valueBio2);

        //
        Assert.True(rawValue == valueBio);
        Assert.False(rawValue != valueBio);
        Assert.True(valueBio == rawValue);
        Assert.False(valueBio != rawValue);

        if (rawValue == (long)rawValue)
        {
            var rawValueLong = (long)rawValue;
            Assert.True(valueBio == rawValueLong);
            Assert.False(valueBio != rawValueLong);
            Assert.True(rawValueLong == valueBio);
            Assert.False(rawValueLong != valueBio);
        }
    }

    [Theory]
    [MemberData(nameof(TestConvertDoubleData))]
    public void TestConvertDouble(decimal preRawValue)
    {
        var rawValue = (double)preRawValue;

        //
        var valueBio = new BigDec(rawValue);
        var valueString = valueBio.ToString(CultureInfo.InvariantCulture);
        var decimalRevert1 = (decimal)valueBio;
        var decimalRevert2 = decimal.Parse(valueString);
        var decimalRevert1a = (double)valueBio.ToType(typeof(double), null);

        Assert.Equal(preRawValue, decimalRevert1);
        Assert.Equal(preRawValue, decimalRevert2);
        Assert.InRange(decimalRevert1a, rawValue - 0.000000001d, rawValue + 0.000000001d);

        var valueBio2 = BigDec.Parse(valueString);
        Assert.Equal(valueBio, valueBio2);
    }

    public static object[][] TestConvertFloatData()
    {
        return TestConvertDoubleData()
            .Where(items =>
            {
                var item = Math.Abs((decimal)items[0]);
                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (item is 1000.0001m)
                {
                    // Nope. IEEE-754 single precision is not enought
                    return false;
                }

                return true;
            })
            .ToArray();
    }

    [Theory]
    [MemberData(nameof(TestConvertFloatData))]
    public void TestConvertFloat(decimal preRawValue)
    {
        var rawValue = (float)preRawValue;

        //
        var valueBio = new BigDec(rawValue);
        var valueString = valueBio.ToString(CultureInfo.InvariantCulture);
        var decimalRevert1a = (float)valueBio.ToType(typeof(float), null);

        Assert.InRange(decimalRevert1a, rawValue - 0.000001f, rawValue + 0.000001f);

        var valueBio2 = BigDec.Parse(valueString);
        Assert.Equal(valueBio, valueBio2);
    }

    [Fact]
    public void TestConvertDoubleCycle()
    {
        for (var value = 0; value < 1000; value++)
        {
            for (var offset1 = 0; offset1 < 3; offset1++)
            {
                var pow1 = Enumerable
                    .Repeat(10m, offset1)
                    .Aggregate(1m, (a, b) => a * b);

                for (var offset2 = 0; offset2 < 7; offset2++)
                {
                    var pow2 = Enumerable
                        .Repeat(0.1m, offset2)
                        .Aggregate(1m, (a, b) => a * b);
                    decimal valueDecimal = (value * pow1) * pow2;
                    TestConvertDouble(valueDecimal);
                }
            }
        }
    }

    #endregion

    #region Culture ToString

    public static object[][] TestCultureData()
    {
        var russiaCulture = CultureInfo.GetCultureInfo("ru-RU");
        var usaCulture = CultureInfo.GetCultureInfo("en-US");

        var items = new (decimal value, CultureInfo cultureInfo, string expectedString)[]
        {
            (1.1m, null, "1.1"),
            (1.1m, CultureInfo.InvariantCulture, "1.1"),
            (1.1m, usaCulture, "1.1"),
            (1.1m, russiaCulture, "1,1"),
            (-1.1m, null, "-1.1"),
            (-1.1m, CultureInfo.InvariantCulture, "-1.1"),
            (-1.1m, russiaCulture, "-1,1"),
            (1.00001m, null, "1.00001"),
            (1.00001m, CultureInfo.InvariantCulture, "1.00001"),
            (1.00001m, russiaCulture, "1,00001"),
        };

        return items
            .Select(t => new object[] { t.value, t.cultureInfo, t.expectedString })
            .ToArray();
    }

    [Theory]
    [MemberData(nameof(TestCultureData))]
    public void TestCulture(decimal value, CultureInfo cultureInfo, string expectedString)
    {
        var decimalActual = value.ToString(cultureInfo);
        Assert.Equal(decimalActual, expectedString);

        //
        var bio = new BigDec(value);
        var actual1 = bio.ToString(cultureInfo);
        var actual2 = bio.ToStringDouble(cultureInfo);
        var actual3 = (string)bio.ToType(typeof(string), cultureInfo);

        Assert.Equal(expectedString, actual1);
        Assert.Equal(expectedString, actual2);
        Assert.Equal(expectedString, actual3);
    }

    #endregion

    #region BigInteger conversion

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public static object[][] TestBigIntegerConversionData()
    {
        var items = new BigInteger[]
        {
            1,
            7,
            -7,
            31,
            -31,
            100_500,
            BigInteger.Parse("1234567890123456789"),
            BigInteger.Parse("12345678901234567890123456789012345678901234567890123456789012345678901234567890"),
            -BigInteger.Parse("12345678901234567890123456789012345678901234567890123456789012345678901234567890"),
        };

        var result = new List<object[]>();
        foreach (var hiddenArg in items)
        {
            foreach (var operand2 in items)
            {
                var operand1 = hiddenArg * operand2;
                result.Add(new object[] { operand1, operand1 });
            }
        }

        return result.ToArray();
    }

    [Theory]
    [MemberData(nameof(TestBigIntegerConversionData))]
    public void TestBigIntegerConversion(
        BigInteger operand1,
        BigInteger operand2
    )
    {
        var arg1 = new BigDec(operand1);
        var arg2 = new BigDec(operand2);

        Assert.Equal(operand1, (BigInteger)arg1);
        Assert.Equal(operand2, (BigInteger)arg2);
        Assert.Equal(operand1, (BigInteger)(arg1.ToType(typeof(BigInteger), null)));
        Assert.Equal(operand2, (BigInteger)(arg2.ToType(typeof(BigInteger), null)));

        Assert.True(arg1 == operand1);
        Assert.False(arg1 != operand1);
        Assert.True(arg2 == operand2);
        Assert.False(arg2 != operand2);

        var delimBI = operand1 / operand2;

        var delimResultBI = arg1 / arg2;
        var delimResultBIO = new BigDec(delimBI);
        Assert.Equal(delimResultBI, delimResultBIO);
        Assert.True(delimResultBIO == delimBI);
        Assert.True(delimResultBI == delimBI);
        Assert.False(delimResultBI != delimBI);

        delimResultBI = operand1 / arg2;
        Assert.Equal(delimResultBI, delimResultBIO);
        Assert.True(delimResultBIO == delimBI);
        Assert.True(delimResultBI == delimBI);
        Assert.False(delimResultBI != delimBI);

        delimResultBI = arg1 / operand2;
        Assert.Equal(delimResultBI, delimResultBIO);
        Assert.True(delimResultBIO == delimBI);
        Assert.True(delimResultBI == delimBI);
        Assert.False(delimResultBI != delimBI);
    }

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public static object[][] TestBigInteger1ConversionData()
    {
        var items = new BigInteger[]
        {
            1,
            7,
            -7,
            31,
            -31,
            100_500,
            BigInteger.Parse("1234567890123456789"),
            BigInteger.Parse("12345678901234567890123456789012345678901234567890123456789012345678901234567890"),
            -BigInteger.Parse("12345678901234567890123456789012345678901234567890123456789012345678901234567890"),
        };

        return items.Select(t => new object[] { t }).ToArray();
    }

    [Theory]
    [MemberData(nameof(TestBigInteger1ConversionData))]
    public void TestBigInteger1Conversion(BigInteger operand)
    {
        var value = new BigDec(operand);
        var value01 = new BigDec(0.1m);

        Assert.True(value != 0.1m);
        Assert.False(value == 0.1m);
        Assert.True(value != value01);
        Assert.False(value == value01);
        Assert.NotEqual(value01, value);
    }

    #endregion
}