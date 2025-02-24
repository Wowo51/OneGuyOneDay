using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExponentiatingBySquaring;

namespace ExponentiatingBySquaringTest
{
    [TestClass]
    public class ExponentiatorTests
    {
        [TestMethod]
        public void TestZeroExponent()
        {
            BigInteger result = Exponentiator.Power(5, 0);
            Assert.AreEqual(BigInteger.One, result, "Any number raised to the power of 0 should be 1.");
        }

        [TestMethod]
        public void TestNegativeExponent()
        {
            BigInteger result = Exponentiator.Power(5, -3);
            Assert.AreEqual(BigInteger.Zero, result, "Negative exponent should return 0.");
        }

        [TestMethod]
        public void TestOneExponent()
        {
            BigInteger result = Exponentiator.Power(7, 1);
            Assert.AreEqual(new BigInteger(7), result, "Any number to the power of 1 should be itself.");
        }

        [TestMethod]
        public void TestEvenExponent()
        {
            BigInteger result = Exponentiator.Power(3, 4);
            Assert.AreEqual(new BigInteger(81), result, "3^4 should be 81.");
        }

        [TestMethod]
        public void TestOddExponent()
        {
            BigInteger result = Exponentiator.Power(3, 3);
            Assert.AreEqual(new BigInteger(27), result, "3^3 should be 27.");
        }

        [TestMethod]
        public void TestLargeExponent()
        {
            BigInteger result = Exponentiator.Power(2, 20);
            Assert.AreEqual(BigInteger.Parse("1048576"), result, "2^20 should be 1048576.");
        }

        [TestMethod]
        public void TestExponentSequence()
        {
            for (int i = 0; i <= 30; i++)
            {
                BigInteger expected = BigInteger.One;
                for (int j = 0; j < i; j++)
                {
                    expected *= 2;
                }

                BigInteger actual = Exponentiator.Power(2, i);
                Assert.AreEqual(expected, actual, $"2^{i} should equal {expected}.");
            }
        }
    }
}