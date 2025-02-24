using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MedianFiltering;

namespace MedianFilteringTest
{
    [TestClass]
    public class MedianFilterTests
    {
        [TestMethod]
        public void NullInput_ReturnsEmptyArray()
        {
            int[, ] result = MedianFilter.ApplyMedianFilter(null, 3);
            Assert.AreEqual(0, result.GetLength(0));
            Assert.AreEqual(0, result.GetLength(1));
        }

        [TestMethod]
        public void WindowSizeOne_ReturnsSameImage()
        {
            int[, ] input = new int[, ]
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
            int[, ] result = MedianFilter.ApplyMedianFilter(input, 1);
            Assert.AreEqual(input[0, 0], result[0, 0]);
            Assert.AreEqual(input[0, 1], result[0, 1]);
            Assert.AreEqual(input[1, 0], result[1, 0]);
            Assert.AreEqual(input[1, 1], result[1, 1]);
        }

        [TestMethod]
        public void EvenWindowSize_IsAdaptedToOdd()
        {
            int[, ] input = new int[, ]
            {
                {
                    1,
                    2,
                    3
                },
                {
                    4,
                    5,
                    6
                },
                {
                    7,
                    8,
                    9
                }
            };
            int[, ] result = MedianFilter.ApplyMedianFilter(input, 2);
            int[, ] expected = new int[, ]
            {
                {
                    4,
                    4,
                    5
                },
                {
                    5,
                    5,
                    6
                },
                {
                    7,
                    7,
                    8
                }
            };
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], "Mismatch at position [" + i + "][" + j + "]");
                }
            }
        }

        [TestMethod]
        public void NonSquareMatrix_EdgeHandling()
        {
            int[, ] input = new int[, ]
            {
                {
                    1,
                    2,
                    3
                },
                {
                    4,
                    5,
                    6
                }
            };
            int[, ] result = MedianFilter.ApplyMedianFilter(input, 3);
            int[, ] expected = new int[, ]
            {
                {
                    4,
                    4,
                    5
                },
                {
                    4,
                    4,
                    5
                }
            };
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], "Mismatch at position [" + i + "][" + j + "]");
                }
            }
        }

        [TestMethod]
        public void SyntheticLargeDataTest()
        {
            int height = 50;
            int width = 50;
            int[, ] input = new int[height, width];
            System.Random rnd = new System.Random(123);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    input[i, j] = rnd.Next(0, 256);
                }
            }

            int[, ] result = MedianFilter.ApplyMedianFilter(input, 3);
            Assert.AreEqual(height, result.GetLength(0));
            Assert.AreEqual(width, result.GetLength(1));
        }

        [TestMethod]
        public void OneElementMatrix_ReturnsSameElement()
        {
            int[, ] input = new int[1, 1]
            {
                {
                    42
                }
            };
            int[, ] result = MedianFilter.ApplyMedianFilter(input, 3);
            Assert.AreEqual(42, result[0, 0]);
        }

        [TestMethod]
        public void SingleRowMatrix_ReturnsCorrect()
        {
            int[, ] input = new int[1, 5]
            {
                {
                    5,
                    3,
                    4,
                    2,
                    1
                }
            };
            int[, ] result = MedianFilter.ApplyMedianFilter(input, 3);
            // Expected medians calculated for each column position:
            // Column 0: considers {5, 3} >> sorted {3, 5} >> median = 5
            // Column 1: considers {5, 3, 4} >> sorted {3, 4, 5} >> median = 4
            // Column 2: considers {3, 4, 2} >> sorted {2, 3, 4} >> median = 3
            // Column 3: considers {4, 2, 1} >> sorted {1, 2, 4} >> median = 2
            // Column 4: considers {2, 1} >> sorted {1, 2} >> median = 2
            int[] expected = new int[5]
            {
                5,
                4,
                3,
                2,
                2
            };
            for (int j = 0; j < expected.Length; j++)
            {
                Assert.AreEqual(expected[j], result[0, j], "Mismatch at column " + j);
            }
        }

        [TestMethod]
        public void SingleColumnMatrix_ReturnsCorrect()
        {
            int[, ] input = new int[5, 1]
            {
                {
                    7
                },
                {
                    3
                },
                {
                    5
                },
                {
                    1
                },
                {
                    9
                }
            };
            int[, ] result = MedianFilter.ApplyMedianFilter(input, 3);
            // Expected medians for each row:
            // Row 0: considers {7, 3} >> sorted {3, 7} >> median = 7
            // Row 1: considers {7, 3, 5} >> sorted {3, 5, 7} >> median = 5
            // Row 2: considers {3, 5, 1} >> sorted {1, 3, 5} >> median = 3
            // Row 3: considers {5, 1, 9} >> sorted {1, 5, 9} >> median = 5
            // Row 4: considers {1, 9} >> sorted {1, 9} >> median = 9
            int[] expected = new int[5]
            {
                7,
                5,
                3,
                5,
                9
            };
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i, 0], "Mismatch at row " + i);
            }
        }
    }
}