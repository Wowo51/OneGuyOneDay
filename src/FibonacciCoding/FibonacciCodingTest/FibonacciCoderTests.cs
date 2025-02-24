using Microsoft.VisualStudio.TestTools.UnitTesting;
using FibonacciCoding;
using System;

namespace FibonacciCodingTest
{
    [TestClass]
    public class FibonacciCoderTests
    {
        [TestMethod]
        public void Encode_NegativeOrZero_ReturnsZero()
        {
            string resultZero = FibonacciCoder.Encode(0);
            string resultNegative = FibonacciCoder.Encode(-5);
            Assert.AreEqual("0", resultZero);
            Assert.AreEqual("0", resultNegative);
        }

        [TestMethod]
        public void Encode_For_1_Returns11()
        {
            string encoded = FibonacciCoder.Encode(1);
            Assert.AreEqual("11", encoded);
        }

        [TestMethod]
        public void Encode_For_6_Returns10011()
        {
            string encoded = FibonacciCoder.Encode(6);
            Assert.AreEqual("10011", encoded);
        }

        [TestMethod]
        public void Decode_InvalidString_ReturnsZero()
        {
            int decodedEmpty = FibonacciCoder.Decode("");
            int decodedNull = FibonacciCoder.Decode(null);
            Assert.AreEqual(0, decodedEmpty);
            Assert.AreEqual(0, decodedNull);
        }

        [TestMethod]
        public void EncodeAndDecode_RoundTrip_Test()
        {
            for (int number = 1; number <= 100; number++)
            {
                string encoded = FibonacciCoder.Encode(number);
                int decoded = FibonacciCoder.Decode(encoded);
                Assert.AreEqual(number, decoded);
            }
        }

        [TestMethod]
        public void EncodeAndDecode_Large_RoundTrip_Test()
        {
            for (int number = 1; number <= 500; number++)
            {
                string encoded = FibonacciCoder.Encode(number);
                int decoded = FibonacciCoder.Decode(encoded);
                Assert.AreEqual(number, decoded, $"Failed for number: {number}");
            }
        }

        [TestMethod]
        public void EncodeAndDecode_RandomRoundTrip_Test()
        {
            Random random = new Random(0);
            for (int i = 0; i < 50; i++)
            {
                int number = random.Next(1, 1000);
                string encoded = FibonacciCoder.Encode(number);
                int decoded = FibonacciCoder.Decode(encoded);
                Assert.AreEqual(number, decoded, $"Failed for random number: {number}");
            }
        }
    }
}