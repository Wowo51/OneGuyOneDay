using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HoughTransform;

namespace HoughTransformTest
{
    [TestClass]
    public class HoughTransformProcessorTests
    {
        [TestMethod]
        public void ProcessImage_NullImage_ReturnsEmptyList()
        {
            HoughTransformProcessor processor = new HoughTransformProcessor();
            List<HoughLine> result = processor.ProcessImage(null, 5);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ProcessImage_EmptyImage_ReturnsEmptyList()
        {
            bool[, ] image = new bool[10, 10];
            HoughTransformProcessor processor = new HoughTransformProcessor();
            List<HoughLine> result = processor.ProcessImage(image, 1);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ProcessImage_SinglePixelThreshold1_ReturnsLines()
        {
            bool[, ] image = new bool[5, 5];
            image[2, 2] = true;
            HoughTransformProcessor processor = new HoughTransformProcessor();
            List<HoughLine> result = processor.ProcessImage(image, 1);
            // For each theta (0 to 179), the single true pixel increments one accumulator cell.
            Assert.AreEqual(180, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(1, result[i].Votes);
            }
        }

        [TestMethod]
        public void ProcessImage_HorizontalLine_ReturnsExpectedLine()
        {
            int rows = 10;
            int cols = 10;
            bool[, ] image = new bool[rows, cols];
            // Draw a horizontal line at row index 5.
            for (int x = 0; x < cols; x++)
            {
                image[5, x] = true;
            }

            HoughTransformProcessor processor = new HoughTransformProcessor();
            // Setting threshold equal to the number of pixels in the horizontal line ensures that
            // the accumulator cell where all votes accumulate (for theta near 90°) is detected.
            List<HoughLine> result = processor.ProcessImage(image, cols);
            bool found = false;
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Theta == 90 && result[i].Votes == cols)
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found, "Expected horizontal line at Theta 90 with correct votes was not found.");
        }

        [TestMethod]
        public void ProcessImage_SyntheticLargeImage_ReturnsNonEmptyResult()
        {
            int rows = 100;
            int cols = 100;
            bool[, ] image = new bool[rows, cols];
            // Generate a synthetic diagonal: activate the main diagonal.
            for (int i = 0; i < (rows < cols ? rows : cols); i++)
            {
                image[i, i] = true;
            }

            HoughTransformProcessor processor = new HoughTransformProcessor();
            List<HoughLine> result = processor.ProcessImage(image, 1);
            Assert.IsTrue(result.Count > 0);
        }
    }
}