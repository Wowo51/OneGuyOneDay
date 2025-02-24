using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockTruncationCoding;

namespace BlockTruncationCodingTest
{
    [TestClass]
    public class BTCCompressorTests
    {
        [TestMethod]
        public void TestUniformImageCompression()
        {
            int height = 4;
            int width = 4;
            byte[, ] image = new byte[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    image[i, j] = 100;
                }
            }

            int blockSize = 4;
            List<BTCBlock> blocks = BTCCompressor.Compress(image, blockSize);
            Assert.AreEqual(1, blocks.Count);
            byte[, ] decompressed = BTCCompressor.Decompress(blocks, width, height, blockSize);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(100, decompressed[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestGradientImageCompression()
        {
            int height = 8;
            int width = 8;
            byte[, ] image = new byte[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    image[i, j] = (byte)((i * width + j) % 256);
                }
            }

            int blockSize = 4;
            List<BTCBlock> blocks = BTCCompressor.Compress(image, blockSize);
            Assert.AreEqual(4, blocks.Count);
            byte[, ] decompressed = BTCCompressor.Decompress(blocks, width, height, blockSize);
            Assert.AreEqual(height, decompressed.GetLength(0));
            Assert.AreEqual(width, decompressed.GetLength(1));
            double maxDiff = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int diff = Math.Abs(image[i, j] - decompressed[i, j]);
                    if (diff > maxDiff)
                    {
                        maxDiff = diff;
                    }

                    Assert.IsTrue(decompressed[i, j] >= 0 && decompressed[i, j] <= 255);
                }
            }

            Assert.IsTrue(maxDiff <= 70, "Max difference too high: " + maxDiff);
        }

        [TestMethod]
        public void TestNonUniformBlocks()
        {
            int height = 5;
            int width = 7;
            byte[, ] image = new byte[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    image[i, j] = (byte)(((i * width + j) * 7) % 256);
                }
            }

            int blockSize = 3;
            List<BTCBlock> blocks = BTCCompressor.Compress(image, blockSize);
            int expectedBlocks = ((height + blockSize - 1) / blockSize) * ((width + blockSize - 1) / blockSize);
            Assert.AreEqual(expectedBlocks, blocks.Count);
            byte[, ] decompressed = BTCCompressor.Decompress(blocks, width, height, blockSize);
            Assert.AreEqual(height, decompressed.GetLength(0));
            Assert.AreEqual(width, decompressed.GetLength(1));
        }

        [TestMethod]
        public void TestEmptyImage()
        {
            byte[, ] image = new byte[0, 0];
            int blockSize = 1;
            List<BTCBlock> blocks = BTCCompressor.Compress(image, blockSize);
            Assert.AreEqual(0, blocks.Count);
            byte[, ] decompressed = BTCCompressor.Decompress(blocks, 0, 0, blockSize);
            Assert.AreEqual(0, decompressed.GetLength(0));
            Assert.AreEqual(0, decompressed.GetLength(1));
        }

        [TestMethod]
        public void TestRandomImageCompression()
        {
            int height = 16;
            int width = 16;
            byte[, ] image = new byte[height, width];
            Random rand = new Random(42);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    image[i, j] = (byte)rand.Next(0, 256);
                }
            }

            int blockSize = 4;
            List<BTCBlock> blocks = BTCCompressor.Compress(image, blockSize);
            byte[, ] decompressed = BTCCompressor.Decompress(blocks, width, height, blockSize);
            Assert.AreEqual(height, decompressed.GetLength(0));
            Assert.AreEqual(width, decompressed.GetLength(1));
            double totalDiff = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int diff = Math.Abs(image[i, j] - decompressed[i, j]);
                    totalDiff += diff;
                    Assert.IsTrue(decompressed[i, j] >= 0 && decompressed[i, j] <= 255);
                }
            }

            double avgDiff = totalDiff / (height * width);
            Assert.IsTrue(avgDiff < 60, "Average difference too high: " + avgDiff);
        }
    }
}