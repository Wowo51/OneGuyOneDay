using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatershedTransformation;

namespace WatershedTransformationTest
{
    [TestClass]
    public class WatershedSegmenterTests
    {
        [TestMethod]
        public void Test_NullInput()
        {
            WatershedSegmenter segmenter = new WatershedSegmenter();
            int[, ] result = segmenter.SegmentImage(null);
            Assert.AreEqual(0, result.GetLength(0));
            Assert.AreEqual(0, result.GetLength(1));
        }

        [TestMethod]
        public void Test_SinglePixel()
        {
            int[, ] image = new int[1, 1]
            {
                {
                    5
                }
            };
            WatershedSegmenter segmenter = new WatershedSegmenter();
            int[, ] result = segmenter.SegmentImage(image);
            Assert.AreEqual(1, result.GetLength(0));
            Assert.AreEqual(1, result.GetLength(1));
            Assert.IsTrue(result[0, 0] > 0);
        }

        [TestMethod]
        public void Test_TwoByTwoMatrix()
        {
            int[, ] image = new int[2, 2]
            {
                {
                    1,
                    2
                },
                {
                    3,
                    4
                }
            };
            WatershedSegmenter segmenter = new WatershedSegmenter();
            int[, ] result = segmenter.SegmentImage(image);
            Assert.AreEqual(2, result.GetLength(0));
            Assert.AreEqual(2, result.GetLength(1));
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.AreEqual(1, result[i, j]);
                }
            }
        }

        [TestMethod]
        public void Test_UniformImage()
        {
            int[, ] image = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    image[i, j] = 100;
                }
            }

            WatershedSegmenter segmenter = new WatershedSegmenter();
            int[, ] result = segmenter.SegmentImage(image);
            Assert.AreEqual(3, result.GetLength(0));
            Assert.AreEqual(3, result.GetLength(1));
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.IsTrue(result[i, j] >= 0);
                }
            }
        }

        [TestMethod]
        public void Test_RowImage()
        {
            int[, ] image = new int[1, 5]
            {
                {
                    5,
                    3,
                    8,
                    2,
                    7
                }
            };
            WatershedSegmenter segmenter = new WatershedSegmenter();
            int[, ] result = segmenter.SegmentImage(image);
            Assert.AreEqual(1, result.GetLength(0));
            Assert.AreEqual(5, result.GetLength(1));
        }

        [TestMethod]
        public void Test_ColumnImage()
        {
            int[, ] image = new int[5, 1]
            {
                {
                    5
                },
                {
                    3
                },
                {
                    8
                },
                {
                    2
                },
                {
                    7
                }
            };
            WatershedSegmenter segmenter = new WatershedSegmenter();
            int[, ] result = segmenter.SegmentImage(image);
            Assert.AreEqual(5, result.GetLength(0));
            Assert.AreEqual(1, result.GetLength(1));
        }

        [TestMethod]
        public void Test_LargeRandomImage()
        {
            int height = 50;
            int width = 50;
            int[, ] image = new int[height, width];
            System.Random random = new System.Random(0);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    image[i, j] = random.Next(0, 256);
                }
            }

            WatershedSegmenter segmenter = new WatershedSegmenter();
            int[, ] result = segmenter.SegmentImage(image);
            Assert.AreEqual(height, result.GetLength(0));
            Assert.AreEqual(width, result.GetLength(1));
        }
    }
}