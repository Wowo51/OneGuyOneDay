using Microsoft.VisualStudio.TestTools.UnitTesting;
using PackratParser;
using PackratParser.Expressions;
using System.Collections.Generic;
using System.Text;

namespace PackratParserTest
{
    [TestClass]
    public class ExtraParserTests
    {
        // Test that an empty sequence succeeds with an empty result list.
        [TestMethod]
        public void TestEmptySequenceExpression()
        {
            SequenceExpression sequence = new SequenceExpression(new List<Expression>());
            Result result = Parser.Parse(sequence, "anything");
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.Position);
            List<object> values = result.Value as List<object>;
            Assert.IsNotNull(values);
            Assert.AreEqual(0, values.Count);
        }

        // Test that an empty choice fails.
        [TestMethod]
        public void TestEmptyChoiceExpression()
        {
            ChoiceExpression choice = new ChoiceExpression(new List<Expression>());
            Result result = Parser.Parse(choice, "anything");
            Assert.IsFalse(result.Success);
            Assert.AreEqual(0, result.Position);
            Assert.IsNull(result.Value);
        }

        // Test that a literal with an empty string succeeds without moving the position.
        [TestMethod]
        public void TestLiteralEmpty()
        {
            LiteralExpression literal = new LiteralExpression("");
            Result result = Parser.Parse(literal, "anything");
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.Position);
            Assert.AreEqual("", result.Value);
        }

        // Test a sequence expression where one element fails.
        [TestMethod]
        public void TestSequenceExpressionFailure()
        {
            List<Expression> expressions = new List<Expression>();
            expressions.Add(new LiteralExpression("a"));
            expressions.Add(new LiteralExpression("b"));
            SequenceExpression sequence = new SequenceExpression(expressions);
            Result result = Parser.Parse(sequence, "ac");
            Assert.IsFalse(result.Success);
            Assert.AreEqual(0, result.Position);
            Assert.IsNull(result.Value);
        }

        // Test a choice expression where no alternative matches.
        [TestMethod]
        public void TestChoiceExpressionFailure()
        {
            List<Expression> alternatives = new List<Expression>();
            alternatives.Add(new LiteralExpression("x"));
            alternatives.Add(new LiteralExpression("y"));
            ChoiceExpression choice = new ChoiceExpression(alternatives);
            Result result = Parser.Parse(choice, "z");
            Assert.IsFalse(result.Success);
            Assert.AreEqual(0, result.Position);
            Assert.IsNull(result.Value);
        }

        // Test that ZeroOrMore returns an empty list when no occurrence is found.
        [TestMethod]
        public void TestZeroOrMoreEmpty()
        {
            ZeroOrMoreExpression zeroOrMore = new ZeroOrMoreExpression(new LiteralExpression("a"));
            Result result = Parser.Parse(zeroOrMore, "b");
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.Position);
            List<object> values = result.Value as List<object>;
            Assert.IsNotNull(values);
            Assert.AreEqual(0, values.Count);
        }

        // A custom expression to test memoization behavior.
        private class CountingExpression : Expression
        {
            public int Count { get; set; }

            public override Result Parse(ParserContext context, int position)
            {
                return context.Memoize(this, position, () =>
                {
                    this.Count = this.Count + 1;
                    // Succeeds without consuming input.
                    return new Result(true, position, null);
                });
            }
        }

        // Test that within the same ParserContext, the memoized result is used.
        [TestMethod]
        public void TestMemoization()
        {
            CountingExpression counting = new CountingExpression();
            ParserContext context = new ParserContext("test");
            Result firstCall = counting.Parse(context, 0);
            Result secondCall = counting.Parse(context, 0);
            Assert.IsTrue(firstCall.Success);
            Assert.IsTrue(secondCall.Success);
            Assert.AreEqual(firstCall.Position, secondCall.Position);
            // The memoization should ensure the lambda is executed only once.
            Assert.AreEqual(1, counting.Count);
        }
    }
}