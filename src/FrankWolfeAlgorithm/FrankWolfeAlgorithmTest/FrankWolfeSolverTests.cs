using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrankWolfeAlgorithm;

namespace FrankWolfeAlgorithmTest
{
    [TestClass]
    public class FrankWolfeSolverTests
    {
        private void AssertVectorsAreEqual(Vector expected, Vector actual, double tolerance)
        {
            Assert.AreEqual(expected.Dimension, actual.Dimension, "Dimensions do not match.");
            for (int i = 0; i < expected.Dimension; i++)
            {
                Assert.AreEqual(expected.Values[i], actual.Values[i], tolerance, "Difference at index " + i);
            }
        }

        [TestMethod]
        public void TestSolve_NullInput()
        {
            // Passing null for x0 should return an empty vector.
            Vector result = FrankWolfeSolver.Solve(null, null, null, null, 1e-6, 10);
            Assert.AreEqual(0, result.Dimension);
        }

        [TestMethod]
        public void TestSolve_NullGradientAndOracle()
        {
            // With x0 non-null but null gradient and oracle, the algorithm generates zero vectors.
            double[] initial = new double[]
            {
                5.0
            };
            Vector x0 = new Vector(initial);
            // Dummy objective function (not used by the solver)
            ObjectiveFunction objective = new ObjectiveFunction(delegate (Vector v)
            {
                return v.Values[0];
            });
            Vector result = FrankWolfeSolver.Solve(x0, objective, null, null, 1e-6, 10);
            Assert.AreEqual(1, result.Dimension);
            Assert.AreEqual(0.0, result.Values[0], 1e-6);
        }

        [TestMethod]
        public void TestSolve_OneDim_Convergence()
        {
            // Solve a simple 1D problem:
            // Minimize f(x) = (x-3)^2 on the feasible set [0,3] where the oracle always returns 3.
            Vector x0 = new Vector(new double[] { 0.0 });
            ObjectiveFunction objective = new ObjectiveFunction(delegate (Vector v)
            {
                return Math.Pow(v.Values[0] - 3.0, 2.0);
            });
            GradientFunction gradient = new GradientFunction(delegate (Vector v)
            {
                return new Vector(new double[] { 2.0 * (v.Values[0] - 3.0) });
            });
            // Oracle returns constant 3, ensuring the update jumps directly to 3.
            LinearMinimizationOracle oracle = new LinearMinimizationOracle(delegate (Vector grad)
            {
                return new Vector(new double[] { 3.0 });
            });
            Vector result = FrankWolfeSolver.Solve(x0, objective, gradient, oracle, 1e-6, 10);
            Assert.AreEqual(1, result.Dimension);
            Assert.AreEqual(3.0, result.Values[0], 1e-6);
        }

        [TestMethod]
        public void TestSolve_MultiDim_Convergence()
        {
            // Test a multi-dimensional case.
            int dimension = 3;
            double[] initialValues = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                initialValues[i] = 0.0;
            }

            Vector x0 = new Vector(initialValues);
            ObjectiveFunction objective = new ObjectiveFunction(delegate (Vector v)
            {
                double sum = 0.0;
                for (int i = 0; i < v.Dimension; i++)
                {
                    sum += Math.Pow(v.Values[i] - 3.0, 2.0);
                }

                return sum;
            });
            GradientFunction gradient = new GradientFunction(delegate (Vector v)
            {
                double[] grad = new double[v.Dimension];
                for (int i = 0; i < v.Dimension; i++)
                {
                    grad[i] = 2.0 * (v.Values[i] - 3.0);
                }

                return new Vector(grad);
            });
            // Oracle returns a constant vector with all entries 3.
            LinearMinimizationOracle oracle = new LinearMinimizationOracle(delegate (Vector grad)
            {
                double[] constant = new double[grad.Dimension];
                for (int i = 0; i < grad.Dimension; i++)
                {
                    constant[i] = 3.0;
                }

                return new Vector(constant);
            });
            Vector result = FrankWolfeSolver.Solve(x0, objective, gradient, oracle, 1e-6, 10);
            Assert.AreEqual(dimension, result.Dimension);
            for (int i = 0; i < dimension; i++)
            {
                Assert.AreEqual(3.0, result.Values[i], 1e-6);
            }
        }

        [TestMethod]
        public void TestSolve_LargeSynthetic_NullFunctions()
        {
            // Generate a large synthetic test with dimension 100.
            int dimension = 100;
            double[] initialValues = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                initialValues[i] = i + 1.0;
            }

            Vector x0 = new Vector(initialValues);
            // With null gradient and oracle the first iteration moves x0 to a zero vector.
            Vector result = FrankWolfeSolver.Solve(x0, null, null, null, 1e-6, 10);
            Assert.AreEqual(dimension, result.Dimension);
            for (int i = 0; i < dimension; i++)
            {
                Assert.AreEqual(0.0, result.Values[i], 1e-6, "Element " + i + " is not zero.");
            }
        }
    }
}