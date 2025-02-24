using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneralisedHoughTransform;

namespace GeneralisedHoughTransformTest
{
    [TestClass]
    public class GeneralisedHoughTransformUnitTests
    {
        [TestMethod]
        public void Test_SetTemplate_Null_DoesNotThrow()
        {
            GeneralisedHoughTransform.GeneralisedHoughTransform ght = new GeneralisedHoughTransform.GeneralisedHoughTransform();
            ght.SetTemplate(null);
        // No exception should be thrown when null is passed.
        }

        [TestMethod]
        public void Test_Detect_NullImage_ReturnsEmptyAccumulator()
        {
            GeneralisedHoughTransform.GeneralisedHoughTransform ght = new GeneralisedHoughTransform.GeneralisedHoughTransform();
            int[, ] accumulator = ght.Detect(null);
            Assert.IsNotNull(accumulator);
            Assert.AreEqual(0, accumulator.GetLength(0));
            Assert.AreEqual(0, accumulator.GetLength(1));
        }

        [TestMethod]
        public void Test_Detect_BasicCase()
        {
            // Create a minimal 3x3 template that produces a strong gradient at its only inner pixel.
            double[, ] template = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    template[i, j] = 10;
                }
            }

            // Introduce a gradient in the horizontal direction at cell (1,1).
            template[1, 2] = 30;
            GeneralisedHoughTransform.GeneralisedHoughTransform ght = new GeneralisedHoughTransform.GeneralisedHoughTransform();
            ght.SetTemplate(template);
            // Create a detection image that mimics the template's gradient.
            double[, ] image = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    image[i, j] = 10;
                }
            }

            image[1, 2] = 30;
            int[, ] accumulator = ght.Detect(image);
            Assert.AreEqual(3, accumulator.GetLength(0));
            Assert.AreEqual(3, accumulator.GetLength(1));
            // The only inner pixel (1,1) should be incremented by 1.
            Assert.AreEqual(1, accumulator[1, 1]);
        }

        [TestMethod]
        public void Test_Detect_RandomImage()
        {
            // Use the same simple template to define detection offsets.
            double[, ] template = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    template[i, j] = 10;
                }
            }

            template[1, 2] = 30;
            GeneralisedHoughTransform.GeneralisedHoughTransform ght = new GeneralisedHoughTransform.GeneralisedHoughTransform();
            ght.SetTemplate(template);
            // Generate a synthetic random 50x50 image with values ranging from 0 to 100.
            int imageSize = 50;
            double[, ] image = new double[imageSize, imageSize];
            Random random = new Random(42);
            for (int i = 0; i < imageSize; i++)
            {
                for (int j = 0; j < imageSize; j++)
                {
                    image[i, j] = random.NextDouble() * 100;
                }
            }

            int[, ] accumulator = ght.Detect(image);
            Assert.AreEqual(imageSize, accumulator.GetLength(0));
            Assert.AreEqual(imageSize, accumulator.GetLength(1));
            // Verify that no cell in the accumulator has a negative count.
            for (int i = 0; i < imageSize; i++)
            {
                for (int j = 0; j < imageSize; j++)
                {
                    Assert.IsTrue(accumulator[i, j] >= 0);
                }
            }
        }

        [TestMethod]
        public void Test_Detect_NoTemplate()
        {
            // Detection without setting a template should not vote any accumulator cells.
            GeneralisedHoughTransform.GeneralisedHoughTransform ght = new GeneralisedHoughTransform.GeneralisedHoughTransform();
            double[, ] image = new double[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    image[i, j] = 50;
                }
            }

            image[2, 3] = 100;
            int[, ] accumulator = ght.Detect(image);
            for (int i = 0; i < accumulator.GetLength(0); i++)
            {
                for (int j = 0; j < accumulator.GetLength(1); j++)
                {
                    Assert.AreEqual(0, accumulator[i, j]);
                }
            }
        }

        [TestMethod]
        public void Test_Detect_EmptyImage()
        {
            // An empty image should result in an empty accumulator.
            GeneralisedHoughTransform.GeneralisedHoughTransform ght = new GeneralisedHoughTransform.GeneralisedHoughTransform();
            double[, ] emptyImage = new double[0, 0];
            int[, ] accumulator = ght.Detect(emptyImage);
            Assert.IsNotNull(accumulator);
            Assert.AreEqual(0, accumulator.GetLength(0));
            Assert.AreEqual(0, accumulator.GetLength(1));
        }

        [TestMethod]
        public void Test_Detect_OnThreshold()
        {
            // Test detection when gradient magnitude equals the threshold exactly.
            double[, ] template = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    template[i, j] = 10;
                }
            }

            template[1, 2] = 20; // gradX = 20 - 10 = 10 exactly
            GeneralisedHoughTransform.GeneralisedHoughTransform ght = new GeneralisedHoughTransform.GeneralisedHoughTransform();
            ght.SetTemplate(template);
            double[, ] image = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    image[i, j] = 10;
                }
            }

            image[1, 2] = 20; // gradX = 10 exactly
            int[, ] accumulator = ght.Detect(image);
            Assert.AreEqual(3, accumulator.GetLength(0));
            Assert.AreEqual(3, accumulator.GetLength(1));
            // The only inner pixel (1,1) should be incremented by 1.
            Assert.AreEqual(1, accumulator[1, 1]);
        }
    }
}