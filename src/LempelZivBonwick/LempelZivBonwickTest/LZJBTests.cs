using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LempelZivBonwick;

namespace LempelZivBonwickTest
{
    [TestClass]
    public class LZJBTests
    {
        [TestMethod]
        public void Compress_NullInput_ReturnsEmptyArray()
        {
            byte[] result = LZJB.Compress(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Decompress_NullInput_ReturnsEmptyArray()
        {
            byte[] result = LZJB.Decompress(null, 0);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void CompressDecompress_RoundTrip_EmptyArray()
        {
            byte[] input = new byte[0];
            byte[] compressed = LZJB.Compress(input);
            byte[] decompressed = LZJB.Decompress(compressed, input.Length);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void CompressDecompress_RoundTrip_SmallData()
        {
            byte[] input = System.Text.Encoding.UTF8.GetBytes("The quick brown fox jumps over the lazy dog.");
            byte[] compressed = LZJB.Compress(input);
            byte[] decompressed = LZJB.Decompress(compressed, input.Length);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void CompressDecompress_RoundTrip_RepetitiveData()
        {
            int size = 10000;
            byte[] input = new byte[size];
            for (int i = 0; i < size; i++)
            {
                input[i] = (byte)((i % 10) + 65);
            }

            byte[] compressed = LZJB.Compress(input);
            byte[] decompressed = LZJB.Decompress(compressed, input.Length);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void CompressDecompress_RoundTrip_LargeData()
        {
            int size = 100000;
            byte[] input = new byte[size];
            Random random = new Random(0);
            for (int i = 0; i < size; i++)
            {
                input[i] = (byte)random.Next(0, 256);
            }

            byte[] compressed = LZJB.Compress(input);
            byte[] decompressed = LZJB.Decompress(compressed, input.Length);
            CollectionAssert.AreEqual(input, decompressed);
        }
    }
}