using System.Numerics;
using Xunit;

namespace NCalc.Tests;

public class BigIntTest
{
    [Fact]
    public void TrivialTest()
    {
        var e = new NCalc.Expression("a + b");
        e.EvaluateParameter += delegate(string name, ParameterArgs args)
        {
            args.Result = name switch
            {
                "a" => new BigInteger(14),
                "b" => new BigInteger(88),
                _ => args.Result
            };
        };

        object val = e.Evaluate();
        Assert.Equal(new BigInteger(14 + 88), val);
    }
}