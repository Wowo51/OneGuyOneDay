using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BestBinFirst;

namespace BestBinFirstTest
{
    [TestClass]
    public class BestBinFirstTests
    {
        [TestMethod]
        public void NearestNeighbor_BasicTest()
        {
            List<Point> points = new List<Point>
            {
                new Point(new double[] { 2, 3 }),
                new Point(new double[] { 5, 4 }),
                new Point(new double[] { 9, 6 }),
                new Point(new double[] { 4, 7 }),
                new Point(new double[] { 8, 1 }),
                new Point(new double[] { 7, 2 })
            };
            BestBinFirstSearch search = new BestBinFirstSearch(points);
            Point query = new Point(new double[] { 9, 2 });
            Point? result = search.NearestNeighbor(query, 100);
            Assert.IsNotNull(result);
            double expectedX = 8;
            double expectedY = 1;
            double diffX = Math.Abs(result!.Coordinates[0] - expectedX);
            double diffY = Math.Abs(result!.Coordinates[1] - expectedY);
            Assert.IsTrue(diffX < 0.001 && diffY < 0.001);
        }

        [TestMethod]
        public void NearestNeighbor_EmptyPointsTest()
        {
            List<Point> points = new List<Point>();
            BestBinFirstSearch search = new BestBinFirstSearch(points);
            Point query = new Point(new double[] { 1, 1 });
            Point? result = search.NearestNeighbor(query, 100);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void NearestNeighbor_NullQueryTest()
        {
            List<Point> points = new List<Point>
            {
                new Point(new double[] { 1, 1 })
            };
            BestBinFirstSearch search = new BestBinFirstSearch(points);
            Point? result = search.NearestNeighbor(null, 100);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void NearestNeighbor_NullPointsInConstructorTest()
        {
            BestBinFirstSearch search = new BestBinFirstSearch(null);
            Point query = new Point(new double[] { 1, 1 });
            Point? result = search.NearestNeighbor(query, 100);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void NearestNeighbor_LargeDataSetTest()
        {
            int numPoints = 1000;
            List<Point> points = new List<Point>();
            Random rnd = new Random(42);
            for (int i = 0; i < numPoints; i++)
            {
                double x = rnd.NextDouble() * 1000;
                double y = rnd.NextDouble() * 1000;
                points.Add(new Point(new double[] { x, y }));
            }

            BestBinFirstSearch search = new BestBinFirstSearch(points);
            double qx = rnd.NextDouble() * 1000;
            double qy = rnd.NextDouble() * 1000;
            Point query = new Point(new double[] { qx, qy });
            Point? expected = null;
            double bestDistance = double.MaxValue;
            for (int i = 0; i < points.Count; i++)
            {
                double distance = query.DistanceSquared(points[i]);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    expected = points[i];
                }
            }

            Point? result = search.NearestNeighbor(query, 2000);
            Assert.IsNotNull(expected);
            Assert.IsNotNull(result);
            double resDistance = query.DistanceSquared(result!);
            Assert.IsTrue(Math.Abs(resDistance - bestDistance) < 0.001);
        }
    }
}