using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeesAlgorithm;

namespace BeesAlgorithmTest
{
    [TestClass]
    public class BeesAlgorithmTests
    {
        private double Tolerance = 0.5;
        [TestMethod]
        public void Test_NullObjective_ReturnsNull()
        {
            BeesAlgorithmSolver solver = new BeesAlgorithmSolver();
            double[] lower = new double[]
            {
                -10,
                -10
            };
            double[] upper = new double[]
            {
                10,
                10
            };
            BeeSolution? result = solver.FindOptimal(null, lower, upper, 50, 10, 5, 20, 2.0, 100);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_NullLowerBounds_ReturnsNull()
        {
            BeesAlgorithmSolver solver = new BeesAlgorithmSolver();
            double[] upper = new double[]
            {
                10,
                10
            };
            BeeSolution? result = solver.FindOptimal(delegate (double[] x)
            {
                return -1;
            }, null, upper, 50, 10, 5, 20, 2.0, 100);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_MismatchedBounds_ReturnsNull()
        {
            BeesAlgorithmSolver solver = new BeesAlgorithmSolver();
            double[] lower = new double[]
            {
                -10,
                -10
            };
            double[] upper = new double[]
            {
                10
            };
            BeeSolution? result = solver.FindOptimal(delegate (double[] x)
            {
                return -1;
            }, lower, upper, 50, 10, 5, 20, 2.0, 100);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_OptimizeQuadraticFunction()
        {
            BeesAlgorithmSolver solver = new BeesAlgorithmSolver();
            double Objective(double[] position)
            {
                double sum = 0;
                for (int i = 0; i < position.Length; i++)
                {
                    sum += position[i] * position[i];
                }

                return -sum;
            }

            double[] lower = new double[]
            {
                -10,
                -10
            };
            double[] upper = new double[]
            {
                10,
                10
            };
            BeeSolution? result = solver.FindOptimal(Objective, lower, upper, 50, 10, 5, 20, 2.0, 100);
            Assert.IsNotNull(result);
            BeeSolution solution = result!;
            Assert.IsTrue(Math.Abs(solution.Fitness) <= 0.5, "Fitness is not near optimal value 0.");
            for (int i = 0; i < solution.Position.Length; i++)
            {
                Assert.IsTrue(solution.Position[i] >= lower[i] && solution.Position[i] <= upper[i]);
            }
        }

        [TestMethod]
        public void Test_OptimizeShiftedQuadraticFunction()
        {
            BeesAlgorithmSolver solver = new BeesAlgorithmSolver();
            double Objective(double[] position)
            {
                double sum = 0;
                for (int i = 0; i < position.Length; i++)
                {
                    double diff = position[i] - 5.0;
                    sum += diff * diff;
                }

                return -sum;
            }

            double[] lower = new double[]
            {
                0,
                0
            };
            double[] upper = new double[]
            {
                10,
                10
            };
            BeeSolution? result = solver.FindOptimal(Objective, lower, upper, 50, 10, 5, 20, 2.0, 100);
            Assert.IsNotNull(result);
            BeeSolution solution = result!;
            Assert.IsTrue(Math.Abs(solution.Fitness) <= 1.0, "Fitness is not near optimal value 0.");
            for (int i = 0; i < solution.Position.Length; i++)
            {
                Assert.IsTrue(Math.Abs(solution.Position[i] - 5.0) <= Tolerance, "Position component not near optimal 5.");
            }
        }

        [TestMethod]
        public void Test_SyntheticLargeTest()
        {
            BeesAlgorithmSolver solver = new BeesAlgorithmSolver();
            double Objective(double[] position)
            {
                double sum = 0;
                for (int i = 0; i < position.Length; i++)
                {
                    double diff = position[i] - 1.0;
                    sum += diff * diff;
                }

                return -sum;
            }

            int dimension = 5;
            double[] lower = new double[dimension];
            double[] upper = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                lower[i] = 0;
                upper[i] = 2;
            }

            BeeSolution? result = solver.FindOptimal(Objective, lower, upper, 100, 20, 10, 40, 0.5, 200);
            Assert.IsNotNull(result);
            BeeSolution solution = result!;
            Assert.IsTrue(Math.Abs(solution.Fitness) <= 1.0, "Fitness is not near optimal value 0.");
            for (int i = 0; i < solution.Position.Length; i++)
            {
                Assert.IsTrue(Math.Abs(solution.Position[i] - 1.0) <= 0.5, "Position component not near optimal 1.");
            }
        }
    }
}