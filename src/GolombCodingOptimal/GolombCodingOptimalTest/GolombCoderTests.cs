using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GolombCodingOptimal;

namespace GolombCodingOptimalTest
{
    [TestClass]
    public class GolombCoderTests
    {
        [TestMethod]
        public void TestEncodeDecodeForM1()
        {
            int[] testNumbers = new int[]
            {
                0,
                1,
                5,
                10
            };
            for (int i = 0; i < testNumbers.Length; i++)
            {
                int number = testNumbers[i];
                string encoded = GolombCoder.EncodeInteger(number, 1);
                int readBits = 0;
                int decoded = GolombCoder.DecodeInteger(encoded, 1, out readBits);
                Assert.AreEqual(number, decoded, "Decoding failed for m=1 with number " + number);
                Assert.AreEqual(encoded.Length, readBits, "Read bits not matching length for m=1 with number " + number);
            }
        }

        [TestMethod]
        public void TestEncodeDecodeGeneral()
        {
            int[] mValues = new int[]
            {
                2,
                3,
                4,
                5,
                7,
                10
            };
            for (int mIndex = 0; mIndex < mValues.Length; mIndex++)
            {
                int m = mValues[mIndex];
                for (int number = 0; number < 100; number++)
                {
                    string encoded = GolombCoder.EncodeInteger(number, m);
                    int readBits = 0;
                    int decoded = GolombCoder.DecodeInteger(encoded, m, out readBits);
                    Assert.AreEqual(number, decoded, $"Decoding failed for m={m} with number {number}");
                    Assert.AreEqual(encoded.Length, readBits, $"Read bits not matching length for m={m} with number {number}");
                }
            }

            System.Random random = new System.Random();
            for (int j = 0; j < 1000; j++)
            {
                int[] mOptions = new int[]
                {
                    2,
                    3,
                    4,
                    5,
                    7,
                    10
                };
                int m = mOptions[random.Next(mOptions.Length)];
                int number = random.Next(0, 10000);
                string encoded = GolombCoder.EncodeInteger(number, m);
                int readBits = 0;
                int decoded = GolombCoder.DecodeInteger(encoded, m, out readBits);
                Assert.AreEqual(number, decoded, $"Decoding failed for m={m} with number {number}");
                Assert.AreEqual(encoded.Length, readBits, $"Read bits not matching length for m={m} with number {number}");
            }
        }

        [TestMethod]
        public void TestInvalidInputs()
        {
            string encodedZero = GolombCoder.EncodeInteger(10, 0);
            Assert.AreEqual(string.Empty, encodedZero, "Encoding with m=0 should return empty string.");
            string encodedNegative = GolombCoder.EncodeInteger(10, -2);
            Assert.AreEqual(string.Empty, encodedNegative, "Encoding with negative m should return empty string.");
            int readBits;
            int decodedNull = GolombCoder.DecodeInteger(null, 1, out readBits);
            Assert.AreEqual(-1, decodedNull, "Decoding null should return -1.");
            int result = GolombCoder.DecodeInteger("111", 1, out readBits);
            Assert.AreEqual(-1, result, "Decoding should fail when unary part does not terminate with '0'.");
            int validNumber = 20;
            int m = 3;
            string validEncoding = GolombCoder.EncodeInteger(validNumber, m);
            if (validEncoding.Length > 0)
            {
                string truncated = validEncoding.Substring(0, validEncoding.Length - 1);
                int decodedTruncated = GolombCoder.DecodeInteger(truncated, m, out readBits);
                Assert.AreEqual(-1, decodedTruncated, "Decoding should fail for truncated encoding string.");
            }
        }

        [TestMethod]
        public void TestLargeNumberEncoding()
        {
            int m = 5;
            int number = 123456;
            string encoded = GolombCoder.EncodeInteger(number, m);
            int readBits = 0;
            int decoded = GolombCoder.DecodeInteger(encoded, m, out readBits);
            Assert.AreEqual(number, decoded, $"Decoding failed for large number {number} with m={m}");
            Assert.AreEqual(encoded.Length, readBits, "Read bits not matching length for large number encoding.");
        }

        [TestMethod]
        public void TestExtensiveRandomData()
        {
            System.Random random = new System.Random();
            int[] mOptions = new int[]
            {
                2,
                3,
                4,
                5,
                7,
                10,
                13,
                17,
                19,
                23
            };
            for (int i = 0; i < 10000; i++)
            {
                int m = mOptions[random.Next(mOptions.Length)];
                int number = random.Next(0, 1000000);
                string encoded = GolombCoder.EncodeInteger(number, m);
                int readBits = 0;
                int decoded = GolombCoder.DecodeInteger(encoded, m, out readBits);
                Assert.AreEqual(number, decoded, "Decoding failed for m=" + m + " with number " + number);
                Assert.AreEqual(encoded.Length, readBits, "Read bits not matching length for m=" + m + " with number " + number);
            }
        }
    }
}