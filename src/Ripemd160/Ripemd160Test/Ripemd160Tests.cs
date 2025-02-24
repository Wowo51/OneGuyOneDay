using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ripemd160;

namespace Ripemd160Test
{
    [TestClass]
    public class Ripemd160Tests
    {
        // Modified to accept a nullable byte array and fail if null.
        private static string ByteArrayToHex(byte[]? bytes)
        {
            if (bytes == null)
            {
                Assert.Fail("Byte array is null.");
                return string.Empty;
            }

            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
            {
                hex.Append(bytes[i].ToString("x2"));
            }

            return hex.ToString();
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            byte[] input = Encoding.UTF8.GetBytes("");
            RIPEMD160Managed hasher = new RIPEMD160Managed();
            byte[] hash = hasher.ComputeHash(input);
            Assert.IsNotNull(hash);
            string result = ByteArrayToHex(hash);
            string expected = "9c1185a5c5e9fc54612808977ee8f548b2258d31";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestQuickBrownFox()
        {
            string text = "The quick brown fox jumps over the lazy dog";
            byte[] input = Encoding.UTF8.GetBytes(text);
            RIPEMD160Managed hasher = new RIPEMD160Managed();
            byte[] hash = hasher.ComputeHash(input);
            Assert.IsNotNull(hash);
            string result = ByteArrayToHex(hash);
            string expected = "37f332f68db77bd9d7edd4969571ad671cf9dd3b";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestLargeRandomData()
        {
            int size = 1024 * 1024;
            byte[] data = new byte[size];
            Random random = new Random(42);
            random.NextBytes(data);
            RIPEMD160Managed hasher1 = new RIPEMD160Managed();
            byte[] hash1 = hasher1.ComputeHash(data);
            Assert.IsNotNull(hash1);
            RIPEMD160Managed hasher2 = new RIPEMD160Managed();
            byte[] hash2 = hasher2.ComputeHash(data);
            Assert.IsNotNull(hash2);
            Assert.AreEqual(ByteArrayToHex(hash1), ByteArrayToHex(hash2));
        }

        [TestMethod]
        public void TestIncrementalHashing()
        {
            string text = "abcdefghijklmnopqrstuvwxyz";
            byte[] data = Encoding.UTF8.GetBytes(text);
            RIPEMD160Managed fullHasher = new RIPEMD160Managed();
            byte[] fullHash = fullHasher.ComputeHash(data);
            Assert.IsNotNull(fullHash);
            RIPEMD160Managed incrementalHasher = new RIPEMD160Managed();
            int chunkSize = 8;
            int offset = 0;
            while (offset < data.Length - chunkSize)
            {
                incrementalHasher.TransformBlock(data, offset, chunkSize, data, offset);
                offset += chunkSize;
            }

            int remaining = data.Length - offset;
            incrementalHasher.TransformFinalBlock(data, offset, remaining);
            byte[] incrementalHash = incrementalHasher.Hash!;
            Assert.IsNotNull(incrementalHash);
            Assert.AreEqual(ByteArrayToHex(fullHash), ByteArrayToHex(incrementalHash));
        }
    }
}