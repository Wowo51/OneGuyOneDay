using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TruncatedBinaryEncoding;

namespace TruncatedBinaryEncodingTest
{
    [TestClass]
    public class TruncatedBinaryEncoderTests
    {
        [TestMethod]
        public void TestEncodeWithInvalidN()
        {
            string encoded = TruncatedBinaryEncoder.Encode(0, 0);
            Assert.AreEqual(string.Empty, encoded);
            encoded = TruncatedBinaryEncoder.Encode(0, 1);
            Assert.AreEqual(string.Empty, encoded);
        }

        [TestMethod]
        public void TestEncodeWithInvalidValue()
        {
            string encoded = TruncatedBinaryEncoder.Encode(-1, 6);
            Assert.AreEqual(string.Empty, encoded);
            encoded = TruncatedBinaryEncoder.Encode(6, 6);
            Assert.AreEqual(string.Empty, encoded);
        }

        [TestMethod]
        public void TestEncodeDecodeWithN6()
        {
            int n = 6;
            for (int i = 0; i < n; i++)
            {
                string encoded = TruncatedBinaryEncoder.Encode(i, n);
                int bitsUsed;
                int decoded = TruncatedBinaryEncoder.Decode(encoded, n, out bitsUsed);
                if (i < 2)
                {
                    Assert.AreEqual(2, encoded.Length, $"Expected 2 bits for value {i}.");
                    Assert.AreEqual(2, bitsUsed, $"Expected bitsUsed of 2 for value {i}.");
                }
                else
                {
                    Assert.AreEqual(3, encoded.Length, $"Expected 3 bits for value {i}.");
                    Assert.AreEqual(3, bitsUsed, $"Expected bitsUsed of 3 for value {i}.");
                }

                Assert.AreEqual(i, decoded, $"Decode failed for value {i}.");
            }
        }

        [TestMethod]
        public void TestEncodeDecodeWithN10()
        {
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                string encoded = TruncatedBinaryEncoder.Encode(i, n);
                int bitsUsed;
                int decoded = TruncatedBinaryEncoder.Decode(encoded, n, out bitsUsed);
                if (i < 6)
                {
                    Assert.AreEqual(3, encoded.Length, $"Expected 3 bits for value {i}.");
                    Assert.AreEqual(3, bitsUsed, $"Expected bitsUsed of 3 for value {i}.");
                }
                else
                {
                    Assert.AreEqual(4, encoded.Length, $"Expected 4 bits for value {i}.");
                    Assert.AreEqual(4, bitsUsed, $"Expected bitsUsed of 4 for value {i}.");
                }

                Assert.AreEqual(i, decoded, $"Decode failed for value {i}.");
            }
        }

        [TestMethod]
        public void TestDecodeWithNotEnoughBits()
        {
            int n = 6;
            int bitsUsed;
            int decoded = TruncatedBinaryEncoder.Decode("0", n, out bitsUsed);
            Assert.AreEqual(0, decoded, "Expected decoded value 0 when bits length is insufficient.");
            Assert.AreEqual(0, bitsUsed, "Expected bitsUsed 0 when bits length is insufficient.");
        }

        [TestMethod]
        public void TestDecodeWithNullBits()
        {
            int n = 6;
            int bitsUsed;
            int decoded = TruncatedBinaryEncoder.Decode(null, n, out bitsUsed);
            Assert.AreEqual(0, decoded, "Expected decoded value 0 when bits is null.");
            Assert.AreEqual(0, bitsUsed, "Expected bitsUsed 0 when bits is null.");
        }

        [TestMethod]
        public void TestRandomEncodeDecode()
        {
            Random random = new Random(12345);
            for (int j = 0; j < 50; j++)
            {
                int n = random.Next(2, 101);
                for (int i = 0; i < n; i++)
                {
                    string encoded = TruncatedBinaryEncoder.Encode(i, n);
                    int bitsUsed;
                    int decoded = TruncatedBinaryEncoder.Decode(encoded, n, out bitsUsed);
                    Assert.AreEqual(i, decoded, $"Decode failed for n={n}, value={i}.");
                }
            }
        }
    }
}