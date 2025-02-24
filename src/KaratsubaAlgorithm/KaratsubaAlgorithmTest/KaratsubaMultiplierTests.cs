using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaratsubaAlgorithm;

namespace KaratsubaAlgorithmTest
{
    [TestClass]
    public class KaratsubaMultiplierTests
    {
        private Random _random = new Random();
        [TestInitialize]
        public void Setup()
        {
            _random = new Random();
        }

        private BigInteger GenerateRandomBigInteger(int digits)
        {
            if (digits <= 0)
            {
                return 0;
            }

            // Ensure the first digit is non-zero.
            string numberStr = _random.Next(1, 10).ToString();
            for (int i = 1; i < digits; i++)
            {
                numberStr += _random.Next(0, 10).ToString();
            }

            return BigInteger.Parse(numberStr);
        }

        [TestMethod]
        public void TestMultiply_Zero()
        {
            BigInteger a = 0;
            BigInteger b = GenerateRandomBigInteger(10);
            BigInteger expected = a * b;
            BigInteger actual = KaratsubaMultiplier.Multiply(a, b);
            Assert.AreEqual(expected, actual, "Multiplying by zero should always yield zero.");
        }

        [TestMethod]
        public void TestMultiply_SmallNumbers()
        {
            BigInteger a = 7;
            BigInteger b = 8;
            BigInteger expected = a * b;
            BigInteger actual = KaratsubaMultiplier.Multiply(a, b);
            Assert.AreEqual(expected, actual, "Multiplying small numbers failed.");
        }

        [TestMethod]
        public void TestMultiply_SingleDigitBaseCase()
        {
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    BigInteger a = i;
                    BigInteger b = j;
                    BigInteger expected = a * b;
                    BigInteger actual = KaratsubaMultiplier.Multiply(a, b);
                    Assert.AreEqual(expected, actual, $"Failed multiplying {a} and {b}.");
                }
            }
        }

        [TestMethod]
        public void TestMultiply_LargeNumbers()
        {
            BigInteger a = GenerateRandomBigInteger(50);
            BigInteger b = GenerateRandomBigInteger(50);
            BigInteger expected = a * b;
            BigInteger actual = KaratsubaMultiplier.Multiply(a, b);
            Assert.AreEqual(expected, actual, "Multiplying large numbers failed.");
        }

        [TestMethod]
        public void TestMultiply_RandomNumbers()
        {
            for (int test = 0; test < 10; test++)
            {
                int digitsA = _random.Next(1, 101);
                int digitsB = _random.Next(1, 101);
                BigInteger a = GenerateRandomBigInteger(digitsA);
                BigInteger b = GenerateRandomBigInteger(digitsB);
                BigInteger expected = a * b;
                BigInteger actual = KaratsubaMultiplier.Multiply(a, b);
                Assert.AreEqual(expected, actual, $"Random test {test} failed with digit lengths: {digitsA} and {digitsB}.");
            }
        }

        [TestMethod]
        public void TestMultiply_SpecificNumbers()
        {
            BigInteger a = BigInteger.Parse("123456789");
            BigInteger b = BigInteger.Parse("987654321");
            BigInteger expected = a * b;
            BigInteger actual = KaratsubaMultiplier.Multiply(a, b);
            Assert.AreEqual(expected, actual, "Multiplying specific known numbers failed.");
        }

        [TestMethod]
        public void TestMultiply_NegativeNumbers()
        {
            // Test multiplying two negative numbers.
            BigInteger a = -GenerateRandomBigInteger(20);
            BigInteger b = -GenerateRandomBigInteger(20);
            BigInteger expected = a * b;
            BigInteger actual = KaratsubaMultiplier.Multiply(a, b);
            Assert.AreEqual(expected, actual, "Multiplying two negatives failed.");
            // Test multiplying a negative number with a positive number.
            a = -GenerateRandomBigInteger(20);
            b = GenerateRandomBigInteger(20);
            expected = a * b;
            actual = KaratsubaMultiplier.Multiply(a, b);
            Assert.AreEqual(expected, actual, "Multiplying a negative with a positive failed.");
        }
    }
}