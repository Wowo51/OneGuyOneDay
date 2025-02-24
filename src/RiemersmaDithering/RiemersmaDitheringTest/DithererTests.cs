using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiemersmaDithering;
using RiemersmaDithering.Drawing;

namespace RiemersmaDitheringTest
{
    [TestClass]
    public class DithererTests
    {
        [TestMethod]
        public void ApplyRiemersmaDithering_NullInput_ReturnsNull()
        {
            Bitmap? result = RiemersmaDitherer.ApplyRiemersmaDithering(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ApplyRiemersmaDithering_SmallImage_AllPixelsBinary()
        {
            Bitmap input = new Bitmap(3, 3);
            input.SetPixel(0, 0, Color.FromArgb(50, 50, 50));
            input.SetPixel(1, 0, Color.FromArgb(100, 100, 100));
            input.SetPixel(2, 0, Color.FromArgb(150, 150, 150));
            input.SetPixel(0, 1, Color.FromArgb(200, 200, 200));
            input.SetPixel(1, 1, Color.FromArgb(120, 120, 120));
            input.SetPixel(2, 1, Color.FromArgb(80, 80, 80));
            input.SetPixel(0, 2, Color.FromArgb(255, 255, 255));
            input.SetPixel(1, 2, Color.FromArgb(30, 30, 30));
            input.SetPixel(2, 2, Color.FromArgb(180, 180, 180));
            Bitmap? output = RiemersmaDitherer.ApplyRiemersmaDithering(input);
            Assert.IsNotNull(output);
            Bitmap nonNullOutput = output;
            Assert.AreEqual(3, nonNullOutput.Width);
            Assert.AreEqual(3, nonNullOutput.Height);
            int[] valid = new int[]
            {
                0,
                255
            };
            for (int y = 0; y < nonNullOutput.Height; y++)
            {
                for (int x = 0; x < nonNullOutput.Width; x++)
                {
                    Color pixel = nonNullOutput.GetPixel(x, y);
                    Assert.IsTrue(Array.Exists(valid, element => element == pixel.R), "Pixel red should be 0 or 255");
                    Assert.IsTrue(Array.Exists(valid, element => element == pixel.G), "Pixel green should be 0 or 255");
                    Assert.IsTrue(Array.Exists(valid, element => element == pixel.B), "Pixel blue should be 0 or 255");
                }
            }
        }

        [TestMethod]
        public void ApplyRiemersmaDithering_LargeImage_RandomizedTest()
        {
            int width = 100;
            int height = 100;
            Bitmap input = new Bitmap(width, height);
            Random random = new Random(42);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int value = random.Next(0, 256);
                    input.SetPixel(x, y, Color.FromArgb(value, value, value));
                }
            }

            Bitmap? output = RiemersmaDitherer.ApplyRiemersmaDithering(input);
            Assert.IsNotNull(output);
            Bitmap nonNullOutput = output;
            Assert.AreEqual(width, nonNullOutput.Width);
            Assert.AreEqual(height, nonNullOutput.Height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = nonNullOutput.GetPixel(x, y);
                    Assert.IsTrue(pixel.R == 0 || pixel.R == 255, $"Pixel at ({x}, {y}) is not 0 or 255");
                    Assert.AreEqual(pixel.R, pixel.G);
                    Assert.AreEqual(pixel.R, pixel.B);
                }
            }
        }

        [TestMethod]
        public void ApplyRiemersmaDithering_AlreadyBinaryImage_RemainsUnchanged()
        {
            Bitmap input = new Bitmap(2, 2);
            input.SetPixel(0, 0, Color.FromArgb(0, 0, 0));
            input.SetPixel(1, 0, Color.FromArgb(255, 255, 255));
            input.SetPixel(0, 1, Color.FromArgb(255, 255, 255));
            input.SetPixel(1, 1, Color.FromArgb(0, 0, 0));
            Bitmap? output = RiemersmaDitherer.ApplyRiemersmaDithering(input);
            Assert.IsNotNull(output);
            Bitmap nonNullOutput = output;
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    Color inPixel = input.GetPixel(x, y);
                    Color outPixel = nonNullOutput.GetPixel(x, y);
                    Assert.AreEqual(inPixel.R, outPixel.R);
                    Assert.AreEqual(inPixel.G, outPixel.G);
                    Assert.AreEqual(inPixel.B, outPixel.B);
                }
            }
        }
    }
}