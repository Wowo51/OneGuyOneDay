using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JacobiMethod;

namespace JacobiMethodTest
{
    [TestClass]
    public class JacobiTests
    {
        private const double Tolerance = 1e-6;
        [TestMethod]
        public void TestJacobiEigenvalue_NullMatrix()
        {
            double[, ]? matrix = null;
            double[] eigen = JacobiEigenvalue.ComputeEigenvalues(matrix, 1e-6, 100);
            Assert.AreEqual(0, eigen.Length);
        }

        [TestMethod]
        public void TestJacobiEigenvalue_NonSquareMatrix()
        {
            double[, ] matrix = new double[2, 3];
            double[] eigen = JacobiEigenvalue.ComputeEigenvalues(matrix, 1e-6, 100);
            Assert.AreEqual(0, eigen.Length);
        }

        [TestMethod]
        public void TestJacobiEigenvalue_SmallSymmetricMatrix()
        {
            double[, ] matrix = new double[2, 2]
            {
                {
                    2,
                    1
                },
                {
                    1,
                    2
                }
            };
            double[] eigen = JacobiEigenvalue.ComputeEigenvalues(matrix, 1e-9, 100);
            Assert.AreEqual(2, eigen.Length);
            double[] sorted = (double[])eigen.Clone();
            Array.Sort(sorted);
            Assert.IsTrue(Math.Abs(sorted[0] - 1.0) < 1e-3, $"Expected eigenvalue approx 1, got {sorted[0]}");
            Assert.IsTrue(Math.Abs(sorted[1] - 3.0) < 1e-3, $"Expected eigenvalue approx 3, got {sorted[1]}");
        }

        [TestMethod]
        public void TestJacobiEigenvalue_RandomSymmetricMatrix()
        {
            int n = 10;
            double[, ] matrix = new double[n, n];
            Random random = new Random(42);
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    double value = random.NextDouble();
                    matrix[i, j] = value;
                    matrix[j, i] = value;
                }
            }

            double trace = 0.0;
            for (int i = 0; i < n; i++)
            {
                trace += matrix[i, i];
            }

            double[] eigen = JacobiEigenvalue.ComputeEigenvalues(matrix, 1e-9, 1000);
            double eigenTrace = 0.0;
            for (int i = 0; i < n; i++)
            {
                eigenTrace += eigen[i];
            }

            Assert.IsTrue(Math.Abs(trace - eigenTrace) < 1e-3, "Trace mismatch in eigenvalues");
        }

        [TestMethod]
        public void TestJacobiSolver_NullInputs()
        {
            double[, ]? matrix = null;
            double[]? vector = new double[]
            {
                1,
                2,
                3
            };
            double[] solution = JacobiSolver.Solve(matrix, vector, 1e-6, 100);
            Assert.AreEqual(0, solution.Length);
            matrix = new double[3, 3];
            vector = null;
            solution = JacobiSolver.Solve(matrix, vector, 1e-6, 100);
            Assert.AreEqual(0, solution.Length);
        }

        [TestMethod]
        public void TestJacobiSolver_NonSquareMatrix()
        {
            double[, ] matrix = new double[2, 3];
            double[] vector = new double[]
            {
                1,
                2
            };
            double[] solution = JacobiSolver.Solve(matrix, vector, 1e-6, 100);
            Assert.AreEqual(2, solution.Length);
            for (int i = 0; i < solution.Length; i++)
            {
                Assert.AreEqual(0.0, solution[i], Tolerance);
            }
        }

        [TestMethod]
        public void TestJacobiSolver_DiagonalMatrix()
        {
            double[, ] matrix = new double[2, 2]
            {
                {
                    4,
                    0
                },
                {
                    0,
                    5
                }
            };
            double[] vector = new double[]
            {
                8,
                10
            };
            double[] solution = JacobiSolver.Solve(matrix, vector, 1e-6, 100);
            Assert.AreEqual(2, solution.Length);
            Assert.AreEqual(2.0, solution[0], Tolerance);
            Assert.AreEqual(2.0, solution[1], Tolerance);
        }

        [TestMethod]
        public void TestJacobiSolver_GeneralMatrix()
        {
            double[, ] matrix = new double[2, 2]
            {
                {
                    4,
                    1
                },
                {
                    2,
                    3
                }
            };
            double[] vector = new double[]
            {
                1,
                2
            };
            double[] solution = JacobiSolver.Solve(matrix, vector, 1e-6, 100);
            Assert.AreEqual(2, solution.Length);
            Assert.IsTrue(Math.Abs(solution[0] - 0.1) < 1e-2, $"x0 expected 0.1 but got {solution[0]}");
            Assert.IsTrue(Math.Abs(solution[1] - 0.6) < 1e-2, $"x1 expected 0.6 but got {solution[1]}");
        }

        [TestMethod]
        public void TestJacobiSolver_LargeRandomSystem()
        {
            int n = 20;
            double[, ] matrix = new double[n, n];
            double[] xTrue = new double[n];
            double[] vector = new double[n];
            Random random = new Random(21);
            for (int i = 0; i < n; i++)
            {
                xTrue[i] = random.NextDouble();
            }

            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        double value = random.NextDouble();
                        matrix[i, j] = value;
                        sum += Math.Abs(value);
                    }
                }

                matrix[i, i] = sum + random.NextDouble() + 1.0;
            }

            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < n; j++)
                {
                    sum += matrix[i, j] * xTrue[j];
                }

                vector[i] = sum;
            }

            double[] solution = JacobiSolver.Solve(matrix, vector, 1e-6, 500);
            Assert.AreEqual(n, solution.Length);
            for (int i = 0; i < n; i++)
            {
                Assert.IsTrue(Math.Abs(solution[i] - xTrue[i]) < 1e-3, $"x[{i}] expected {xTrue[i]} but got {solution[i]}");
            }
        }
    }
}