using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ConnectedComponentLabeling;

namespace ConnectedComponentLabelingTest
{
    [TestClass]
    public class ConnectedComponentLabelerTests
    {
        [TestMethod]
        public void LabelComponents_NullInput_ReturnsEmptyArray()
        {
            int[, ] result = ConnectedComponentLabeler.LabelComponents(null);
            Assert.AreEqual(0, result.GetLength(0));
            Assert.AreEqual(0, result.GetLength(1));
        }

        [TestMethod]
        public void LabelComponents_AllFalse_ReturnsAllZeros()
        {
            bool[, ] image = new bool[3, 3];
            int[, ] result = ConnectedComponentLabeler.LabelComponents(image);
            for (int row = 0; row < result.GetLength(0); row++)
            {
                for (int col = 0; col < result.GetLength(1); col++)
                {
                    Assert.AreEqual(0, result[row, col]);
                }
            }
        }

        [TestMethod]
        public void LabelComponents_SimplePattern_ReturnsExpectedLabels()
        {
            bool[, ] image = new bool[, ]
            {
                {
                    true,
                    true,
                    false,
                    false
                },
                {
                    true,
                    false,
                    false,
                    true
                },
                {
                    false,
                    false,
                    true,
                    true
                },
                {
                    false,
                    true,
                    true,
                    false
                }
            };
            int[, ] expected = new int[, ]
            {
                {
                    1,
                    1,
                    0,
                    0
                },
                {
                    1,
                    0,
                    0,
                    2
                },
                {
                    0,
                    0,
                    2,
                    2
                },
                {
                    0,
                    2,
                    2,
                    0
                }
            };
            int[, ] result = ConnectedComponentLabeler.LabelComponents(image);
            int rows = result.GetLength(0);
            int cols = result.GetLength(1);
            Assert.AreEqual(expected.GetLength(0), rows);
            Assert.AreEqual(expected.GetLength(1), cols);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Assert.AreEqual(expected[row, col], result[row, col], string.Format("Mismatch at ({0},{1})", row, col));
                }
            }
        }

        [TestMethod]
        public void LabelComponents_OnePixelTrue_ReturnsSingleLabel()
        {
            bool[, ] image = new bool[1, 1];
            image[0, 0] = true;
            int[, ] result = ConnectedComponentLabeler.LabelComponents(image);
            Assert.AreEqual(1, result[0, 0]);
        }

        [TestMethod]
        public void LabelComponents_RandomImage_TestInvariants()
        {
            int rows = 50;
            int cols = 50;
            bool[, ] image = new bool[rows, cols];
            Random random = new Random(42);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    image[i, j] = random.NextDouble() < 0.3;
                }
            }

            int[, ] result = ConnectedComponentLabeler.LabelComponents(image);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (image[i, j])
                    {
                        Assert.IsTrue(result[i, j] > 0, "Expected label > 0 at true pixel");
                    }
                    else
                    {
                        Assert.AreEqual(0, result[i, j], "Expected 0 label at false pixel");
                    }
                }
            }
        }

        [TestMethod]
        public void LabelComponents_AllTrue_ReturnsSingleComponent()
        {
            int rows = 4;
            int cols = 4;
            bool[, ] image = new bool[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    image[i, j] = true;
                }
            }

            int[, ] result = ConnectedComponentLabeler.LabelComponents(image);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Assert.AreEqual(1, result[i, j], string.Format("Expected label 1 at ({0},{1})", i, j));
                }
            }
        }

        [TestMethod]
        public void LabelComponents_DiagonalTrue_NotConnected()
        {
            bool[, ] image = new bool[, ]
            {
                {
                    true,
                    false
                },
                {
                    false,
                    true
                }
            };
            int[, ] result = ConnectedComponentLabeler.LabelComponents(image);
            Assert.AreEqual(1, result[0, 0]);
            Assert.AreEqual(0, result[0, 1]);
            Assert.AreEqual(0, result[1, 0]);
            Assert.AreEqual(2, result[1, 1]);
        }
    }
}