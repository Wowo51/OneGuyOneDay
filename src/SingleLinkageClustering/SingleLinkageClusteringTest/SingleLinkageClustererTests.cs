using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SingleLinkageClustering;

namespace SingleLinkageClusteringTest
{
    [TestClass]
    public class SingleLinkageClustererTests
    {
        [TestMethod]
        public void ClusterPoints_NullPoints_ReturnsEmptyList()
        {
            SingleLinkageClusterer<int> clusterer = new SingleLinkageClusterer<int>();
            System.Collections.Generic.IList<int> points = null !;
            List<List<int>> clusters = clusterer.ClusterPoints(points, (int a, int b) => Math.Abs(a - b), 2);
            Assert.AreEqual(0, clusters.Count);
        }

        [TestMethod]
        public void ClusterPoints_NullDistanceFunc_ReturnsEmptyList()
        {
            SingleLinkageClusterer<int> clusterer = new SingleLinkageClusterer<int>();
            List<int> points = new List<int>
            {
                1,
                2,
                3
            };
            List<List<int>> clusters = clusterer.ClusterPoints(points, null !, 2);
            Assert.AreEqual(0, clusters.Count);
        }

        [TestMethod]
        public void ClusterPoints_SinglePoint_ReturnsSingleCluster()
        {
            SingleLinkageClusterer<int> clusterer = new SingleLinkageClusterer<int>();
            List<int> points = new List<int>
            {
                5
            };
            List<List<int>> clusters = clusterer.ClusterPoints(points, (int a, int b) => Math.Abs(a - b), 2);
            Assert.AreEqual(1, clusters.Count);
            CollectionAssert.AreEqual(points, clusters[0]);
        }

        [TestMethod]
        public void ClusterPoints_TwoPoints_DesiredClusters2_ReturnsTwoClusters()
        {
            SingleLinkageClusterer<int> clusterer = new SingleLinkageClusterer<int>();
            List<int> points = new List<int>
            {
                5,
                10
            };
            List<List<int>> clusters = clusterer.ClusterPoints(points, (int a, int b) => Math.Abs(a - b), 2);
            Assert.AreEqual(2, clusters.Count);
        }

        [TestMethod]
        public void ClusterPoints_TwoPoints_DesiredClusters1_ReturnsOneCluster()
        {
            SingleLinkageClusterer<int> clusterer = new SingleLinkageClusterer<int>();
            List<int> points = new List<int>
            {
                5,
                10
            };
            List<List<int>> clusters = clusterer.ClusterPoints(points, (int a, int b) => Math.Abs(a - b), 1);
            Assert.AreEqual(1, clusters.Count);
            List<int> expected = new List<int>
            {
                5,
                10
            };
            clusters[0].Sort();
            expected.Sort();
            CollectionAssert.AreEqual(expected, clusters[0]);
        }

        [TestMethod]
        public void ClusterPoints_MultiplePoints_DesiredClusters1_ReturnsSingleCluster()
        {
            SingleLinkageClusterer<int> clusterer = new SingleLinkageClusterer<int>();
            List<int> points = new List<int>
            {
                1,
                3,
                7,
                15,
                31
            };
            List<List<int>> clusters = clusterer.ClusterPoints(points, (int a, int b) => Math.Abs(a - b), 1);
            Assert.AreEqual(1, clusters.Count);
            List<int> expected = new List<int>
            {
                1,
                3,
                7,
                15,
                31
            };
            clusters[0].Sort();
            expected.Sort();
            CollectionAssert.AreEqual(expected, clusters[0]);
        }

        [TestMethod]
        public void ClusterPoints_DesiredClustersLessThan1_TreatedAs1()
        {
            SingleLinkageClusterer<int> clusterer = new SingleLinkageClusterer<int>();
            List<int> points = new List<int>
            {
                1,
                2,
                3
            };
            List<List<int>> clusters = clusterer.ClusterPoints(points, (int a, int b) => Math.Abs(a - b), 0);
            Assert.AreEqual(1, clusters.Count);
        }

        [TestMethod]
        public void ClusterPoints_SyntheticLargeDataTest()
        {
            SingleLinkageClusterer<int> clusterer = new SingleLinkageClusterer<int>();
            List<int> points = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                points.Add(i);
            }

            List<List<int>> clusters = clusterer.ClusterPoints(points, (int a, int b) => Math.Abs(a - b), 10);
            Assert.AreEqual(10, clusters.Count);
            List<int> merged = new List<int>();
            for (int i = 0; i < clusters.Count; i++)
            {
                merged.AddRange(clusters[i]);
            }

            merged.Sort();
            List<int> expected = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                expected.Add(i);
            }

            CollectionAssert.AreEqual(expected, merged);
        }
    }
}