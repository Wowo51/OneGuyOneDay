using System;
using System.Collections.Generic;

namespace CuthillMcKeeAlgorithm
{
    public static class CuthillMcKee
    {
        /// <summary>
        /// Computes the Cuthill-McKee ordering for a graph represented as an adjacency list.
        /// </summary>
        /// <param name = "graph">Adjacency list representation of the graph. Each inner list contains the indices of neighboring vertices.</param>
        /// <returns>Array of vertex indices in the Cuthill-McKee ordering.</returns>
        public static int[] ComputeOrdering(IList<IList<int>> graph)
        {
            if (graph == null)
            {
                return new int[0];
            }

            int vertexCount = graph.Count;
            bool[] visited = new bool[vertexCount];
            List<int> ordering = new List<int>();
            for (int i = 0; i < vertexCount; i++)
            {
                if (!visited[i])
                {
                    // Select the starting vertex for the connected component.
                    int startVertex = i;
                    int minDegree = (graph[i] != null) ? graph[i].Count : 0;
                    for (int j = i + 1; j < vertexCount; j++)
                    {
                        if (!visited[j] && graph[j] != null && graph[j].Count < minDegree)
                        {
                            startVertex = j;
                            minDegree = graph[j].Count;
                        }
                    }

                    Queue<int> queue = new Queue<int>();
                    visited[startVertex] = true;
                    queue.Enqueue(startVertex);
                    while (queue.Count > 0)
                    {
                        int current = queue.Dequeue();
                        ordering.Add(current);
                        List<int> neighbors = new List<int>();
                        if (graph[current] != null)
                        {
                            for (int k = 0; k < graph[current].Count; k++)
                            {
                                int neighbor = graph[current][k];
                                if (neighbor >= 0 && neighbor < vertexCount && !visited[neighbor])
                                {
                                    neighbors.Add(neighbor);
                                    visited[neighbor] = true;
                                }
                            }
                        }

                        // Sort neighbors by increasing degree.
                        neighbors.Sort(delegate (int a, int b)
                        {
                            int degreeA = (graph[a] != null) ? graph[a].Count : 0;
                            int degreeB = (graph[b] != null) ? graph[b].Count : 0;
                            return degreeA.CompareTo(degreeB);
                        });
                        for (int m = 0; m < neighbors.Count; m++)
                        {
                            queue.Enqueue(neighbors[m]);
                        }
                    }
                }
            }

            return ordering.ToArray();
        }

        /// <summary>
        /// Computes the Cuthill-McKee ordering for a symmetric sparse matrix.
        /// The matrix is assumed to be square and symmetric, where a non-zero element (except the diagonal) indicates connectivity.
        /// </summary>
        /// <param name = "matrix">A square symmetric sparse matrix.</param>
        /// <returns>Array of vertex indices in the Cuthill-McKee ordering.</returns>
        public static int[] ComputeOrderingFromMatrix(int[, ] matrix)
        {
            if (matrix == null)
            {
                return new int[0];
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            if (rows != cols)
            {
                return new int[0];
            }

            List<IList<int>> graph = new List<IList<int>>();
            for (int i = 0; i < rows; i++)
            {
                List<int> neighbors = new List<int>();
                for (int j = 0; j < cols; j++)
                {
                    if (i != j && matrix[i, j] != 0)
                    {
                        neighbors.Add(j);
                    }
                }

                graph.Add(neighbors);
            }

            return ComputeOrdering(graph);
        }
    }
}