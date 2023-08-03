using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using NCalc.BigIntOffset;
using Xunit;

namespace NCalc.Tests.BigIntegerTests;

[SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Local")]
public class CompositeTests
{
    #region MathOperations

    public static (object a, object b, string expr, object expected)[] MathOperationsData_Division()
    {
        var rawInput = new (string a, string b)[]
        {
            ("100", "10"),
            ("5", "10"),
            ("1488", "31"),
            ("1488", "48"),
            ("1.1", "5"),
            ("1.1", "5.0"),
            ("1.11", "3"),
            ("1.11", "37"),
            ("1.11", "3.7"),
            ("1.11", "0.0000000000037"),
        };

        var types = new[] { "int", "uint", "long", "ulong", "decimal", "double", "BigInteger", "BigIntegerOffset", };

        var divisionTests = rawInput
            .SelectMany(item =>
            {
                var decA = decimal.Parse(item.a);
                var decB = decimal.Parse(item.b);

                return types.SelectMany(typeA =>
                {
                    try
                    {
                        object varA = ConvertType(item.a, typeA);
                        if (varA is null)
                        {
                            throw new Exception($"Can not convert '{item.a}' to '{typeA}'");
                        }

                        if (!IsThisDecimalRounded(decA) && IsTypeInteger(varA))
                        {
                            return default;
                        }

                        return types.Select(typeB =>
                        {
                            try
                            {
                                object varB = ConvertType(item.b, typeB);
                                if (varB is null)
                                {
                                    throw new Exception($"Can not convert '{item.b}' to '{typeB}'");
                                }

                                if (!IsThisDecimalRounded(decB) && IsTypeInteger(varB))
                                {
                                    return default;
                                }

                                return (
                                    a: varA,
                                    b: varB,
                                    expr: "x / y",
                                    expected: (object)(new BigIntegerOffset(decA / decB))
                                );
                            }
                            catch (Exception)
                            {
                                return default;
                            }
                        });
                    }
                    catch (Exception)
                    {
                        return Array.Empty<(object a, object b, string expr, object expected)>();
                    }
                });
            })
            .Where(x => x.a is not null)
            .ToArray();

        return divisionTests;
    }

    [SuppressMessage("ReSharper", "RedundantIfElseBlock")]
    public static object[][] MathOperationsData()
    {
        var rawInput = new (string a, string b)[]
        {
            ("14", "88"),
            ("123", "654"),
            ("123654", "987654"),
            ("1.1", "5"),
            ("1.1", "5.0"),
            ("1.1", "5.1"),
            ("100500", "5.0"),
            ("0.1", "0.0654"),
        };

        var types = new[] { "int", "uint", "long", "ulong", "decimal", "double", "BigInteger", "BigIntegerOffset", };
        var emptyResponse = Array.Empty<(object a, object b, string expr, object expected)>();

        var firstTests = rawInput
            .SelectMany(item =>
            {
                var decA = decimal.Parse(item.a);
                var decB = decimal.Parse(item.b);

                return types.SelectMany(typeA =>
                {
                    try
                    {
                        object varA = ConvertType(item.a, typeA);

                        return types.SelectMany(typeB =>
                        {
                            try
                            {
                                object varB = ConvertType(item.b, typeB);

                                return GetMutations(decA, decB, varA, varB);
                            }
                            catch (Exception)
                            {
                                return emptyResponse;
                            }
                        });
                    }
                    catch (Exception)
                    {
                        return emptyResponse;
                    }
                });
            })
            .ToArray();

        return firstTests
            .Take(0) // todo delme
            .Concat(MathOperationsData_Division())
            .Select(t => new object[] { t.a, t.b, t.expr, t.expected })
            .Where(x => (x[0] is BigInteger) || (x[0] is BigIntOffset.BigIntegerOffset) ||
                        (x[1] is BigInteger) || (x[1] is BigIntOffset.BigIntegerOffset))
            .Where(x =>
            {
                if ((x[0] is BigIntegerOffset) || (x[1] is BigIntegerOffset))
                {
                    return true;
                }

                var vars = new[] { x[0], x[1] }
                    .Select(t => (value: t, name: t.GetType().Name.ToLowerInvariant()))
                    .OrderBy(t => t.name)
                    .Select(t => t.value)
                    .ToArray();

                if (vars[0] is BigInteger)
                {
                    return vars[1] switch
                    {
                        decimal var1Decimal => IsThisDecimalRounded(var1Decimal),
                        double var1Double => IsThisDoubleRounded(var1Double),
                        _ => true,
                    };
                }
                else
                {
                    return true;
                }
            })
            .Select(t => new object[]
            {
                new ParamWrapper(t[0]),
                new ParamWrapper(t[1]),
                t[2],
                t[3],
            })
            .ToArray();
    }

    private static object ConvertType(string value, string type)
    {
        return type switch
        {
            "int" => int.Parse(value),
            "uint" => uint.Parse(value),
            "long" => long.Parse(value),
            "ulong" => ulong.Parse(value),
            "double" => double.Parse(value),
            "decimal" => decimal.Parse(value),
            "BigInteger" => BigInteger.Parse(value),
            "BigIntegerOffset" => NCalc.BigIntOffset.BigIntegerOffset.Parse(value),
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }

    [SuppressMessage("ReSharper", "EmptyGeneralCatchClause")]
    private static (object a, object b, string expr, object expected)[] GetMutations(
        decimal decA,
        decimal decB,
        object varA,
        object varB
    )
    {
        var emptyResponse = Array.Empty<(object a, object b, string expr, object expected)>();

        if (!IsThisDecimalRounded(decA) && IsTypeInteger(varA))
        {
            return emptyResponse;
        }

        if (!IsThisDecimalRounded(decB) && IsTypeInteger(varB))
        {
            return emptyResponse;
        }

        var result = new List<(object a, object b, string expr, object expected)>();
        try
        {
            var expected = decA + decB;
            result.Add((varA, varB, "x + y", expected));
            result.Add((varA, varB, "x - (-y)", expected));
            result.Add((varA, varB, "y + x", expected));
            result.Add((varA, varB, "-(-x - y)", expected));
        }
        catch (Exception)
        {
        }

        try
        {
            var expected = decA - decB;

            result.Add((varA, varB, "x - y", expected));
            result.Add((varA, varB, "x +(-y)", expected));
        }
        catch (Exception)
        {
        }

        try
        {
            var expected = decA * decB;

            result.Add((varA, varB, "x * y", expected));
            result.Add((varA, varB, "y * x", expected));
        }
        catch (Exception)
        {
        }

        return result.ToArray();
    }

    [Theory]
    [MemberData(nameof(MathOperationsData))]
    public void MathOperations(ParamWrapper x, ParamWrapper y, string expr, object expected)
    {
        var e = new NCalc.Expression(expr);
        e.EvaluateParameter += delegate(string name, ParameterArgs args)
        {
            args.Result = name switch
            {
                "x" => x.value,
                "y" => y.value,
                _ => args.Result
            };
        };

        object actual = e.Evaluate();
        // ReSharper disable once ConvertIfStatementToSwitchStatement
        if (actual is BigIntOffset.BigIntegerOffset actualBIO)
        {
            switch (expected)
            {
                case decimal expDecimal:
                    Assert.Equal(new BigIntOffset.BigIntegerOffset(expDecimal), actualBIO);
                    break;
                case BigInteger expBI:
                    Assert.Equal(new BigIntOffset.BigIntegerOffset(expBI), actualBIO);
                    break;
                case BigIntOffset.BigIntegerOffset expBIO:
                    Assert.Equal(expBIO, actualBIO);
                    break;
                default:
                    throw new NotImplementedException("Tests: MathOperations");
            }
        }
        else if (actual is BigInteger actualBI)
        {
            switch (expected)
            {
                case decimal expDecimal:
                    Assert.Equal(expDecimal, (decimal)actualBI);
                    break;
                case BigInteger expBI:
                    Assert.Equal(expBI, actualBI);
                    break;
                case BigIntOffset.BigIntegerOffset expBIO:
                    Assert.Equal(expBIO, new BigIntOffset.BigIntegerOffset(actualBI));
                    break;
                default:
                    throw new NotImplementedException("Tests: MathOperations");
            }
        }
        else if (actual is decimal actualDecimal)
        {
            switch (expected)
            {
                case decimal expDecimal:
                    Assert.Equal(expDecimal, actualDecimal);
                    break;
                case BigInteger:
                    throw new ArgumentOutOfRangeException(nameof(expected), "expected value can't be BigInteger");
                case BigIntOffset.BigIntegerOffset expBIO:
                    Assert.Equal(expBIO, new BigIntOffset.BigIntegerOffset(actualDecimal));
                    break;
                default:
                    throw new NotImplementedException("Tests: MathOperations");
            }
        }
        else
        {
            throw new NotImplementedException("Tests: MathOperations");
        }
    }

    private static bool IsThisDecimalRounded(decimal value)
    {
        value = Math.Abs(value);
        return (value == Math.Floor(value));
    }

    private static bool IsThisDoubleRounded(double value)
    {
        value = Math.Abs(value);
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        return (value == Math.Floor(value));
    }

    private static bool IsTypeInteger(object obj)
    {
        return (obj is long or ulong or int or uint or short or ushort or sbyte or byte or BigInteger);
    }

    private static bool IsTypeUnsignedInteger(object obj)
    {
        return (obj is ulong or uint or ushort or byte);
    }

    public class ParamWrapper
    {
        public readonly object value;

        public ParamWrapper(object value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return $"'{value}' [{value.GetType().Name}]";
        }
    }

    #endregion

    #region Convert decimal

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
        var valueBio1 = new BigIntegerOffset(rawValue);
        var stringify = valueBio1.ToStringDouble();
        var valueBio2 = BigIntegerOffset.Parse(stringify);
        Assert.Equal(valueBio1, valueBio2);
    }

    [Theory]
    [MemberData(nameof(TestConvertDecimalData))]
    public void TestConvertDecimal(decimal rawValue)
    {
        var valueBio = new BigIntegerOffset(rawValue);
        var valueString = valueBio.ToString();
        var decimalRevert1 = (decimal)valueBio;
        var decimalRevert2 = decimal.Parse(valueString);

        Assert.Equal(rawValue, decimalRevert1);
        Assert.Equal(rawValue, decimalRevert2);

        var valueBio2 = BigIntegerOffset.Parse(valueString);
        Assert.Equal(valueBio, valueBio2);
    }

    [Theory]
    [MemberData(nameof(TestConvertDoubleData))]
    public void TestConvertDouble(decimal preRawValue)
    {
        var rawValue = (double)preRawValue;

        //
        var valueBio = new BigIntegerOffset(rawValue);
        var valueString = valueBio.ToString();
        var decimalRevert1 = (decimal)valueBio;
        var decimalRevert2 = decimal.Parse(valueString);

        Assert.Equal(preRawValue, decimalRevert1);
        Assert.Equal(preRawValue, decimalRevert2);

        var valueBio2 = BigIntegerOffset.Parse(valueString);
        Assert.Equal(valueBio, valueBio2);
    }

    #endregion
}