using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LloydsAlgorithm;

namespace LloydsAlgorithmTest
{
    [TestClass]
    public class KMeansTests
    {
        [TestMethod]
        public void TestNullInput()
        {
            List<double[]>? points = null;
            KMeansResult result = KMeans.Cluster(points, 3, 100);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Centroids);
            Assert.IsNotNull(result.Labels);
            Assert.AreEqual(0, result.Centroids.Count);
            Assert.AreEqual(0, result.Labels.Length);
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            List<double[]> points = new List<double[]>();
            KMeansResult result = KMeans.Cluster(points, 3, 100);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Centroids);
            Assert.IsNotNull(result.Labels);
            Assert.AreEqual(0, result.Centroids.Count);
            Assert.AreEqual(0, result.Labels.Length);
        }

        [TestMethod]
        public void TestSimpleClustering()
        {
            List<double[]> points = new List<double[]>();
            points.Add(new double[] { 1.0, 1.0 });
            points.Add(new double[] { 1.2, 0.8 });
            points.Add(new double[] { 0.9, 1.1 });
            points.Add(new double[] { 10.0, 10.0 });
            points.Add(new double[] { 10.2, 9.8 });
            points.Add(new double[] { 9.9, 10.1 });
            int k = 2;
            int maxIterations = 100;
            KMeansResult result = KMeans.Cluster(points, k, maxIterations);
            Assert.AreEqual(k, result.Centroids.Count);
            Assert.AreEqual(points.Count, result.Labels.Length);
            for (int i = 0; i < result.Centroids.Count; i++)
            {
                Assert.AreEqual(2, result.Centroids[i].Length);
            }
        }

        [TestMethod]
        public void TestLargeDatasetClustering()
        {
            const int numPoints = 1000;
            const int dimensions = 3;
            const int k = 4;
            const int maxIterations = 100;
            List<double[]> points = GenerateRandomPoints(numPoints, dimensions);
            KMeansResult result = KMeans.Cluster(points, k, maxIterations);
            Assert.AreEqual(k, result.Centroids.Count);
            Assert.AreEqual(numPoints, result.Labels.Length);
            for (int i = 0; i < result.Centroids.Count; i++)
            {
                Assert.AreEqual(dimensions, result.Centroids[i].Length);
            }
        }

        private List<double[]> GenerateRandomPoints(int count, int dimensions)
        {
            List<double[]> points = new List<double[]>();
            Random random = new Random(42);
            for (int i = 0; i < count; i++)
            {
                double[] point = new double[dimensions];
                for (int d = 0; d < dimensions; d++)
                {
                    point[d] = random.NextDouble() * 100;
                }

                points.Add(point);
            }

            return points;
        }
    }
}