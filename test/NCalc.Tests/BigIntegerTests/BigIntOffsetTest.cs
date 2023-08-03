using System;
using System.Linq;
using System.Numerics;
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
        var a = new BigIntegerOffset(1m, 5);

        var b = new BigIntegerOffset(1m, 30);
        b /= BigInteger.Pow(new BigInteger(10), 20);

        var c = a / b;
    }
}