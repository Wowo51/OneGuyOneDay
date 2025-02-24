using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneExpressionProgramming;

namespace GeneExpressionProgrammingTest
{
    [TestClass]
    public class ChromosomeTests
    {
        [TestMethod]
        public void TestClone()
        {
            Chromosome original = new Chromosome("abc");
            original.Fitness = 50.0;
            Chromosome cloneInstance = original.Clone();
            Assert.AreEqual(original.Gene, cloneInstance.Gene);
            Assert.AreEqual(original.Fitness, cloneInstance.Fitness);
            cloneInstance.Fitness = 25.0;
            Assert.AreNotEqual(original.Fitness, cloneInstance.Fitness);
        }

        [TestMethod]
        public void TestCrossoverNonEmptyGenes()
        {
            Chromosome parent1 = new Chromosome("abcdef");
            Chromosome parent2 = new Chromosome("uvwxyz");
            Chromosome child = Chromosome.Crossover(parent1, parent2);
            Assert.AreEqual(parent1.Gene.Length, child.Gene.Length);
            Assert.IsNotNull(child.Gene);
        }

        [TestMethod]
        public void TestCrossoverEmptyGene()
        {
            Chromosome parent1 = new Chromosome("");
            Chromosome parent2 = new Chromosome("uvwxyz");
            Chromosome child = Chromosome.Crossover(parent1, parent2);
            Assert.AreEqual(string.Empty, child.Gene);
        }

        [TestMethod]
        public void TestMutateNoMutation()
        {
            Chromosome chromosome = new Chromosome("abcdef");
            Chromosome mutated = chromosome.Mutate(0.0);
            Assert.AreEqual(chromosome.Gene, mutated.Gene);
        }
    }

    [TestClass]
    public class ProgramInterpreterTests
    {
        [TestMethod]
        public void TestExecute_Addition()
        {
            // 'a' yields 1 added for each occurrence.
            string gene = "aaa";
            double result = ProgramInterpreter.Execute(gene);
            Assert.AreEqual(3.0, result);
        }

        [TestMethod]
        public void TestExecute_Subtraction()
        {
            // 'z' yields 26 subtracted for each occurrence.
            string gene = "zz";
            double result = ProgramInterpreter.Execute(gene);
            Assert.AreEqual(-52.0, result);
        }

        [TestMethod]
        public void TestExecute_Mixed()
        {
            // 'a' (1) and 'm' (13) add; 'z' (26) subtracts.
            // Calculation: 1 + 13 - 26 = -12.
            string gene = "amz";
            double result = ProgramInterpreter.Execute(gene);
            Assert.AreEqual(-12.0, result);
        }
    }

    [TestClass]
    public class GEPAlgorithmTests
    {
        [TestMethod]
        public void TestInitializePopulation()
        {
            int populationSize = 10;
            int geneLength = 5;
            GEPAlgorithm algorithm = new GEPAlgorithm(populationSize, geneLength, 0.1, 10);
            algorithm.InitializePopulation();
            Assert.AreEqual(populationSize, algorithm.Population.Count);
            for (int i = 0; i < populationSize; i++)
            {
                Assert.AreEqual(geneLength, algorithm.Population[i].Gene.Length);
            }
        }

        [TestMethod]
        public void TestEvaluateFitness()
        {
            // Manually create a population with predictable genes.
            GEPAlgorithm algorithm = new GEPAlgorithm(0, 0, 0.1, 10);
            List<Chromosome> population = new List<Chromosome>();
            Chromosome c1 = new Chromosome("aaaa"); // Expected: 1+1+1+1=4, error=38, fitness=62
            population.Add(c1);
            Chromosome c2 = new Chromosome("zzzz"); // Expected: -26*4=-104, error=146, fitness=0
            population.Add(c2);
            algorithm.Population = population;
            algorithm.EvaluateFitness();
            Assert.AreEqual(62.0, c1.Fitness, 0.001);
            Assert.AreEqual(0.0, c2.Fitness, 0.001);
        }

        [TestMethod]
        public void TestTournamentSelection()
        {
            int populationSize = 5;
            GEPAlgorithm algorithm = new GEPAlgorithm(populationSize, 5, 0.1, 10);
            algorithm.InitializePopulation();
            // Set fitness values manually.
            algorithm.Population[0].Fitness = 10;
            algorithm.Population[1].Fitness = 20;
            algorithm.Population[2].Fitness = 30;
            algorithm.Population[3].Fitness = 40;
            algorithm.Population[4].Fitness = 50;
            Chromosome selected = algorithm.TournamentSelection(3);
            bool found = false;
            for (int i = 0; i < populationSize; i++)
            {
                if (algorithm.Population[i].Fitness == selected.Fitness)
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestEvolvePopulationCount()
        {
            int populationSize = 15;
            GEPAlgorithm algorithm = new GEPAlgorithm(populationSize, 10, 0.1, 5);
            algorithm.Evolve();
            Assert.AreEqual(populationSize, algorithm.Population.Count);
        }
    }
}