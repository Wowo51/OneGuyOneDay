using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeamCarvingAlgorithm;
using SeamCarvingAlgorithm.Drawing;

namespace SeamCarvingAlgorithmTest
{
    [TestClass]
    public class SeamCarverTests
    {
        [TestMethod]
        public void Test_NullBitmap_ShouldDefaultToOnePixel()
        {
            SeamCarver seamCarver = new SeamCarver(null);
            Bitmap result = seamCarver.Resize(1, 1);
            Assert.AreEqual(1, result.Width);
            Assert.AreEqual(1, result.Height);
        }

        [TestMethod]
        public void Test_ResizeWidthOnly()
        {
            int initialWidth = 4;
            int initialHeight = 3;
            Bitmap bitmap = CreateTestBitmap(initialWidth, initialHeight, 50);
            SeamCarver seamCarver = new SeamCarver(bitmap);
            Bitmap result = seamCarver.Resize(3, initialHeight);
            Assert.AreEqual(3, result.Width);
            Assert.AreEqual(initialHeight, result.Height);
        }

        [TestMethod]
        public void Test_ResizeHeightOnly()
        {
            int initialWidth = 3;
            int initialHeight = 4;
            Bitmap bitmap = CreateTestBitmap(initialWidth, initialHeight, 100);
            SeamCarver seamCarver = new SeamCarver(bitmap);
            Bitmap result = seamCarver.Resize(initialWidth, 3);
            Assert.AreEqual(initialWidth, result.Width);
            Assert.AreEqual(3, result.Height);
        }

        [TestMethod]
        public void Test_ResizeBothDimensions()
        {
            int initialWidth = 5;
            int initialHeight = 5;
            Bitmap bitmap = CreateTestBitmap(initialWidth, initialHeight, 150);
            SeamCarver seamCarver = new SeamCarver(bitmap);
            Bitmap result = seamCarver.Resize(3, 3);
            Assert.AreEqual(3, result.Width);
            Assert.AreEqual(3, result.Height);
        }

        [TestMethod]
        public void Test_ResizeNoChange()
        {
            int initialWidth = 4;
            int initialHeight = 4;
            Bitmap bitmap = CreateTestBitmap(initialWidth, initialHeight, 200);
            SeamCarver seamCarver = new SeamCarver(bitmap);
            Bitmap result = seamCarver.Resize(initialWidth, initialHeight);
            Assert.AreEqual(initialWidth, result.Width);
            Assert.AreEqual(initialHeight, result.Height);
        }

        [TestMethod]
        public void Test_SyntheticLargeData()
        {
            int initialWidth = 50;
            int initialHeight = 50;
            Bitmap bitmap = CreateRandomBitmap(initialWidth, initialHeight, new Random(0));
            SeamCarver seamCarver = new SeamCarver(bitmap);
            Bitmap result = seamCarver.Resize(45, 45);
            Assert.AreEqual(45, result.Width);
            Assert.AreEqual(45, result.Height);
        }

        private Bitmap CreateTestBitmap(int width, int height, int baseValue)
        {
            Bitmap bitmap = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bitmap.SetPixel(x, y, Color.FromArgb(baseValue, baseValue, baseValue));
                }
            }

            return bitmap;
        }

        private Bitmap CreateRandomBitmap(int width, int height, Random random)
        {
            Bitmap bitmap = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int r = random.Next(0, 256);
                    int g = random.Next(0, 256);
                    int b = random.Next(0, 256);
                    bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return bitmap;
        }
    }
}