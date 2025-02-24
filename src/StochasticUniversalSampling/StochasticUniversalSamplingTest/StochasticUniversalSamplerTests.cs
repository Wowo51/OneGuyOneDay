using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using StochasticUniversalSampling;

namespace StochasticUniversalSamplingTest
{
    [TestClass]
    public class StochasticUniversalSamplerTests
    {
        [TestMethod]
        public void Select_NullPopulation_ReturnsEmptyList()
        {
            List<int> result = StochasticUniversalSampler.Select<int>(null, x => (double)x, 5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Select_NullFitnessSelector_ReturnsEmptyList()
        {
            List<int> population = new List<int>
            {
                1,
                2,
                3
            };
            List<int> result = StochasticUniversalSampler.Select<int>(population, null, 5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Select_NonPositiveSelectionCount_ReturnsEmptyList()
        {
            List<int> population = new List<int>
            {
                1,
                2,
                3
            };
            List<int> result = StochasticUniversalSampler.Select<int>(population, x => (double)x, 0);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Select_EmptyPopulation_ReturnsEmptyList()
        {
            List<int> population = new List<int>();
            List<int> result = StochasticUniversalSampler.Select<int>(population, x => (double)x, 5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Select_UniformSelection_FitnessNonPositive()
        {
            List<int> population = new List<int>
            {
                10,
                20,
                30,
                40,
                50
            };
            List<int> result = StochasticUniversalSampler.Select<int>(population, x => 0.0, 10);
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count);
            foreach (int item in result)
            {
                Assert.IsTrue(population.Contains(item));
            }
        }

        [TestMethod]
        public void Select_NormalSelection_ReturnsExpectedCount()
        {
            List<int> population = new List<int>
            {
                10,
                20,
                30,
                40,
                50
            };
            List<int> result = StochasticUniversalSampler.Select<int>(population, x => (double)x, 3);
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            foreach (int item in result)
            {
                Assert.IsTrue(population.Contains(item));
            }
        }

        [TestMethod]
        public void Select_LargeSyntheticData()
        {
            int dataCount = 1000;
            List<int> population = new List<int>();
            for (int i = 1; i <= dataCount; i++)
            {
                population.Add(i);
            }

            List<int> result = StochasticUniversalSampler.Select<int>(population, x => (double)x, 100);
            Assert.IsNotNull(result);
            Assert.AreEqual(100, result.Count);
            foreach (int item in result)
            {
                Assert.IsTrue(item >= 1 && item <= dataCount);
            }
        }

        [TestMethod]
        public void Select_SingleElement_ReturnsSameElementMultipleTimes()
        {
            List<int> population = new List<int>()
            {
                100
            };
            List<int> result = StochasticUniversalSampler.Select<int>(population, x => 1.0, 5);
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count);
            foreach (int item in result)
            {
                Assert.AreEqual(100, item);
            }
        }

        [TestMethod]
        public void Select_OverSelection_DuplicatesAllowed()
        {
            List<int> population = new List<int>()
            {
                10,
                20,
                30
            };
            List<int> result = StochasticUniversalSampler.Select<int>(population, x => (double)x, 5);
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count);
            foreach (int item in result)
            {
                Assert.IsTrue(population.Contains(item));
            }
        }

        [TestMethod]
        public void Select_CustomType_ReturnsExpectedCount()
        {
            List<CustomCandidate> population = new List<CustomCandidate>()
            {
                new CustomCandidate(1),
                new CustomCandidate(2),
                new CustomCandidate(3),
                new CustomCandidate(4),
                new CustomCandidate(5)
            };
            List<CustomCandidate> result = StochasticUniversalSampler.Select<CustomCandidate>(population, candidate => (double)candidate.Value, 3);
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            foreach (CustomCandidate candidate in result)
            {
                Assert.IsTrue(population.Contains(candidate));
            }
        }
    }
}