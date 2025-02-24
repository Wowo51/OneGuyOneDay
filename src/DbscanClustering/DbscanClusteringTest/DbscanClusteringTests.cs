using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbscanClustering;
using System;
using System.Collections.Generic;

namespace DbscanClusteringTest
{
    [TestClass]
    public class DBSCANTests
    {
        [TestMethod]
        public void Test_NullInput()
        {
            DBSCAN dbscan = new DBSCAN();
            List<List<Point>> clusters = dbscan.Cluster(null !, 1.0, 2);
            Assert.IsNotNull(clusters);
            Assert.AreEqual(0, clusters.Count);
        }

        [TestMethod]
        public void Test_EmptyInput()
        {
            DBSCAN dbscan = new DBSCAN();
            List<Point> points = new List<Point>();
            List<List<Point>> clusters = dbscan.Cluster(points, 1.0, 2);
            Assert.IsNotNull(clusters);
            Assert.AreEqual(0, clusters.Count);
        }

        [TestMethod]
        public void Test_SimpleClustering()
        {
            DBSCAN dbscan = new DBSCAN();
            List<Point> points = new List<Point>
            {
                new Point(new double[] { 0, 0 }),
                new Point(new double[] { 0.5, 0.5 }),
                new Point(new double[] { 10, 10 })
            };
            List<List<Point>> clusters = dbscan.Cluster(points, 1.0, 2);
            // Expect a single cluster for the first two points; the third point remains noise.
            Assert.AreEqual(1, clusters.Count);
            List<Point> cluster = clusters[0];
            Assert.AreEqual(2, cluster.Count);
            Assert.AreEqual(-1, points[2].ClusterId);
        }

        [TestMethod]
        public void Test_MultipleClusters()
        {
            DBSCAN dbscan = new DBSCAN();
            List<Point> points = new List<Point>();
            // Group 1: points around (1,1)
            points.Add(new Point(new double[] { 1, 1 }));
            points.Add(new Point(new double[] { 1.05, 1.0 }));
            points.Add(new Point(new double[] { 0.95, 1.0 }));
            // Group 2: points around (5,5)
            points.Add(new Point(new double[] { 5, 5 }));
            points.Add(new Point(new double[] { 5.1, 5 }));
            points.Add(new Point(new double[] { 5, 5.1 }));
            // Noise point
            points.Add(new Point(new double[] { 10, 10 }));
            // Using eps = 0.2 ensures that only close points join clusters.
            List<List<Point>> clusters = dbscan.Cluster(points, 0.2, 2);
            Assert.AreEqual(2, clusters.Count);
            // Verify that the noise point remains unclustered.
            Assert.AreEqual(-1, points[6].ClusterId);
        }

        [TestMethod]
        public void Test_LargeDataset()
        {
            DBSCAN dbscan = new DBSCAN();
            List<Point> points = new List<Point>();
            Random random = new Random(42);
            int clusterSize = 100;
            double[][] centers = new double[][]
            {
                new double[]
                {
                    0,
                    0
                },
                new double[]
                {
                    100,
                    100
                },
                new double[]
                {
                    -100,
                    -100
                }
            };
            // Generate three distinct clusters.
            for (int c = 0; c < centers.Length; c++)
            {
                for (int i = 0; i < clusterSize; i++)
                {
                    double[] coords = new double[2];
                    for (int j = 0; j < 2; j++)
                    {
                        double offset = (random.NextDouble() * 2.0) - 1.0;
                        coords[j] = centers[c][j] + offset;
                    }

                    points.Add(new Point(coords));
                }
            }

            // Add 10 isolated noise points far from any cluster.
            for (int i = 0; i < 10; i++)
            {
                double[] coords = new double[2];
                coords[0] = random.NextDouble() * 1000 + 2000;
                coords[1] = random.NextDouble() * 1000 + 2000;
                points.Add(new Point(coords));
            }

            // Use eps = 2.0 and minPts = 5 to form the clusters.
            List<List<Point>> clusters = dbscan.Cluster(points, 2.0, 5);
            // Expect three clusters from the three generated groups.
            Assert.AreEqual(3, clusters.Count);
            int totalPointsInClusters = 0;
            foreach (List<Point> cluster in clusters)
            {
                totalPointsInClusters += cluster.Count;
            }

            Assert.AreEqual(300, totalPointsInClusters);
        }
    }
}