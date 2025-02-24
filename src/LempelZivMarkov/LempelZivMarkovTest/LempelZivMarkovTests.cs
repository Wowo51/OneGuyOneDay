using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LempelZivMarkov;

namespace LempelZivMarkovTest
{
    [TestClass]
    public class LempelZivMarkovTests
    {
        [TestMethod]
        public void TestEmptyInput()
        {
            byte[] input = new byte[0];
            byte[] compressed = LzmaCompressor.Compress(input);
            byte[] decompressed = LzmaCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void TestSingleByteInput()
        {
            byte[] input = new byte[]
            {
                0x01
            };
            byte[] compressed = LzmaCompressor.Compress(input);
            byte[] decompressed = LzmaCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void TestSimpleRoundTrip()
        {
            byte[] input = new byte[]
            {
                0x10,
                0x20,
                0x30,
                0x20,
                0x10,
                0x20,
                0x30,
                0x20,
                0x10
            };
            byte[] compressed = LzmaCompressor.Compress(input);
            byte[] decompressed = LzmaCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void TestRandomDataRoundTrip()
        {
            System.Random random = new System.Random(42);
            int dataSize = 10000;
            byte[] input = new byte[dataSize];
            random.NextBytes(input);
            byte[] compressed = LzmaCompressor.Compress(input);
            byte[] decompressed = LzmaCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void TestNonCompressedData()
        {
            // When data does not have a valid LZMA header, decompression should return the input itself.
            byte[] input = new byte[]
            {
                0,
                1,
                2,
                3,
                4
            };
            byte[] output = LzmaCompressor.Decompress(input);
            CollectionAssert.AreEqual(input, output);
        }

        [TestMethod]
        public void TestEscapedLiteral()
        {
            // Test data containing the ESC marker (0xFF) to ensure correct escaping.
            byte[] input = new byte[]
            {
                0xFF,
                0xFF,
                0xFF
            };
            byte[] compressed = LzmaCompressor.Compress(input);
            byte[] decompressed = LzmaCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }
    }
}