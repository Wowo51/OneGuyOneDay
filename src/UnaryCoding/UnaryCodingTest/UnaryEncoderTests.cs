using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnaryCoding;

namespace UnaryCodingTest
{
    [TestClass]
    public class UnaryEncoderTests
    {
        [TestMethod]
        public void Encode_Negative_ReturnsEmpty()
        {
            string result = UnaryEncoder.Encode(-5);
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void Encode_Zero_Returns0()
        {
            string result = UnaryEncoder.Encode(0);
            Assert.AreEqual("0", result);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(10)]
        [DataRow(20)]
        public void Encode_Positive_GeneratesCorrectOutput(int n)
        {
            string expected = new string ('1', n) + "0";
            string result = UnaryEncoder.Encode(n);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Decode_NullOrEmpty_ReturnsZero()
        {
            int resultNull = UnaryEncoder.Decode(null);
            int resultEmpty = UnaryEncoder.Decode("");
            Assert.AreEqual(0, resultNull);
            Assert.AreEqual(0, resultEmpty);
        }

        [DataTestMethod]
        [DataRow("10", 1)]
        [DataRow("110", 2)]
        [DataRow("1110", 3)]
        [DataRow("1111110", 6)]
        public void Decode_ValidCode_ReturnsCountOfOnes(string code, int expected)
        {
            int result = UnaryEncoder.Decode(code);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("1", 0)]
        [DataRow("101", 0)]
        [DataRow("0110", 0)]
        [DataRow("111", 0)]
        [DataRow("1101", 0)]
        public void Decode_InvalidCode_ReturnsZero(string code, int expected)
        {
            int result = UnaryEncoder.Decode(code);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(15)]
        public void EncodeDecode_RoundTrip(int value)
        {
            string encoded = UnaryEncoder.Encode(value);
            int decoded = UnaryEncoder.Decode(encoded);
            if (value < 0)
            {
                Assert.AreEqual(0, decoded);
            }
            else
            {
                Assert.AreEqual(value, decoded);
            }
        }

        [TestMethod]
        public void EncodeDecode_RandomStressTest()
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                int value = random.Next(-10, 101);
                string encoded = UnaryEncoder.Encode(value);
                int decoded = UnaryEncoder.Decode(encoded);
                if (value < 0)
                {
                    Assert.AreEqual(0, decoded);
                }
                else
                {
                    Assert.AreEqual(value, decoded);
                }
            }
        }
    }
}