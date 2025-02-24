using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CannyEdgeDetector;
using CannyEdgeDetector.Imaging;

namespace CannyEdgeDetectorTest
{
    [TestClass]
    public class EdgeDetectorTests
    {
        [TestMethod]
        public void DetectEdges_AllBlackImage_ReturnsAllBlack()
        {
            int width = 10;
            int height = 10;
            Bitmap blackImage = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    blackImage.SetPixel(x, y, Color.Black);
                }
            }

            Bitmap result = EdgeDetector.DetectEdges(blackImage, 20.0, 50.0);
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    CannyEdgeDetector.Imaging.Color pixelColor = result.GetPixel(x, y);
                    Assert.AreEqual(0, pixelColor.R, "Pixel R value mismatch");
                    Assert.AreEqual(0, pixelColor.G, "Pixel G value mismatch");
                    Assert.AreEqual(0, pixelColor.B, "Pixel B value mismatch");
                }
            }
        }

        [TestMethod]
        public void DetectEdges_RandomNoiseImage_ReturnsImageWithSameDimensions()
        {
            int width = 50;
            int height = 50;
            Bitmap randomImage = new Bitmap(width, height);
            System.Random random = new System.Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int gray = random.Next(0, 256);
                    CannyEdgeDetector.Imaging.Color color = CannyEdgeDetector.Imaging.Color.FromArgb(gray, gray, gray);
                    randomImage.SetPixel(x, y, color);
                }
            }

            Bitmap result = EdgeDetector.DetectEdges(randomImage, 20.0, 50.0);
            Assert.AreEqual(width, result.Width, "Width should match.");
            Assert.AreEqual(height, result.Height, "Height should match.");
        }

        [TestMethod]
        public void DetectEdges_WhiteLineImage_DetectsEdge()
        {
            int width = 20;
            int height = 20;
            Bitmap image = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    image.SetPixel(x, y, CannyEdgeDetector.Imaging.Color.Black);
                }
            }

            int lineY = height / 2;
            for (int x = 0; x < width; x++)
            {
                image.SetPixel(x, lineY, CannyEdgeDetector.Imaging.Color.White);
            }

            Bitmap result = EdgeDetector.DetectEdges(image, 20.0, 50.0);
            bool foundWhite = false;
            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    CannyEdgeDetector.Imaging.Color pixelColor = result.GetPixel(x, y);
                    if (pixelColor.R == 255 && pixelColor.G == 255 && pixelColor.B == 255)
                    {
                        foundWhite = true;
                        break;
                    }
                }

                if (foundWhite)
                {
                    break;
                }
            }

            Assert.IsTrue(foundWhite, "Edge detection should detect a white line edge.");
        }

        [TestMethod]
        public void DetectEdges_LargeImage_PerformanceTest()
        {
            int width = 200;
            int height = 200;
            Bitmap image = new Bitmap(width, height);
            System.Random random = new System.Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int gray = random.Next(0, 256);
                    CannyEdgeDetector.Imaging.Color color = CannyEdgeDetector.Imaging.Color.FromArgb(gray, gray, gray);
                    image.SetPixel(x, y, color);
                }
            }

            Bitmap result = EdgeDetector.DetectEdges(image, 20.0, 50.0);
            Assert.AreEqual(width, result.Width, "Width should match.");
            Assert.AreEqual(height, result.Height, "Height should match.");
        }
    }
}