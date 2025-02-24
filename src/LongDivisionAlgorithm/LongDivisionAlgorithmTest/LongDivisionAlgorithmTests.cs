using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LongDivisionAlgorithm;

namespace LongDivisionAlgorithmTest
{
    [TestClass]
    public class LongDivisionAlgorithmTests
    {
        [TestMethod]
        public void TestDivisionByZero()
        {
            int dividend = 123;
            int divisor = 0;
            string expected = "Division by zero is undefined";
            string result = LongDivision.FormatDivision(dividend, divisor);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDividendLessThanDivisor()
        {
            int dividend = 5;
            int divisor = 10;
            string expected = "5\n10|0";
            string result = LongDivision.FormatDivision(dividend, divisor);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestStandardDivision()
        {
            int dividend = 78945;
            int divisor = 4;
            string expected = "_78945\n" + "4    |19736\n" + "-    |-----\n" + "_38\n" + "36\n" + "--\n" + " _29\n" + " 28\n" + " --\n" + "  _14\n" + "  12\n" + "  --\n" + "   _25\n" + "   24\n" + "   --\n" + "    1";
            string result = LongDivision.FormatDivision(dividend, divisor);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestRandomDivisions()
        {
            Random random = new Random(0);
            for (int i = 0; i < 10; i++)
            {
                int dividend = random.Next(1, 10000);
                int divisor = random.Next(1, 100);
                string result = LongDivision.FormatDivision(dividend, divisor);
                if (dividend < divisor)
                {
                    string expected = dividend.ToString() + "\n" + divisor.ToString() + "|0";
                    Assert.AreEqual(expected, result);
                }
                else
                {
                    string[] lines = result.Split('\n');
                    int barIndex = lines[1].LastIndexOf('|');
                    string quotientStr = lines[1].Substring(barIndex + 1);
                    int expectedQuotient = dividend / divisor;
                    Assert.AreEqual(expectedQuotient, int.Parse(quotientStr));
                    string lastLine = lines[lines.Length - 1].Trim();
                    int expectedRemainder = dividend % divisor;
                    Assert.AreEqual(expectedRemainder, int.Parse(lastLine));
                }
            }
        }
    }
}