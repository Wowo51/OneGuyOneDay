using System;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeflateTest
{
    [TestClass]
    public class DeflateTests
    {
        [TestMethod]
        public void Compress_NullInput_ReturnsEmptyArray()
        {
            byte[] compressed = Deflate.DeflateCompressor.Compress(null);
            Assert.IsNotNull(compressed);
            Assert.AreEqual(0, compressed.Length);
        }

        [TestMethod]
        public void Decompress_NullInput_ReturnsEmptyArray()
        {
            byte[] decompressed = Deflate.DeflateCompressor.Decompress(null);
            Assert.IsNotNull(decompressed);
            Assert.AreEqual(0, decompressed.Length);
        }

        [TestMethod]
        public void Compress_Decompress_EmptyArray_RoundTrip()
        {
            byte[] input = new byte[0];
            byte[] compressed = Deflate.DeflateCompressor.Compress(input);
            byte[] decompressed = Deflate.DeflateCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void Compress_Decompress_KnownData_RoundTrip()
        {
            string original = "This is a test string for compression.";
            byte[] input = Encoding.UTF8.GetBytes(original);
            byte[] compressed = Deflate.DeflateCompressor.Compress(input);
            byte[] decompressed = Deflate.DeflateCompressor.Decompress(compressed);
            string result = Encoding.UTF8.GetString(decompressed);
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void Compress_Decompress_LargeData_RoundTrip()
        {
            int size = 1024 * 1024; // 1 MB of data
            byte[] input = new byte[size];
            Random random = new Random(12345);
            for (int i = 0; i < size; i++)
            {
                input[i] = (byte)random.Next(0, 256);
            }

            byte[] compressed = Deflate.DeflateCompressor.Compress(input);
            byte[] decompressed = Deflate.DeflateCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void Decompress_CorruptedData_ThrowsInvalidDataException()
        {
            byte[] invalidData = new byte[]
            {
                0,
                1,
                2,
                3,
                4,
                5
            };
            Assert.ThrowsException<InvalidDataException>(() =>
            {
                Deflate.DeflateCompressor.Decompress(invalidData);
            });
        }

        [TestMethod]
        public void Compress_Decompress_AllBytes_RoundTrip()
        {
            byte[] input = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                input[i] = (byte)i;
            }

            byte[] compressed = Deflate.DeflateCompressor.Compress(input);
            byte[] decompressed = Deflate.DeflateCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }
    }
}