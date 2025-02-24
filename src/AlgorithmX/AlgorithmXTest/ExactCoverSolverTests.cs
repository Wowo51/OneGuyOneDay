using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmX;

namespace AlgorithmXTest
{
    [TestClass]
    public class ExactCoverSolverTests
    {
        [TestMethod]
        public void Test_NullMatrix()
        {
            ExactCoverSolver solver = new ExactCoverSolver();
            List<List<int>> solutions = solver.Solve(null);
            Assert.IsNotNull(solutions);
            Assert.AreEqual(0, solutions.Count, "Null matrix should yield no solutions.");
        }

        [TestMethod]
        public void Test_SingleRowSingleColTrue()
        {
            ExactCoverSolver solver = new ExactCoverSolver();
            bool[, ] matrix = new bool[1, 1];
            matrix[0, 0] = true;
            List<List<int>> solutions = solver.Solve(matrix);
            Assert.IsNotNull(solutions);
            Assert.AreEqual(1, solutions.Count, "Expected one solution.");
            Assert.AreEqual(1, solutions[0].Count, "Solution should contain one row index.");
            Assert.AreEqual(0, solutions[0][0], "Row index should be 0.");
        }

        [TestMethod]
        public void Test_SingleRowSingleColFalse()
        {
            ExactCoverSolver solver = new ExactCoverSolver();
            bool[, ] matrix = new bool[1, 1];
            matrix[0, 0] = false;
            List<List<int>> solutions = solver.Solve(matrix);
            Assert.IsNotNull(solutions);
            Assert.AreEqual(0, solutions.Count, "Expected no solutions.");
        }

        [TestMethod]
        public void Test_EmptyMatrix()
        {
            bool[, ] matrix = new bool[0, 0];
            ExactCoverSolver solver = new ExactCoverSolver();
            List<List<int>> solutions = solver.Solve(matrix);
            Assert.IsNotNull(solutions);
            Assert.AreEqual(1, solutions.Count, "Empty matrix should yield one solution (empty candidate).");
            Assert.AreEqual(0, solutions[0].Count, "Solution candidate should be empty.");
        }

        [TestMethod]
        public void Test_ThreeByThreeExactCover()
        {
            bool[, ] matrix = new bool[3, 3];
            matrix[0, 0] = true;
            matrix[1, 1] = true;
            matrix[2, 2] = true;
            ExactCoverSolver solver = new ExactCoverSolver();
            List<List<int>> solutions = solver.Solve(matrix);
            Assert.IsNotNull(solutions);
            Assert.AreEqual(1, solutions.Count, "Expected one solution for a diagonal matrix.");
            Assert.IsTrue(IsValidSolution(matrix, solutions[0]), "Solution does not cover all columns disjointly.");
        }

        [TestMethod]
        public void Test_MultipleSolutions()
        {
            bool[, ] matrix = new bool[4, 3];
            // Row0 and Row3 both cover column 0.
            matrix[0, 0] = true;
            matrix[1, 1] = true;
            matrix[2, 2] = true;
            matrix[3, 0] = true;
            ExactCoverSolver solver = new ExactCoverSolver();
            List<List<int>> solutions = solver.Solve(matrix);
            Assert.IsNotNull(solutions);
            Assert.AreEqual(2, solutions.Count, "Expected two solutions using either row0 or row3 for column 0.");
            foreach (List<int> candidate in solutions)
            {
                Assert.IsTrue(IsValidSolution(matrix, candidate), "Solution candidate is not valid.");
            }
        }

        [TestMethod]
        public void Test_NoSolution()
        {
            // A scenario where column 1 is never covered.
            bool[, ] matrix = new bool[2, 2];
            matrix[0, 0] = true;
            // Row1 remains all false.
            ExactCoverSolver solver = new ExactCoverSolver();
            List<List<int>> solutions = solver.Solve(matrix);
            Assert.IsNotNull(solutions);
            Assert.AreEqual(0, solutions.Count, "Expected no solutions since column 1 is never covered.");
        }

        [TestMethod]
        public void Test_AllTrueMatrix()
        {
            int rowCount = 3;
            int colCount = 3;
            bool[, ] matrix = new bool[rowCount, colCount];
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    matrix[r, c] = true;
                }
            }

            ExactCoverSolver solver = new ExactCoverSolver();
            List<List<int>> solutions = solver.Solve(matrix);
            Assert.IsNotNull(solutions);
            Assert.AreEqual(3, solutions.Count, "Expected exactly 3 solutions for a full true matrix, one for each row selected.");
            foreach (List<int> candidate in solutions)
            {
                Assert.IsTrue(candidate.Count == 1, "Each solution in an all true matrix should have exactly one row.");
            }
        }

        [TestMethod]
        public void Test_LargeSyntheticData()
        {
            int rowCount = 20;
            int colCount = 10;
            bool[, ] matrix = new bool[rowCount, colCount];
            // Ensure a valid trivial cover by letting the first 'colCount' rows cover unique columns.
            for (int i = 0; i < colCount; i++)
            {
                matrix[i, i] = true;
            }

            // Fill the remaining rows with random data (30% chance for each cell to be true).
            System.Random random = new System.Random(42);
            for (int r = colCount; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    matrix[r, c] = (random.NextDouble() < 0.3);
                }
            }

            ExactCoverSolver solver = new ExactCoverSolver();
            List<List<int>> solutions = solver.Solve(matrix);
            Assert.IsNotNull(solutions);
            Assert.IsTrue(solutions.Count > 0, "Expected at least one solution in large synthetic data.");
            foreach (List<int> candidate in solutions)
            {
                Assert.IsTrue(IsValidSolution(matrix, candidate), "Solution candidate is not valid in synthetic data.");
            }
        }

        private bool IsValidSolution(bool[, ] matrix, List<int> candidate)
        {
            int colCount = matrix.GetLength(1);
            bool[] covered = new bool[colCount];
            for (int i = 0; i < candidate.Count; i++)
            {
                int rowIndex = candidate[i];
                for (int c = 0; c < colCount; c++)
                {
                    if (matrix[rowIndex, c])
                    {
                        if (covered[c])
                        {
                            return false;
                        }

                        covered[c] = true;
                    }
                }
            }

            for (int c = 0; c < colCount; c++)
            {
                if (!covered[c])
                {
                    return false;
                }
            }

            return true;
        }
    }
}