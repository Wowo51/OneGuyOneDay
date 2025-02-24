using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FuzzyCMeans;

namespace FuzzyCMeansTest
{
    [TestClass]
    public class FuzzyCMeansTests
    {
        [TestMethod]
        public void Compute_NullDataPoints_ReturnsEmptyResult()
        {
            List<double[]>? dataPoints = null;
            int clusters = 3;
            double m = 2.0;
            double tolerance = 0.001;
            int maxIterations = 100;
            FuzzyCMeansResult result = FuzzyCMeansAlgorithm.Compute(dataPoints, clusters, m, tolerance, maxIterations);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Centroids.Count, "Centroids list should be empty.");
            Assert.AreEqual(0, result.Memberships.GetLength(0), "Memberships should have zero rows.");
            Assert.AreEqual(0, result.Memberships.GetLength(1), "Memberships should have zero columns.");
        }

        [TestMethod]
        public void Compute_EmptyDataPoints_ReturnsEmptyResult()
        {
            List<double[]> dataPoints = new List<double[]>();
            int clusters = 3;
            double m = 2.0;
            double tolerance = 0.001;
            int maxIterations = 100;
            FuzzyCMeansResult result = FuzzyCMeansAlgorithm.Compute(dataPoints, clusters, m, tolerance, maxIterations);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Centroids.Count, "Centroids list should be empty.");
            Assert.AreEqual(0, result.Memberships.GetLength(0), "Memberships should have zero rows.");
            Assert.AreEqual(0, result.Memberships.GetLength(1), "Memberships should have zero columns.");
        }

        [TestMethod]
        public void Compute_SingleDataPoint_ReturnsValidResult()
        {
            List<double[]> dataPoints = new List<double[]>
            {
                new double[]
                {
                    1.0,
                    2.0
                }
            };
            int clusters = 1;
            double m = 2.0;
            double tolerance = 0.001;
            int maxIterations = 100;
            FuzzyCMeansResult result = FuzzyCMeansAlgorithm.Compute(dataPoints, clusters, m, tolerance, maxIterations);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(clusters, result.Centroids.Count, "There should be one centroid.");
            Assert.AreEqual(1, result.Memberships.GetLength(0), "Memberships should have one row.");
            Assert.AreEqual(clusters, result.Memberships.GetLength(1), "Memberships should have one column.");
            double membershipValue = result.Memberships[0, 0];
            Assert.AreEqual(1.0, membershipValue, 0.0001, "Membership for the single cluster should equal 1.");
        }

        [TestMethod]
        public void Compute_SyntheticDataPoints_ReturnsExpectedDimensions()
        {
            // Generate synthetic data with 3 clusters in 2D space.
            int numClusters = 3;
            int pointsPerCluster = 50;
            List<double[]> dataPoints = new List<double[]>();
            Random random = new Random(42);
            double[][] clusterCenters = new double[][]
            {
                new double[]
                {
                    0.0,
                    0.0
                },
                new double[]
                {
                    10.0,
                    10.0
                },
                new double[]
                {
                    -10.0,
                    10.0
                }
            };
            for (int c = 0; c < numClusters; c++)
            {
                for (int i = 0; i < pointsPerCluster; i++)
                {
                    double[] point = new double[2];
                    point[0] = clusterCenters[c][0] + random.NextDouble() * 2 - 1;
                    point[1] = clusterCenters[c][1] + random.NextDouble() * 2 - 1;
                    dataPoints.Add(point);
                }
            }

            int clusters = 3;
            double m = 2.0;
            double tolerance = 0.0001;
            int maxIterations = 200;
            FuzzyCMeansResult result = FuzzyCMeansAlgorithm.Compute(dataPoints, clusters, m, tolerance, maxIterations);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(clusters, result.Centroids.Count, "Centroids list should have three centroids.");
            Assert.AreEqual(dataPoints.Count, result.Memberships.GetLength(0), "Memberships rows should equal data points count.");
            Assert.AreEqual(clusters, result.Memberships.GetLength(1), "Memberships columns should equal clusters count.");
            // Ensure that each membership row sums to approximately 1.
            for (int i = 0; i < dataPoints.Count; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < clusters; j++)
                {
                    sum += result.Memberships[i, j];
                }

                Assert.AreEqual(1.0, sum, 0.001, "Sum of memberships for a data point should be approximately 1.");
            }
        }
    }
}