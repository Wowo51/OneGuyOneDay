using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EuclideanAlgorithm;

namespace EuclideanAlgorithmTest
{
    [TestClass]
    public class GcdCalculatorTests
    {
        [TestMethod]
        public void Compute_BasicTest()
        {
            int result = GcdCalculator.Compute(48, 18);
            Assert.AreEqual(6, Math.Abs(result), "GCD of 48 and 18 should be 6");
        }

        [TestMethod]
        public void Compute_ZeroInputTest()
        {
            int result1 = GcdCalculator.Compute(7, 0);
            int result2 = GcdCalculator.Compute(0, 7);
            int result3 = GcdCalculator.Compute(0, 0);
            Assert.AreEqual(7, Math.Abs(result1), "GCD of 7 and 0 should be 7");
            Assert.AreEqual(7, Math.Abs(result2), "GCD of 0 and 7 should be 7");
            Assert.AreEqual(0, result3, "GCD of 0 and 0 should be 0");
        }

        [TestMethod]
        public void Compute_EqualNumbersTest()
        {
            int number = 42;
            int result = GcdCalculator.Compute(number, number);
            Assert.AreEqual(number, Math.Abs(result), "GCD of equal numbers should be the number itself");
        }

        [TestMethod]
        public void Compute_NegativeInputTest()
        {
            int result1 = GcdCalculator.Compute(-24, 18);
            int result2 = GcdCalculator.Compute(24, -18);
            int result3 = GcdCalculator.Compute(-24, -18);
            Assert.AreEqual(6, Math.Abs(result1), "GCD of -24 and 18 should be 6");
            Assert.AreEqual(6, Math.Abs(result2), "GCD of 24 and -18 should be 6");
            Assert.AreEqual(6, Math.Abs(result3), "GCD of -24 and -18 should be 6");
        }

        [TestMethod]
        public void Compute_CommutativeTest()
        {
            int a = 60;
            int b = 24;
            int result1 = GcdCalculator.Compute(a, b);
            int result2 = GcdCalculator.Compute(b, a);
            Assert.AreEqual(Math.Abs(result1), Math.Abs(result2), "GCD should be commutative");
        }

        private int ExpectedGcd(int a, int b)
        {
            int absA = Math.Abs(a);
            int absB = Math.Abs(b);
            while (absB != 0)
            {
                int temp = absB;
                absB = absA % absB;
                absA = temp;
            }

            return absA;
        }

        [TestMethod]
        public void Compute_RandomTest()
        {
            Random random = new Random(0);
            for (int i = 0; i < 100; i++)
            {
                int a = random.Next(-1000, 1000);
                int b = random.Next(-1000, 1000);
                int expected = ExpectedGcd(a, b);
                int actual = Math.Abs(GcdCalculator.Compute(a, b));
                Assert.AreEqual(expected, actual, $"GCD of {a} and {b} should be {expected}");
            }
        }
    }
}