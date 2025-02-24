using Microsoft.VisualStudio.TestTools.UnitTesting;
using LossyNormalMap;
using System;

namespace LossyNormalMapTest
{
    [TestClass]
    public class LossyNormalMapTests
    {
        private float QuantizeAndDequantize(float value)
        {
            double quantized = (value + 1.0) * 127.5;
            int intValue = (int)Math.Round(quantized);
            if (intValue < 0)
            {
                intValue = 0;
            }

            if (intValue > 255)
            {
                intValue = 255;
            }

            return (float)(intValue / 127.5 - 1.0);
        }

        [TestMethod]
        public void Compress_WithNullMap_ReturnsEmptyByteArray()
        {
            byte[] result = LossyNormalMapCompressor.Compress(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Decompress_WithNullOrShortData_ReturnsEmptyMap()
        {
            NormalMap map1 = LossyNormalMapCompressor.Decompress(null);
            Assert.AreEqual(0, map1.Width);
            Assert.AreEqual(0, map1.Height);
            byte[] shortData = new byte[5];
            NormalMap map2 = LossyNormalMapCompressor.Decompress(shortData);
            Assert.AreEqual(0, map2.Width);
            Assert.AreEqual(0, map2.Height);
        }

        [TestMethod]
        public void RoundTrip_SmallMap()
        {
            int width = 2;
            int height = 2;
            NormalMap original = new NormalMap(width, height);
            original.SetNormal(0, 0, new Normal(-1.0f, 0.0f, 1.0f));
            original.SetNormal(1, 0, new Normal(0.5f, -0.5f, 0.0f));
            original.SetNormal(0, 1, new Normal(0.25f, 0.75f, -0.25f));
            original.SetNormal(1, 1, new Normal(-0.8f, 0.8f, 0.0f));
            byte[] compressed = LossyNormalMapCompressor.Compress(original);
            Assert.AreEqual(8 + width * height * 3, compressed.Length);
            NormalMap decompressed = LossyNormalMapCompressor.Decompress(compressed);
            Assert.AreEqual(width, decompressed.Width);
            Assert.AreEqual(height, decompressed.Height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Normal originalNormal = original.GetNormal(x, y);
                    Normal decompressedNormal = decompressed.Normals[x, y];
                    float expectedX = QuantizeAndDequantize(originalNormal.X);
                    float expectedY = QuantizeAndDequantize(originalNormal.Y);
                    float expectedZ = QuantizeAndDequantize(originalNormal.Z);
                    Assert.AreEqual(expectedX, decompressedNormal.X, 0.001f, $"Mismatch at ({x},{y}) for X");
                    Assert.AreEqual(expectedY, decompressedNormal.Y, 0.001f, $"Mismatch at ({x},{y}) for Y");
                    Assert.AreEqual(expectedZ, decompressedNormal.Z, 0.001f, $"Mismatch at ({x},{y}) for Z");
                }
            }
        }

        [TestMethod]
        public void RoundTrip_LargeMap()
        {
            int width = 128;
            int height = 128;
            NormalMap original = new NormalMap(width, height);
            Random rand = new Random(42);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float nx = (float)(rand.NextDouble() * 2.0 - 1.0);
                    float ny = (float)(rand.NextDouble() * 2.0 - 1.0);
                    float nz = (float)(rand.NextDouble() * 2.0 - 1.0);
                    original.SetNormal(x, y, new Normal(nx, ny, nz));
                }
            }

            byte[] compressed = LossyNormalMapCompressor.Compress(original);
            Assert.AreEqual(8 + width * height * 3, compressed.Length);
            NormalMap decompressed = LossyNormalMapCompressor.Decompress(compressed);
            Assert.AreEqual(width, decompressed.Width);
            Assert.AreEqual(height, decompressed.Height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Normal originalNormal = original.GetNormal(x, y);
                    Normal decompressedNormal = decompressed.Normals[x, y];
                    float expectedX = QuantizeAndDequantize(originalNormal.X);
                    float expectedY = QuantizeAndDequantize(originalNormal.Y);
                    float expectedZ = QuantizeAndDequantize(originalNormal.Z);
                    Assert.AreEqual(expectedX, decompressedNormal.X, 0.001f, $"Mismatch at ({x},{y}) for X");
                    Assert.AreEqual(expectedY, decompressedNormal.Y, 0.001f, $"Mismatch at ({x},{y}) for Y");
                    Assert.AreEqual(expectedZ, decompressedNormal.Z, 0.001f, $"Mismatch at ({x},{y}) for Z");
                }
            }
        }

        [TestMethod]
        public void EdgeValues_Test()
        {
            int width = 3;
            int height = 1;
            NormalMap map = new NormalMap(width, height);
            map.SetNormal(0, 0, new Normal(-1f, -1f, -1f));
            map.SetNormal(1, 0, new Normal(0f, 0f, 0f));
            map.SetNormal(2, 0, new Normal(1f, 1f, 1f));
            byte[] compressed = LossyNormalMapCompressor.Compress(map);
            NormalMap decompressed = LossyNormalMapCompressor.Decompress(compressed);
            Normal normal0 = decompressed.Normals[0, 0];
            Normal normal1 = decompressed.Normals[1, 0];
            Normal normal2 = decompressed.Normals[2, 0];
            Assert.AreEqual(QuantizeAndDequantize(-1f), normal0.X, 0.001f);
            Assert.AreEqual(QuantizeAndDequantize(-1f), normal0.Y, 0.001f);
            Assert.AreEqual(QuantizeAndDequantize(-1f), normal0.Z, 0.001f);
            Assert.AreEqual(QuantizeAndDequantize(0f), normal1.X, 0.001f);
            Assert.AreEqual(QuantizeAndDequantize(0f), normal1.Y, 0.001f);
            Assert.AreEqual(QuantizeAndDequantize(0f), normal1.Z, 0.001f);
            Assert.AreEqual(QuantizeAndDequantize(1f), normal2.X, 0.001f);
            Assert.AreEqual(QuantizeAndDequantize(1f), normal2.Y, 0.001f);
            Assert.AreEqual(QuantizeAndDequantize(1f), normal2.Z, 0.001f);
        }

        [TestMethod]
        public void Compress_WithEmptyMap_Returns8ByteArray()
        {
            NormalMap empty = new NormalMap(0, 0);
            byte[] result = LossyNormalMapCompressor.Compress(empty);
            Assert.IsNotNull(result);
            Assert.AreEqual(8, result.Length, "Empty map should compress to just header length.");
            NormalMap decompressed = LossyNormalMapCompressor.Decompress(result);
            Assert.AreEqual(0, decompressed.Width, "Width should be 0.");
            Assert.AreEqual(0, decompressed.Height, "Height should be 0.");
        }
    }
}