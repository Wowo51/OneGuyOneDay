using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvolutionStrategy;
using System;
using System.IO;

namespace EvolutionStrategyTest
{
    [TestClass]
    public class EvolutionStrategyTests
    {
        [TestMethod]
        public void Individual_EvaluationTest()
        {
            double[] parameters = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            Individual individual = new Individual(parameters);
            individual.Evaluate();
            Assert.AreEqual(-14.0, individual.Fitness, 1e-6);
        }

        [TestMethod]
        public void EvolutionStrategyAlgorithm_Run_NoExceptionsTest()
        {
            EvolutionStrategyAlgorithm algorithm = new EvolutionStrategyAlgorithm();
            StringWriter writer = new StringWriter();
            Console.SetOut(writer);
            algorithm.Run();
            string output = writer.ToString();
            Assert.IsTrue(output.Contains("Best fitness:"), "Output does not contain best fitness information.");
        }

        [TestMethod]
        public void Individual_LongParameterEvaluationTest()
        {
            int count = 10000;
            Random rnd = new Random();
            double[] parameters = new double[count];
            double expectedSum = 0;
            for (int i = 0; i < count; i++)
            {
                double value = rnd.NextDouble() * 100 - 50;
                parameters[i] = value;
                expectedSum += value * value;
            }

            Individual individual = new Individual(parameters);
            individual.Evaluate();
            Assert.AreEqual(-expectedSum, individual.Fitness, 0.001);
        }
    }
}