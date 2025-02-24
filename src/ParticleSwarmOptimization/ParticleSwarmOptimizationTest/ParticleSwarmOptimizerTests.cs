using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParticleSwarmOptimization;

namespace ParticleSwarmOptimizationTest
{
    [TestClass]
    public class ParticleSwarmOptimizerTests
    {
        [TestMethod]
        public void Test_SphereFunctionOptimization()
        {
            int particleCount = 30;
            int dimension = 3;
            double minBound = -10.0;
            double maxBound = 10.0;
            int iterations = 100;
            double inertiaWeight = 0.729;
            double cognitiveCoefficient = 1.49445;
            double socialCoefficient = 1.49445;
            ParticleSwarmOptimizer optimizer = new ParticleSwarmOptimizer(particleCount, dimension, minBound, maxBound, iterations, inertiaWeight, cognitiveCoefficient, socialCoefficient);
            Func<double[], double> sphereFunction = delegate (double[] position)
            {
                double sum = 0;
                for (int i = 0; i < position.Length; i++)
                {
                    sum += position[i] * position[i];
                }

                return sum;
            };
            OptimizationResult result = optimizer.Optimize(sphereFunction);
            Assert.IsNotNull(result);
            Assert.AreEqual(dimension, result.BestPosition.Length);
            Assert.IsTrue(result.BestFitness >= 0, "BestFitness should be non-negative.");
        }

        [TestMethod]
        public void Test_OneDimensionalQuadraticOptimization()
        {
            int particleCount = 50;
            int dimension = 1;
            double minBound = -10.0;
            double maxBound = 10.0;
            int iterations = 500;
            double inertiaWeight = 0.729;
            double cognitiveCoefficient = 1.49445;
            double socialCoefficient = 1.49445;
            ParticleSwarmOptimizer optimizer = new ParticleSwarmOptimizer(particleCount, dimension, minBound, maxBound, iterations, inertiaWeight, cognitiveCoefficient, socialCoefficient);
            Func<double[], double> quadraticFunction = delegate (double[] position)
            {
                double x = position[0];
                return (x - 3.0) * (x - 3.0);
            };
            OptimizationResult result = optimizer.Optimize(quadraticFunction);
            Assert.IsNotNull(result);
            Assert.AreEqual(dimension, result.BestPosition.Length);
            double bestX = result.BestPosition[0];
            Assert.IsTrue(System.Math.Abs(bestX - 3.0) < 1.0, $"Expected best position near 3.0, got {bestX}.");
        }

        [TestMethod]
        public void Test_MultipleRuns()
        {
            int particleCount = 20;
            int dimension = 2;
            double minBound = -10.0;
            double maxBound = 10.0;
            int iterations = 50;
            double inertiaWeight = 0.729;
            double cognitiveCoefficient = 1.49445;
            double socialCoefficient = 1.49445;
            Func<double[], double> sphereFunction = delegate (double[] position)
            {
                double sum = 0;
                for (int i = 0; i < position.Length; i++)
                {
                    sum += position[i] * position[i];
                }

                return sum;
            };
            for (int i = 0; i < 10; i++)
            {
                ParticleSwarmOptimizer optimizer = new ParticleSwarmOptimizer(particleCount, dimension, minBound, maxBound, iterations, inertiaWeight, cognitiveCoefficient, socialCoefficient);
                OptimizationResult result = optimizer.Optimize(sphereFunction);
                Assert.IsNotNull(result);
                Assert.AreEqual(dimension, result.BestPosition.Length);
                Assert.IsTrue(result.BestFitness >= 0);
            }
        }

        [TestMethod]
        public void Test_BestFitnessNonNaN()
        {
            int particleCount = 40;
            int dimension = 5;
            double minBound = -10.0;
            double maxBound = 10.0;
            int iterations = 100;
            double inertiaWeight = 0.729;
            double cognitiveCoefficient = 1.49445;
            double socialCoefficient = 1.49445;
            ParticleSwarmOptimizer optimizer = new ParticleSwarmOptimizer(particleCount, dimension, minBound, maxBound, iterations, inertiaWeight, cognitiveCoefficient, socialCoefficient);
            Func<double[], double> sphereFunction = delegate (double[] position)
            {
                double sum = 0;
                for (int i = 0; i < position.Length; i++)
                {
                    sum += position[i] * position[i];
                }

                return sum;
            };
            OptimizationResult result = optimizer.Optimize(sphereFunction);
            Assert.IsNotNull(result);
            Assert.IsFalse(double.IsNaN(result.BestFitness));
        }
    }
}