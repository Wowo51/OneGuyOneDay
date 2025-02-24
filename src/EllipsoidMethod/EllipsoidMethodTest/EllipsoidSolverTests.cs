using Microsoft.VisualStudio.TestTools.UnitTesting;
using EllipsoidMethod;
using System;

namespace EllipsoidMethodTest
{
    [TestClass]
    public class EllipsoidSolverTests
    {
        private static bool AreArraysEqual(double[] expected, double[] actual, double tolerance)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
            {
                if (System.Math.Abs(expected[i] - actual[i]) > tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void TestSolveWithNullOracle_ReturnsInitialCenter()
        {
            int dimension = 3;
            double[] initialCenter = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double[, ] initialShape = new double[, ]
            {
                {
                    1.0,
                    0.0,
                    0.0
                },
                {
                    0.0,
                    1.0,
                    0.0
                },
                {
                    0.0,
                    0.0,
                    1.0
                }
            };
            EllipsoidSolver solver = new EllipsoidSolver(dimension, initialCenter, initialShape);
            double[] result = solver.Solve(NullOracle);
            Assert.IsTrue(AreArraysEqual(initialCenter, result, 1e-10));
        }

        private static double[]? NullOracle(double[] point)
        {
            return null;
        }

        [TestMethod]
        public void TestSolveWithProvidedOracle_ReturnsFeasibleSolution()
        {
            int dimension = 2;
            double[] initialCenter = new double[]
            {
                0.0,
                0.0
            };
            double[, ] initialShape = new double[, ]
            {
                {
                    4.0,
                    0.0
                },
                {
                    0.0,
                    4.0
                }
            };
            EllipsoidSolver solver = new EllipsoidSolver(dimension, initialCenter, initialShape);
            double[] result = solver.Solve(Program.EllipsoidMethodOracle);
            double sumSquares = result[0] * result[0] + result[1] * result[1];
            double sum = result[0] + result[1];
            Assert.IsTrue(sumSquares <= 4.0 + 1e-8, "Sum of squares exceeds bound.");
            Assert.IsTrue(sum >= 1.0 - 1e-8, "Sum of components is below lower bound.");
        }

        [TestMethod]
        public void TestSolveWithFixedSubgradient_IterationsLimit()
        {
            int dimension = 2;
            double[] initialCenter = new double[]
            {
                10.0,
                -10.0
            };
            double[, ] initialShape = new double[, ]
            {
                {
                    5.0,
                    0.0
                },
                {
                    0.0,
                    5.0
                }
            };
            EllipsoidSolver solver = new EllipsoidSolver(dimension, initialCenter, initialShape);
            double[] result = solver.Solve(ConstantSubgradientOracle);
            Assert.AreEqual(dimension, result.Length);
        }

        private static double[] ConstantSubgradientOracle(double[] point)
        {
            int length = point.Length;
            double[] subgradient = new double[length];
            for (int i = 0; i < length; i++)
            {
                subgradient[i] = 1.0;
            }

            return subgradient;
        }
    }
}