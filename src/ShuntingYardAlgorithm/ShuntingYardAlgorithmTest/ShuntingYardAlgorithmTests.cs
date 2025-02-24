using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShuntingYardAlgorithm;

namespace ShuntingYardAlgorithmTest
{
    [TestClass]
    public class ShuntingYardAlgorithmTests
    {
        [TestMethod]
        public void ConvertToPostfix_NullInput_ReturnsEmpty()
        {
            string result = ShuntingYardConverter.ConvertToPostfix(null);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ConvertToPostfix_EmptyInput_ReturnsEmpty()
        {
            string result = ShuntingYardConverter.ConvertToPostfix("");
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ConvertToPostfix_SingleNumber_ReturnsSameNumber()
        {
            string result = ShuntingYardConverter.ConvertToPostfix("123");
            Assert.AreEqual("123", result);
        }

        [TestMethod]
        public void ConvertToPostfix_SimpleAddition_ReturnsCorrectPostfix()
        {
            string result = ShuntingYardConverter.ConvertToPostfix("3+4");
            Assert.AreEqual("3 4 +", result);
        }

        [TestMethod]
        public void ConvertToPostfix_OperatorPrecedence_ReturnsCorrectPostfix()
        {
            string result = ShuntingYardConverter.ConvertToPostfix("3+4*5");
            Assert.AreEqual("3 4 5 * +", result);
        }

        [TestMethod]
        public void ConvertToPostfix_Parentheses_ReturnsCorrectPostfix()
        {
            string result = ShuntingYardConverter.ConvertToPostfix("a*(b+c)");
            Assert.AreEqual("a b c + *", result);
        }

        [TestMethod]
        public void ConvertToPostfix_RightAssociativeExponent_ReturnsCorrectPostfix()
        {
            string result = ShuntingYardConverter.ConvertToPostfix("2^3^2");
            Assert.AreEqual("2 3 2 ^ ^", result);
        }

        [TestMethod]
        public void ConvertToPostfix_ComplexExpression_ReturnsCorrectPostfix()
        {
            string result = ShuntingYardConverter.ConvertToPostfix("3+4*2/(1-5)^2^3");
            Assert.AreEqual("3 4 2 * 1 5 - 2 3 ^ ^ / +", result);
        }

        [TestMethod]
        public void ConvertToPostfix_DecimalNumbers_ReturnsCorrectPostfix()
        {
            string result = ShuntingYardConverter.ConvertToPostfix("3.5+4.2*2");
            Assert.AreEqual("3.5 4.2 2 * +", result);
        }

        [TestMethod]
        public void ConvertToPostfix_Variables_ReturnsCorrectPostfix()
        {
            string result = ShuntingYardConverter.ConvertToPostfix("x1+y2");
            Assert.AreEqual("x1 y2 +", result);
        }

        [TestMethod]
        public void ConvertToPostfix_LargeExpression_ReturnsCorrectPostfix()
        {
            int numberOfTerms = 100;
            StringBuilder inputBuilder = new StringBuilder();
            for (int i = 0; i < numberOfTerms; i++)
            {
                if (i > 0)
                {
                    inputBuilder.Append("+");
                }

                inputBuilder.Append("1");
            }

            string input = inputBuilder.ToString();
            string result = ShuntingYardConverter.ConvertToPostfix(input);
            StringBuilder expectedBuilder = new StringBuilder();
            expectedBuilder.Append("1");
            for (int i = 1; i < numberOfTerms; i++)
            {
                expectedBuilder.Append(" 1 +");
            }

            string expected = expectedBuilder.ToString();
            Assert.AreEqual(expected, result);
        }
    }
}