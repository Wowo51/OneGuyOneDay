using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderedDithering;
using OrderedDithering.Drawing;

namespace OrderedDitheringTest
{
    [TestClass]
    public class DitheringProcessorTests
    {
        [TestMethod]
        public void ApplyOrderedDithering_NullSource_ReturnsMinimalBitmap()
        {
            DitheringProcessor processor = new DitheringProcessor();
            Bitmap result = processor.ApplyOrderedDithering(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Width);
            Assert.AreEqual(1, result.Height);
            Color pixel = result.GetPixel(0, 0);
            Assert.AreEqual(Color.White.R, pixel.R);
            Assert.AreEqual(Color.White.G, pixel.G);
            Assert.AreEqual(Color.White.B, pixel.B);
        }

        [TestMethod]
        public void ApplyOrderedDithering_UniformColor_ProducesExpectedPattern()
        {
            int width = 4;
            int height = 4;
            Bitmap source = new Bitmap(width, height);
            // Fill with a uniform color (100, 100, 100)
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    source.SetPixel(x, y, new Color(100, 100, 100));
                }
            }

            DitheringProcessor processor = new DitheringProcessor();
            Bitmap result = processor.ApplyOrderedDithering(source);
            // Expected pattern based on the Bayer matrix and threshold calculation:
            // Row0: [White, Black, White, Black]
            // Row1: [Black, White, Black, Black]
            // Row2: [White, Black, White, Black]
            // Row3: [Black, Black, Black, White]
            Color white = Color.White;
            Color black = Color.Black;
            Color[, ] expected = new Color[4, 4]
            {
                {
                    white,
                    black,
                    white,
                    black
                },
                {
                    black,
                    white,
                    black,
                    black
                },
                {
                    white,
                    black,
                    white,
                    black
                },
                {
                    black,
                    black,
                    black,
                    white
                }
            };
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color actual = result.GetPixel(x, y);
                    Color expColor = expected[y, x];
                    Assert.AreEqual(expColor.R, actual.R, $"Mismatch at ({x},{y}) R");
                    Assert.AreEqual(expColor.G, actual.G, $"Mismatch at ({x},{y}) G");
                    Assert.AreEqual(expColor.B, actual.B, $"Mismatch at ({x},{y}) B");
                }
            }
        }

        [TestMethod]
        public void ApplyOrderedDithering_RandomData_ProducesBlackAndWhiteOutput()
        {
            int width = 128;
            int height = 128;
            Bitmap source = new Bitmap(width, height);
            Random random = new Random(42);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int r = random.Next(256);
                    int g = random.Next(256);
                    int b = random.Next(256);
                    source.SetPixel(x, y, new Color(r, g, b));
                }
            }

            DitheringProcessor processor = new DitheringProcessor();
            Bitmap result = processor.ApplyOrderedDithering(source);
            Assert.AreEqual(width, result.Width);
            Assert.AreEqual(height, result.Height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixel = result.GetPixel(x, y);
                    bool isBlack = (pixel.R == Color.Black.R && pixel.G == Color.Black.G && pixel.B == Color.Black.B);
                    bool isWhite = (pixel.R == Color.White.R && pixel.G == Color.White.G && pixel.B == Color.White.B);
                    Assert.IsTrue(isBlack || isWhite, $"Pixel at ({x},{y}) is not black or white.");
                }
            }
        }
    }
}