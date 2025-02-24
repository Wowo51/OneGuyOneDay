using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm;

namespace GeneticAlgorithmTest
{
    [TestClass]
    public class GeneticAlgorithmTests
    {
        [TestMethod]
        public void TestBinaryIndividual_EvaluateFitness()
        {
            int length = 10;
            BinaryIndividual individual = new BinaryIndividual(length);
            bool[] genes = new bool[length];
            genes[0] = true;
            genes[1] = false;
            genes[2] = true;
            genes[3] = true;
            genes[4] = false;
            genes[5] = false;
            genes[6] = true;
            genes[7] = false;
            genes[8] = false;
            genes[9] = true;
            individual.Genes = genes;
            individual.EvaluateFitness();
            Assert.AreEqual(5, individual.Fitness, "Fitness should count number of true genes (expected 5).");
        }

        [TestMethod]
        public void TestBinaryIndividual_CloneIndependence()
        {
            int length = 5;
            BinaryIndividual original = new BinaryIndividual(length);
            bool[] originalGenes = new bool[]
            {
                true,
                false,
                true,
                false,
                true
            };
            original.Genes = originalGenes;
            original.EvaluateFitness();
            BinaryIndividual clone = original.Clone();
            // Modify original and recalculate its fitness.
            original.Genes[0] = false;
            original.EvaluateFitness();
            // After modification, original fitness should be 2.
            Assert.AreEqual(2, original.Fitness, "Original fitness should update after gene change.");
            // Clone remains unaffected with initial fitness of 3.
            Assert.AreEqual(3, clone.Fitness, "Clone fitness should remain unaffected.");
            Assert.IsTrue(clone.Genes[0], "Clone gene at position 0 should remain true.");
        }

        [TestMethod]
        public void TestGeneticAlgorithmEngine_RunProducesValidIndividual()
        {
            int populationSize = 10;
            int individualLength = 20;
            int maxGenerations = 50;
            double crossoverRate = 0.8;
            double mutationRate = 0.05;
            GeneticAlgorithmEngine engine = new GeneticAlgorithmEngine(populationSize, individualLength, maxGenerations, crossoverRate, mutationRate);
            BinaryIndividual best = engine.Run();
            Assert.IsNotNull(best, "Engine should return a non-null individual.");
            Assert.IsTrue(best.Fitness >= 0 && best.Fitness <= individualLength, "Best fitness should be within valid range.");
        }

        [TestMethod]
        public void TestGeneticAlgorithmEngine_RunImprovesFitnessThanZero()
        {
            int populationSize = 50;
            int individualLength = 30;
            int maxGenerations = 100;
            double crossoverRate = 0.7;
            double mutationRate = 0.01;
            GeneticAlgorithmEngine engine = new GeneticAlgorithmEngine(populationSize, individualLength, maxGenerations, crossoverRate, mutationRate);
            BinaryIndividual best = engine.Run();
            Assert.IsTrue(best.Fitness > 0, "Best fitness should be greater than 0, indicating some improvement.");
        }

        [TestMethod]
        public void TestLargePopulationLargeIndividualSize()
        {
            int populationSize = 200;
            int individualLength = 100;
            int maxGenerations = 10;
            double crossoverRate = 0.9;
            double mutationRate = 0.02;
            GeneticAlgorithmEngine engine = new GeneticAlgorithmEngine(populationSize, individualLength, maxGenerations, crossoverRate, mutationRate);
            BinaryIndividual best = engine.Run();
            Assert.IsNotNull(best, "Engine should return non-null individual for large population.");
            Assert.IsTrue(best.Fitness >= 0 && best.Fitness <= individualLength, "Fitness should be within valid range for large population test.");
        }
    }
}