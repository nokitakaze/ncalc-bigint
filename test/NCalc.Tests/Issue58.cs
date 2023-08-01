using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Xunit;

namespace NCalc.Tests;

/// <summary>
/// </summary>
/// https://github.com/ncalc/ncalc/issues/58
public class Issue58
{
    public static readonly List<TypeCode> AllTypes = new List<TypeCode>()
    {
        TypeCode.Boolean, TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32,
        TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal,
    };

    private static readonly Dictionary<TypeCode, List<TypeCode>> ShouldNotWork = new();

    static Issue58()
    {
        // We want to test all of the cases in numbers.cs which means we need to test both LHS/RHS
        ShouldNotWork[TypeCode.Boolean] = AllTypes;
        ShouldNotWork[TypeCode.Byte] = new List<TypeCode> { TypeCode.Boolean };
        ShouldNotWork[TypeCode.SByte] = new List<TypeCode> { TypeCode.Boolean };
        ShouldNotWork[TypeCode.Int16] = new List<TypeCode> { TypeCode.Boolean };
        ShouldNotWork[TypeCode.UInt16] = new List<TypeCode> { TypeCode.Boolean };
        ShouldNotWork[TypeCode.Int32] = new List<TypeCode> { TypeCode.Boolean };
        ShouldNotWork[TypeCode.UInt32] = new List<TypeCode> { TypeCode.Boolean };
        ShouldNotWork[TypeCode.Int64] = new List<TypeCode> { TypeCode.Boolean };
        ShouldNotWork[TypeCode.UInt64] = new List<TypeCode> { TypeCode.Boolean };
        ShouldNotWork[TypeCode.Single] = new List<TypeCode> { TypeCode.Boolean };
        ShouldNotWork[TypeCode.Double] = new List<TypeCode> { TypeCode.Boolean };
        ShouldNotWork[TypeCode.Decimal] = new List<TypeCode> { TypeCode.Boolean };
    }

    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public static object[][] Should_Add_All_Numeric_Types_Issue_58__should_work_data()
    {
        var result = new List<object[]>();
        foreach (var (operat, expectedResult) in new ( string operat, int expectedResult )[]
                 {
                     ("+", 14 + 88),
                     ("-", 88 - 14),
                     ("%", 88 % 14),
                     ("*", 8 * 7),
                 })
        {
            foreach (var typeCodeA in AllTypes)
            {
                foreach (var typeCodeB in AllTypes.Except(ShouldNotWork[typeCodeA]))
                {
                    result.Add(new object[] { typeCodeA, typeCodeB, operat, expectedResult });
                }
            }
        }

        return result.ToArray();
    }

    [Theory]
    [MemberData(nameof(Should_Add_All_Numeric_Types_Issue_58__should_work_data))]
    public void Should_Add_All_Numeric_Types_Issue_58__should_work(
        TypeCode typeCodeA,
        TypeCode typeCodeB,
        string operat,
        int expectedResult
    )
    {
        var lhsValue = "88";
        var rhsValue = "14";
        if (operat == "*")
        {
            lhsValue = "8";
            rhsValue = "7";
        }

        var expr = $"x {operat} y";
        try
        {
            var result = new Expression(expr, CultureInfo.InvariantCulture)
                {
                    Parameters =
                    {
                        ["x"] = Convert.ChangeType(lhsValue, typeCodeA),
                        ["y"] = Convert.ChangeType(rhsValue, typeCodeB)
                    }
                }
                .Evaluate();
            Assert.True(Convert.ToInt64(result) == expectedResult,
                $"{expr}: {typeCodeA} = {lhsValue}, {typeCodeB} = {rhsValue} should return {expectedResult}");
        }
        catch (Exception ex)
        {
            Assert.Fail($"{expr}: {typeCodeA}, {typeCodeB} should not throw an exception but {ex} was thrown");
        }
    }

    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public static object[][] Should_Add_All_Numeric_Types_Issue_58__should_not_work_data()
    {
        var result = new List<object[]>();

        foreach (var operat in new string[] { "+", "-", "*", "%" })
        {
            foreach (var typeCodeA in AllTypes)
            {
                foreach (var typeCodeB in ShouldNotWork[typeCodeA])
                {
                    result.Add(new object[] { typeCodeA, typeCodeB, operat });
                }
            }
        }

        return result.Cast<object[]>().ToArray();
    }

    [Theory]
    [MemberData(nameof(Should_Add_All_Numeric_Types_Issue_58__should_not_work_data))]
    public void Should_Add_All_Numeric_Types_Issue_58__should_not_work(
        TypeCode typeCodeA,
        TypeCode typeCodeB,
        string operat
    )
    {
        string expr = $"x {operat} y";

        Assert.Throws<InvalidOperationException>(() => new Expression(expr, CultureInfo.InvariantCulture)
            {
                Parameters =
                {
                    ["x"] = Convert.ChangeType(1, typeCodeA),
                    ["y"] = Convert.ChangeType(1, typeCodeB)
                }
            }
            .Evaluate());
    }
}