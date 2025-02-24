using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HammingWeight;

namespace HammingWeightTest
{
    [TestClass]
    public class HammingCalculatorTests
    {
        [DataTestMethod]
        [DataRow(0u, 0)]
        [DataRow(1u, 1)]
        [DataRow(2u, 1)]
        [DataRow(3u, 2)]
        [DataRow(4u, 1)]
        [DataRow(7u, 3)]
        [DataRow(15u, 4)]
        public void Compute_KnownValues(uint number, int expected)
        {
            int result = HammingWeightCalculator.Compute(number);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Compute_RandomValues()
        {
            const int testCount = 1000;
            Random random = new Random(0);
            for (int index = 0; index < testCount; index++)
            {
                uint number = ((uint)random.Next()) | (((uint)random.Next()) << 16);
                int expected = CountBits(number);
                int result = HammingWeightCalculator.Compute(number);
                Assert.AreEqual(expected, result, "Failed for number: " + number);
            }
        }

        private int CountBits(uint number)
        {
            int count = 0;
            while (number != 0u)
            {
                count += (int)(number & 1);
                number >>= 1;
            }

            return count;
        }

        [TestMethod]
        public void Compute_AllBitsSet()
        {
            uint number = 0xFFFFFFFF;
            int result = HammingWeightCalculator.Compute(number);
            Assert.AreEqual(32, result);
        }
    }
}