using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KMedoids;

namespace KMedoidsTest
{
    [TestClass]
    public class KMedoidsAlgorithmTests
    {
        [TestMethod]
        public void TestClusterWithEmptyPoints()
        {
            KMedoidsAlgorithm algorithm = new KMedoidsAlgorithm();
            List<DataPoint> points = new List<DataPoint>();
            int k = 2;
            KMedoidsResult result = algorithm.Cluster(points, k);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(0, result.Medoids.Count, "Medoids should be empty when input is empty.");
            Assert.AreEqual(0, result.Clusters.Count, "Clusters should be empty when input is empty.");
        }

        [TestMethod]
        public void TestClusterWithInvalidK()
        {
            KMedoidsAlgorithm algorithm = new KMedoidsAlgorithm();
            List<DataPoint> points = new List<DataPoint>()
            {
                new DataPoint(new double[] { 1.0, 2.0 }),
                new DataPoint(new double[] { 2.0, 3.0 }),
                new DataPoint(new double[] { 3.0, 4.0 })
            };
            // Test with k = 0
            int kZero = 0;
            KMedoidsResult resultZero = algorithm.Cluster(points, kZero);
            Assert.IsNotNull(resultZero, "Result should not be null for k = 0.");
            Assert.AreEqual(0, resultZero.Medoids.Count, "Medoids should be empty for k = 0.");
            Assert.AreEqual(0, resultZero.Clusters.Count, "Clusters should be empty for k = 0.");
            // Test with k greater than points count
            int kTooHigh = 4;
            KMedoidsResult resultTooHigh = algorithm.Cluster(points, kTooHigh);
            Assert.IsNotNull(resultTooHigh, "Result should not be null for k greater than points count.");
            Assert.AreEqual(0, resultTooHigh.Medoids.Count, "Medoids should be empty when k > points count.");
            Assert.AreEqual(0, resultTooHigh.Clusters.Count, "Clusters should be empty when k > points count.");
        }

        [TestMethod]
        public void TestBasicClustering()
        {
            KMedoidsAlgorithm algorithm = new KMedoidsAlgorithm();
            List<DataPoint> points = new List<DataPoint>()
            {
                new DataPoint(new double[] { 0.0, 0.0 }),
                new DataPoint(new double[] { 1.0, 1.0 }),
                new DataPoint(new double[] { 100.0, 100.0 }),
                new DataPoint(new double[] { 101.0, 101.0 })
            };
            int k = 2;
            KMedoidsResult result = algorithm.Cluster(points, k);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(2, result.Medoids.Count, "There should be exactly 2 medoids.");
            Assert.AreEqual(2, result.Clusters.Count, "There should be exactly 2 clusters.");
            foreach (KeyValuePair<DataPoint, List<DataPoint>> cluster in result.Clusters)
            {
                Assert.IsTrue(cluster.Value.Count > 0, "Each cluster should have at least one point.");
            }
        }

        [TestMethod]
        public void TestLargeDataset()
        {
            KMedoidsAlgorithm algorithm = new KMedoidsAlgorithm();
            List<DataPoint> points = new List<DataPoint>();
            Random random = new Random(42);
            // Generate 500 points around (0,0)
            for (int i = 0; i < 500; i = i + 1)
            {
                double[] features = new double[2];
                features[0] = random.NextDouble() * 10.0;
                features[1] = random.NextDouble() * 10.0;
                points.Add(new DataPoint(features));
            }

            // Generate 500 points around (50,50)
            for (int i = 0; i < 500; i = i + 1)
            {
                double[] features = new double[2];
                features[0] = 50.0 + random.NextDouble() * 10.0;
                features[1] = 50.0 + random.NextDouble() * 10.0;
                points.Add(new DataPoint(features));
            }

            int k = 2;
            KMedoidsResult result = algorithm.Cluster(points, k);
            Assert.IsNotNull(result, "Result should not be null.");
            Assert.AreEqual(2, result.Medoids.Count, "There should be exactly 2 medoids.");
            Assert.AreEqual(2, result.Clusters.Count, "There should be exactly 2 clusters.");
            foreach (KeyValuePair<DataPoint, List<DataPoint>> cluster in result.Clusters)
            {
                Assert.IsTrue(cluster.Value.Count > 0, "Each cluster should have at least one point.");
            }
        }
    }
}