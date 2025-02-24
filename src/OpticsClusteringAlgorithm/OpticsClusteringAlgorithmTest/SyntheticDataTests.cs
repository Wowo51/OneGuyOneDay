using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpticsClusteringAlgorithm;
using System;
using System.Collections.Generic;

namespace OpticsClusteringAlgorithmTest
{
    [TestClass]
    public class SyntheticDataTests
    {
        [TestMethod]
        public void Run_WithSinglePoint_ReturnsSinglePointInOrdering()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(10.0, 20.0));
            double epsilon = 1.0;
            int minPoints = 2;
            OpticsAlgorithm algorithm = new OpticsAlgorithm();
            List<PointEntry> ordering = algorithm.Run(points, epsilon, minPoints);
            Assert.AreEqual(1, ordering.Count);
            Assert.IsTrue(ordering[0].Processed);
        }

        [TestMethod]
        public void Run_WithSyntheticClusters_CompletesSuccessfully()
        {
            List<Point> points = new List<Point>();
            int clusters = 3;
            int pointsPerCluster = 50;
            double clusterSpread = 2.0;
            double epsilon = 5.0;
            int minPoints = 5;
            for (int c = 0; c < clusters; c++)
            {
                double centerX = c * 20.0;
                double centerY = c * 20.0;
                for (int i = 0; i < pointsPerCluster; i++)
                {
                    Random random = new Random(c * pointsPerCluster + i);
                    double x = centerX + (random.NextDouble() - 0.5) * clusterSpread;
                    double y = centerY + (random.NextDouble() - 0.5) * clusterSpread;
                    points.Add(new Point(x, y));
                }
            }

            OpticsAlgorithm algorithm = new OpticsAlgorithm();
            List<PointEntry> ordering = algorithm.Run(points, epsilon, minPoints);
            Assert.AreEqual(clusters * pointsPerCluster, ordering.Count);
            foreach (PointEntry entry in ordering)
            {
                Assert.IsTrue(entry.Processed);
            }
        }
    }
}