using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PackratParser;
using PackratParser.Expressions;

namespace PackratParserTest
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestLiteralExpressionSuccess()
        {
            LiteralExpression literal = new LiteralExpression("abc");
            Result result = Parser.Parse(literal, "abc");
            Assert.IsTrue(result.Success);
            Assert.AreEqual(3, result.Position);
            Assert.AreEqual("abc", result.Value);
        }

        [TestMethod]
        public void TestLiteralExpressionFail()
        {
            LiteralExpression literal = new LiteralExpression("abc");
            Result result = Parser.Parse(literal, "abd");
            Assert.IsFalse(result.Success);
            Assert.AreEqual(0, result.Position);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void TestChoiceExpression()
        {
            List<Expression> alternatives = new List<Expression>();
            alternatives.Add(new LiteralExpression("xyz"));
            alternatives.Add(new LiteralExpression("abc"));
            ChoiceExpression choice = new ChoiceExpression(alternatives);
            Result result = Parser.Parse(choice, "abc");
            Assert.IsTrue(result.Success);
            Assert.AreEqual(3, result.Position);
            Assert.AreEqual("abc", result.Value);
        }

        [TestMethod]
        public void TestSequenceExpression()
        {
            List<Expression> sequenceList = new List<Expression>();
            sequenceList.Add(new LiteralExpression("abc"));
            sequenceList.Add(new LiteralExpression("def"));
            SequenceExpression sequence = new SequenceExpression(sequenceList);
            Result result = Parser.Parse(sequence, "abcdef");
            Assert.IsTrue(result.Success);
            Assert.AreEqual(6, result.Position);
            List<object> values = result.Value as List<object>;
            Assert.IsNotNull(values);
            Assert.AreEqual(2, values.Count);
            Assert.AreEqual("abc", values[0]);
            Assert.AreEqual("def", values[1]);
        }

        [TestMethod]
        public void TestZeroOrMoreExpression()
        {
            ZeroOrMoreExpression zeroOrMore = new ZeroOrMoreExpression(new LiteralExpression("a"));
            Result result = Parser.Parse(zeroOrMore, "aaab");
            Assert.IsTrue(result.Success);
            // It should consume only the "aaa" at the beginning.
            Assert.AreEqual(3, result.Position);
            List<object> values = result.Value as List<object>;
            Assert.IsNotNull(values);
            Assert.AreEqual(3, values.Count);
            Assert.AreEqual("a", values[0]);
            Assert.AreEqual("a", values[1]);
            Assert.AreEqual("a", values[2]);
        }

        [TestMethod]
        public void TestNullExpression()
        {
            Result result = Parser.Parse(null, "anything");
            Assert.IsFalse(result.Success);
            Assert.AreEqual(0, result.Position);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void TestNullInput()
        {
            LiteralExpression literal = new LiteralExpression("test");
            // With null input, the parser treats it as an empty string, so matching fails.
            Result result = Parser.Parse(literal, null);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(0, result.Position);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void TestLargeSequence()
        {
            Int32 count = 1000;
            List<Expression> expressions = new List<Expression>();
            StringBuilder sb = new StringBuilder();
            for (Int32 i = 0; i < count; i++)
            {
                expressions.Add(new LiteralExpression("ab"));
                sb.Append("ab");
            }

            String input = sb.ToString();
            SequenceExpression sequence = new SequenceExpression(expressions);
            Result result = Parser.Parse(sequence, input);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(input.Length, result.Position);
            List<object> values = result.Value as List<object>;
            Assert.IsNotNull(values);
            Assert.AreEqual(count, values.Count);
            for (Int32 i = 0; i < count; i++)
            {
                Assert.AreEqual("ab", values[i]);
            }
        }
    }
}