using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lzss;

namespace LzssTest
{
    [TestClass]
    public class LzssCompressorTests
    {
        [TestMethod]
        public void Test_NullInput_Compress_ReturnsEmpty()
        {
            byte[] result = LzssCompressor.Compress(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Test_NullInput_Decompress_ReturnsEmpty()
        {
            byte[] result = LzssCompressor.Decompress(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Test_EmptyInput_CompressDecompress()
        {
            byte[] input = new byte[0];
            byte[] compressed = LzssCompressor.Compress(input);
            byte[] decompressed = LzssCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void Test_RepeatedPattern_CompressDecompress()
        {
            byte[] input = Encoding.ASCII.GetBytes("AAAAAAABBBBBBBCCCCCCCDDDDDD");
            byte[] compressed = LzssCompressor.Compress(input);
            byte[] decompressed = LzssCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void Test_LargeData_CompressDecompress()
        {
            int size = 10000;
            byte[] input = new byte[size];
            Random random = new Random(0);
            for (int i = 0; i < size; i++)
            {
                if (i % 100 == 0 && i + 50 <= size)
                {
                    byte value = (byte)('A' + ((i / 100) % 26));
                    for (int j = 0; j < 50; j++)
                    {
                        input[i + j] = value;
                    }

                    i += 49;
                }
                else
                {
                    input[i] = (byte)random.Next(0, 256);
                }
            }

            byte[] compressed = LzssCompressor.Compress(input);
            byte[] decompressed = LzssCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void Test_RandomData_CompressDecompress()
        {
            int size = 5000;
            byte[] input = new byte[size];
            Random random = new Random(1);
            for (int i = 0; i < size; i++)
            {
                input[i] = (byte)random.Next(0, 256);
            }

            byte[] compressed = LzssCompressor.Compress(input);
            byte[] decompressed = LzssCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void Test_NonCompressibleData()
        {
            byte[] input = new byte[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10
            };
            byte[] compressed = LzssCompressor.Compress(input);
            Assert.AreEqual(input.Length * 2, compressed.Length);
            byte[] decompressed = LzssCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void Test_RepetitiveSequence()
        {
            List<byte> inputList = new List<byte>();
            for (int i = 0; i < 300; i++)
            {
                byte value = (byte)(i % 256);
                inputList.Add(value);
                if (i % 10 == 0 && i > 0)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        inputList.Add(value);
                    }
                }
            }

            byte[] input = inputList.ToArray();
            byte[] compressed = LzssCompressor.Compress(input);
            byte[] decompressed = LzssCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }

        [TestMethod]
        public void Test_SingleByte_CompressDecompress()
        {
            byte[] input = new byte[]
            {
                42
            };
            byte[] compressed = LzssCompressor.Compress(input);
            Assert.AreEqual(2, compressed.Length);
            byte[] decompressed = LzssCompressor.Decompress(compressed);
            CollectionAssert.AreEqual(input, decompressed);
        }
    }
}