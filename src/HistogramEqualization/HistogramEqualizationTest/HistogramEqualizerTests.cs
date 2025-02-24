using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HistogramEqualization;
using HistogramEqualization.Drawing;

namespace HistogramEqualizationTest
{
    [TestClass]
    public class HistogramEqualizerTests
    {
        [TestMethod]
        public void Test_NullInput_ReturnsNull()
        {
            HistogramEqualizer equalizer = new HistogramEqualizer();
            Bitmap? result = equalizer.Equalize(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_UniformImage_ReturnsCopyEqualToOriginal()
        {
            int width = 3;
            int height = 3;
            Bitmap original = new Bitmap(width, height);
            Color uniform = Color.FromArgb(100, 100, 100);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    original.SetPixel(x, y, uniform);
                }
            }

            HistogramEqualizer equalizer = new HistogramEqualizer();
            Bitmap? result = equalizer.Equalize(original);
            Assert.IsNotNull(result);
            Bitmap nonNullResult = result!;
            Assert.IsFalse(object.ReferenceEquals(original, nonNullResult));
            Assert.IsTrue(BitmapsEqual(original, nonNullResult));
        }

        [TestMethod]
        public void Test_KnownValues_Equalization()
        {
            int width = 2;
            int height = 2;
            Bitmap original = new Bitmap(width, height);
            // Set known intensities: 10, 20, 30, 40.
            original.SetPixel(0, 0, Color.FromArgb(10, 10, 10));
            original.SetPixel(1, 0, Color.FromArgb(20, 20, 20));
            original.SetPixel(0, 1, Color.FromArgb(30, 30, 30));
            original.SetPixel(1, 1, Color.FromArgb(40, 40, 40));
            HistogramEqualizer equalizer = new HistogramEqualizer();
            Bitmap? result = equalizer.Equalize(original);
            Assert.IsNotNull(result);
            Bitmap nonNullResult = result!;
            // Expected mapping:
            // 10 -> 0, 20 -> 85, 30 -> 170, 40 -> 255.
            AssertColorEqual(Color.FromArgb(0, 0, 0), nonNullResult.GetPixel(0, 0));
            AssertColorEqual(Color.FromArgb(85, 85, 85), nonNullResult.GetPixel(1, 0));
            AssertColorEqual(Color.FromArgb(170, 170, 170), nonNullResult.GetPixel(0, 1));
            AssertColorEqual(Color.FromArgb(255, 255, 255), nonNullResult.GetPixel(1, 1));
        }

        [TestMethod]
        public void Test_RandomLargeImage_EqualizationProducesFullRange()
        {
            int width = 100;
            int height = 100;
            Bitmap original = new Bitmap(width, height);
            Random random = new Random(0);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int intensity = random.Next(0, 256);
                    original.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
                }
            }

            HistogramEqualizer equalizer = new HistogramEqualizer();
            Bitmap? result = equalizer.Equalize(original);
            Assert.IsNotNull(result);
            Bitmap nonNullResult = result!;
            int minIntensity = 256;
            int maxIntensity = -1;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = nonNullResult.GetPixel(x, y);
                    int intensity = pixel.R;
                    if (intensity < minIntensity)
                    {
                        minIntensity = intensity;
                    }

                    if (intensity > maxIntensity)
                    {
                        maxIntensity = intensity;
                    }
                }
            }

            Assert.AreEqual(0, minIntensity, "Minimum intensity should be 0 after equalization.");
            Assert.AreEqual(255, maxIntensity, "Maximum intensity should be 255 after equalization.");
        }

        private static bool BitmapsEqual(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
            {
                return false;
            }

            for (int y = 0; y < bmp1.Height; y++)
            {
                for (int x = 0; x < bmp1.Width; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y);
                    Color c2 = bmp2.GetPixel(x, y);
                    if (c1.R != c2.R || c1.G != c2.G || c1.B != c2.B)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void AssertColorEqual(Color expected, Color actual)
        {
            Assert.AreEqual(expected.R, actual.R, "Red components differ.");
            Assert.AreEqual(expected.G, actual.G, "Green components differ.");
            Assert.AreEqual(expected.B, actual.B, "Blue components differ.");
        }
    }
}