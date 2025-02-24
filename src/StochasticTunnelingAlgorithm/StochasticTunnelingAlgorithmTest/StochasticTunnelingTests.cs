using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using StochasticTunnelingAlgorithm;

namespace StochasticTunnelingAlgorithmTest
{
    [TestClass]
    public class StochasticTunnelingTests
    {
        [TestMethod]
        public void TestTransformObjective()
        {
            double gamma = 1.0;
            double objective = 1.0;
            double bestObjective = 0.0;
            StochasticTunneling tunneling = new StochasticTunneling(gamma);
            double expected = 1.0 - Math.Exp(-gamma * (objective - bestObjective));
            double actual = tunneling.TransformObjective(objective, bestObjective);
            Assert.AreEqual(expected, actual, 1e-6);
        }

        [TestMethod]
        public void TestOptimizeZeroIterations()
        {
            double gamma = 1.0;
            StochasticTunneling tunneling = new StochasticTunneling(gamma);
            double[] initialGuess = new double[]
            {
                1.0,
                2.0
            };
            double[] result = tunneling.Optimize(SquareSum, initialGuess, 0);
            Assert.AreEqual(initialGuess.Length, result.Length);
            for (int i = 0; i < initialGuess.Length; i++)
            {
                Assert.AreEqual(initialGuess[i], result[i], 1e-6);
            }
        }

        [TestMethod]
        public void TestOptimizeImprovement()
        {
            double gamma = 10.0;
            StochasticTunneling tunneling = new StochasticTunneling(gamma);
            double[] initialGuess = new double[]
            {
                10.0,
                -10.0
            };
            double initialVal = SquareSum(initialGuess);
            int iterations = 1000;
            double[] result = tunneling.Optimize(SquareSum, initialGuess, iterations);
            double resultVal = SquareSum(result);
            Assert.IsTrue(resultVal <= initialVal, "Optimized objective should be lower than or equal to the initial objective");
        }

        [TestMethod]
        public void TestOptimizeLargeDimension()
        {
            double gamma = 1.0;
            StochasticTunneling tunneling = new StochasticTunneling(gamma);
            const int dimension = 100;
            double[] initialGuess = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                initialGuess[i] = (double)(i + 1);
            }

            double initialVal = SquareSum(initialGuess);
            int iterations = 5000;
            double[] result = tunneling.Optimize(SquareSum, initialGuess, iterations);
            double resultVal = SquareSum(result);
            Assert.IsTrue(resultVal < initialVal, "Optimized objective should be lower than the initial objective for large dimensions");
            Assert.AreEqual(dimension, result.Length);
        }

        private static double SquareSum(double[] vector)
        {
            double sum = 0.0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += vector[i] * vector[i];
            }

            return sum;
        }
    }
}