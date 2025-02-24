using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NonRestoringDivision;

namespace NonRestoringDivisionTest
{
    [TestClass]
    public class NonRestoringDividerTests
    {
        [TestMethod]
        public void TestDivideByZero()
        {
            int dividend = 10;
            int divisor = 0;
            DivisionResult result = NonRestoringDivider.Divide(dividend, divisor);
            Assert.AreEqual(0, result.Quotient, "Quotient for division by zero");
            Assert.AreEqual(dividend, result.Remainder, "Remainder for division by zero");
        }

        [TestMethod]
        public void TestDividePositive()
        {
            int dividend = 7;
            int divisor = 2;
            DivisionResult result = NonRestoringDivider.Divide(dividend, divisor);
            int expectedQuotient = dividend / divisor;
            int expectedRemainder = dividend - expectedQuotient * divisor;
            Assert.AreEqual(expectedQuotient, result.Quotient, "Positive division quotient");
            Assert.AreEqual(expectedRemainder, result.Remainder, "Positive division remainder");
        }

        [TestMethod]
        public void TestDivideNegativeDividend()
        {
            int dividend = -7;
            int divisor = 2;
            DivisionResult result = NonRestoringDivider.Divide(dividend, divisor);
            int expectedQuotient = dividend / divisor;
            int expectedRemainder = dividend - expectedQuotient * divisor;
            Assert.AreEqual(expectedQuotient, result.Quotient, "Negative dividend quotient");
            Assert.AreEqual(expectedRemainder, result.Remainder, "Negative dividend remainder");
        }

        [TestMethod]
        public void TestDivideNegativeDivisor()
        {
            int dividend = 7;
            int divisor = -2;
            DivisionResult result = NonRestoringDivider.Divide(dividend, divisor);
            int expectedQuotient = dividend / divisor;
            int expectedRemainder = dividend - expectedQuotient * divisor;
            Assert.AreEqual(expectedQuotient, result.Quotient, "Negative divisor quotient");
            Assert.AreEqual(expectedRemainder, result.Remainder, "Negative divisor remainder");
        }

        [TestMethod]
        public void TestDivideBothNegatives()
        {
            int dividend = -7;
            int divisor = -2;
            DivisionResult result = NonRestoringDivider.Divide(dividend, divisor);
            int expectedQuotient = dividend / divisor;
            int expectedRemainder = dividend - expectedQuotient * divisor;
            Assert.AreEqual(expectedQuotient, result.Quotient, "Both negatives quotient");
            Assert.AreEqual(expectedRemainder, result.Remainder, "Both negatives remainder");
        }

        [TestMethod]
        public void TestDivideZeroDividend()
        {
            int dividend = 0;
            int divisor = 5;
            DivisionResult result = NonRestoringDivider.Divide(dividend, divisor);
            Assert.AreEqual(0, result.Quotient, "Zero dividend quotient");
            Assert.AreEqual(0, result.Remainder, "Zero dividend remainder");
        }

        [TestMethod]
        public void TestRandomDivisions()
        {
            Random random = new Random(12345);
            int iterations = 100;
            for (int i = 0; i < iterations; i++)
            {
                int dividend = random.Next(-10000, 10000);
                int divisor = random.Next(-10000, 10000);
                if (divisor == 0)
                {
                    divisor = 1;
                }

                DivisionResult result = NonRestoringDivider.Divide(dividend, divisor);
                int expectedQuotient = dividend / divisor;
                int expectedRemainder = dividend - expectedQuotient * divisor;
                Assert.AreEqual(expectedQuotient, result.Quotient, $"Random test iteration {i}: Quotient mismatch for {dividend} / {divisor}");
                Assert.AreEqual(expectedRemainder, result.Remainder, $"Random test iteration {i}: Remainder mismatch for {dividend} / {divisor}");
            }
        }
    }
}