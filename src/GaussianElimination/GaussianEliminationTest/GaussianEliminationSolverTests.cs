using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GaussianElimination;

namespace GaussianEliminationTest
{
    [TestClass]
    public class GaussianEliminationSolverTests
    {
        [TestMethod]
        public void Test2x2UniqueSolution()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    2,
                    1
                },
                {
                    1,
                    3
                }
            };
            double[] b = new double[]
            {
                5,
                6
            };
            double[] result = GaussianEliminationSolver.Solve(matrix, b);
            Assert.AreEqual(2, result.Length, "Expected 2-dim solution.");
            Assert.AreEqual(1.8, result[0], 1e-9, "Expected 1.8 as first element.");
            Assert.AreEqual(1.4, result[1], 1e-9, "Expected 1.4 as second element.");
        }

        [TestMethod]
        public void Test3x3UniqueSolution()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    2,
                    -1,
                    0
                },
                {
                    -1,
                    2,
                    -1
                },
                {
                    0,
                    -1,
                    2
                }
            };
            double[] b = new double[]
            {
                1,
                0,
                1
            };
            double[] result = GaussianEliminationSolver.Solve(matrix, b);
            Assert.AreEqual(3, result.Length, "Expected 3-dim solution.");
            Assert.AreEqual(1.0, result[0], 1e-9, "Expected 1.0 as first element.");
            Assert.AreEqual(1.0, result[1], 1e-9, "Expected 1.0 as second element.");
            Assert.AreEqual(1.0, result[2], 1e-9, "Expected 1.0 as third element.");
        }

        [TestMethod]
        public void TestSingularMatrix()
        {
            double[, ] matrix = new double[, ]
            {
                {
                    1,
                    2
                },
                {
                    2,
                    4
                }
            };
            double[] b = new double[]
            {
                3,
                6
            };
            double[] result = GaussianEliminationSolver.Solve(matrix, b);
            Assert.AreEqual(0, result.Length, "Expected empty array for singular matrix.");
        }

        [TestMethod]
        public void TestDimensionMismatch()
        {
            double[, ] matrix = new double[, ]
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
            double[] b = new double[]
            {
                5
            };
            double[] result = GaussianEliminationSolver.Solve(matrix, b);
            Assert.AreEqual(0, result.Length, "Expected empty array for dimension mismatch.");
        }

        [TestMethod]
        public void TestLargeSyntheticSystem()
        {
            const int size = 5;
            double[, ] matrix = new double[size, size];
            double[] solution = new double[size];
            double[] b = new double[size];
            Random random = new Random(0);
            for (int i = 0; i < size; i++)
            {
                double sum = 0;
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        matrix[i, j] = random.NextDouble();
                        sum += System.Math.Abs(matrix[i, j]);
                    }
                }

                matrix[i, i] = sum + random.NextDouble() + 1.0;
            }

            for (int i = 0; i < size; i++)
            {
                solution[i] = random.NextDouble() * 10;
            }

            for (int i = 0; i < size; i++)
            {
                double s = 0;
                for (int j = 0; j < size; j++)
                {
                    s += matrix[i, j] * solution[j];
                }

                b[i] = s;
            }

            double[] result = GaussianEliminationSolver.Solve(matrix, b);
            Assert.AreEqual(size, result.Length, "Expected solution size match.");
            for (int i = 0; i < size; i++)
            {
                Assert.AreEqual(solution[i], result[i], 1e-6, "Solution mismatch at index " + i);
            }
        }
    }
}