using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplePrecedenceParser;

namespace SimplePrecedenceParserTest
{
    [TestClass]
    public class PrecedenceParserTests
    {
        private double Evaluate(Expr expr)
        {
            if (expr is NumberExpr numberExpr)
            {
                return numberExpr.Value;
            }

            if (expr is BinaryExpr binaryExpr)
            {
                double left = Evaluate(binaryExpr.Left);
                double right = Evaluate(binaryExpr.Right);
                if (binaryExpr.Operator == '+')
                {
                    return left + right;
                }
                else if (binaryExpr.Operator == '-')
                {
                    return left - right;
                }
                else if (binaryExpr.Operator == '*')
                {
                    return left * right;
                }
                else if (binaryExpr.Operator == '/')
                {
                    return left / right;
                }
                else
                {
                    Assert.Fail("Unknown operator encountered: " + binaryExpr.Operator);
                    return 0;
                }
            }

            Assert.Fail("Unknown expression type encountered.");
            return 0;
        }

        [TestMethod]
        public void TestSimpleNumber()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse("42");
            Assert.IsTrue(expr is NumberExpr, "Expected a NumberExpr instance.");
            double value = Evaluate(expr);
            Assert.AreEqual(42.0, value, 0.0001);
        }

        [TestMethod]
        public void TestAddition()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse("1+2");
            double value = Evaluate(expr);
            Assert.AreEqual(3.0, value, 0.0001);
        }

        [TestMethod]
        public void TestMultiplication()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse("2*3");
            double value = Evaluate(expr);
            Assert.AreEqual(6.0, value, 0.0001);
        }

        [TestMethod]
        public void TestCombinedPrecedence()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse("1+2*3");
            double value = Evaluate(expr);
            Assert.AreEqual(7.0, value, 0.0001);
        }

        [TestMethod]
        public void TestParentheses()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse("(1+2)*3");
            double value = Evaluate(expr);
            Assert.AreEqual(9.0, value, 0.0001);
        }

        [TestMethod]
        public void TestWhitespace()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse("  4 +   5 * 6 ");
            double value = Evaluate(expr);
            Assert.AreEqual(34.0, value, 0.0001);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse("10-3");
            double value = Evaluate(expr);
            Assert.AreEqual(7.0, value, 0.0001);
        }

        [TestMethod]
        public void TestDivision()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse("10/2");
            double value = Evaluate(expr);
            Assert.AreEqual(5.0, value, 0.0001);
        }

        [TestMethod]
        public void TestMissingParenthesis()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse("(1+2");
            double value = Evaluate(expr);
            Assert.AreEqual(3.0, value, 0.0001);
        }

        [TestMethod]
        public void TestNullInput()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse(null);
            double value = Evaluate(expr);
            Assert.AreEqual(0.0, value, 0.0001);
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            PrecedenceParser parser = new PrecedenceParser();
            Expr expr = parser.Parse("");
            double value = Evaluate(expr);
            Assert.AreEqual(0.0, value, 0.0001);
        }

        [TestMethod]
        public void TestLargeAdditionChain()
        {
            PrecedenceParser parser = new PrecedenceParser();
            int count = 50;
            StringBuilder expressionBuilder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                expressionBuilder.Append("1");
                if (i < count - 1)
                {
                    expressionBuilder.Append("+");
                }
            }

            string expression = expressionBuilder.ToString();
            Expr expr = parser.Parse(expression);
            double value = Evaluate(expr);
            Assert.AreEqual((double)count, value, 0.0001);
        }
    }
}