using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BensonAlgorithm.Algorithms;
using BensonAlgorithm.Models;

namespace BensonAlgorithmTest
{
    [TestClass]
    public class BensonSolverTests
    {
        [TestMethod]
        public void BensonSolver_NullProblem_ReturnsEmptySolutions()
        {
            OptimizationResult result = BensonSolver.Solve(null, 3);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.ParetoSolutions.Count);
        }

        [TestMethod]
        public void BensonSolver_SimpleProblem_ReturnsSolution()
        {
            double[, ] A = new double[1, 2]
            {
                {
                    1.0,
                    1.0
                }
            };
            double[] b = new double[1]
            {
                1.0
            };
            double[, ] C = new double[2, 2]
            {
                {
                    1.0,
                    0.0
                },
                {
                    0.0,
                    1.0
                }
            };
            LinearVectorOptimizationProblem problem = new LinearVectorOptimizationProblem(A, b, C);
            OptimizationResult result = BensonSolver.Solve(problem, 3);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ParetoSolutions.Count > 0);
            foreach (ParetoSolution sol in result.ParetoSolutions)
            {
                Assert.AreEqual(2, sol.DecisionVector.Length);
                double sum = sol.DecisionVector[0] + sol.DecisionVector[1];
                Assert.IsTrue(sum <= 1.000001);
            }
        }

        [TestMethod]
        public void SimplexSolver_BoundedProblem_ReturnsCorrectSolution()
        {
            double[] c = new double[2]
            {
                0.0,
                -1.0
            };
            double[, ] A = new double[1, 2]
            {
                {
                    1.0,
                    1.0
                }
            };
            double[] b = new double[1]
            {
                1.0
            };
            double[]? solution = SimplexSolver.SolveMinimization(c, A, b);
            Assert.IsNotNull(solution);
            Assert.AreEqual(2, solution!.Length);
            Assert.IsTrue(Math.Abs(solution![0] - 0.0) < 1e-6);
            Assert.IsTrue(Math.Abs(solution![1] - 1.0) < 1e-6);
        }

        [TestMethod]
        public void SimplexSolver_UnboundedProblem_ReturnsNull()
        {
            double[] c = new double[1]
            {
                -1.0
            };
            double[, ] A = new double[1, 1]
            {
                {
                    -1.0
                }
            };
            double[] b = new double[1]
            {
                0.0
            };
            double[]? solution = SimplexSolver.SolveMinimization(c, A, b);
            Assert.IsNull(solution);
        }

        [TestMethod]
        public void SimplexSolver_RandomLargeProblem_ReturnsZeroSolution()
        {
            Random random = new Random(42);
            int n = 5;
            int m = 3;
            double[] c = new double[n];
            for (int i = 0; i < n; i++)
            {
                c[i] = random.NextDouble();
            }

            double[, ] A = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = random.NextDouble();
                }
            }

            double[] b = new double[m];
            for (int i = 0; i < m; i++)
            {
                b[i] = random.NextDouble() + 1.0;
            }

            double[]? solution = SimplexSolver.SolveMinimization(c, A, b);
            Assert.IsNotNull(solution);
            for (int i = 0; i < n; i++)
            {
                Assert.IsTrue(Math.Abs(solution![i]) < 1e-6);
            }
        }
    }
}