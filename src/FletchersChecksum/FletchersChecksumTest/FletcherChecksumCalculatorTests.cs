using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FletchersChecksum;

namespace FletchersChecksumTest
{
    [TestClass]
    public class FletcherChecksumCalculatorTests
    {
        private static ushort ComputeFletcher16Reference(byte[] input)
        {
            if (input == null || input.Length == 0)
            {
                return 0;
            }

            int sum1 = 0;
            int sum2 = 0;
            for (int i = 0; i < input.Length; i++)
            {
                sum1 = (sum1 + input[i]) % 255;
                sum2 = (sum2 + sum1) % 255;
            }

            return (ushort)((sum2 << 8) | sum1);
        }

        [TestMethod]
        public void ComputeFletcher16_NullInput_ReturnsZero()
        {
            ushort result = FletcherChecksumCalculator.ComputeFletcher16(null);
            Assert.AreEqual((ushort)0, result);
        }

        [TestMethod]
        public void ComputeFletcher16_EmptyInput_ReturnsZero()
        {
            byte[] input = new byte[0];
            ushort result = FletcherChecksumCalculator.ComputeFletcher16(input);
            Assert.AreEqual((ushort)0, result);
        }

        [TestMethod]
        public void ComputeFletcher16_SingleElement_ReturnsCorrectChecksum()
        {
            byte[] input = new byte[]
            {
                1
            };
            // Expected: (1 << 8) | 1 = 257.
            ushort result = FletcherChecksumCalculator.ComputeFletcher16(input);
            Assert.AreEqual((ushort)257, result);
        }

        [TestMethod]
        public void ComputeFletcher16_TwoElements_ReturnsCorrectChecksum()
        {
            byte[] input = new byte[]
            {
                1,
                2
            };
            // Calculation:
            // i=0: sum1 = 1, sum2 = 1;
            // i=1: sum1 = (1+2)%255 = 3, sum2 = (1+3)%255 = 4;
            // Expected: (4 << 8) | 3 = 1027.
            ushort result = FletcherChecksumCalculator.ComputeFletcher16(input);
            Assert.AreEqual((ushort)1027, result);
        }

        [TestMethod]
        public void ComputeFletcher16_MultipleElements_ReturnsCorrectChecksum()
        {
            byte[] input = new byte[]
            {
                1,
                2,
                3,
                4,
                5
            };
            // Calculation:
            // i=0: sum1 = 1, sum2 = 1;
            // i=1: sum1 = 3, sum2 = 4;
            // i=2: sum1 = 6, sum2 = 10;
            // i=3: sum1 = 10, sum2 = 20;
            // i=4: sum1 = 15, sum2 = 35;
            // Expected: (35 << 8) | 15 = 8975.
            ushort result = FletcherChecksumCalculator.ComputeFletcher16(input);
            Assert.AreEqual((ushort)8975, result);
        }

        [TestMethod]
        public void ComputeFletcher16_RolloverValues_ReturnsZeroWhenSumsRollover()
        {
            byte[] input = new byte[]
            {
                255,
                255
            };
            // Calculation:
            // i=0: sum1 = (0+255)%255 = 0, sum2 = (0+0)%255 = 0;
            // i=1: sum1 remains 0, sum2 remains 0;
            // Expected: 0.
            ushort result = FletcherChecksumCalculator.ComputeFletcher16(input);
            Assert.AreEqual((ushort)0, result);
        }

        [TestMethod]
        public void ComputeFletcher16_RandomDataConsistency()
        {
            byte[] input = new byte[10000];
            Random random = new Random(12345);
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = (byte)random.Next(0, 256);
            }

            ushort expected = ComputeFletcher16Reference(input);
            ushort result = FletcherChecksumCalculator.ComputeFletcher16(input);
            Assert.AreEqual(expected, result);
        }
    }
}