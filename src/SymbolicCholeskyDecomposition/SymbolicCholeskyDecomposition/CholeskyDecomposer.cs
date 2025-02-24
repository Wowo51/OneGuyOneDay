using System;
using System.Collections.Generic;

namespace SymbolicCholeskyDecomposition
{
    public class CholeskyDecomposer
    {
        public static SparseMatrix Decompose(SparseMatrix A)
        {
            if (A == null)
            {
                return new SparseMatrix(0);
            }

            int n = A.Size;
            int[] parent = new int[n];
            for (int j = 0; j < n; j++)
            {
                parent[j] = -1;
            }

            // Compute the elimination tree.
            for (int j = 0; j < n; j++)
            {
                if (A.Data.TryGetValue(j, out Dictionary<int, double> row))
                {
                    foreach (KeyValuePair<int, double> pair in row)
                    {
                        int i = pair.Key;
                        if (i > j)
                        {
                            if (parent[i] == -1 || parent[i] > j)
                            {
                                parent[i] = j;
                            }
                        }
                    }
                }
            }

            SparseMatrix L = new SparseMatrix(n);
            // Compute symbolic factorization for L.
            for (int j = 0; j < n; j++)
            {
                bool[] visited = new bool[n];
                int current = j;
                while (current != -1 && !visited[current])
                {
                    visited[current] = true;
                    current = parent[current];
                }

                for (int i = j; i < n; i++)
                {
                    if (visited[i])
                    {
                        L.AddValue(i, j, 0.0);
                    }
                }
            }

            return L;
        }
    }
}