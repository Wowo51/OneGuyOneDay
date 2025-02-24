using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarnsdorffsRule;

namespace WarnsdorffsRuleTest
{
    [TestClass]
    public class KnightTourSolverTests
    {
        [TestMethod]
        public void Test_BoardSizeOne_ReturnsSingleCellTour()
        {
            KnightTourSolver solver = new KnightTourSolver();
            List<Coordinate> tour = solver.Solve(1, 0, 0);
            Assert.AreEqual(1, tour.Count, "Tour for board size 1 should have exactly one cell.");
            Assert.AreEqual(0, tour[0].X, "Starting X should be 0 for board size 1.");
            Assert.AreEqual(0, tour[0].Y, "Starting Y should be 0 for board size 1.");
        }

        [TestMethod]
        public void Test_BoardSizeTwo_ReturnsEmptyTour()
        {
            KnightTourSolver solver = new KnightTourSolver();
            List<Coordinate> tour = solver.Solve(2, 0, 0);
            Assert.AreEqual(0, tour.Count, "Tour for board size 2 should be empty as a complete tour is impossible.");
        }

        [TestMethod]
        public void Test_BoardSizeEight_CompleteTour()
        {
            KnightTourSolver solver = new KnightTourSolver();
            List<Coordinate> tour = solver.Solve(8, 0, 0);
            // The solver returns a complete tour (64 cells) or an empty tour if it fails.
            Assert.IsTrue(tour.Count == 0 || tour.Count == 64, "Tour for board size 8 should be either complete (64 cells) or empty.");
            if (tour.Count == 64)
            {
                ValidateTour(tour, 8);
            }
        }

        [DataTestMethod]
        [DataRow(1, 0, 0, true)]
        [DataRow(2, 0, 0, false)]
        [DataRow(3, 0, 0, false)]
        [DataRow(4, 0, 0, false)]
        [DataRow(5, 0, 0, true)]
        [DataRow(6, 0, 0, true)]
        [DataRow(8, 0, 0, true)]
        public void Test_MultipleBoardSizes(int boardSize, int startX, int startY, bool shouldSucceed)
        {
            KnightTourSolver solver = new KnightTourSolver();
            List<Coordinate> tour = solver.Solve(boardSize, startX, startY);
            if (shouldSucceed)
            {
                Assert.AreEqual(boardSize * boardSize, tour.Count, $"Tour should be complete for board size {boardSize}.");
                ValidateTour(tour, boardSize);
            }
            else
            {
                Assert.AreEqual(0, tour.Count, $"Tour should be empty for board size {boardSize} as a complete tour is impossible.");
            }
        }

        [TestMethod]
        public void Test_AllStartingPositions_8x8()
        {
            KnightTourSolver solver = new KnightTourSolver();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    List<Coordinate> tour = solver.Solve(8, x, y);
                    Assert.IsTrue(tour.Count == 64 || tour.Count == 0, $"For starting position ({x},{y}), tour count should be either 0 or 64.");
                    if (tour.Count == 64)
                    {
                        ValidateTour(tour, 8);
                    }
                }
            }
        }

        [TestMethod]
        public void Test_RandomStartingPositions_8x8()
        {
            KnightTourSolver solver = new KnightTourSolver();
            Random random = new Random(42);
            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, 8);
                int y = random.Next(0, 8);
                List<Coordinate> tour = solver.Solve(8, x, y);
                Assert.IsTrue(tour.Count == 64 || tour.Count == 0, $"For random starting position ({x},{y}), tour count should be either 0 or 64.");
                if (tour.Count == 64)
                {
                    ValidateTour(tour, 8);
                }
            }
        }

        private void ValidateTour(List<Coordinate> tour, int boardSize)
        {
            HashSet<string> visited = new HashSet<string>();
            for (int i = 0; i < tour.Count; i++)
            {
                Coordinate coord = tour[i];
                Assert.IsTrue(coord.X >= 0 && coord.X < boardSize, $"Coordinate X out of range at index {i}.");
                Assert.IsTrue(coord.Y >= 0 && coord.Y < boardSize, $"Coordinate Y out of range at index {i}.");
                string key = coord.X + "," + coord.Y;
                Assert.IsFalse(visited.Contains(key), $"Duplicate coordinate ({coord.X}, {coord.Y}) found.");
                visited.Add(key);
                if (i > 0)
                {
                    Coordinate prev = tour[i - 1];
                    int dx = Math.Abs(coord.X - prev.X);
                    int dy = Math.Abs(coord.Y - prev.Y);
                    bool validMove = (dx == 2 && dy == 1) || (dx == 1 && dy == 2);
                    Assert.IsTrue(validMove, $"Invalid knight move from ({prev.X},{prev.Y}) to ({coord.X},{coord.Y}).");
                }
            }
        }
    }
}