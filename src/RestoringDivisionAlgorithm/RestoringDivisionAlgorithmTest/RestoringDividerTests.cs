using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestoringDivisionAlgorithm;

namespace RestoringDivisionAlgorithmTest
{
    [TestClass]
    public class RestoringDividerTests
    {
        [TestMethod]
        public void TestBasicDivision()
        {
            uint dividend = 37U;
            uint divisor = 5U;
            (uint quotient, uint remainder) = RestoringDivider.Divide(dividend, divisor);
            Assert.AreEqual(7U, quotient, "Expected quotient of 37 ÷ 5 is 7.");
            Assert.AreEqual(2U, remainder, "Expected remainder of 37 ÷ 5 is 2.");
        }

        [TestMethod]
        public void TestDivisionByZero()
        {
            uint dividend = 100U;
            uint divisor = 0U;
            (uint quotient, uint remainder) = RestoringDivider.Divide(dividend, divisor);
            Assert.AreEqual(0U, quotient, "Division by zero should yield quotient 0.");
            Assert.AreEqual(100U, remainder, "Division by zero should yield remainder equal to dividend.");
        }

        [TestMethod]
        public void TestZeroDividend()
        {
            uint dividend = 0U;
            uint divisor = 5U;
            (uint quotient, uint remainder) = RestoringDivider.Divide(dividend, divisor);
            Assert.AreEqual(0U, quotient, "Zero dividend should produce a zero quotient.");
            Assert.AreEqual(0U, remainder, "Zero dividend should produce a zero remainder.");
        }

        [TestMethod]
        public void TestDivisorGreaterThanDividend()
        {
            uint dividend = 3U;
            uint divisor = 5U;
            (uint quotient, uint remainder) = RestoringDivider.Divide(dividend, divisor);
            Assert.AreEqual(0U, quotient, "When divisor is greater than dividend, the quotient should be 0.");
            Assert.AreEqual(3U, remainder, "When divisor is greater than dividend, the remainder should equal the dividend.");
        }

        [TestMethod]
        public void TestRandomDivisions()
        {
            System.Random random = new System.Random(42);
            int testCount = 1000;
            for (int i = 0; i < testCount; i++)
            {
                uint dividend = (uint)random.Next(0, 100000);
                uint divisor = (uint)random.Next(1, 1000); // Avoid division by zero
                (uint quotient, uint remainder) = RestoringDivider.Divide(dividend, divisor);
                uint expectedQuotient = dividend / divisor;
                uint expectedRemainder = dividend % divisor;
                Assert.AreEqual(expectedQuotient, quotient, $"Mismatch for dividend {dividend} and divisor {divisor}");
                Assert.AreEqual(expectedRemainder, remainder, $"Mismatch for dividend {dividend} and divisor {divisor}");
            }
        }

        [TestMethod]
        public void TestMaxValueDivision()
        {
            uint dividend = uint.MaxValue;
            uint divisor = 2U;
            (uint quotient, uint remainder) = RestoringDivider.Divide(dividend, divisor);
            uint expectedQuotient = dividend / divisor;
            uint expectedRemainder = dividend % divisor;
            Assert.AreEqual(expectedQuotient, quotient, "Max value division quotient mismatch.");
            Assert.AreEqual(expectedRemainder, remainder, "Max value division remainder mismatch.");
        }
    }
}