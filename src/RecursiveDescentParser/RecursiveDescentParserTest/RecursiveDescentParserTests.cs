using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecursiveDescentParser;

namespace RecursiveDescentParserTest
{
    [TestClass]
    public class RecursiveDescentParserTests
    {
        [TestMethod]
        public void TestAddition()
        {
            Parser parser = new Parser();
            double result = parser.Parse("1+2");
            Assert.AreEqual(3.0, result);
            Assert.IsNull(parser.Error);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            Parser parser = new Parser();
            double result = parser.Parse("5-2");
            Assert.AreEqual(3.0, result);
            Assert.IsNull(parser.Error);
        }

        [TestMethod]
        public void TestMultiplication()
        {
            Parser parser = new Parser();
            double result = parser.Parse("3*4");
            Assert.AreEqual(12.0, result);
            Assert.IsNull(parser.Error);
        }

        [TestMethod]
        public void TestDivision()
        {
            Parser parser = new Parser();
            double result = parser.Parse("10/2");
            Assert.AreEqual(5.0, result);
            Assert.IsNull(parser.Error);
        }

        [TestMethod]
        public void TestOperatorPrecedence()
        {
            Parser parser = new Parser();
            double result = parser.Parse("1+2*3");
            Assert.AreEqual(7.0, result);
            Assert.IsNull(parser.Error);
        }

        [TestMethod]
        public void TestParentheses()
        {
            Parser parser = new Parser();
            double result = parser.Parse("(1+2)*3");
            Assert.AreEqual(9.0, result);
            Assert.IsNull(parser.Error);
        }

        [TestMethod]
        public void TestDivisionByZero()
        {
            Parser parser = new Parser();
            double result = parser.Parse("1/0");
            Assert.AreEqual(0.0, result);
            Assert.AreEqual("Division by zero.", parser.Error);
        }

        [TestMethod]
        public void TestMissingParenthesis()
        {
            Parser parser = new Parser();
            double result = parser.Parse("(1+2");
            Assert.AreEqual(0.0, result);
            Assert.AreEqual("Missing closing parenthesis.", parser.Error);
        }

        [TestMethod]
        public void TestUnexpectedToken()
        {
            Parser parser = new Parser();
            double result = parser.Parse("abc");
            Assert.AreEqual(0.0, result);
            Assert.AreEqual("Unexpected token.", parser.Error);
        }

        [TestMethod]
        public void TestComplexExpression()
        {
            Parser parser = new Parser();
            double result = parser.Parse("1+2-3*4/2+(6/3)");
            double expected = 1.0 + 2.0 - 3.0 * 4.0 / 2.0 + (6.0 / 3.0);
            Assert.AreEqual(expected, result, 0.0001);
            Assert.IsNull(parser.Error);
        }

        [TestMethod]
        public void TestLargeDataset()
        {
            Parser parser = new Parser();
            StringBuilder inputBuilder = new StringBuilder();
            double expected = 0;
            int count = 1000;
            for (int i = 0; i < count; i++)
            {
                inputBuilder.Append("1+");
                expected += 1;
            }

            inputBuilder.Append("0");
            double result = parser.Parse(inputBuilder.ToString());
            Assert.AreEqual(expected, result, 0.0001);
            Assert.IsNull(parser.Error);
        }
    }
}