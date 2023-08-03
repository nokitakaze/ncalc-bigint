using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            .Where(x => (x.a is BigIntegerOffset) || (x.b is BigIntegerOffset))
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
        if ((x.value is BigIntOffset.BigIntegerOffset) || (y.value is BigIntOffset.BigIntegerOffset))
        {
            Assert.True(
                actual is BigIntOffset.BigIntegerOffset,
                $"Arithmetic operation '{expr}' made '{actual.GetType().Name}' from '{x.value.GetType().Name}' and '{y.value.GetType().Name}' instead of BigIntegerOffset"
            );
        }

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
                    throw new NotImplementedException(
                        $"Tests: MathOperations. '{actual.GetType().Name}' & '{expected.GetType().Name}'");
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
                    throw new NotImplementedException(
                        $"Tests: MathOperations. '{actual.GetType().Name}' & '{expected.GetType().Name}'");
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
                    throw new NotImplementedException(
                        $"Tests: MathOperations. '{actual.GetType().Name}' & '{expected.GetType().Name}'");
            }
        }
        else if (actual is double actualDouble)
        {
            switch (expected)
            {
                case decimal expDecimal:
                    Assert.Equal(expDecimal, (decimal)actualDouble);
                    break;
                case BigInteger:
                    throw new ArgumentOutOfRangeException(nameof(expected), "expected value can't be BigInteger");
                case BigIntOffset.BigIntegerOffset expBIO:
                    Assert.Equal(expBIO, new BigIntOffset.BigIntegerOffset(actualDouble));
                    break;
                default:
                    throw new NotImplementedException(
                        $"Tests: MathOperations. '{actual.GetType().Name}' & '{expected.GetType().Name}'");
            }
        }
        else
        {
            throw new NotImplementedException(
                $"Tests: MathOperations. '{actual.GetType().Name}' & '{expected.GetType().Name}'");
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

    #region Second part

    public static object[][] TestSecondCompositeData()
    {
        var items = new List<(string expr, string[] arguments, decimal expected)>()
        {
            ("x - 0.00001", new string[] { "3" }, 3m - 0.000_01m),
            ("x - 0.00001", new string[] { "0.03" }, 0.03m - 0.000_01m),
            ("x + 0.00001", new string[] { "3" }, 3m + 0.000_01m),
            ("x + 0.00001", new string[] { "0.03" }, 0.03m + 0.000_01m),
        };

        {
            var s = string.Empty;
            var dec1 = 1m;
            var dec3 = 3m;
            for (var i = 1; i < 10; i++)
            {
                dec1 *= 0.1m;
                dec3 *= 0.1m;

                //
                items.Add(("0.3 - x", new string[] { $"0.{s}1" }, 0.3m - dec1));
                items.Add(("0.03 - x", new string[] { $"0.{s}1" }, 0.03m - dec1));
                items.Add(("0.3 + x", new string[] { $"0.{s}1" }, 0.3m + dec1));
                items.Add(("0.03 + x", new string[] { $"0.{s}1" }, 0.03m + dec1));
                items.Add(("3.7 + x", new string[] { $"0.{s}1" }, 3.7m + dec1));
                items.Add(("3.7 - x", new string[] { $"0.{s}1" }, 3.7m - dec1));

                //
                items.Add(("x - 0.00001", new string[] { $"0.{s}3" }, dec3 - 0.00001m));
                items.Add(("x + 0.00001", new string[] { $"0.{s}3" }, dec3 + 0.00001m));

                //
                s += "0";
            }
        }

        var varNames = new string[] { "x", "y", "z", "a", "b", "c" };

        return items
            .Select(t =>
            {
                var arguments = t.arguments
                    .Select((value, index) => (value, index))
                    .ToDictionary(t1 => varNames[t1.index], t1 => new ParamWrapper(BigIntegerOffset.Parse(t1.value)));

                return new object[]
                {
                    t.expr,
                    arguments,
                    t.expected
                };
            })
            .ToArray();
    }

    [Theory]
    [MemberData(nameof(TestSecondCompositeData))]
    public void TestSecondComposite(
        string expr,
        Dictionary<string, ParamWrapper> arguments,
        decimal expected
    )
    {
        var e = new NCalc.Expression(expr);
        e.EvaluateParameter += delegate(string name, ParameterArgs args)
        {
            var rawValue = arguments[name].value;
            BigIntegerOffset castedValue;
            if (rawValue is BigIntegerOffset a1)
            {
                castedValue = a1;
            }
            else
            {
                throw new NotImplementedException();
            }

            args.Result = castedValue;
        };

        object actual = e.Evaluate();
        if (actual is BigIntOffset.BigIntegerOffset actualBIO)
        {
            Assert.Equal(new BigIntegerOffset(expected), actualBIO);
        }
        else
        {
            throw new NotImplementedException(
                $"Tests: MathOperations. '{actual.GetType().Name}' & '{expected.GetType().Name}'");
        }
    }

    #endregion
}