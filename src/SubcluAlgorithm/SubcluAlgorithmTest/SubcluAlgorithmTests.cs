using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubcluAlgorithm;

namespace SubcluAlgorithmTest
{
    [TestClass]
    public class SubcluAlgorithmTests
    {
        [TestMethod]
        public void TestEmptyInput()
        {
            Subclu subclu = new Subclu();
            List<DataPoint> points = new List<DataPoint>();
            double epsilon = 1.0;
            int minPoints = 2;
            List<SubspaceCluster> clusters = subclu.FindClusters(points, epsilon, minPoints);
            Assert.AreEqual(0, clusters.Count, "Empty input should result in no clusters.");
        }

        [TestMethod]
        public void TestNullInput()
        {
            Subclu subclu = new Subclu();
            List<DataPoint>? points = null;
            double epsilon = 1.0;
            int minPoints = 2;
            List<SubspaceCluster> clusters = subclu.FindClusters(points, epsilon, minPoints);
            Assert.AreEqual(0, clusters.Count, "Null input should result in no clusters.");
        }

        [TestMethod]
        public void TestSingleCluster()
        {
            Subclu subclu = new Subclu();
            List<DataPoint> points = new List<DataPoint>
            {
                new DataPoint(new double[] { 0.0, 0.0 }),
                new DataPoint(new double[] { 0.5, 0.5 }),
                new DataPoint(new double[] { 1.0, 1.0 })
            };
            double epsilon = 1.5;
            int minPoints = 2;
            List<SubspaceCluster> clusters = subclu.FindClusters(points, epsilon, minPoints);
            Assert.AreEqual(3, clusters.Count, "Expected 3 clusters for non-empty subspaces.");
            foreach (SubspaceCluster cluster in clusters)
            {
                Assert.AreEqual(3, cluster.Points.Count, "Each cluster should contain all 3 points.");
            }
        }

        [TestMethod]
        public void TestNoiseIsolation()
        {
            Subclu subclu = new Subclu();
            List<DataPoint> points = new List<DataPoint>
            {
                new DataPoint(new double[] { 0.0, 0.0 }),
                new DataPoint(new double[] { 0.1, 0.1 }),
                new DataPoint(new double[] { 0.2, 0.2 }),
                new DataPoint(new double[] { 0.3, 0.3 }),
                new DataPoint(new double[] { 100.0, 100.0 })
            };
            double epsilon = 0.5;
            int minPoints = 3;
            List<SubspaceCluster> clusters = subclu.FindClusters(points, epsilon, minPoints);
            foreach (SubspaceCluster cluster in clusters)
            {
                foreach (DataPoint point in cluster.Points)
                {
                    Assert.IsFalse(point.Coordinates[0] > 10, "Outlier should not be included in the cluster.");
                }
            }
        }

        [TestMethod]
        public void TestSyntheticLargeData()
        {
            Subclu subclu = new Subclu();
            List<DataPoint> points = new List<DataPoint>();
            int count = 100;
            Random randomGenerator = new Random(42);
            for (int i = 0; i < count; i++)
            {
                double[] coordinates = new double[2];
                coordinates[0] = 0.5 + (randomGenerator.NextDouble() - 0.5) * 0.1;
                coordinates[1] = 0.5 + (randomGenerator.NextDouble() - 0.5) * 0.1;
                points.Add(new DataPoint(coordinates));
            }

            double epsilon = 0.2;
            int minPoints = 5;
            List<SubspaceCluster> clusters = subclu.FindClusters(points, epsilon, minPoints);
            Assert.IsTrue(clusters.Count > 0, "Synthetic data should produce at least one valid cluster per subspace.");
            foreach (SubspaceCluster cluster in clusters)
            {
                Assert.IsTrue(cluster.Points.Count >= minPoints, "Each cluster must have at least the minimum number of points.");
            }
        }
    }
}