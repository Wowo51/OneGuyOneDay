using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryGCD;

namespace BinaryGCDTest
{
    [TestClass]
    public class GcdCalculatorTests
    {
        [TestMethod]
        public void Test_GcdOfZeroAndNumber()
        {
            int result = GcdCalculator.BinaryGcd(0, 5);
            Assert.AreEqual(5, result);
            result = GcdCalculator.BinaryGcd(5, 0);
            Assert.AreEqual(5, result);
            result = GcdCalculator.BinaryGcd(0, 0);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_GcdOfPositiveNumbers()
        {
            int result = GcdCalculator.BinaryGcd(48, 18);
            Assert.AreEqual(6, result);
            result = GcdCalculator.BinaryGcd(54, 24);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Test_GcdOfNegativeNumbers()
        {
            int result = GcdCalculator.BinaryGcd(-48, 18);
            Assert.AreEqual(6, result);
            result = GcdCalculator.BinaryGcd(48, -18);
            Assert.AreEqual(6, result);
            result = GcdCalculator.BinaryGcd(-48, -18);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Test_GcdCommutativeProperty()
        {
            int a = 270;
            int b = 192;
            int gcd1 = GcdCalculator.BinaryGcd(a, b);
            int gcd2 = GcdCalculator.BinaryGcd(b, a);
            Assert.AreEqual(gcd1, gcd2);
        }

        [TestMethod]
        public void Test_SyntheticLargeTestData()
        {
            for (int i = 1; i <= 100; i++)
            {
                for (int j = 1; j <= 100; j++)
                {
                    int gcd = GcdCalculator.BinaryGcd(i, j);
                    Assert.AreEqual(0, i % gcd);
                    Assert.AreEqual(0, j % gcd);
                    for (int k = gcd + 1; k <= (i < j ? i : j); k++)
                    {
                        if ((i % k) == 0 && (j % k) == 0)
                        {
                            Assert.Fail("Found a divisor larger than computed gcd.");
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void Test_GcdOfPrimeNumbers()
        {
            int result = GcdCalculator.BinaryGcd(13, 19);
            Assert.AreEqual(1, result);
        }
    }
}