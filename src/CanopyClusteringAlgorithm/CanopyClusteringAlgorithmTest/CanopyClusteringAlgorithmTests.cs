using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CanopyClusteringAlgorithm;

namespace CanopyClusteringAlgorithmTest
{
    [TestClass]
    public class CanopyClusteringAlgorithmTests
    {
        [TestMethod]
        public void TestEuclideanDistance_Valid()
        {
            double[] first = new double[]
            {
                0.0,
                0.0
            };
            double[] second = new double[]
            {
                3.0,
                4.0
            };
            double result = DistanceMetrics.EuclideanDistance(first, second);
            Assert.AreEqual(5.0, result, 0.0001, "Euclidean distance between (0,0) and (3,4) must be 5.");
        }

        [TestMethod]
        public void TestEuclideanDistance_Invalid()
        {
            double[] first = new double[]
            {
                1.0,
                2.0
            };
            double[] second = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            double result = DistanceMetrics.EuclideanDistance(first, second);
            Assert.IsTrue(double.IsNaN(result), "Euclidean distance must be NaN for arrays of different lengths.");
        }

        [TestMethod]
        public void TestClusterWithNullAndEmptyInput()
        {
            List<DataPoint>? dataPoints = null;
            List<Canopy> canopies = CanopyClusterer.Cluster(dataPoints, 10.0, 5.0);
            Assert.AreEqual(0, canopies.Count, "Clustering with null input should return an empty canopy list.");
            dataPoints = new List<DataPoint>();
            canopies = CanopyClusterer.Cluster(dataPoints, 10.0, 5.0);
            Assert.AreEqual(0, canopies.Count, "Clustering with empty input should return an empty canopy list.");
        }

        [TestMethod]
        public void TestClusterSingleCluster()
        {
            List<DataPoint> dataPoints = new List<DataPoint>()
            {
                new DataPoint("A", new double[] { 0.0, 0.0 }),
                new DataPoint("B", new double[] { 1.0, 1.0 }),
                new DataPoint("C", new double[] { 0.5, 0.5 })
            };
            List<Canopy> canopies = CanopyClusterer.Cluster(dataPoints, 3.0, 2.0);
            Assert.AreEqual(1, canopies.Count, "Expected a single canopy when all points are close.");
            Assert.AreEqual(3, canopies[0].Members.Count, "All points should be in the single canopy.");
        }

        [TestMethod]
        public void TestClusterMultipleClusters()
        {
            List<DataPoint> dataPoints = new List<DataPoint>()
            {
                new DataPoint("A", new double[] { 0.0, 0.0 }),
                new DataPoint("B", new double[] { 1.0, 1.0 }),
                new DataPoint("C", new double[] { 5.0, 5.0 }),
                new DataPoint("D", new double[] { 6.0, 6.0 })
            };
            List<Canopy> canopies = CanopyClusterer.Cluster(dataPoints, 3.0, 2.0);
            Assert.AreEqual(2, canopies.Count, "Expected two canopies for well-separated clusters.");
            Assert.AreEqual(2, canopies[0].Members.Count, "First canopy should include two points.");
            Assert.AreEqual(2, canopies[1].Members.Count, "Second canopy should include two points.");
        }

        [TestMethod]
        public void TestCluster_SyntheticLargeDataset()
        {
            int numberOfPoints = 1000;
            List<DataPoint> dataPoints = new List<DataPoint>();
            Random randomGenerator = new Random(12345);
            for (int i = 0; i < numberOfPoints; i++)
            {
                double x = randomGenerator.NextDouble() * 100.0;
                double y = randomGenerator.NextDouble() * 100.0;
                dataPoints.Add(new DataPoint(i.ToString(), new double[] { x, y }));
            }

            List<Canopy> canopies = CanopyClusterer.Cluster(dataPoints, 20.0, 10.0);
            HashSet<string> uniqueIds = new HashSet<string>();
            foreach (Canopy canopy in canopies)
            {
                foreach (DataPoint point in canopy.Members)
                {
                    uniqueIds.Add(point.Id);
                }
            }

            Assert.AreEqual(numberOfPoints, uniqueIds.Count, "All points should be clustered into canopies.");
        }
    }
}