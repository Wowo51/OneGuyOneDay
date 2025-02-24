using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpticsClusteringAlgorithm;
using System;
using System.Collections.Generic;

namespace OpticsClusteringAlgorithmTest
{
    [TestClass]
    public class ClusterVisualizerTests
    {
        [TestMethod]
        public void GenerateReachabilityPlotSvg_NullOrEmpty_ReturnsEmpty()
        {
            ClusterVisualizer visualizer = new ClusterVisualizer();
            string resultNull = visualizer.GenerateReachabilityPlotSvg(null);
            Assert.AreEqual(string.Empty, resultNull);
            List<PointEntry> emptyList = new List<PointEntry>();
            string resultEmpty = visualizer.GenerateReachabilityPlotSvg(emptyList);
            Assert.AreEqual(string.Empty, resultEmpty);
        }

        [TestMethod]
        public void GenerateReachabilityPlotSvg_WithData_ReturnsValidSvg()
        {
            List<PointEntry> entries = new List<PointEntry>();
            int i = 0;
            while (i < 10)
            {
                Point point = new Point((double)i, (double)i);
                PointEntry entry = new PointEntry(point);
                entry.ReachabilityDistance = (double)i;
                entries.Add(entry);
                i++;
            }

            ClusterVisualizer visualizer = new ClusterVisualizer();
            string svg = visualizer.GenerateReachabilityPlotSvg(entries);
            Assert.IsTrue(svg.Contains("<svg"));
            Assert.IsTrue(svg.Contains("</svg>"));
            Assert.IsTrue(svg.Contains("<rect"));
        }
    }

    [TestClass]
    public class OpticsAlgorithmTests
    {
        [TestMethod]
        public void Run_NullPoints_ReturnsEmptyList()
        {
            OpticsAlgorithm algorithm = new OpticsAlgorithm();
            List<PointEntry> ordering = algorithm.Run(null, 1.0, 2);
            Assert.AreEqual(0, ordering.Count);
        }

        [TestMethod]
        public void Run_WithSimpleData_ReturnsFullOrdering()
        {
            List<Point> points = new List<Point>();
            int i = 0;
            while (i < 5)
            {
                points.Add(new Point((double)i, (double)i));
                i++;
            }

            double epsilon = 10.0;
            int minPoints = 2;
            OpticsAlgorithm algorithm = new OpticsAlgorithm();
            List<PointEntry> ordering = algorithm.Run(points, epsilon, minPoints);
            Assert.AreEqual(points.Count, ordering.Count);
            foreach (PointEntry entry in ordering)
            {
                Assert.IsTrue(entry.Processed);
            }
        }

        [TestMethod]
        public void Run_WithLargeRandomData_CompletesSuccessfully()
        {
            List<Point> points = new List<Point>();
            Random random = new Random(42);
            int count = 1000;
            int i = 0;
            while (i < count)
            {
                double x = random.NextDouble() * 100;
                double y = random.NextDouble() * 100;
                points.Add(new Point(x, y));
                i++;
            }

            double epsilon = 15.0;
            int minPoints = 5;
            OpticsAlgorithm algorithm = new OpticsAlgorithm();
            List<PointEntry> ordering = algorithm.Run(points, epsilon, minPoints);
            Assert.AreEqual(count, ordering.Count);
            foreach (PointEntry entry in ordering)
            {
                Assert.IsTrue(entry.Processed);
            }
        }
    }
}