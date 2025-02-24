using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TruncationSelection;

namespace TruncationSelectionTest
{
    [TestClass]
    public class TruncationSelectorTests
    {
        [TestMethod]
        public void Select_NullIndividuals_ReturnsEmptyList()
        {
            TruncationSelector selector = new TruncationSelector();
            List<Individual> result = selector.Select(null, 0.5);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Select_EmptyIndividuals_ReturnsEmptyList()
        {
            TruncationSelector selector = new TruncationSelector();
            List<Individual> individuals = new List<Individual>();
            List<Individual> result = selector.Select(individuals, 0.5);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Select_InvalidFraction_LessOrEqualZero_ReturnsEmptyList()
        {
            TruncationSelector selector = new TruncationSelector();
            List<Individual> individuals = new List<Individual>
            {
                new Individual(10.0),
                new Individual(20.0)
            };
            List<Individual> resultZero = selector.Select(individuals, 0.0);
            Assert.AreEqual(0, resultZero.Count);
            List<Individual> resultNegative = selector.Select(individuals, -0.5);
            Assert.AreEqual(0, resultNegative.Count);
        }

        [TestMethod]
        public void Select_InvalidFraction_GreaterThanOne_ReturnsEmptyList()
        {
            TruncationSelector selector = new TruncationSelector();
            List<Individual> individuals = new List<Individual>
            {
                new Individual(10.0),
                new Individual(20.0)
            };
            List<Individual> result = selector.Select(individuals, 1.1);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Select_ValidFraction_ReturnsCorrectNumberOfIndividuals()
        {
            TruncationSelector selector = new TruncationSelector();
            List<Individual> individuals = new List<Individual>
            {
                new Individual(10.0),
                new Individual(40.0),
                new Individual(30.0),
                new Individual(20.0)
            };
            List<Individual> result = selector.Select(individuals, 0.5); // Ceil(4 * 0.5) = 2
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void Select_ValidFraction_ReturnsIndividualsSortedByFitness()
        {
            TruncationSelector selector = new TruncationSelector();
            List<Individual> individuals = new List<Individual>
            {
                new Individual(10.0),
                new Individual(40.0),
                new Individual(30.0),
                new Individual(20.0)
            };
            List<Individual> result = selector.Select(individuals, 1.0);
            Assert.AreEqual(4, result.Count);
            for (int i = 1; i < result.Count; i++)
            {
                Assert.IsTrue(result[i - 1].Fitness >= result[i].Fitness);
            }
        }

        [TestMethod]
        public void Select_LargeSyntheticDataset_ReturnsCorrectResult()
        {
            TruncationSelector selector = new TruncationSelector();
            List<Individual> individuals = new List<Individual>();
            Random random = new Random(12345);
            int totalCount = 100;
            for (int i = 0; i < totalCount; i++)
            {
                double fitness = random.NextDouble() * 100.0;
                individuals.Add(new Individual(fitness));
            }

            double fraction = 0.3;
            List<Individual> result = selector.Select(individuals, fraction);
            int expectedCount = (int)Math.Ceiling(totalCount * fraction);
            Assert.AreEqual(expectedCount, result.Count);
            for (int i = 1; i < result.Count; i++)
            {
                Assert.IsTrue(result[i - 1].Fitness >= result[i].Fitness);
            }
        }

        // Additional tests for thorough unit coverage
        [TestMethod]
        public void Select_SingleIndividual_ReturnsTheIndividual()
        {
            TruncationSelector selector = new TruncationSelector();
            List<Individual> individuals = new List<Individual>
            {
                new Individual(42.0)
            };
            List<Individual> result = selector.Select(individuals, 1.0);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(42.0, result[0].Fitness);
        }

        [TestMethod]
        public void Select_FractionForOddCount_RoundsUp()
        {
            TruncationSelector selector = new TruncationSelector();
            List<Individual> individuals = new List<Individual>
            {
                new Individual(10.0),
                new Individual(5.0),
                new Individual(15.0)
            };
            List<Individual> result = selector.Select(individuals, 0.5);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0].Fitness >= result[1].Fitness);
            Assert.AreEqual(15.0, result[0].Fitness);
            Assert.AreEqual(10.0, result[1].Fitness);
        }
    }
}