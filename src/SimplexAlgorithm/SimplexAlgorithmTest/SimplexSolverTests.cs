using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplexAlgorithm;

namespace SimplexAlgorithmTest
{
    [TestClass]
    public class SimplexSolverTests
    {
        [TestMethod]
        public void TestSolve_NullInput_A_ReturnsEmptyArray()
        {
            double[, ] A = null;
            double[] b = new double[]
            {
                1
            };
            double[] c = new double[]
            {
                1
            };
            double[] result = SimplexSolver.Solve(A, b, c);
            Assert.AreEqual(0, result.Length, "Solver should return empty array for null A.");
        }

        [TestMethod]
        public void TestSolve_NullInput_B_ReturnsEmptyArray()
        {
            double[, ] A = new double[, ]
            {
                {
                    1
                }
            };
            double[] b = null;
            double[] c = new double[]
            {
                1
            };
            double[] result = SimplexSolver.Solve(A, b, c);
            Assert.AreEqual(0, result.Length, "Solver should return empty array for null b.");
        }

        [TestMethod]
        public void TestSolve_NullInput_C_ReturnsEmptyArray()
        {
            double[, ] A = new double[, ]
            {
                {
                    1
                }
            };
            double[] b = new double[]
            {
                1
            };
            double[] c = null;
            double[] result = SimplexSolver.Solve(A, b, c);
            Assert.AreEqual(0, result.Length, "Solver should return empty array for null c.");
        }

        [TestMethod]
        public void TestSolve_Optimal()
        {
            double[, ] A = new double[, ]
            {
                {
                    1,
                    2
                },
                {
                    1,
                    1
                }
            };
            double[] b = new double[]
            {
                6,
                4
            };
            double[] c = new double[]
            {
                3,
                2
            };
            double[] result = SimplexSolver.Solve(A, b, c);
            Assert.AreEqual(2, result.Length, "Solution length should equal number of variables.");
            double tolerance = 1e-6;
            Assert.AreEqual(4.0, result[0], tolerance, "First variable should be 4.");
            Assert.AreEqual(0.0, result[1], tolerance, "Second variable should be 0.");
        }

        [TestMethod]
        public void TestSolve_Unbounded()
        {
            double[, ] A = new double[, ]
            {
                {
                    -1
                }
            };
            double[] b = new double[]
            {
                -1
            };
            double[] c = new double[]
            {
                1
            };
            double[] result = SimplexSolver.Solve(A, b, c);
            Assert.AreEqual(0, result.Length, "Solver should return empty array for unbounded solution.");
        }

        [TestMethod]
        public void TestSolve_Trivial()
        {
            double[, ] A = new double[, ]
            {
                {
                    1
                }
            };
            double[] b = new double[]
            {
                10
            };
            double[] c = new double[]
            {
                1
            };
            double[] result = SimplexSolver.Solve(A, b, c);
            Assert.AreEqual(1, result.Length, "Solution length should be 1.");
            double tolerance = 1e-6;
            Assert.AreEqual(10.0, result[0], tolerance, "Solution should be 10.");
        }

        [TestMethod]
        public void TestSolve_Random()
        {
            int numConstraints = 3;
            int numVariables = 3;
            double[, ] A = new double[numConstraints, numVariables];
            double[] b = new double[numConstraints];
            double[] c = new double[numVariables];
            System.Random random = new System.Random(42);
            for (int i = 0; i < numConstraints; i++)
            {
                double sum = 0;
                for (int j = 0; j < numVariables; j++)
                {
                    double value = random.NextDouble() * 9.0 + 1.0;
                    A[i, j] = value;
                    sum += value;
                }

                b[i] = sum + random.NextDouble() * 10.0;
            }

            for (int j = 0; j < numVariables; j++)
            {
                c[j] = random.NextDouble() * 10.0;
            }

            double[] result = SimplexSolver.Solve(A, b, c);
            Assert.AreEqual(numVariables, result.Length, "Solution length should equal number of variables.");
            double tolerance = 1e-6;
            for (int i = 0; i < numConstraints; i++)
            {
                double sum = 0;
                for (int j = 0; j < numVariables; j++)
                {
                    sum += A[i, j] * result[j];
                }

                Assert.IsTrue(sum <= b[i] + tolerance, "Constraint " + i.ToString() + " is not satisfied.");
            }
        }
    }
}