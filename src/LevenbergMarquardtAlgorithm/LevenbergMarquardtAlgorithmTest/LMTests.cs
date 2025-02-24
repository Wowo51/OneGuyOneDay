using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LevenbergMarquardtAlgorithm;

namespace LevenbergMarquardtAlgorithmTest
{
    [TestClass]
    public class LMTests
    {
        [TestMethod]
        public void TestSolverLinearSmall()
        {
            double[] target = new double[]
            {
                3.0,
                5.0
            };
            Func<double[], double[]> residual = (double[] parameters) =>
            {
                double[] res = new double[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    res[i] = parameters[i] - target[i];
                }

                return res;
            };
            Func<double[], double[, ]> jacobian = (double[] parameters) =>
            {
                int n = parameters.Length;
                double[, ] J = new double[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        J[i, j] = (i == j) ? 1.0 : 0.0;
                    }
                }

                return J;
            };
            LevenbergMarquardtSolver solver = new LevenbergMarquardtSolver();
            double[] initial = new double[]
            {
                0.0,
                0.0
            };
            double[] result = solver.Solve(initial, residual, jacobian, 0.001, 100, 1e-6);
            Assert.AreEqual(target.Length, result.Length);
            for (int i = 0; i < target.Length; i++)
            {
                Assert.IsTrue(Math.Abs(result[i] - target[i]) < 1e-4, "Parameter " + i + " did not converge.");
            }
        }

        [TestMethod]
        public void TestSolverHighDimension()
        {
            int n = 100;
            double[] target = new double[n];
            double[] initial = new double[n];
            System.Random random = new System.Random(12345);
            for (int i = 0; i < n; i++)
            {
                target[i] = random.NextDouble() * 10.0;
                initial[i] = 0.0;
            }

            Func<double[], double[]> residual = (double[] parameters) =>
            {
                double[] res = new double[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    res[i] = parameters[i] - target[i];
                }

                return res;
            };
            Func<double[], double[, ]> jacobian = (double[] parameters) =>
            {
                int dim = parameters.Length;
                double[, ] J = new double[dim, dim];
                for (int i = 0; i < dim; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        J[i, j] = (i == j) ? 1.0 : 0.0;
                    }
                }

                return J;
            };
            LevenbergMarquardtSolver solver = new LevenbergMarquardtSolver();
            double[] result = solver.Solve(initial, residual, jacobian, 0.001, 200, 1e-6);
            Assert.AreEqual(target.Length, result.Length);
            for (int i = 0; i < target.Length; i++)
            {
                Assert.IsTrue(Math.Abs(result[i] - target[i]) < 1e-4, $"Parameter {i} did not converge.");
            }
        }

        [TestMethod]
        public void TestLinearAlgebra_SolveLinearSystem_NonSingular()
        {
            double[, ] matrix = new double[2, 2]
            {
                {
                    2.0,
                    1.0
                },
                {
                    1.0,
                    3.0
                }
            };
            double[] b = new double[2]
            {
                5.0,
                8.0
            };
            double[]? x = LinearAlgebra.SolveLinearSystem(matrix, b);
            Assert.IsNotNull(x);
            Assert.AreEqual(2, x.Length);
            Assert.IsTrue(Math.Abs(x[0] - 1.4) < 1e-4, "x[0] is incorrect.");
            Assert.IsTrue(Math.Abs(x[1] - 2.2) < 1e-4, "x[1] is incorrect.");
        }

        [TestMethod]
        public void TestLinearAlgebra_SolveLinearSystem_Singular()
        {
            double[, ] matrix = new double[2, 2]
            {
                {
                    1.0,
                    2.0
                },
                {
                    2.0,
                    4.0
                }
            };
            double[] b = new double[2]
            {
                3.0,
                6.0
            };
            double[]? x = LinearAlgebra.SolveLinearSystem(matrix, b);
            Assert.IsNull(x, "Expected null for singular matrix.");
        }
    }
}