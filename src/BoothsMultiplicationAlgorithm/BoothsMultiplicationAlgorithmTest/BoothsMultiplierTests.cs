using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoothsMultiplicationAlgorithm;
using System;

namespace BoothsMultiplicationAlgorithmTest
{
    [TestClass]
    public class BoothsMultiplierTests
    {
        [TestMethod]
        public void TestMultiply_Zero()
        {
            BoothsMultiplier multiplier = new BoothsMultiplier();
            for (int multiplicand = -100; multiplicand <= 100; multiplicand += 25)
            {
                long result = multiplier.Multiply(multiplicand, 0);
                Assert.AreEqual(0, result);
            }
        }

        [TestMethod]
        public void TestMultiply_PositiveNumbers()
        {
            BoothsMultiplier multiplier = new BoothsMultiplier();
            int multiplicand = 7;
            int multiplierVal = 3;
            long expected = (long)multiplicand * multiplierVal;
            long result = multiplier.Multiply(multiplicand, multiplierVal);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMultiply_NegativeNumbers()
        {
            BoothsMultiplier multiplier = new BoothsMultiplier();
            int multiplicand = -7;
            int multiplierVal = -3;
            long expected = (long)multiplicand * multiplierVal;
            long result = multiplier.Multiply(multiplicand, multiplierVal);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMultiply_PositiveAndNegative()
        {
            BoothsMultiplier multiplier = new BoothsMultiplier();
            int multiplicand = -7;
            int multiplierVal = 3;
            long expected = (long)multiplicand * multiplierVal;
            long result = multiplier.Multiply(multiplicand, multiplierVal);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMultiply_RandomData()
        {
            BoothsMultiplier multiplier = new BoothsMultiplier();
            System.Random random = new System.Random();
            for (int i = 0; i < 1000; i++)
            {
                int multiplicand = random.Next(int.MinValue, int.MaxValue);
                int multiplierVal = random.Next(int.MinValue, int.MaxValue);
                long expected = (long)multiplicand * multiplierVal;
                long result = multiplier.Multiply(multiplicand, multiplierVal);
                Assert.AreEqual(expected, result, $"Failed on inputs: {multiplicand}, {multiplierVal}");
            }
        }
    }
}