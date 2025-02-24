using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using LempelZivWilliams;

namespace LempelZivWilliamsTest
{
    [TestClass]
    public class LZRWCompressorTests
    {
        [TestMethod]
        public void Compress_NullInput_ReturnsNull()
        {
            byte[]? input = null;
            byte[]? result = LZRWCompressor.Compress(input);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Decompress_NullInput_ReturnsNull()
        {
            byte[]? input = null;
            byte[]? result = LZRWCompressor.Decompress(input);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Compress_EmptyInput_ReturnsEmpty()
        {
            byte[] input = new byte[0];
            byte[]? result = LZRWCompressor.Compress(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Decompress_EmptyInput_ReturnsEmpty()
        {
            byte[] input = new byte[0];
            byte[]? result = LZRWCompressor.Decompress(input);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void RoundTrip_TextInput_ReturnsOriginal()
        {
            string text = "The quick brown fox jumps over the lazy dog";
            byte[] original = Encoding.UTF8.GetBytes(text);
            byte[]? compressed = LZRWCompressor.Compress(original);
            Assert.IsNotNull(compressed);
            byte[]? decompressed = LZRWCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(original, decompressed);
        }

        [TestMethod]
        public void RoundTrip_RepeatedPatternInput_ReturnsOriginal()
        {
            byte[] original = new byte[300];
            char[] pattern = new char[]
            {
                'A',
                'B',
                'C'
            };
            for (int i = 0; i < original.Length; i++)
            {
                original[i] = (byte)pattern[i % 3];
            }

            byte[]? compressed = LZRWCompressor.Compress(original);
            Assert.IsNotNull(compressed);
            byte[]? decompressed = LZRWCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(original, decompressed);
        }

        [TestMethod]
        public void RoundTrip_RandomData_ReturnsOriginal()
        {
            Random random = new Random(42);
            byte[] original = new byte[1024];
            random.NextBytes(original);
            byte[]? compressed = LZRWCompressor.Compress(original);
            Assert.IsNotNull(compressed);
            byte[]? decompressed = LZRWCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(original, decompressed);
        }

        [TestMethod]
        public void RoundTrip_HighlyRepetitiveData_ReturnsOriginal()
        {
            byte[] original = new byte[1000];
            for (int i = 0; i < original.Length; i++)
            {
                original[i] = 0xAA;
            }

            byte[]? compressed = LZRWCompressor.Compress(original);
            Assert.IsNotNull(compressed);
            byte[]? decompressed = LZRWCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(original, decompressed);
        }
    }
}