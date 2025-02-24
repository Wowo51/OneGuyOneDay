using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GaussJordanElimination;

namespace GaussJordanEliminationTest
{
    [TestClass]
    public class GaussJordanSolverTests
    {
        private const double Epsilon = 1e-9;
        private void AssertMatrixEqual(double[, ] expected, double[, ] actual)
        {
            int expectedRows = expected.GetLength(0);
            int expectedCols = expected.GetLength(1);
            int actualRows = actual.GetLength(0);
            int actualCols = actual.GetLength(1);
            Assert.AreEqual(expectedRows, actualRows, "Row counts differ.");
            Assert.AreEqual(expectedCols, actualCols, "Column counts differ.");
            for (int i = 0; i < expectedRows; i++)
            {
                for (int j = 0; j < expectedCols; j++)
                {
                    Assert.IsTrue(Math.Abs(expected[i, j] - actual[i, j]) < Epsilon, $"Element mismatch at [{i},{j}]: expected {expected[i, j]} but got {actual[i, j]}.");
                }
            }
        }

        private bool IsRREF(double[, ] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);
            int lastPivot = -1;
            for (int i = 0; i < rowCount; i++)
            {
                int pivotCol = -1;
                for (int j = 0; j < colCount; j++)
                {
                    if (Math.Abs(matrix[i, j]) > Epsilon)
                    {
                        pivotCol = j;
                        break;
                    }
                }

                if (pivotCol != -1)
                {
                    if (Math.Abs(matrix[i, pivotCol] - 1.0) > Epsilon || pivotCol <= lastPivot)
                    {
                        return false;
                    }

                    lastPivot = pivotCol;
                    for (int k = 0; k < rowCount; k++)
                    {
                        if (k != i && Math.Abs(matrix[k, pivotCol]) > Epsilon)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        [TestMethod]
        public void TestNullMatrix()
        {
            double[, ]? matrix = null;
            double[, ] result = GaussJordanSolver.ReduceToRREF(matrix);
            Assert.AreEqual(0, result.GetLength(0), "Expected 0 rows for null input.");
            Assert.AreEqual(0, result.GetLength(1), "Expected 0 columns for null input.");
        }

        [TestMethod]
        public void TestIdentityMatrix()
        {
            double[, ] identity = new double[3, 3]
            {
                {
                    1,
                    0,
                    0
                },
                {
                    0,
                    1,
                    0
                },
                {
                    0,
                    0,
                    1
                }
            };
            double[, ] result = GaussJordanSolver.ReduceToRREF(identity);
            AssertMatrixEqual(identity, result);
        }

        [TestMethod]
        public void Test2x2Matrix()
        {
            double[, ] matrix = new double[2, 2]
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
            double[, ] expected = new double[2, 2]
            {
                {
                    1,
                    0
                },
                {
                    0,
                    1
                }
            };
            double[, ] result = GaussJordanSolver.ReduceToRREF(matrix);
            AssertMatrixEqual(expected, result);
        }

        [TestMethod]
        public void TestDependentRows()
        {
            double[, ] matrix = new double[3, 3]
            {
                {
                    1,
                    2,
                    3
                },
                {
                    2,
                    4,
                    6
                },
                {
                    3,
                    6,
                    9
                }
            };
            double[, ] expected = new double[3, 3]
            {
                {
                    1,
                    2,
                    3
                },
                {
                    0,
                    0,
                    0
                },
                {
                    0,
                    0,
                    0
                }
            };
            double[, ] result = GaussJordanSolver.ReduceToRREF(matrix);
            AssertMatrixEqual(expected, result);
        }

        [TestMethod]
        public void TestNonSquareMatrix()
        {
            double[, ] matrix = new double[2, 3]
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
            double[, ] expected = new double[2, 3]
            {
                {
                    1,
                    0,
                    -1
                },
                {
                    0,
                    1,
                    2
                }
            };
            double[, ] result = GaussJordanSolver.ReduceToRREF(matrix);
            AssertMatrixEqual(expected, result);
        }

        [TestMethod]
        public void TestRandomMatrixRREFProperty()
        {
            Random random = new Random(42);
            int rows = 10;
            int cols = 10;
            double[, ] matrix = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = random.NextDouble() * 20 - 10;
                }
            }

            double[, ] result = GaussJordanSolver.ReduceToRREF(matrix);
            Assert.IsTrue(IsRREF(result), "The resulting matrix is not in RREF.");
        }
    }
}