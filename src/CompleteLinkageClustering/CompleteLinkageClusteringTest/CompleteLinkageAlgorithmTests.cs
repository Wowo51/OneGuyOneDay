using System;
using System.Collections.Generic;
using CompleteLinkageClustering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompleteLinkageClusteringTest
{
    [TestClass]
    public class CompleteLinkageAlgorithmTests
    {
        [TestMethod]
        public void Cluster_NullParameters_ReturnsEmptyList()
        {
            CompleteLinkageAlgorithm algorithm = new CompleteLinkageAlgorithm();
            List<List<int>> result = algorithm.Cluster<int>(null, Distance, 3.0);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Cluster_NullDistanceFunction_ReturnsEmptyList()
        {
            CompleteLinkageAlgorithm algorithm = new CompleteLinkageAlgorithm();
            List<int> data = new List<int>
            {
                1,
                2,
                3
            };
            List<List<int>> result = algorithm.Cluster(data, null, 3.0);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Cluster_SingleElement_ReturnsSingleCluster()
        {
            CompleteLinkageAlgorithm algorithm = new CompleteLinkageAlgorithm();
            List<int> data = new List<int>
            {
                5
            };
            List<List<int>> result = algorithm.Cluster(data, Distance, 3.0);
            Assert.AreEqual(1, result.Count);
            CollectionAssert.AreEquivalent(data, result[0]);
        }

        [TestMethod]
        public void Cluster_ThresholdTooLow_NoMergeOccurs()
        {
            CompleteLinkageAlgorithm algorithm = new CompleteLinkageAlgorithm();
            List<int> data = new List<int>
            {
                1,
                4,
                9
            };
            List<List<int>> result = algorithm.Cluster(data, Distance, 2.0);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void Cluster_ThresholdHigh_MergeOccurs()
        {
            CompleteLinkageAlgorithm algorithm = new CompleteLinkageAlgorithm();
            List<int> data = new List<int>
            {
                1,
                3,
                2
            };
            List<List<int>> result = algorithm.Cluster(data, Distance, 3.0);
            Assert.AreEqual(1, result.Count);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2, 3 }, result[0]);
        }

        [TestMethod]
        public void Cluster_LargeDataset_PerformanceTest()
        {
            CompleteLinkageAlgorithm algorithm = new CompleteLinkageAlgorithm();
            List<int> data = new List<int>();
            Random random = new Random(42);
            for (int i = 0; i < 1000; i++)
            {
                data.Add(random.Next(0, 1000));
            }

            List<List<int>> result = algorithm.Cluster(data, Distance, 1000.0);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count >= 1 && result.Count <= data.Count);
        }

        public double Distance(int a, int b)
        {
            return Math.Abs(a - b);
        }
    }
}