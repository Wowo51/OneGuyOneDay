using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AStarSearch;

namespace AStarSearchTest
{
    [TestClass]
    public class AStarAlgorithmTests
    {
        [TestMethod]
        public void TestStartEqualsGoal()
        {
            string start = "A";
            string goal = "A";
            List<string> result = AStarAlgorithm.FindPath<string>(start, goal, (node) => new List<string>(), (current, neighbor) => 1.0, (current, neighbor) => 0.0);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(start, result[0]);
        }

        [TestMethod]
        public void TestSimplePath()
        {
            string start = "A";
            string goal = "D";
            List<string> result = AStarAlgorithm.FindPath<string>(start, goal, (node) =>
            {
                if (node == "A")
                {
                    return new List<string>
                    {
                        "B",
                        "D"
                    };
                }
                else if (node == "B")
                {
                    return new List<string>
                    {
                        "C"
                    };
                }
                else if (node == "C")
                {
                    return new List<string>
                    {
                        "D"
                    };
                }

                return new List<string>();
            }, (current, neighbor) =>
            {
                if (current == "A" && neighbor == "B")
                {
                    return 1.0;
                }
                else if (current == "A" && neighbor == "D")
                {
                    return 10.0;
                }
                else if (current == "B" && neighbor == "C")
                {
                    return 1.0;
                }
                else if (current == "C" && neighbor == "D")
                {
                    return 1.0;
                }

                return 0.0;
            }, (current, neighbor) => 0.0);
            List<string> expected = new List<string>
            {
                "A",
                "B",
                "C",
                "D"
            };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNoPath()
        {
            string start = "A";
            string goal = "B";
            List<string> result = AStarAlgorithm.FindPath<string>(start, goal, (node) => new List<string>(), (current, neighbor) => 1.0, (current, neighbor) => 0.0);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestNullDelegates()
        {
            List<string> result = AStarAlgorithm.FindPath<string>("A", "B", null, (current, neighbor) => 1.0, (current, neighbor) => 0.0);
            Assert.AreEqual(0, result.Count);
            result = AStarAlgorithm.FindPath<string>("A", "B", (node) => new List<string>(), null, (current, neighbor) => 0.0);
            Assert.AreEqual(0, result.Count);
            result = AStarAlgorithm.FindPath<string>("A", "B", (node) => new List<string>(), (current, neighbor) => 1.0, null);
            Assert.AreEqual(0, result.Count);
        }

        private struct Point
        {
            public int X { get; }
            public int Y { get; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override bool Equals(object? obj)
            {
                if (!(obj is Point))
                {
                    return false;
                }

                Point other = (Point)obj;
                return X == other.X && Y == other.Y;
            }

            public override int GetHashCode()
            {
                return X * 31 + Y;
            }
        }

        [TestMethod]
        public void TestGridPath()
        {
            int width = 10;
            int height = 10;
            Point start = new Point(0, 0);
            Point goal = new Point(width - 1, height - 1);
            List<Point> result = AStarAlgorithm.FindPath<Point>(start, goal, (point) =>
            {
                List<Point> neighbors = new List<Point>();
                if (point.X > 0)
                    neighbors.Add(new Point(point.X - 1, point.Y));
                if (point.X < width - 1)
                    neighbors.Add(new Point(point.X + 1, point.Y));
                if (point.Y > 0)
                    neighbors.Add(new Point(point.X, point.Y - 1));
                if (point.Y < height - 1)
                    neighbors.Add(new Point(point.X, point.Y + 1));
                return neighbors;
            }, (current, neighbor) => 1.0, (current, neighbor) => Math.Abs(neighbor.X - goal.X) + Math.Abs(neighbor.Y - goal.Y));
            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual(start, result[0]);
            Assert.AreEqual(goal, result[result.Count - 1]);
            Assert.AreEqual(width + height - 1, result.Count);
        }

        [TestMethod]
        public void TestLargeGridPath()
        {
            int width = 50;
            int height = 50;
            Point start = new Point(0, 0);
            Point goal = new Point(width - 1, height - 1);
            List<Point> result = AStarAlgorithm.FindPath<Point>(start, goal, (point) =>
            {
                List<Point> neighbors = new List<Point>();
                if (point.X > 0)
                    neighbors.Add(new Point(point.X - 1, point.Y));
                if (point.X < width - 1)
                    neighbors.Add(new Point(point.X + 1, point.Y));
                if (point.Y > 0)
                    neighbors.Add(new Point(point.X, point.Y - 1));
                if (point.Y < height - 1)
                    neighbors.Add(new Point(point.X, point.Y + 1));
                return neighbors;
            }, (current, neighbor) => 1.0, (current, neighbor) => Math.Abs(neighbor.X - goal.X) + Math.Abs(neighbor.Y - goal.Y));
            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual(start, result[0]);
            Assert.AreEqual(goal, result[result.Count - 1]);
            Assert.AreEqual(width + height - 1, result.Count);
        }
    }
}