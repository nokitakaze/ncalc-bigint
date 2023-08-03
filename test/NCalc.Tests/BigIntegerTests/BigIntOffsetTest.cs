using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection;
using NCalc.BigIntOffset;
using Xunit;

namespace NCalc.Tests.BigIntegerTests;

public class BigIntOffsetTest
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
            var operand1 = new BigIntegerOffset(rawValues[i]);

            for (var j = 0; j < rawValues.Length; j++)
            {
                var operand2 = new BigIntegerOffset(rawValues[j]);

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
                new BigIntegerOffset(t.operand1),
                new BigIntegerOffset(t.operand2),
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
    public void TestDivision(BigIntegerOffset a, BigIntegerOffset b, decimal expected)
    {
        var actual = a / b;

        Assert.Equal(new BigIntegerOffset(expected), actual);
        Assert.True(actual == expected);
    }

    #endregion

    [Fact]
    public void CheckLongTail()
    {
        var bi10 = new BigInteger(10);

        var valueField = typeof(BigIntegerOffset)
            .GetField("Value", BindingFlags.NonPublic | BindingFlags.Instance);
        if (valueField is null)
        {
            throw new Exception();
        }

        var offsetField = typeof(BigIntegerOffset)
            .GetField("_offset", BindingFlags.NonPublic | BindingFlags.Instance);
        if (offsetField is null)
        {
            throw new Exception();
        }

        for (var exp1 = -30; exp1 <= 30; exp1++)
        {
            BigIntegerOffset operand1 = new BigIntegerOffset(1);
            if (exp1 >= 0)
            {
                operand1 *= BigInteger.Pow(bi10, exp1);
            }
            else
            {
                operand1 /= BigInteger.Pow(bi10, -exp1);
                if (operand1 == BigIntegerOffset.Zero)
                {
                    continue;
                }
            }

            for (var exp2 = -30; exp2 <= 30; exp2++)
            {
                BigIntegerOffset operand2 = new BigIntegerOffset(1);
                if (exp2 >= 0)
                {
                    operand2 *= BigInteger.Pow(bi10, exp2);
                }
                else
                {
                    operand2 /= BigInteger.Pow(bi10, -exp2);
                    if (operand2 == BigIntegerOffset.Zero)
                    {
                        continue;
                    }
                }

                var result = operand1 / operand2;
                var expected = exp1 - exp2;
                if (-expected > result.MaxPrecision)
                {
                    Assert.Equal(BigIntegerOffset.Zero, result);
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
        var a = new BigIntegerOffset(1m, 5);
        var b = new BigIntegerOffset(1m, 30);
        b /= BigInteger.Pow(new BigInteger(10), 20);

        var c = a / b;
        var actual = 0;
        while (c >= new BigIntegerOffset(10))
        {
            actual++;
            c /= new BigIntegerOffset(10);
        }

        Assert.Equal(BigIntegerOffset.One, c);
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
            var a = new BigIntegerOffset(operand1);

            foreach (var operand2 in rawValues)
            {
                var b = new BigIntegerOffset(operand2);

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
        BigIntegerOffset operand1,
        BigIntegerOffset operand2,
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
                        new BigIntegerOffset(operand1, operand1Prec),
                        new BigIntegerOffset(operand2),
                        operand1Prec,
                    })
                ))
            .ToArray();
    }

    [Theory]
    [MemberData(nameof(CheckPrecisionData))]
    public void CheckPrecision(
        BigIntegerOffset operand1,
        BigIntegerOffset operand2,
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

        if (operand2 > BigIntegerOffset.Zero)
        {
            result = operand1 / operand2;
            if ((result != BigIntegerOffset.One) && (result != BigIntegerOffset.Zero))
            {
                Assert.InRange(result.MaxPrecision, minPrecision, int.MaxValue);
            }
        }

        // ReSharper disable once InvertIf
        if (operand1 > BigIntegerOffset.Zero)
        {
            result = operand2 / operand1;
            if ((result != BigIntegerOffset.One) && (result != BigIntegerOffset.Zero))
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
        Assert.Throws<BigIntegerOffsetException>(() => { BigIntegerOffset.Parse("123.321.11"); });
    }

    [Fact]
    public void DivisionByZero()
    {
        Assert.Throws<BigIntegerOffsetException>(() =>
        {
            var _ = BigIntegerOffset.One / BigIntegerOffset.Zero;
        });
    }

    #endregion

    #region different small tests

    [Fact]
    public void EqualWithNullViaOperator()
    {
        Assert.False(BigIntegerOffset.One == null);
        Assert.True(BigIntegerOffset.One != null);
        Assert.False(null == BigIntegerOffset.One);
        Assert.True(null != BigIntegerOffset.One);
    }

    [Fact]
    public void EqualWithNull()
    {
        Assert.False(BigIntegerOffset.One.Equals(null));
    }

    [Fact]
    public void EqualWithBigInt()
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        Assert.False(BigIntegerOffset.One.Equals(BigInteger.One));
    }

    #endregion

    #region conversion

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
        var bio = new BigIntegerOffset(value);

        var reUlong = bio.ToUInt64(null);
        Assert.Equal(value, reUlong);
        reUlong = (ulong)bio;
        Assert.Equal(value, reUlong);

        if (value <= uint.MaxValue)
        {
            uint valueCasted = (uint)value;
            uint reUint = bio.ToUInt32(null);
            Assert.Equal(valueCasted, reUint);
        }

        if (value <= ushort.MaxValue)
        {
            ushort valueCasted = (ushort)value;
            ushort reUshort = bio.ToUInt16(null);
            Assert.Equal(valueCasted, reUshort);
        }

        if (value <= byte.MaxValue)
        {
            byte valueCasted = (byte)value;
            byte reUshort = bio.ToByte(null);
            Assert.Equal(valueCasted, reUshort);
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
        var bio = new BigIntegerOffset(value);

        var reUlong = bio.ToInt64(null);
        Assert.Equal(value, reUlong);
        reUlong = (long)bio;
        Assert.Equal(value, reUlong);

        if (value is >= int.MinValue and <= int.MaxValue)
        {
            int valueCasted = (int)value;
            int reUint = bio.ToInt32(null);
            Assert.Equal(valueCasted, reUint);
        }

        if (value is >= short.MinValue and <= short.MaxValue)
        {
            short valueCasted = (short)value;
            short reUshort = bio.ToInt16(null);
            Assert.Equal(valueCasted, reUshort);
        }

        if (value is >= sbyte.MinValue and <= sbyte.MaxValue)
        {
            sbyte valueCasted = (sbyte)value;
            sbyte reUshort = bio.ToSByte(null);
            Assert.Equal(valueCasted, reUshort);
        }
    }

    #endregion
}