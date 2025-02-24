using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AverageLinkageClustering;

namespace AverageLinkageClusteringTest
{
    [TestClass]
    public class AverageLinkageClustererTests
    {
        [TestMethod]
        public void Test_NullItems_ReturnsEmpty()
        {
            AverageLinkageClusterer<int> clusterer = new AverageLinkageClusterer<int>();
            List<Cluster<int>> result = clusterer.ClusterData(null, (int a, int b) => System.Math.Abs(a - b), 1);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Test_NullDistance_ReturnsEmpty()
        {
            List<int> items = new List<int>
            {
                1,
                2,
                3
            };
            AverageLinkageClusterer<int> clusterer = new AverageLinkageClusterer<int>();
            List<Cluster<int>> result = clusterer.ClusterData(items, null, 1);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Test_SingleItem_ClusterCount1()
        {
            List<int> items = new List<int>
            {
                42
            };
            AverageLinkageClusterer<int> clusterer = new AverageLinkageClusterer<int>();
            List<Cluster<int>> result = clusterer.ClusterData(items, (int a, int b) => System.Math.Abs(a - b), 1);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Members.Count);
            Assert.AreEqual(42, result[0].Members[0]);
        }

        [TestMethod]
        public void Test_TwoItems_Merge()
        {
            List<int> items = new List<int>
            {
                1,
                2
            };
            AverageLinkageClusterer<int> clusterer = new AverageLinkageClusterer<int>();
            // Test merging into one cluster when desired cluster count is 1.
            List<Cluster<int>> mergedResult = clusterer.ClusterData(items, (int a, int b) => System.Math.Abs(a - b), 1);
            Assert.AreEqual(1, mergedResult.Count);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2 }, mergedResult[0].Members);
            // Test no merging when desired cluster count equals the number of items.
            List<Cluster<int>> separateResult = clusterer.ClusterData(items, (int a, int b) => System.Math.Abs(a - b), 2);
            Assert.AreEqual(2, separateResult.Count);
            List<int> combinedMembers = new List<int>();
            combinedMembers.AddRange(separateResult[0].Members);
            combinedMembers.AddRange(separateResult[1].Members);
            CollectionAssert.AreEquivalent(new List<int> { 1, 2 }, combinedMembers);
        }

        [TestMethod]
        public void Test_DesiredClusterCount_LessThanOne()
        {
            List<int> items = new List<int>
            {
                5,
                10,
                15
            };
            AverageLinkageClusterer<int> clusterer = new AverageLinkageClusterer<int>();
            // A desired cluster count less than 1 should default to 1.
            List<Cluster<int>> result = clusterer.ClusterData(items, (int a, int b) => System.Math.Abs(a - b), 0);
            Assert.AreEqual(1, result.Count);
            CollectionAssert.AreEquivalent(new List<int> { 5, 10, 15 }, result[0].Members);
        }

        [TestMethod]
        public void Test_LargerDataSet()
        {
            List<int> items = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                items.Add(i);
            }

            AverageLinkageClusterer<int> clusterer = new AverageLinkageClusterer<int>();
            List<Cluster<int>> result = clusterer.ClusterData(items, (int a, int b) => System.Math.Abs(a - b), 5);
            Assert.AreEqual(5, result.Count);
            List<int> allMembers = new List<int>();
            foreach (Cluster<int> cluster in result)
            {
                allMembers.AddRange(cluster.Members);
            }

            Assert.AreEqual(100, allMembers.Count);
            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(allMembers.Contains(i));
            }
        }
    }
}