using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionGrowing;

namespace RegionGrowingTest
{
    [TestClass]
    public class RegionGrowerTests
    {
        [TestMethod]
        public void GrowRegion_NullImage_ReturnsZeroDimension()
        {
            RegionGrower regionGrower = new RegionGrower();
            bool[, ] result = regionGrower.GrowRegion(null, 0, 0, 10);
            Assert.AreEqual(0, result.GetLength(0));
            Assert.AreEqual(0, result.GetLength(1));
        }

        [TestMethod]
        public void GrowRegion_SeedOutOfBounds_ReturnsEmptyRegion()
        {
            int[, ] image = new int[2, 3]
            {
                {
                    5,
                    5,
                    5
                },
                {
                    5,
                    5,
                    5
                }
            };
            RegionGrower regionGrower = new RegionGrower();
            bool[, ] result = regionGrower.GrowRegion(image, 2, 1, 1);
            Assert.AreEqual(2, result.GetLength(0));
            Assert.AreEqual(3, result.GetLength(1));
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Assert.IsFalse(result[i, j], $"Cell at ({i}, {j}) should be false.");
                }
            }
        }

        [TestMethod]
        public void GrowRegion_SinglePixelImage_ReturnsTrue()
        {
            int[, ] image = new int[1, 1]
            {
                {
                    42
                }
            };
            RegionGrower regionGrower = new RegionGrower();
            bool[, ] result = regionGrower.GrowRegion(image, 0, 0, 0);
            Assert.AreEqual(1, result.GetLength(0));
            Assert.AreEqual(1, result.GetLength(1));
            Assert.IsTrue(result[0, 0], "The only pixel should be in the region.");
        }

        [TestMethod]
        public void GrowRegion_ValidRegionTest()
        {
            int[, ] image = new int[3, 3]
            {
                {
                    10,
                    10,
                    5
                },
                {
                    10,
                    10,
                    6
                },
                {
                    5,
                    6,
                    10
                }
            };
            RegionGrower regionGrower = new RegionGrower();
            bool[, ] result = regionGrower.GrowRegion(image, 0, 0, 2);
            bool[, ] expected = new bool[3, 3]
            {
                {
                    true,
                    true,
                    false
                },
                {
                    true,
                    true,
                    false
                },
                {
                    false,
                    false,
                    false
                }
            };
            Assert.AreEqual(3, result.GetLength(0));
            Assert.AreEqual(3, result.GetLength(1));
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], $"Mismatch at ({i}, {j}).");
                }
            }
        }

        [TestMethod]
        public void GrowRegion_AllTrueWithLargeThreshold()
        {
            int rows = 50;
            int cols = 50;
            int[, ] image = new int[rows, cols];
            Random randomGenerator = new Random(0);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    image[i, j] = randomGenerator.Next(0, 100);
                }
            }

            RegionGrower regionGrower = new RegionGrower();
            bool[, ] result = regionGrower.GrowRegion(image, rows / 2, cols / 2, 1000);
            Assert.AreEqual(rows, result.GetLength(0));
            Assert.AreEqual(cols, result.GetLength(1));
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Assert.IsTrue(result[i, j], $"Cell at ({i}, {j}) should be true.");
                }
            }
        }
    }
}