using System;
using NCalc.Domain;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using BinaryExpression = NCalc.Domain.BinaryExpression;
using UnaryExpression = NCalc.Domain.UnaryExpression;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace NCalc.Tests
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public class Fixtures
    {
        private readonly ITestOutputHelper _testOutputHelper;

        [Fact]
        public void ExpressionShouldEvaluate()
        {
            var expressions = new []
            {
                "2 + 3 + 5",
                "2 * 3 + 5",
                "2 * (3 + 5)",
                "2 * (2*(2*(2+1)))",
                "10 % 3",
                "true or false",
                "not true",
                "false || not (false and true)",
                "3 > 2 and 1 <= (3-2)",
                "3 % 2 != 10 % 3"
            };

            foreach (string expression in expressions)
                _testOutputHelper.WriteLine("{0} = {1}",
                    expression,
                    new Expression(expression).Evaluate());
        }

        [Fact]
        public void ShouldParseValues()
        {
            Assert.Equal(123456, new Expression("123456").Evaluate());
            Assert.Equal(new DateTime(2001, 01, 01), new Expression("#01/01/2001#").Evaluate());
            Assert.Equal(0.2d, new Expression(".2").Evaluate());
            Assert.Equal(123.456d, new Expression("123.456").Evaluate());
            Assert.Equal(123d, new Expression("123.").Evaluate());
            Assert.Equal(12300d, new Expression("123.E2").Evaluate());
            Assert.Equal(true, new Expression("true").Evaluate());
            Assert.Equal("true", new Expression("'true'").Evaluate());
            Assert.Equal("azerty", new Expression("'azerty'").Evaluate());
        }

        [Fact]
        public void ShouldHandleUnicode()
        {
            Assert.Equal("経済協力開発機構", new Expression("'経済協力開発機構'").Evaluate());
            Assert.Equal("Hello", new Expression(@"'\u0048\u0065\u006C\u006C\u006F'").Evaluate());
            Assert.Equal("だ", new Expression(@"'\u3060'").Evaluate());
            Assert.Equal("\u0100", new Expression(@"'\u0100'").Evaluate());
        }

        [Fact]
        public void ShouldEscapeCharacters()
        {
            Assert.Equal("'hello'", new Expression(@"'\'hello\''").Evaluate());
            Assert.Equal(" ' hel lo ' ", new Expression(@"' \' hel lo \' '").Evaluate());
            Assert.Equal("hel\nlo", new Expression(@"'hel\nlo'").Evaluate());
        }

        [Fact]
        public void ShouldDisplayErrorMessages()
        {
            try
            {
                new Expression("(3 + 2").Evaluate();
                Assert.Fail("'(3 + 2' didn't raised an exception");
            }
            catch(EvaluationException e)
            {
                _testOutputHelper.WriteLine("Error catched: " + e.Message);
            }
        }

        [Fact]
        public void Maths()
        {
            Assert.Equal(1M, new Expression("Abs(-1)").Evaluate());
            Assert.Equal(0d, new Expression("Acos(1)").Evaluate());
            Assert.Equal(0d, new Expression("Asin(0)").Evaluate());
            Assert.Equal(0d, new Expression("Atan(0)").Evaluate());
            Assert.Equal(2d, new Expression("Ceiling(1.5)").Evaluate());
            Assert.Equal(1d, new Expression("Cos(0)").Evaluate());
            Assert.Equal(1d, new Expression("Exp(0)").Evaluate());
            Assert.Equal(1d, new Expression("Floor(1.5)").Evaluate());
            Assert.Equal(-1d, new Expression("IEEERemainder(3,2)").Evaluate());
            Assert.Equal(0d, new Expression("Log(1,10)").Evaluate());
            Assert.Equal(0d, new Expression("Ln(1)").Evaluate());
            Assert.Equal(0d, new Expression("Log10(1)").Evaluate());
            Assert.Equal(9d, new Expression("Pow(3,2)").Evaluate());
            Assert.Equal(3.22d, new Expression("Round(3.222,2)").Evaluate());
            Assert.Equal(-1, new Expression("Sign(-10)").Evaluate());
            Assert.Equal(0d, new Expression("Sin(0)").Evaluate());
            Assert.Equal(2d, new Expression("Sqrt(4)").Evaluate());
            Assert.Equal(0d, new Expression("Tan(0)").Evaluate());
            Assert.Equal(1d, new Expression("Truncate(1.7)").Evaluate());
            Assert.Equal(-Math.PI/2, (double) new Expression("Atan2(-1,0)").Evaluate(), 1e-16);
            Assert.Equal(Math.PI/2, (double) new Expression("Atan2(1,0)").Evaluate(), 1e-16);
            Assert.Equal(Math.PI, (double) new Expression("Atan2(0,-1)").Evaluate(), 1e-16);
            Assert.Equal(0, (double) new Expression("Atan2(0,1)").Evaluate(), 1e-16);
            Assert.Equal(10, new Expression("Max(1,10)").Evaluate());
            Assert.Equal(1, new Expression("Min(1,10)").Evaluate());
        }

        [Fact]
        public void ShouldHandleTrailingDecimalPoint()
        {
            Assert.Equal(3.0, new Expression("1. + 2.").Evaluate());
        }

        [Fact]
        public void ExpressionShouldEvaluateCustomFunctions()
        {
            var e = new Expression("SecretOperation(3, 6)");

            e.EvaluateFunction += delegate(string name, FunctionArgs args)
                {
                    if (name == "SecretOperation")
                        args.Result = (int)args.Parameters[0].Evaluate() + (int)args.Parameters[1].Evaluate();
                };

            Assert.Equal(9, e.Evaluate());
        }

        [Fact]
        public void ExpressionShouldEvaluateCustomFunctionsWithParameters()
        {
            var e = new Expression("SecretOperation([e], 6) + f");
            e.Parameters["e"] = 3;
            e.Parameters["f"] = 1;

            e.EvaluateFunction += delegate(string name, FunctionArgs args)
                {
                    if (name == "SecretOperation")
                        args.Result = (int)args.Parameters[0].Evaluate() + (int)args.Parameters[1].Evaluate();
                };

            Assert.Equal(10, e.Evaluate());
        }

        [Fact]
        public void ExpressionShouldEvaluateParameters()
        {
            var e = new Expression("Round(Pow(Pi, 2) + Pow([Pi Squared], 2) + [X], 2)");

            e.Parameters["Pi Squared"] = new Expression("Pi * [Pi]");
            e.Parameters["X"] = 10;

            e.EvaluateParameter += delegate(string name, ParameterArgs args)
                {
                    if (name == "Pi")
                        args.Result = 3.14;
                };

            Assert.Equal(117.07, e.Evaluate());
        }

        [Fact]
        public void ShouldEvaluateConditionnal()
        {
            var eif = new Expression("if([divider] <> 0, [divided] / [divider], 0)");
            eif.Parameters["divider"] = 5;
            eif.Parameters["divided"] = 5;

            Assert.Equal(1d, eif.Evaluate());

            eif = new Expression("if([divider] <> 0, [divided] / [divider], 0)");
            eif.Parameters["divider"] = 0;
            eif.Parameters["divided"] = 5;
            Assert.Equal(0, eif.Evaluate());
        }

        [Fact]
        public void ShouldOverrideExistingFunctions()
        {
            var e = new Expression("Round(1.99, 2)");

            Assert.Equal(1.99d, e.Evaluate());

            e.EvaluateFunction += delegate(string name, FunctionArgs args)
            {
                if (name == "Round")
                    args.Result = 3;
            };

            Assert.Equal(3, e.Evaluate());
        }

        [Fact]
        public void ShouldEvaluateInOperator()
        {
            // The last argument should not be evaluated
            var ein = new Expression("in((2 + 2), [1], [2], 1 + 2, 4, 1 / 0)");
            ein.Parameters["1"] = 2;
            ein.Parameters["2"] = 5;

            Assert.Equal(true, ein.Evaluate());

            var eout = new Expression("in((2 + 2), [1], [2], 1 + 2, 3)");
            eout.Parameters["1"] = 2;
            eout.Parameters["2"] = 5;

            Assert.Equal(false, eout.Evaluate());

            // Should work with strings
            var estring = new Expression("in('to' + 'to', 'titi', 'toto')");

            Assert.Equal(true, estring.Evaluate());

        }

        [Fact]
        public void ShouldEvaluateOperators()
        {
            var expressions = new Dictionary<string, object>
                                  {
                                      {"!true", false},
                                      {"not false", true},
                                      {"Not false", true},
                                      {"NOT false", true},
                                      {"-10", -10},
                                      {"+20", 20},
                                      {"2**-1", 0.5},
                                      {"2**+2", 4.0},
                                      {"2 * 3", 6},
                                      {"6 / 2", 3d},
                                      {"7 % 2", 1},
                                      {"2 + 3", 5},
                                      {"2 - 1", 1},
                                      {"1 < 2", true},
                                      {"1 > 2", false},
                                      {"1 <= 2", true},
                                      {"1 <= 1", true},
                                      {"1 >= 2", false},
                                      {"1 >= 1", true},
                                      {"1 = 1", true},
                                      {"1 == 1", true},
                                      {"1 != 1", false},
                                      {"1 <> 1", false},
                                      {"1 & 1", 1},
                                      {"1 | 1", 1},
                                      {"1 ^ 1", 0},
                                      {"~1", ~1},
                                      {"2 >> 1", 1},
                                      {"2 << 1", 4},
                                      {"true && false", false},
                                      {"True and False", false},
                                      {"tRue aNd faLse", false},
                                      {"TRUE ANd fALSE", false},
                                      {"true AND FALSE", false},
                                      {"true || false", true},
                                      {"true or false", true},
                                      {"true Or false", true},
                                      {"true OR false", true},
                                      {"if(true, 0, 1)", 0},
                                      {"if(false, 0, 1)", 1}
                                  };

            foreach (KeyValuePair<string, object> pair in expressions)
            {
                Assert.Equal(pair.Value, new Expression(pair.Key).Evaluate());
            }

        }

        [Fact]
        public void ShouldHandleOperatorsPriority()
        {
            Assert.Equal(8, new Expression("2+2+2+2").Evaluate());
            Assert.Equal(16, new Expression("2*2*2*2").Evaluate());
            Assert.Equal(6, new Expression("2*2+2").Evaluate());
            Assert.Equal(6, new Expression("2+2*2").Evaluate());

            Assert.Equal(9d, new Expression("1 + 2 + 3 * 4 / 2").Evaluate());
            Assert.Equal(13.5, new Expression("18/2/2*3").Evaluate());

            Assert.Equal(-1d, new Expression("-1 ** 2").Evaluate());
            Assert.Equal(1d, new Expression("(-1) ** 2").Evaluate());
            Assert.Equal(512d, new Expression("2 ** 3 ** 2").Evaluate());
            Assert.Equal(64d, new Expression("(2 ** 3) ** 2").Evaluate());
            Assert.Equal(18d, new Expression("2 * 3 ** 2").Evaluate());
            Assert.Equal(8d, new Expression("2 ** 4 / 2").Evaluate());
        }

        [Fact]
        public void ShouldNotLoosePrecision()
        {
            Assert.Equal(0.5, new Expression("3/6").Evaluate());
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenInvalidNumber()
        {
            try
            {
                new Expression(". + 2").Evaluate();
                Assert.Fail("'. + 2' didn't raised an exception");
            }
            catch (EvaluationException e)
            {
                _testOutputHelper.WriteLine("Error catched: " + e.Message);
            }
        }

        [Fact]
        public void ShouldNotRoundDecimalValues()
        {
            Assert.Equal(false, new Expression("0 <= -0.6").Evaluate());
        }

        [Fact]
        public void ShouldEvaluateTernaryExpression()
        {
            Assert.Equal(1, new Expression("1+2<3 ? 3+4 : 1").Evaluate());
        }

        [Fact]
        public void ShouldSerializeExpression()
        {
            Assert.Equal("True and False", new BinaryExpression(BinaryExpressionType.And, new ValueExpression(true), new ValueExpression(false)).ToString());
            Assert.Equal("1 / 2", new BinaryExpression(BinaryExpressionType.Div, new ValueExpression(1), new ValueExpression(2)).ToString());
            Assert.Equal("1 = 2", new BinaryExpression(BinaryExpressionType.Equal, new ValueExpression(1), new ValueExpression(2)).ToString());
            Assert.Equal("1 > 2", new BinaryExpression(BinaryExpressionType.Greater, new ValueExpression(1), new ValueExpression(2)).ToString());
            Assert.Equal("1 >= 2", new BinaryExpression(BinaryExpressionType.GreaterOrEqual, new ValueExpression(1), new ValueExpression(2)).ToString());
            Assert.Equal("1 < 2", new BinaryExpression(BinaryExpressionType.Lesser, new ValueExpression(1), new ValueExpression(2)).ToString());
            Assert.Equal("1 <= 2", new BinaryExpression(BinaryExpressionType.LesserOrEqual, new ValueExpression(1), new ValueExpression(2)).ToString());
            Assert.Equal("1 - 2", new BinaryExpression(BinaryExpressionType.Minus, new ValueExpression(1), new ValueExpression(2)).ToString());
            Assert.Equal("1 % 2", new BinaryExpression(BinaryExpressionType.Modulo, new ValueExpression(1), new ValueExpression(2)).ToString());
            Assert.Equal("1 != 2", new BinaryExpression(BinaryExpressionType.NotEqual, new ValueExpression(1), new ValueExpression(2)).ToString());
            Assert.Equal("True or False", new BinaryExpression(BinaryExpressionType.Or, new ValueExpression(true), new ValueExpression(false)).ToString());
            Assert.Equal("1 + 2", new BinaryExpression(BinaryExpressionType.Plus, new ValueExpression(1), new ValueExpression(2)).ToString());
            Assert.Equal("1 * 2", new BinaryExpression(BinaryExpressionType.Times, new ValueExpression(1), new ValueExpression(2)).ToString());

            Assert.Equal("-(True and False)",new UnaryExpression(UnaryExpressionType.Negate, new BinaryExpression(BinaryExpressionType.And, new ValueExpression(true), new ValueExpression(false))).ToString());
            Assert.Equal("!(True and False)",new UnaryExpression(UnaryExpressionType.Not, new BinaryExpression(BinaryExpressionType.And, new ValueExpression(true), new ValueExpression(false))).ToString());

            Assert.Equal("test(True and False, -(True and False))",new Function(new Identifier("test"), new LogicalExpression[] { new BinaryExpression(BinaryExpressionType.And, new ValueExpression(true), new ValueExpression(false)), new UnaryExpression(UnaryExpressionType.Negate, new BinaryExpression(BinaryExpressionType.And, new ValueExpression(true), new ValueExpression(false))) }).ToString());

            Assert.Equal("True", new ValueExpression(true).ToString());
            Assert.Equal("False", new ValueExpression(false).ToString());
            Assert.Equal("1", new ValueExpression(1).ToString());
            Assert.Equal("1.234", new ValueExpression(1.234).ToString());
            Assert.Equal("'hello'", new ValueExpression("hello").ToString());
            Assert.Equal("#" + new DateTime(2009, 1, 1) + "#", new ValueExpression(new DateTime(2009, 1, 1)).ToString());

            Assert.Equal("Sum(1 + 2)", new Function(new Identifier("Sum"),
                new [] { new BinaryExpression(BinaryExpressionType.Plus, new ValueExpression(1), new ValueExpression(2))}).ToString());
        }

        [Fact]
        public void ShouldHandleStringConcatenation()
        {
            Assert.Equal("toto", new Expression("'to' + 'to'").Evaluate());
            Assert.Equal("one2", new Expression("'one' + 2").Evaluate());
            Assert.Equal(3M, new Expression("1 + '2'").Evaluate());
        }

        [Fact]
        public void ShouldDetectSyntaxErrorsBeforeEvaluation()
        {
            var e = new Expression("a + b * (");
            Assert.Null(e.Error);
            Assert.True(e.HasErrors());
            Assert.True(e.HasErrors());
            Assert.NotNull(e.Error);

            e = new Expression("* b ");
            Assert.Null(e.Error);
            Assert.True(e.HasErrors());
            Assert.NotNull(e.Error);
        }

        [Fact]
        public void ShouldReuseCompiledExpressionsInMultiThreadedMode()
        {
            // Repeats the tests n times
            for (int cpt = 0; cpt < 20; cpt++)
            {
                const int nbthreads = 30;
                _exceptions = new List<Exception>();
                var threads = new Thread[nbthreads];

                // Starts threads
                for (int i = 0; i < nbthreads; i++)
                {
                    var thread = new Thread(WorkerThread);
                    thread.Start();
                    threads[i] = thread;
                }

                // Waits for end of threads
                bool running = true;
                while (running)
                {
                    Thread.Sleep(100);
                    running = false;
                    for (int i = 0; i < nbthreads; i++)
                    {
                        if (threads[i].ThreadState == ThreadState.Running)
                            running = true;
                    }
                }

                if (_exceptions.Count > 0)
                {
                    _testOutputHelper.WriteLine(_exceptions[0].StackTrace);
                    Assert.Fail(_exceptions[0].Message);
                }
            }
        }

        private List<Exception> _exceptions;

        public Fixtures(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private void WorkerThread()
        {
            try
            {
                var r1 = new Random((int)DateTime.Now.Ticks);
                var r2 = new Random((int)DateTime.Now.Ticks);
                int n1 = r1.Next(10);
                int n2 = r2.Next(10);

                // Constructs a simple addition randomly. Odds are that the same expression gets constructed multiple times by different threads
                var exp = n1 + " + " + n2;
                var e = new Expression(exp);
                Assert.True(e.Evaluate().Equals(n1 + n2));
            }
            catch (Exception e)
            {
                _exceptions.Add(e);
            }
        }

        [Fact]
        public void ShouldHandleCaseSensitiveness()
        {
            Assert.Equal(1M, new Expression("aBs(-1)", EvaluateOptions.IgnoreCase).Evaluate());
            Assert.Equal(1M, new Expression("Abs(-1)", EvaluateOptions.None).Evaluate());

            try
            {
                Assert.Equal(1M, new Expression("aBs(-1)", EvaluateOptions.None).Evaluate());
            }
            catch (ArgumentException)
            {
                return;
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected exception");
            }

            Assert.Fail("Should throw ArgumentException");
        }

        [Fact]
        public void ShouldHandleCustomParametersWhenNoSpecificParameterIsDefined()
        {
            var e = new Expression("Round(Pow([Pi], 2) + Pow([Pi], 2) + 10, 2)");

            e.EvaluateParameter += delegate(string name, ParameterArgs arg)
            {
                if (name == "Pi")
                    arg.Result = 3.14;
            };

            e.Evaluate();
        }

        [Fact]
        public void ShouldHandleCustomFunctionsInFunctions()
        {
            var e = new Expression("if(true, func1(x) + func2(func3(y)), 0)");

            e.EvaluateFunction += delegate(string name, FunctionArgs arg)
            {
                switch (name)
                {
                    case "func1": arg.Result = 1;
                        break;
                    case "func2": arg.Result = 2 * Convert.ToDouble(arg.Parameters[0].Evaluate());
                        break;
                    case "func3": arg.Result = 3 * Convert.ToDouble(arg.Parameters[0].Evaluate());
                        break;
                }
            };

            e.EvaluateParameter += delegate(string name, ParameterArgs arg)
            {
                switch (name)
                {
                    case "x": arg.Result = 1;
                        break;
                    case "y": arg.Result = 2;
                        break;
                    case "z": arg.Result = 3;
                        break;
                }
            };

            Assert.Equal(13d, e.Evaluate());
        }


        [Fact]
        public void ShouldParseScientificNotation()
        {
            Assert.Equal(12.2d, new Expression("1.22e1").Evaluate());
            Assert.Equal(100d, new Expression("1e2").Evaluate());
            Assert.Equal(100d, new Expression("1e+2").Evaluate());
            Assert.Equal(0.01d, new Expression("1e-2").Evaluate());
            Assert.Equal(0.001d, new Expression(".1e-2").Evaluate());
            Assert.Equal(10000000000d, new Expression("1e10").Evaluate());
        }

        [Fact]
        public void ShouldEvaluateArrayParameters()
        {
            var e = new Expression("x * x", EvaluateOptions.IterateParameters);
            e.Parameters["x"] = new [] { 0, 1, 2, 3, 4 };

            var result = (IList)e.Evaluate();

            Assert.Equal(0, result[0]);
            Assert.Equal(1, result[1]);
            Assert.Equal(4, result[2]);
            Assert.Equal(9, result[3]);
            Assert.Equal(16, result[4]);
        }

        [Fact]
        public void CustomFunctionShouldReturnNull()
        {
            var e = new Expression("SecretOperation(3, 6)");

            e.EvaluateFunction += delegate(string name, FunctionArgs args)
            {
                Assert.False(args.HasResult);
                if (name == "SecretOperation")
                    args.Result = null;
                Assert.True(args.HasResult);
            };

            Assert.Null(e.Evaluate());
        }

        [Fact]
        public void CustomParametersShouldReturnNull()
        {
            var e = new Expression("x");

            e.EvaluateParameter += delegate(string name, ParameterArgs args)
            {
                Assert.False(args.HasResult);
                if (name == "x")
                    args.Result = null;
                Assert.True(args.HasResult);
            };

            Assert.Equal(null, e.Evaluate());
        }

        [Fact]
        public void ShouldCompareDates()
        {
            Assert.Equal(true, new Expression("#1/1/2009#==#1/1/2009#").Evaluate());
            Assert.Equal(false, new Expression("#2/1/2009#==#1/1/2009#").Evaluate());
        }

        [Fact]
        public void ShouldRoundAwayFromZero()
        {
            Assert.Equal(22d, new Expression("Round(22.5, 0)").Evaluate());
            Assert.Equal(23d, new Expression("Round(22.5, 0)", EvaluateOptions.RoundAwayFromZero).Evaluate());
        }

        [Fact]
        public void ShouldEvaluateSubExpressions()
        {
            var volume = new Expression("[surface] * h");
            var surface = new Expression("[l] * [L]");
            volume.Parameters["surface"] = surface;
            volume.Parameters["h"] = 3;
            surface.Parameters["l"] = 1;
            surface.Parameters["L"] = 2;

            Assert.Equal(6, volume.Evaluate());
        }

        [Fact]
        public void ShouldHandleLongValues()
        {
            Assert.Equal(40_000_000_000 + 1, new Expression("40000000000+1").Evaluate());
        }

        [Fact]
        public void ShouldCompareLongValues()
        {
            Assert.Equal(false, new Expression("(0=1500000)||(((0+2200000000)-1500000)<0)").Evaluate());
        }

        [Fact]
        public void ShouldDisplayErrorIfUncompatibleTypes()
        {
            try
            {
                var e = new Expression("(a > b) + 10");
                e.Parameters["a"] = 1;
                e.Parameters["b"] = 2;
                e.Evaluate();
            }
            catch (InvalidOperationException)
            {
                Assert.True(true);
                return;
            }

            Assert.True(false, userMessage: $"Exception {nameof(InvalidOperationException)} didn't raise");
        }

        [Fact]
        public void ShouldNotConvertRealTypes()
        {
            var e = new Expression("x/2");
            e.Parameters["x"] = 2F;
            Assert.Equal(typeof(float), e.Evaluate().GetType());

            e = new Expression("x/2");
            e.Parameters["x"] = 2D;
            Assert.Equal(typeof(double), e.Evaluate().GetType());

            e = new Expression("x/2");
            e.Parameters["x"] = 2m;
            Assert.Equal(typeof(decimal), e.Evaluate().GetType());

            e = new Expression("a / b * 100");
            e.Parameters["a"] = 20M;
            e.Parameters["b"] = 20M;
            Assert.Equal(100M, e.Evaluate());

        }

        [Fact]
        public void ShouldShortCircuitBooleanExpressions()
        {
            var e = new Expression("([a] != 0) && ([b]/[a]>2)");
            e.Parameters["a"] = 0;

            Assert.Equal(false, e.Evaluate());
        }

        [Fact]
        public void ShouldAddDoubleAndDecimal()
        {
            var e = new Expression("1.8 + Abs([var1])");
            e.Parameters["var1"] = 9.2;

            Assert.Equal(11M, e.Evaluate());
        }

        [Fact]
        public void ShouldSubtractDoubleAndDecimal()
        {
            var e = new Expression("1.8 - Abs([var1])");
            e.Parameters["var1"] = 0.8;

            Assert.Equal(1M, e.Evaluate());
        }

        [Fact]
        public void ShouldMultiplyDoubleAndDecimal()
        {
            var e = new Expression("1.8 * Abs([var1])");
            e.Parameters["var1"] = 9.2;

            Assert.Equal(16.56M, e.Evaluate());
        }

        [Fact]
        public void ShouldDivideDoubleAndDecimal()
        {
            var e = new Expression("1.8 / Abs([var1])");
            e.Parameters["var1"] = 0.5;

            Assert.Equal(3.6M, e.Evaluate());
        }

        [Fact]
        public void IncorrectCalculation_NCalcAsync_Issue_4()
        {
            Expression e = new Expression("(1604326026000-1604325747000)/60000");
            var evalutedResult = e.Evaluate();

            Assert.True(evalutedResult is double);
            Assert.Equal(4.65, (double)evalutedResult, 0.001);
        }

        [Fact]
        public void Should_Throw_Exception_On_Lexer_Errors_Issue_6()
        {
            // https://github.com/ncalc/ncalc-async/issues/6

            var result1 = Assert.Throws<EvaluationException>(() => Expression.Compile("\"0\"", true));
            Assert.Equal($"token recognition error at: '\"' at 1:1{Environment.NewLine}token recognition error at: '\"' at 1:3", result1.Message);

            var result2 = Assert.Throws<EvaluationException>(() => Expression.Compile("Format(\"{0:(###) ###-####}\", \"9999999999\")", true));
            Assert.True(
                result2.Message.Contains("was not recognized as a valid DateTime.") ||
                result2.Message.Contains("не распознана как действительное значение DateTime")
            );
        }

        [Fact]
        public void Should_Divide_Decimal_By_Double_Issue_16()
        {
            // https://github.com/ncalc/ncalc/issues/16

            var e = new Expression("x / 1.0");
            e.Parameters["x"] = 1m;

            Assert.Equal(1m, e.Evaluate());
        }

        [Fact]
        public void Should_Divide_Decimal_By_Single()
        {
            var e = new Expression("x / y");
            e.Parameters["x"] = 1m;
            e.Parameters["y"] = 1f;

            Assert.Equal(1m, e.Evaluate());
        }

        [Fact]
        public void ShouldParseInvariantCulture()
        {
            var originalCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            try
            {
                var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                culture.NumberFormat.NumberDecimalSeparator = ",";
                Thread.CurrentThread.CurrentCulture = culture;
                var exceptionThrown = false;
                try
                {
                    var expr = new Expression("[a]<2.0") { Parameters = { ["a"] = "1.7" } };
                    expr.Evaluate();
                }
                catch (FormatException)
                {
                    exceptionThrown = true;
                }

                Assert.True(exceptionThrown);

                var e = new Expression("[a]<2.0", CultureInfo.InvariantCulture) { Parameters = { ["a"] = "1.7" } };
                Assert.Equal(true, e.Evaluate());
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = originalCulture;
            }
        }

        [Fact]
        public void Should_Add_All_Numeric_Types_Issue_58()
        {
            // https://github.com/ncalc/ncalc/issues/58
            var expectedResult = 100;
            var operand = "+";
            var lhsValue = "50";
            var rhsValue = "50";

            var allTypes = new List<TypeCode>()
            {
                TypeCode.Boolean, TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32,
                TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal
            };

            var exceptionThrown = false;

            var shouldNotWork = new Dictionary<TypeCode, List<TypeCode>>();

            // We want to test all of the cases in numbers.cs which means we need to test both LHS/RHS
            shouldNotWork[TypeCode.Boolean] = allTypes;
            shouldNotWork[TypeCode.Byte] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.SByte] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.Int16] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt16] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Int32] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt32] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Int64] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt64] = new List<TypeCode>
                { TypeCode.Boolean, TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64 };
            shouldNotWork[TypeCode.Single] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Double] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Decimal] = new List<TypeCode> { TypeCode.Boolean };

            // These should all work and return a value
            foreach (var typecodeA in allTypes)
            {
                var toTest = allTypes.Except(shouldNotWork[typecodeA]);
                foreach (var typecodeB in toTest)
                {
                    var expr = $"x {operand} y";
                    try
                    {
                        var result = new Expression(expr, CultureInfo.InvariantCulture)
                            {
                                Parameters =
                                {
                                    ["x"] = Convert.ChangeType(lhsValue, typecodeA),
                                    ["y"] = Convert.ChangeType(rhsValue, typecodeB)
                                } 
                            }
                            .Evaluate();
                        Assert.True(Convert.ToInt64(result) == expectedResult,
                            $"{expr}: {typecodeA} = {lhsValue}, {typecodeB} = {rhsValue} should return {expectedResult}");
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail($"{expr}: {typecodeA}, {typecodeB} should not throw an exception but {ex} was thrown");
                    }
                }

                // These should throw exceptions

                foreach (var typecodeB in shouldNotWork[typecodeA])
                {
                    var expr = $"x {operand} y";
                    Assert.Throws<InvalidOperationException>(() => new Expression(expr, CultureInfo.InvariantCulture)
                        {
                            Parameters =
                            {
                                ["x"] = Convert.ChangeType(1, typecodeA),
                                ["y"] = Convert.ChangeType(1, typecodeB)
                            }
                        }
                        .Evaluate());
                    // $"{expr}: {typecodeA}, {typecodeB}"
                }
            }
        }

        [Fact]
        public void Should_Subtract_All_Numeric_Types_Issue_58()
        {
            // https://github.com/ncalc/ncalc/issues/58
            var expectedResult = 0;
            var operand = "-";
            var lhsValue = 50;
            var rhsValue = 50;

            var allTypes = new List<TypeCode>()
            {
                TypeCode.Boolean, TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32,
                TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal
            };

            var exceptionThrown = false;

            var shouldNotWork = new Dictionary<TypeCode, List<TypeCode>>();

            // We want to test all of the cases in numbers.cs which means we need to test both LHS/RHS
            shouldNotWork[TypeCode.Boolean] = allTypes;
            shouldNotWork[TypeCode.Byte] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.SByte] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.Int16] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt16] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Int32] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt32] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Int64] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt64] = new List<TypeCode>
                { TypeCode.Boolean, TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64 };
            shouldNotWork[TypeCode.Single] = new List<TypeCode> { TypeCode.Boolean, TypeCode.Decimal };
            shouldNotWork[TypeCode.Double] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Decimal] = new List<TypeCode> { TypeCode.Boolean };

            // These should all work and return a value
            foreach (var typecodeA in allTypes)
            {
                var toTest = allTypes.Except(shouldNotWork[typecodeA]);
                foreach (var typecodeB in toTest)
                {
                    var expr = $"x {operand} y";
                    try
                    {
                        var result = new Expression(expr, CultureInfo.InvariantCulture)
                            {
                                Parameters =
                                {
                                    ["x"] = Convert.ChangeType(lhsValue, typecodeA),
                                    ["y"] = Convert.ChangeType(rhsValue, typecodeB)
                                }
                            }
                            .Evaluate();
                        Assert.True(Convert.ToInt64(result) == expectedResult,
                            $"{expr}: {typecodeA} = {lhsValue}, {typecodeB} = {rhsValue} should return {expectedResult}");
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail($"{expr}: {typecodeA}, {typecodeB} should not throw an exception but {ex} was thrown");
                    }
                }

                // These should throw exceptions

                foreach (var typecodeB in shouldNotWork[typecodeA])
                {
                    var expr = $"x {operand} y";
                    Assert.Throws<InvalidOperationException>(() => new Expression(expr, CultureInfo.InvariantCulture)
                        {
                            Parameters =
                            {
                                ["x"] = Convert.ChangeType(lhsValue, typecodeA),
                                ["y"] = Convert.ChangeType(rhsValue, typecodeB)
                            }
                        }
                        .Evaluate());
                    // $"{expr}: {typecodeA}, {typecodeB}"
                }
            }
        }

        [Fact]
        public void Should_Multiply_All_Numeric_Types_Issue_58()
        {
            // https://github.com/ncalc/ncalc/issues/58
            var expectedResult = 64;
            var operand = "*";
            var lhsValue = 8;
            var rhsValue = 8;

            var allTypes = new List<TypeCode>()
            {
                TypeCode.Boolean, TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32,
                TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal
            };

            var exceptionThrown = false;

            var shouldNotWork = new Dictionary<TypeCode, List<TypeCode>>();

            // We want to test all of the cases in numbers.cs which means we need to test both LHS/RHS
            shouldNotWork[TypeCode.Boolean] = allTypes;
            shouldNotWork[TypeCode.Byte] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.SByte] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.Int16] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt16] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Int32] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt32] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Int64] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt64] = new List<TypeCode>
                { TypeCode.Boolean, TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64 };
            shouldNotWork[TypeCode.Single] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Double] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Decimal] = new List<TypeCode> { TypeCode.Boolean };

            // These should all work and return a value
            foreach (var typecodeA in allTypes)
            {
                var toTest = allTypes.Except(shouldNotWork[typecodeA]);
                foreach (var typecodeB in toTest)
                {
                    var expr = $"x {operand} y";
                    try
                    {
                        var result = new Expression(expr, CultureInfo.InvariantCulture)
                            {
                                Parameters =
                                {
                                    ["x"] = Convert.ChangeType(lhsValue, typecodeA),
                                    ["y"] = Convert.ChangeType(rhsValue, typecodeB)
                                }
                            }
                            .Evaluate();
                        Assert.True(Convert.ToInt64(result) == expectedResult,
                            $"{expr}: {typecodeA} = {lhsValue}, {typecodeB} = {rhsValue} should return {expectedResult}");
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail($"{expr}: {typecodeA}, {typecodeB} should not throw an exception but {ex} was thrown");
                    }
                }

                // These should throw exceptions

                foreach (var typecodeB in shouldNotWork[typecodeA])
                {
                    var expr = $"x {operand} y";
                    Assert.Throws<InvalidOperationException>(() => new Expression(expr, CultureInfo.InvariantCulture)
                        {
                            Parameters =
                            {
                                ["x"] = Convert.ChangeType(lhsValue, typecodeA),
                                ["y"] = Convert.ChangeType(rhsValue, typecodeB)
                            }
                        }
                        .Evaluate());
                    // $"{expr}: {typecodeA}, {typecodeB}"
                }
            }
        }

        [Fact]
        public void Should_Modulo_All_Numeric_Types_Issue_58()
        {
            // https://github.com/ncalc/ncalc/issues/58
            var expectedResult = 0;
            var operand = "%";
            var lhsValue = 50;
            var rhsValue = 50;

            var allTypes = new List<TypeCode>()
            {
                TypeCode.Boolean, TypeCode.Byte, TypeCode.SByte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32,
                TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal
            };

            var exceptionThrown = false;

            var shouldNotWork = new Dictionary<TypeCode, List<TypeCode>>();

            // We want to test all of the cases in numbers.cs which means we need to test both LHS/RHS
            shouldNotWork[TypeCode.Boolean] = allTypes;
            shouldNotWork[TypeCode.Byte] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.SByte] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.Int16] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt16] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Int32] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt32] = new List<TypeCode> { TypeCode.Boolean };
            shouldNotWork[TypeCode.Int64] = new List<TypeCode> { TypeCode.Boolean, TypeCode.UInt64 };
            shouldNotWork[TypeCode.UInt64] = new List<TypeCode>
                { TypeCode.Boolean, TypeCode.SByte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64 };
            shouldNotWork[TypeCode.Single] = new List<TypeCode> { TypeCode.Boolean, TypeCode.Decimal };
            shouldNotWork[TypeCode.Double] = new List<TypeCode> { TypeCode.Boolean, TypeCode.Decimal };
            shouldNotWork[TypeCode.Decimal] = new List<TypeCode> { TypeCode.Boolean, TypeCode.Single, TypeCode.Double };

            // These should all work and return a value
            foreach (var typecodeA in allTypes)
            {
                var toTest = allTypes.Except(shouldNotWork[typecodeA]);
                foreach (var typecodeB in toTest)
                {
                    var expr = $"x {operand} y";
                    try
                    {
                        var result = new Expression(expr, CultureInfo.InvariantCulture)
                            {
                                Parameters =
                                {
                                    ["x"] = Convert.ChangeType(lhsValue, typecodeA),
                                    ["y"] = Convert.ChangeType(rhsValue, typecodeB)
                                }
                            }
                            .Evaluate();
                        Assert.True(Convert.ToInt64(result) == expectedResult,
                            $"{expr}: {typecodeA} = {lhsValue}, {typecodeB} = {rhsValue} should return {expectedResult}");
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail($"{expr}: {typecodeA}, {typecodeB} should not throw an exception but {ex} was thrown");
                    }
                }

                // These should throw exceptions

                foreach (var typecodeB in shouldNotWork[typecodeA])
                {
                    var expr = $"x {operand} y";
                    Assert.Throws<InvalidOperationException>(() => new Expression(expr, CultureInfo.InvariantCulture)
                        {
                            Parameters =
                            {
                                ["x"] = Convert.ChangeType(lhsValue, typecodeA),
                                ["y"] = Convert.ChangeType(rhsValue, typecodeB)
                            }
                        }
                        .Evaluate());
                    // $"{expr}: {typecodeA}, {typecodeB}"
                }
            }
        }

        [Fact]
        public void ShouldCorrectlyParseCustomCultureParameter()
        {
            var cultureDot = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            cultureDot.NumberFormat.NumberGroupSeparator = " ";
            var cultureComma = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            cultureComma.NumberFormat.CurrencyDecimalSeparator = ",";
            cultureComma.NumberFormat.NumberGroupSeparator = " ";

            //use 1*[A] to avoid evaluating expression parameters as string - force numeric conversion
            ExecuteTest("1*[A]-[B]", 1.5m);
            ExecuteTest("1*[A]+[B]", 2.5m);
            ExecuteTest("1*[A]/[B]", 4m);
            ExecuteTest("1*[A]*[B]", 1m);
            ExecuteTest("1*[A]>[B]", true);
            ExecuteTest("1*[A]<[B]", false);


            void ExecuteTest(string formula, object expectedValue)
            {
                //Correctly evaluate with decimal dot culture and parameter with dot
                Assert.Equal(expectedValue, new Expression(formula, cultureDot)
                {
                    Parameters = new Dictionary<string, object>
                        {
                            {"A","2.0"},
                            {"B","0.5"}

                        }
                }.Evaluate());

                //Correctly evaluate with decimal comma and parameter with comma
                Assert.Equal(expectedValue, new Expression(formula, cultureComma)
                {
                    Parameters = new Dictionary<string, object>
                    {
                        {"A","2.0"},
                        {"B","0.5"}

                    }
                }.Evaluate());

                //combining decimal dot and comma fails
                Assert.Throws<FormatException>(() => new Expression(formula, cultureComma)
                {
                    Parameters = new Dictionary<string, object>
                    {
                        {"A","2,0"},
                        {"B","0.5"}

                    }
                }.Evaluate());

                //combining decimal dot and comma fails
                Assert.Throws<FormatException>(() => new Expression(formula, cultureDot)
                {
                    Parameters = new Dictionary<string, object>
                    {
                        {"A","2,0"},
                        {"B","0.5"}

                    }
                }.Evaluate());

            }
        }

        [Fact]
        public void SerializeAndDeserialize_ShouldWork()
        {
            
            ExecuteTest(@"(waterlevel > 1 AND waterlevel <= 3)", false, 3.2);
            ExecuteTest(@"(waterlevel > 3 AND waterlevel <= 5)", true, 3.2);
            ExecuteTest(@"(waterlevel > 1 AND waterlevel <= 3)", false, 3.1);
            ExecuteTest(@"(waterlevel > 3 AND waterlevel <= 5)", true, 3.1);
            ExecuteTest(@"(3 < waterlevel AND 5 >= waterlevel)", true, 3.1);
            ExecuteTest(@"(3.2 < waterlevel AND 5.3 >= waterlevel)", true, 4);

            void ExecuteTest(string expression, bool expected, double inputValue)
            {
                var compiled = Expression.Compile(expression, true);
                var serialized = JsonConvert.SerializeObject(compiled, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All // We need this to allow serializing abstract classes
                });

                var deserialized = JsonConvert.DeserializeObject<LogicalExpression>(serialized, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                Expression.CacheEnabled = false;
                var exp = new Expression(deserialized);
                exp.Parameters = new Dictionary<string, object> {
                    {"waterlevel", inputValue}
                };

                object evaluated;
                try {
                    evaluated = exp.Evaluate();
                } catch {
                    evaluated = false;
                }

                // Assert
                Assert.Equal(evaluated, expected);
            }

        }
    }
}
