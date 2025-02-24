using System;
using System.Collections.Generic;

namespace AlgorithmX
{
    public class ExactCoverSolver
    {
        public List<List<int>> Solve(bool[, ] matrix)
        {
            if (matrix is null)
            {
                return new List<List<int>>();
            }

            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);
            List<List<int>> rowOptions = new List<List<int>>();
            for (int r = 0; r < rowCount; r++)
            {
                List<int> options = new List<int>();
                for (int c = 0; c < colCount; c++)
                {
                    if (matrix[r, c])
                    {
                        options.Add(c);
                    }
                }

                rowOptions.Add(options);
            }

            HashSet<int> uncovered = new HashSet<int>();
            for (int c = 0; c < colCount; c++)
            {
                uncovered.Add(c);
            }

            List<List<int>> solutions = new List<List<int>>();
            List<int> candidate = new List<int>();
            List<int> activeRows = new List<int>();
            for (int r = 0; r < rowCount; r++)
            {
                activeRows.Add(r);
            }

            SolveRecursive(rowOptions, uncovered, candidate, activeRows, solutions);
            return solutions;
        }

        private void SolveRecursive(List<List<int>> rowOptions, HashSet<int> uncovered, List<int> candidate, List<int> activeRows, List<List<int>> solutions)
        {
            if (uncovered.Count == 0)
            {
                solutions.Add(new List<int>(candidate));
                return;
            }

            int chosenColumn = -1;
            int minCount = Int32.MaxValue;
            foreach (int col in uncovered)
            {
                int count = 0;
                foreach (int r in activeRows)
                {
                    if (rowOptions[r].Contains(col))
                    {
                        count++;
                    }
                }

                if (count < minCount)
                {
                    minCount = count;
                    chosenColumn = col;
                }
            }

            if (chosenColumn == -1 || minCount == 0)
            {
                return;
            }

            List<int> rowsToTry = new List<int>();
            foreach (int r in activeRows)
            {
                if (rowOptions[r].Contains(chosenColumn))
                {
                    rowsToTry.Add(r);
                }
            }

            foreach (int r in rowsToTry)
            {
                candidate.Add(r);
                HashSet<int> newUncovered = new HashSet<int>(uncovered);
                foreach (int col in rowOptions[r])
                {
                    newUncovered.Remove(col);
                }

                List<int> newActiveRows = new List<int>();
                foreach (int r2 in activeRows)
                {
                    if (r2 == r)
                    {
                        continue;
                    }

                    bool conflict = false;
                    foreach (int col in rowOptions[r])
                    {
                        if (rowOptions[r2].Contains(col))
                        {
                            conflict = true;
                            break;
                        }
                    }

                    if (!conflict)
                    {
                        newActiveRows.Add(r2);
                    }
                }

                SolveRecursive(rowOptions, newUncovered, candidate, newActiveRows, solutions);
                candidate.RemoveAt(candidate.Count - 1);
            }
        }
    }
}