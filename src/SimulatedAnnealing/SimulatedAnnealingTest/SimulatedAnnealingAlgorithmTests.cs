using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimulatedAnnealing;

namespace SimulatedAnnealingTest
{
    [TestClass]
    public class SimulatedAnnealingAlgorithmTests
    {
        [TestMethod]
        public void TestExecuteReturnsFiniteValue()
        {
            SimulatedAnnealingAlgorithm algorithm = new SimulatedAnnealingAlgorithm();
            double result = algorithm.Execute();
            Assert.IsFalse(double.IsNaN(result), "Result should not be NaN.");
            Assert.IsFalse(double.IsInfinity(result), "Result should be finite.");
        }

        [TestMethod]
        public void TestMultipleRunsStatisticalDistribution()
        {
            SimulatedAnnealingAlgorithm algorithm = new SimulatedAnnealingAlgorithm();
            algorithm.InitialTemperature = 100.0;
            algorithm.CoolingRate = 0.95;
            algorithm.Iterations = 1000;
            double bestObjective = double.MaxValue;
            int runs = 50;
            for (int i = 0; i < runs; i++)
            {
                double candidate = algorithm.Execute();
                double objective = candidate * candidate;
                if (objective < bestObjective)
                {
                    bestObjective = objective;
                }
            }

            Assert.IsTrue(bestObjective < 1.0, "Best objective value should be less than 1.0 in multiple runs.");
        }

        [TestMethod]
        public void TestEffectOfIterations()
        {
            int runs = 30;
            double sumLow = 0.0;
            double sumHigh = 0.0;
            for (int i = 0; i < runs; i++)
            {
                SimulatedAnnealingAlgorithm lowIterationAlgorithm = new SimulatedAnnealingAlgorithm();
                lowIterationAlgorithm.Iterations = 10;
                double candidateLow = lowIterationAlgorithm.Execute();
                sumLow += candidateLow * candidateLow;
            }

            for (int i = 0; i < runs; i++)
            {
                SimulatedAnnealingAlgorithm highIterationAlgorithm = new SimulatedAnnealingAlgorithm();
                highIterationAlgorithm.Iterations = 1000;
                double candidateHigh = highIterationAlgorithm.Execute();
                sumHigh += candidateHigh * candidateHigh;
            }

            double averageLow = sumLow / runs;
            double averageHigh = sumHigh / runs;
            Assert.IsTrue(averageHigh < averageLow, "High iterations should yield lower average objective than low iterations.");
        }
    }
}