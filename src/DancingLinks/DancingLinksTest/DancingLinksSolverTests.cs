using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DancingLinks;

namespace DancingLinksTest
{
    [TestClass]
    public class DancingLinksSolverTests
    {
        private int[, ] GenerateIdentityMatrix(int size)
        {
            int[, ] matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = (i == j) ? 1 : 0;
                }
            }

            return matrix;
        }

        [TestMethod]
        public void TestSingleCellSolution()
        {
            int[, ] matrix = new int[1, 1]
            {
                {
                    1
                }
            };
            ColumnNode header = DancingLinksBuilder.BuildDLXBoard(matrix);
            DancingLinksSolver solver = new DancingLinksSolver(header);
            List<List<DancingNode>> solutions = solver.Solve();
            Assert.AreEqual(1, solutions.Count);
            Assert.AreEqual(1, solutions[0].Count);
        }

        [TestMethod]
        public void TestNoSolution()
        {
            int[, ] matrix = new int[1, 1]
            {
                {
                    0
                }
            };
            ColumnNode header = DancingLinksBuilder.BuildDLXBoard(matrix);
            DancingLinksSolver solver = new DancingLinksSolver(header);
            List<List<DancingNode>> solutions = solver.Solve();
            Assert.AreEqual(0, solutions.Count);
        }

        [TestMethod]
        public void TestIdentityMatrixSolution()
        {
            int size = 5;
            int[, ] matrix = GenerateIdentityMatrix(size);
            ColumnNode header = DancingLinksBuilder.BuildDLXBoard(matrix);
            DancingLinksSolver solver = new DancingLinksSolver(header);
            List<List<DancingNode>> solutions = solver.Solve();
            Assert.AreEqual(1, solutions.Count);
            Assert.AreEqual(size, solutions[0].Count);
        }

        [TestMethod]
        public void TestMultipleSolutions()
        {
            // Matrix with two valid exact cover solutions:
            // Option 1: Use row3 alone (covers all columns).
            // Option 2: Use rows 0, 1, and 2 (each covers one column).
            int[, ] matrix = new int[4, 3]
            {
                {
                    1,
                    0,
                    0
                },
                {
                    0,
                    1,
                    0
                },
                {
                    0,
                    0,
                    1
                },
                {
                    1,
                    1,
                    1
                }
            };
            ColumnNode header = DancingLinksBuilder.BuildDLXBoard(matrix);
            DancingLinksSolver solver = new DancingLinksSolver(header);
            List<List<DancingNode>> solutions = solver.Solve();
            Assert.AreEqual(2, solutions.Count);
        }

        [TestMethod]
        public void TestLargeIdentityMatrix()
        {
            int size = 20;
            int[, ] matrix = GenerateIdentityMatrix(size);
            ColumnNode header = DancingLinksBuilder.BuildDLXBoard(matrix);
            DancingLinksSolver solver = new DancingLinksSolver(header);
            List<List<DancingNode>> solutions = solver.Solve();
            Assert.AreEqual(1, solutions.Count);
            Assert.AreEqual(size, solutions[0].Count);
        }
    }
}