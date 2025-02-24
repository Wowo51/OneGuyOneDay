using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomSearch;

namespace RandomSearchTest
{
    [TestClass]
    public class RandomSearchTests
    {
        [TestMethod]
        public void TestOptimizeWithValidInput()
        {
            double[] lowerBounds = new double[]
            {
                -10.0,
                -10.0
            };
            double[] upperBounds = new double[]
            {
                10.0,
                10.0
            };
            int iterations = 1000;
            RandomSearchAlgorithm algorithm = new RandomSearchAlgorithm();
            Func<double[], double> objective = delegate (double[] candidate)
            {
                return candidate[0] * candidate[0] + candidate[1] * candidate[1];
            };
            RandomSearchResult result = algorithm.Optimize(lowerBounds, upperBounds, objective, iterations);
            Assert.IsNotNull(result.Candidate, "Candidate should not be null.");
            Assert.AreEqual(2, result.Candidate.Length, "Candidate length should be 2.");
            for (int i = 0; i < result.Candidate.Length; i++)
            {
                Assert.IsTrue(result.Candidate[i] >= lowerBounds[i] && result.Candidate[i] <= upperBounds[i], "Candidate value out of bounds.");
            }

            double computed = objective(result.Candidate);
            Assert.AreEqual(computed, result.ObjectiveValue, 1e-6, "Objective value mismatch.");
        }

        [TestMethod]
        public void TestOptimizeWithNullInput()
        {
            double[] lowerBounds = null;
            double[] upperBounds = new double[]
            {
                10.0
            };
            RandomSearchAlgorithm algorithm = new RandomSearchAlgorithm();
            Func<double[], double> objective = delegate (double[] candidate)
            {
                return 0.0;
            };
            RandomSearchResult result = algorithm.Optimize(lowerBounds, upperBounds, objective, 10);
            Assert.IsNotNull(result.Candidate, "Candidate should not be null even for null input.");
            Assert.AreEqual(0, result.Candidate.Length, "Candidate should be empty for null input.");
            Assert.AreEqual(double.PositiveInfinity, result.ObjectiveValue, "Objective value should be PositiveInfinity.");
        }

        [TestMethod]
        public void TestOptimizeWithMismatchedArrays()
        {
            double[] lowerBounds = new double[]
            {
                -10.0,
                -10.0
            };
            double[] upperBounds = new double[]
            {
                10.0
            };
            RandomSearchAlgorithm algorithm = new RandomSearchAlgorithm();
            Func<double[], double> objective = delegate (double[] candidate)
            {
                return 0.0;
            };
            RandomSearchResult result = algorithm.Optimize(lowerBounds, upperBounds, objective, 10);
            Assert.IsNotNull(result.Candidate, "Candidate should not be null for mismatched arrays.");
            Assert.AreEqual(0, result.Candidate.Length, "Candidate should be empty for mismatched arrays.");
            Assert.AreEqual(double.PositiveInfinity, result.ObjectiveValue, "Objective value should be PositiveInfinity for mismatched arrays.");
        }

        [TestMethod]
        public void TestOptimizeWithLargeDimensions()
        {
            int dimensions = 10;
            double[] lowerBounds = new double[dimensions];
            double[] upperBounds = new double[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                lowerBounds[i] = -100.0;
                upperBounds[i] = 100.0;
            }

            RandomSearchAlgorithm algorithm = new RandomSearchAlgorithm();
            int iterations = 10000;
            Func<double[], double> objective = delegate (double[] candidate)
            {
                double sum = 0.0;
                for (int j = 0; j < candidate.Length; j++)
                {
                    sum += candidate[j] * candidate[j];
                }

                return sum;
            };
            RandomSearchResult result = algorithm.Optimize(lowerBounds, upperBounds, objective, iterations);
            Assert.IsNotNull(result.Candidate, "Candidate should not be null.");
            Assert.AreEqual(dimensions, result.Candidate.Length, "Candidate length mismatch.");
            for (int i = 0; i < dimensions; i++)
            {
                Assert.IsTrue(result.Candidate[i] >= lowerBounds[i] && result.Candidate[i] <= upperBounds[i], "Candidate value out of bounds.");
            }

            double computed = objective(result.Candidate);
            Assert.AreEqual(computed, result.ObjectiveValue, 1e-6, "Objective value mismatch.");
            Assert.IsTrue(result.ObjectiveValue >= 0.0, "Objective value should be non-negative.");
        }
    }
}