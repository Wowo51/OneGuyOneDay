using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DifferentialEvolution;

namespace DifferentialEvolutionTest
{
    [TestClass]
    public class DifferentialEvolutionAlgorithmTests
    {
        [TestMethod]
        public void TestCandidateWithinBounds()
        {
            int populationSize = 20;
            int dimensions = 2;
            int maxGenerations = 100;
            double[] lowerBounds = new double[]
            {
                -5.0,
                -5.0
            };
            double[] upperBounds = new double[]
            {
                5.0,
                5.0
            };
            Func<double[], double> objectiveFunction = delegate (double[] vector)
            {
                double sum = 0.0;
                for (int i = 0; i < vector.Length; i++)
                {
                    sum += vector[i] * vector[i];
                }

                return sum;
            };
            DifferentialEvolutionAlgorithm algorithm = new DifferentialEvolutionAlgorithm(populationSize, dimensions, lowerBounds, upperBounds, maxGenerations, 0.8, 0.9, objectiveFunction);
            Candidate best = algorithm.Optimize();
            Assert.IsNotNull(best);
            Assert.IsNotNull(best.Vector);
            Assert.AreEqual(dimensions, best.Vector.Length);
            for (int i = 0; i < dimensions; i++)
            {
                Assert.IsTrue(best.Vector[i] >= lowerBounds[i] && best.Vector[i] <= upperBounds[i], "Candidate value out of bounds");
            }
        }

        [TestMethod]
        public void TestDefaultBoundsWhenNullProvided()
        {
            int populationSize = 10;
            int dimensions = 3;
            int maxGenerations = 50;
            DifferentialEvolutionAlgorithm algorithm = new DifferentialEvolutionAlgorithm(populationSize, dimensions, null, null, maxGenerations, 0.5, 0.7, null);
            Candidate best = algorithm.Optimize();
            Assert.IsNotNull(best);
            Assert.IsNotNull(best.Vector);
            Assert.AreEqual(dimensions, best.Vector.Length);
            for (int i = 0; i < dimensions; i++)
            {
                Assert.IsTrue(best.Vector[i] >= 0.0 && best.Vector[i] <= 1.0, "Candidate value not within default bounds");
            }
        }

        [TestMethod]
        public void TestPopulationSizeMinimum()
        {
            int populationSize = 3;
            int dimensions = 2;
            int maxGenerations = 50;
            double[] lowerBounds = new double[]
            {
                -1.0,
                -1.0
            };
            double[] upperBounds = new double[]
            {
                1.0,
                1.0
            };
            Func<double[], double> objectiveFunction = delegate (double[] vector)
            {
                return vector[0] * vector[0] + vector[1] * vector[1];
            };
            DifferentialEvolutionAlgorithm algorithm = new DifferentialEvolutionAlgorithm(populationSize, dimensions, lowerBounds, upperBounds, maxGenerations, 0.8, 0.9, objectiveFunction);
            Candidate best = algorithm.Optimize();
            Assert.IsNotNull(best);
            Assert.AreEqual(dimensions, best.Vector.Length);
        }

        [TestMethod]
        public void TestOptimizeFindsApproximateOptimum()
        {
            int populationSize = 15;
            int dimensions = 1;
            int maxGenerations = 500;
            double[] lowerBounds = new double[]
            {
                0.0
            };
            double[] upperBounds = new double[]
            {
                1.0
            };
            Func<double[], double> objectiveFunction = delegate (double[] vector)
            {
                double diff = vector[0] - 0.3;
                return diff * diff;
            };
            DifferentialEvolutionAlgorithm algorithm = new DifferentialEvolutionAlgorithm(populationSize, dimensions, lowerBounds, upperBounds, maxGenerations, 0.8, 0.9, objectiveFunction);
            Candidate best = algorithm.Optimize();
            Assert.IsNotNull(best);
            double bestValue = best.Vector[0];
            double error = Math.Abs(bestValue - 0.3);
            Assert.IsTrue(error < 0.1, "Optimum not approximated correctly. Error: " + error);
        }

        [TestMethod]
        public void TestLargeDimensionsAndPopulation()
        {
            int populationSize = 100;
            int dimensions = 50;
            int maxGenerations = 200;
            double[] lowerBounds = new double[dimensions];
            double[] upperBounds = new double[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                lowerBounds[i] = -100.0;
                upperBounds[i] = 100.0;
            }

            Func<double[], double> objectiveFunction = delegate (double[] vector)
            {
                double sum = 0.0;
                for (int i = 0; i < vector.Length; i++)
                {
                    sum += vector[i] * vector[i];
                }

                return sum;
            };
            DifferentialEvolutionAlgorithm algorithm = new DifferentialEvolutionAlgorithm(populationSize, dimensions, lowerBounds, upperBounds, maxGenerations, 0.8, 0.9, objectiveFunction);
            Candidate best = algorithm.Optimize();
            Assert.IsNotNull(best);
            Assert.IsNotNull(best.Vector);
            Assert.AreEqual(dimensions, best.Vector.Length);
            for (int i = 0; i < dimensions; i++)
            {
                Assert.IsTrue(best.Vector[i] >= lowerBounds[i] && best.Vector[i] <= upperBounds[i], "Candidate value out of bounds");
            }
        }
    }
}