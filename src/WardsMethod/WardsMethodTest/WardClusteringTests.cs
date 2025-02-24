using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WardsMethod;

namespace WardsMethodTest
{
    [TestClass]
    public class ClusterTests
    {
        [TestMethod]
        public void ComputeCentroid_ValidPoints_ReturnsCorrectCentroid()
        {
            List<double[]> points = new List<double[]>();
            points.Add(new double[] { 1.0, 2.0 });
            points.Add(new double[] { 3.0, 4.0 });
            double[] centroid = Cluster.ComputeCentroid(points);
            Assert.AreEqual(2.0, centroid[0], 1e-6);
            Assert.AreEqual(3.0, centroid[1], 1e-6);
        }

        [TestMethod]
        public void ComputeCentroid_EmptyPoints_ReturnsEmptyArray()
        {
            List<double[]> points = new List<double[]>();
            double[] centroid = Cluster.ComputeCentroid(points);
            Assert.AreEqual(0, centroid.Length);
        }

        [TestMethod]
        public void ClusterConstructor_NullDataPoints_InitializesEmptyDataPoints()
        {
            Cluster cluster = new Cluster(null);
            Assert.IsNotNull(cluster.DataPoints);
            Assert.AreEqual(0, cluster.DataPoints.Count);
            Assert.AreEqual(0, cluster.Centroid.Length);
        }
    }

    [TestClass]
    public class WardClusteringTests
    {
        [TestMethod]
        public void WardClustering_NullDataPoints_ProducesEmptyClusters()
        {
            WardClustering wc = new WardClustering(null);
            Assert.IsNotNull(wc.Clusters);
            Assert.AreEqual(0, wc.Clusters.Count);
        }

        [TestMethod]
        public void WardClustering_PerformClustering_SingleClusterRemains()
        {
            List<double[]> points = new List<double[]>();
            points.Add(new double[] { 1.0, 2.0 });
            points.Add(new double[] { 2.0, 3.0 });
            points.Add(new double[] { 3.0, 4.0 });
            WardClustering wc = new WardClustering(points);
            wc.PerformClustering();
            Assert.AreEqual(1, wc.Clusters.Count);
            double[] expectedCentroid = Cluster.ComputeCentroid(points);
            double[] actualCentroid = wc.Clusters[0].Centroid;
            Assert.AreEqual(expectedCentroid.Length, actualCentroid.Length);
            for (int i = 0; i < expectedCentroid.Length; i++)
            {
                Assert.AreEqual(expectedCentroid[i], actualCentroid[i], 1e-6);
            }
        }

        [TestMethod]
        public void WardClustering_CalculateIncrease()
        {
            List<double[]> points1 = new List<double[]>
            {
                new double[]
                {
                    1.0,
                    1.0
                },
                new double[]
                {
                    2.0,
                    2.0
                }
            };
            List<double[]> points2 = new List<double[]>
            {
                new double[]
                {
                    3.0,
                    3.0
                },
                new double[]
                {
                    4.0,
                    4.0
                }
            };
            Cluster cluster1 = new Cluster(points1);
            Cluster cluster2 = new Cluster(points2);
            WardClustering wc = new WardClustering(null);
            double increase = wc.CalculateIncrease(cluster1, cluster2);
            double[] mergedCentroid = Cluster.ComputeCentroid(new List<double[]> { new double[] { 1.0, 1.0 }, new double[] { 2.0, 2.0 }, new double[] { 3.0, 3.0 }, new double[] { 4.0, 4.0 } });
            double expectedIncrease = wc.SumOfSquares(cluster1, mergedCentroid) + wc.SumOfSquares(cluster2, mergedCentroid) - (wc.SumOfSquares(cluster1, cluster1.Centroid) + wc.SumOfSquares(cluster2, cluster2.Centroid));
            Assert.AreEqual(expectedIncrease, increase, 1e-6);
        }

        [TestMethod]
        public void WardClustering_MergeClusters_Test()
        {
            List<double[]> points1 = new List<double[]>
            {
                new double[]
                {
                    1.0,
                    2.0
                }
            };
            List<double[]> points2 = new List<double[]>
            {
                new double[]
                {
                    3.0,
                    4.0
                }
            };
            Cluster cluster1 = new Cluster(points1);
            Cluster cluster2 = new Cluster(points2);
            WardClustering wc = new WardClustering(null);
            Cluster merged = wc.MergeClusters(cluster1, cluster2);
            Assert.AreEqual(2, merged.DataPoints.Count);
            double[] expectedCentroid = new double[]
            {
                2.0,
                3.0
            };
            Assert.AreEqual(expectedCentroid.Length, merged.Centroid.Length);
            for (int i = 0; i < expectedCentroid.Length; i++)
            {
                Assert.AreEqual(expectedCentroid[i], merged.Centroid[i], 1e-6);
            }
        }

        [TestMethod]
        public void WardClustering_SyntheticLargeDataset_Test()
        {
            int numberOfPoints = 1000;
            int dimensions = 5;
            List<double[]> points = new List<double[]>();
            System.Random random = new System.Random(42);
            for (int i = 0; i < numberOfPoints; i++)
            {
                double[] point = new double[dimensions];
                for (int j = 0; j < dimensions; j++)
                {
                    point[j] = random.NextDouble() * 100;
                }

                points.Add(point);
            }

            WardClustering wc = new WardClustering(points);
            wc.PerformClustering();
            Assert.AreEqual(1, wc.Clusters.Count);
            Assert.AreEqual(dimensions, wc.Clusters[0].Centroid.Length);
        }

        [TestMethod]
        public void MergeClusters_WithEmptyCluster_Test()
        {
            List<double[]> points = new List<double[]>
            {
                new double[]
                {
                    10.0,
                    20.0
                }
            };
            Cluster nonEmptyCluster = new Cluster(points);
            Cluster emptyCluster = new Cluster(null);
            WardClustering wc = new WardClustering(null);
            Cluster merged = wc.MergeClusters(nonEmptyCluster, emptyCluster);
            Assert.AreEqual(nonEmptyCluster.DataPoints.Count, merged.DataPoints.Count);
            Assert.AreEqual(nonEmptyCluster.Centroid.Length, merged.Centroid.Length);
            for (int i = 0; i < nonEmptyCluster.Centroid.Length; i++)
            {
                Assert.AreEqual(nonEmptyCluster.Centroid[i], merged.Centroid[i], 1e-6);
            }
        }
    }
}