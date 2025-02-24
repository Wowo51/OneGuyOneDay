using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperatorPrecedenceParser;

namespace OperatorPrecedenceParserTest
{
    [TestClass]
    public class OperatorPrecedenceParserTests
    {
        [TestMethod]
        public void TestNumber()
        {
            string input = "42";
            Parser parser = new Parser(input);
            ExpressionNode? node = parser.Parse();
            Assert.IsNotNull(node, "Parsed node should not be null.");
            double result = node!.Evaluate();
            Assert.AreEqual(42.0, result, 0.0001, "Number evaluation failed.");
        }

        [TestMethod]
        public void TestAddition()
        {
            string input = "2+3";
            Parser parser = new Parser(input);
            ExpressionNode? node = parser.Parse();
            Assert.IsNotNull(node, "Parsed node should not be null.");
            double result = node!.Evaluate();
            Assert.AreEqual(5.0, result, 0.0001, "Addition evaluation failed.");
        }

        [TestMethod]
        public void TestSubtraction()
        {
            string input = "10-3";
            Parser parser = new Parser(input);
            ExpressionNode? node = parser.Parse();
            Assert.IsNotNull(node);
            double result = node!.Evaluate();
            Assert.AreEqual(7.0, result, 0.0001, "Subtraction evaluation failed.");
        }

        [TestMethod]
        public void TestMultiplication()
        {
            string input = "3*4";
            Parser parser = new Parser(input);
            ExpressionNode? node = parser.Parse();
            Assert.IsNotNull(node);
            double result = node!.Evaluate();
            Assert.AreEqual(12.0, result, 0.0001, "Multiplication evaluation failed.");
        }

        [TestMethod]
        public void TestDivision()
        {
            string input = "12/3";
            Parser parser = new Parser(input);
            ExpressionNode? node = parser.Parse();
            Assert.IsNotNull(node);
            double result = node!.Evaluate();
            Assert.AreEqual(4.0, result, 0.0001, "Division evaluation failed.");
        }

        [TestMethod]
        public void TestOperatorPrecedence()
        {
            string input = "2+3*4";
            Parser parser = new Parser(input);
            ExpressionNode? node = parser.Parse();
            Assert.IsNotNull(node);
            double result = node!.Evaluate();
            Assert.AreEqual(14.0, result, 0.0001, "Operator precedence evaluation failed.");
        }

        [TestMethod]
        public void TestParentheses()
        {
            string input = "(2+3)*4";
            Parser parser = new Parser(input);
            ExpressionNode? node = parser.Parse();
            Assert.IsNotNull(node);
            double result = node!.Evaluate();
            Assert.AreEqual(20.0, result, 0.0001, "Parentheses evaluation failed.");
        }

        [TestMethod]
        public void TestDivisionByZero()
        {
            string input = "5/0";
            Parser parser = new Parser(input);
            ExpressionNode? node = parser.Parse();
            Assert.IsNotNull(node);
            double result = node!.Evaluate();
            Assert.AreEqual(0.0, result, 0.0001, "Division by zero evaluation failed.");
        }

        [TestMethod]
        public void TestFloatingPointNumbers()
        {
            string input = "3.14+2.86";
            Parser parser = new Parser(input);
            ExpressionNode? node = parser.Parse();
            Assert.IsNotNull(node);
            double result = node!.Evaluate();
            Assert.AreEqual(6.0, result, 0.0001, "Floating point addition evaluation failed.");
        }

        [TestMethod]
        public void TestLargeAdditionExpression()
        {
            int count = 1000;
            StringBuilder sb = new StringBuilder();
            sb.Append("1");
            for (int i = 1; i < count; i++)
            {
                sb.Append("+1");
            }

            string input = sb.ToString();
            Parser parser = new Parser(input);
            ExpressionNode? node = parser.Parse();
            Assert.IsNotNull(node, "Large expression node should not be null.");
            double result = node!.Evaluate();
            Assert.AreEqual((double)count, result, 0.0001, "Large addition expression evaluation failed.");
        }
    }
}