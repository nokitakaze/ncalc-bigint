using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using Xunit;

namespace NCalc.Tests.BigIntegerTests;

[SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Local")]
public class CompositeTests
{
    public static object[][] MathOperationsData()
    {
        var rawInput = new (string a, string b)[]
        {
            ("14", "88"),
            ("123", "654"),
            ("123654", "987654"),
        };

        var types = new[] { "int", "uint", "long", "ulong", "BigInteger", "BigIntegerOffset", };
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
            .Select(t => new object[] { t.a, t.b, t.expr, t.expected })
            .Where(x => (x[0] is BigInteger) || (x[0] is BigIntegerOffset.BigIntegerOffset) ||
                        (x[1] is BigInteger) || (x[1] is BigIntegerOffset.BigIntegerOffset))
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
            "BigInteger" => BigInteger.Parse(value),
            "BigIntegerOffset" => NCalc.BigIntegerOffset.BigIntegerOffset.Parse(value),
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

        if (!IsDecimalInteger(decA) && IsTypeInteger(varA))
        {
            return emptyResponse;
        }

        if (!IsDecimalInteger(decB) && IsTypeInteger(varB))
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
    public void MathOperations(object x, object y, string expr, object expected)
    {
        var e = new NCalc.Expression(expr);
        e.EvaluateParameter += delegate(string name, ParameterArgs args)
        {
            args.Result = name switch
            {
                "x" => x,
                "y" => y,
                _ => args.Result
            };
        };

        object val = e.Evaluate();
        if (val is BigIntegerOffset.BigIntegerOffset valBIO)
        {
            switch (expected)
            {
                case decimal expDecimal:
                    Assert.Equal(new BigIntegerOffset.BigIntegerOffset(expDecimal), valBIO);
                    break;
                case BigInteger expBI:
                    Assert.Equal(new BigIntegerOffset.BigIntegerOffset(expBI), valBIO);
                    break;
                case BigIntegerOffset.BigIntegerOffset expBIO:
                    Assert.Equal(expBIO, valBIO);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        else if (val is BigInteger valBI)
        {
            switch (expected)
            {
                case decimal expDecimal:
                    Assert.Equal(new BigInteger(expDecimal), valBI);
                    break;
                case BigInteger expBI:
                    Assert.Equal(expBI, valBI);
                    break;
                case BigIntegerOffset.BigIntegerOffset expBIO:
                    Assert.Equal(expBIO, new BigIntegerOffset.BigIntegerOffset(valBI));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    private static bool IsDecimalInteger(decimal value)
    {
        value = Math.Abs(value);
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
}