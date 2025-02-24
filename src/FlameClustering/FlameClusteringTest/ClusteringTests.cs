using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FlameClustering.Algorithms;
using FlameClustering.Models;

namespace FlameClusteringTest
{
    [TestClass]
    public class ClusteringTests
    {
        [TestMethod]
        public void TestEmptyDataPoints()
        {
            FlameClusteringAlgorithm algorithm = new FlameClusteringAlgorithm();
            List<DataPoint> data = new List<DataPoint>();
            List<Cluster> clusters = algorithm.Run(data, 1.0, 1);
            Assert.AreEqual(0, clusters.Count, "Expected no clusters for empty data.");
        }

        [TestMethod]
        public void TestNullDataPoints()
        {
            FlameClusteringAlgorithm algorithm = new FlameClusteringAlgorithm();
            List<Cluster> clusters = algorithm.Run(null, 1.0, 1);
            Assert.AreEqual(0, clusters.Count, "Expected no clusters for null data.");
        }

        [TestMethod]
        public void TestBasicClustering()
        {
            FlameClusteringAlgorithm algorithm = new FlameClusteringAlgorithm();
            List<DataPoint> data = new List<DataPoint>
            {
                new DataPoint(new double[] { 0, 0 }),
                new DataPoint(new double[] { 1, 1 }),
                new DataPoint(new double[] { 2, 2 }),
                new DataPoint(new double[] { 10, 10 }),
                new DataPoint(new double[] { 10, 11 })
            };
            double radius = 3.0;
            int minNeighbors = 1;
            List<Cluster> clusters = algorithm.Run(data, radius, minNeighbors);
            Assert.IsTrue(clusters.Count > 0, "Expected at least one cluster.");
            foreach (DataPoint dp in data)
            {
                double membershipSum = 0.0;
                foreach (Cluster cluster in clusters)
                {
                    if (cluster.Memberships.ContainsKey(dp))
                    {
                        membershipSum += cluster.Memberships[dp];
                    }
                }

                Assert.IsTrue(Math.Abs(membershipSum - 1.0) < 1e-6, "Membership values for a data point should sum up to 1.");
            }
        }

        [TestMethod]
        public void TestLargeSyntheticDataSet()
        {
            FlameClusteringAlgorithm algorithm = new FlameClusteringAlgorithm();
            List<DataPoint> data = GenerateSyntheticData(1000, 2);
            List<Cluster> clusters = algorithm.Run(data, 5.0, 10);
            foreach (DataPoint dp in data)
            {
                double membershipSum = 0.0;
                foreach (Cluster cluster in clusters)
                {
                    if (cluster.Memberships.ContainsKey(dp))
                    {
                        membershipSum += cluster.Memberships[dp];
                    }
                }

                if (membershipSum > 0)
                {
                    Assert.IsTrue(Math.Abs(membershipSum - 1.0) < 1e-6, "Normalized memberships should sum to 1.");
                }
            }
        }

        [TestMethod]
        public void TestDistanceCalculation()
        {
            DataPoint p1 = new DataPoint(new double[] { 0, 0 });
            DataPoint p2 = new DataPoint(new double[] { 3, 4 });
            double distance = p1.DistanceTo(p2);
            Assert.IsTrue(Math.Abs(distance - 5.0) < 1e-6, "Distance should be 5 for points (0,0) and (3,4).");
        }

        [TestMethod]
        public void TestDistanceToNull()
        {
            DataPoint point = new DataPoint(new double[] { 1, 1 });
            double distance = point.DistanceTo(null);
            Assert.AreEqual(double.MaxValue, distance, "Distance to null should be double.MaxValue.");
        }

        private List<DataPoint> GenerateSyntheticData(int count, int dimensions)
        {
            List<DataPoint> data = new List<DataPoint>();
            Random random = new Random(42);
            for (int i = 0; i < count; i++)
            {
                double[] coordinates = new double[dimensions];
                for (int j = 0; j < dimensions; j++)
                {
                    coordinates[j] = random.NextDouble() * 100;
                }

                data.Add(new DataPoint(coordinates));
            }

            return data;
        }
    }
}