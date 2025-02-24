using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using System.Text;
using ToomCookMultiplication;

namespace ToomCookMultiplicationTest
{
    [TestClass]
    public class ToomCookMultiplierTests
    {
        private static string GenerateRandomNumericString(int length, Random random)
        {
            if (length <= 0)
            {
                return "0";
            }

            StringBuilder sb = new StringBuilder();
            int firstDigit = random.Next(1, 10);
            sb.Append(firstDigit);
            for (int i = 1; i < length; i++)
            {
                int digit = random.Next(0, 10);
                sb.Append(digit);
            }

            return sb.ToString();
        }

        [TestMethod]
        public void TestMultiply_NullInputs()
        {
            string result = ToomCookMultiplier.Multiply(null, null);
            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public void TestMultiply_EmptyInputs()
        {
            string result = ToomCookMultiplier.Multiply("   ", " ");
            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public void TestMultiply_Zero()
        {
            string result1 = ToomCookMultiplier.Multiply("0", "12345");
            Assert.AreEqual("0", result1);
            string result2 = ToomCookMultiplier.Multiply("12345", "0");
            Assert.AreEqual("0", result2);
        }

        [TestMethod]
        public void TestMultiply_SmallNumbers()
        {
            string num1 = "123";
            string num2 = "456";
            BigInteger expected = BigInteger.Parse(num1) * BigInteger.Parse(num2);
            string result = ToomCookMultiplier.Multiply(num1, num2);
            Assert.AreEqual(expected.ToString(), result);
        }

        [TestMethod]
        public void TestMultiply_LeadingZeros()
        {
            string num1 = "000123";
            string num2 = "0456";
            BigInteger expected = BigInteger.Parse("123") * BigInteger.Parse("456");
            string result = ToomCookMultiplier.Multiply(num1, num2);
            Assert.AreEqual(expected.ToString(), result);
        }

        [TestMethod]
        public void TestMultiply_DifferentLengths()
        {
            string num1 = "123456789";
            string num2 = "123";
            BigInteger expected = BigInteger.Parse(num1) * BigInteger.Parse(num2);
            string result = ToomCookMultiplier.Multiply(num1, num2);
            Assert.AreEqual(expected.ToString(), result);
        }

        [TestMethod]
        public void TestMultiply_LargeNumbers()
        {
            string num1 = "12345678901234567890";
            string num2 = "98765432109876543210";
            BigInteger expected = BigInteger.Parse(num1) * BigInteger.Parse(num2);
            string result = ToomCookMultiplier.Multiply(num1, num2);
            Assert.AreEqual(expected.ToString(), result);
        }

        [TestMethod]
        public void TestMultiply_RandomLargeNumbers()
        {
            Random random = new Random();
            string num1 = GenerateRandomNumericString(50, random);
            string num2 = GenerateRandomNumericString(50, random);
            BigInteger expected = BigInteger.Parse(num1) * BigInteger.Parse(num2);
            string result = ToomCookMultiplier.Multiply(num1, num2);
            Assert.AreEqual(expected.ToString(), result);
        }
    }
}