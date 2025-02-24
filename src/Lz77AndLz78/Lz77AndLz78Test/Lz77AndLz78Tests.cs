using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using Lz77AndLz78;

namespace Lz77AndLz78Test
{
    [TestClass]
    public class Lz77AndLz78Tests
    {
        private static string GenerateRandomString(int length)
        {
            StringBuilder stringBuilder = new StringBuilder(length);
            Random random = new Random(12345);
            for (int i = 0; i < length; i++)
            {
                char ch = (char)random.Next(32, 127);
                stringBuilder.Append(ch);
            }

            return stringBuilder.ToString();
        }

        [TestMethod]
        public void Lz77_NullInput_ReturnsEmpty()
        {
            string compressed = LZ77Compressor.Compress(null);
            Assert.AreEqual(string.Empty, compressed);
            string decompressed = LZ77Compressor.Decompress(compressed);
            Assert.AreEqual(string.Empty, decompressed);
        }

        [TestMethod]
        public void Lz77_EmptyInput_ReturnsEmpty()
        {
            string compressed = LZ77Compressor.Compress(string.Empty);
            Assert.AreEqual(string.Empty, compressed);
            string decompressed = LZ77Compressor.Decompress(compressed);
            Assert.AreEqual(string.Empty, decompressed);
        }

        [TestMethod]
        public void Lz77_RoundTrip_SmallInput()
        {
            string original = "TOBEORNOTTOBEORTOBEORNOT";
            string compressed = LZ77Compressor.Compress(original);
            string decompressed = LZ77Compressor.Decompress(compressed);
            Assert.AreEqual(original, decompressed);
        }

        [TestMethod]
        public void Lz77_RoundTrip_LargeInput()
        {
            string original = GenerateRandomString(1000);
            string compressed = LZ77Compressor.Compress(original);
            string decompressed = LZ77Compressor.Decompress(compressed);
            Assert.AreEqual(original, decompressed);
        }

        [TestMethod]
        public void Lz78_NullInput_ReturnsEmpty()
        {
            string compressed = LZ78Compressor.Compress(null);
            Assert.AreEqual(string.Empty, compressed);
            string decompressed = LZ78Compressor.Decompress(compressed);
            Assert.AreEqual(string.Empty, decompressed);
        }

        [TestMethod]
        public void Lz78_EmptyInput_ReturnsEmpty()
        {
            string compressed = LZ78Compressor.Compress(string.Empty);
            Assert.AreEqual(string.Empty, compressed);
            string decompressed = LZ78Compressor.Decompress(compressed);
            Assert.AreEqual(string.Empty, decompressed);
        }

        [TestMethod]
        public void Lz78_RoundTrip_SmallInput()
        {
            string original = "TOBEORNOTTOBEORTOBEORNOT";
            string compressed = LZ78Compressor.Compress(original);
            string decompressed = LZ78Compressor.Decompress(compressed);
            Assert.AreEqual(original, decompressed);
        }

        [TestMethod]
        public void Lz78_RoundTrip_LargeInput()
        {
            string original = GenerateRandomString(1000);
            string compressed = LZ78Compressor.Compress(original);
            string decompressed = LZ78Compressor.Decompress(compressed);
            Assert.AreEqual(original, decompressed);
        }

        [TestMethod]
        public void Lz77_RoundTrip_RepeatingInput()
        {
            string original = "aaaaaaaaaa";
            string compressed = LZ77Compressor.Compress(original);
            string decompressed = LZ77Compressor.Decompress(compressed);
            Assert.AreEqual(original, decompressed);
        }

        [TestMethod]
        public void Lz78_RoundTrip_RepeatingInput()
        {
            string original = "ababababab";
            string compressed = LZ78Compressor.Compress(original);
            string decompressed = LZ78Compressor.Decompress(compressed);
            Assert.AreEqual(original, decompressed);
        }
    }
}