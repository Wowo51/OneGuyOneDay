using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using FurerAlgorithm;

namespace FurerAlgorithmTest
{
    [TestClass]
    public class FurerAlgorithmTests
    {
        [TestMethod]
        public void Multiply_WithZero_ReturnsZero()
        {
            BigInteger zero = BigInteger.Zero;
            BigInteger number = new BigInteger(123456789);
            BigInteger result = FurerMultiplier.Multiply(zero, number);
            Assert.AreEqual(BigInteger.Zero, result);
            result = FurerMultiplier.Multiply(number, zero);
            Assert.AreEqual(BigInteger.Zero, result);
        }

        [TestMethod]
        public void Multiply_Basic()
        {
            BigInteger a = new BigInteger(1234);
            BigInteger b = new BigInteger(5678);
            BigInteger expected = a * b;
            BigInteger result = FurerMultiplier.Multiply(a, b);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Multiply_NegativeNumbers()
        {
            BigInteger a = new BigInteger(-123456);
            BigInteger b = new BigInteger(654321);
            BigInteger expected = a * b;
            BigInteger result = FurerMultiplier.Multiply(a, b);
            Assert.AreEqual(expected, result);
            a = new BigInteger(-98765);
            b = new BigInteger(-43210);
            expected = a * b;
            result = FurerMultiplier.Multiply(a, b);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Multiply_LargeRandomNumbers()
        {
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                BigInteger a = GenerateRandomBigInteger(random, 50 + random.Next(50));
                BigInteger b = GenerateRandomBigInteger(random, 50 + random.Next(50));
                BigInteger expected = a * b;
                BigInteger result = FurerMultiplier.Multiply(a, b);
                Assert.AreEqual(expected, result);
            }
        }

        private BigInteger GenerateRandomBigInteger(Random random, int digitCount)
        {
            if (digitCount <= 0)
            {
                return BigInteger.Zero;
            }

            char[] digits = new char[digitCount];
            digits[0] = (char)('1' + random.Next(9));
            for (int i = 1; i < digitCount; i++)
            {
                digits[i] = (char)('0' + random.Next(10));
            }

            string numberString = new string (digits);
            BigInteger result = BigInteger.Parse(numberString);
            if (random.Next(2) == 0)
            {
                result = -result;
            }

            return result;
        }
    }
}