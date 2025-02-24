using System;
using System.Collections.Generic;

namespace ChristofidesAlgorithm
{
    public static class ChristofidesSolver
    {
        public static List<int> SolveTSP(double[, ] distances)
        {
            int n = distances.GetLength(0);
            if (n == 0)
            {
                return new List<int>();
            }

            List<(int, int)> mstEdges = MinimumSpanningTree(distances);
            List<int> oddVertices = GetOddDegreeVertices(mstEdges, n);
            List<(int, int)> matchingEdges = MinimumPerfectMatching(oddVertices, distances);
            Dictionary<int, List<int>> multigraph = BuildMultigraph(mstEdges, matchingEdges, n);
            List<int> eulerianTour = EulerianTour(multigraph);
            List<int> hamiltonianCircuit = ShortcutEulerianTour(eulerianTour);
            return hamiltonianCircuit;
        }

        private static List<(int, int)> MinimumSpanningTree(double[, ] distances)
        {
            int n = distances.GetLength(0);
            bool[] inMST = new bool[n];
            double[] minEdge = new double[n];
            int[] parent = new int[n];
            List<(int, int)> mstEdges = new List<(int, int)>();
            for (int i = 0; i < n; i++)
            {
                minEdge[i] = double.MaxValue;
                parent[i] = -1;
            }

            minEdge[0] = 0;
            for (int i = 0; i < n; i++)
            {
                int u = -1;
                double minDistance = double.MaxValue;
                for (int j = 0; j < n; j++)
                {
                    if (!inMST[j] && minEdge[j] < minDistance)
                    {
                        minDistance = minEdge[j];
                        u = j;
                    }
                }

                if (u == -1)
                {
                    break;
                }

                inMST[u] = true;
                if (parent[u] != -1)
                {
                    mstEdges.Add((parent[u], u));
                }

                for (int v = 0; v < n; v++)
                {
                    if (!inMST[v] && distances[u, v] < minEdge[v])
                    {
                        minEdge[v] = distances[u, v];
                        parent[v] = u;
                    }
                }
            }

            return mstEdges;
        }

        private static List<int> GetOddDegreeVertices(List<(int, int)> edges, int n)
        {
            int[] degree = new int[n];
            foreach ((int u, int v)in edges)
            {
                degree[u]++;
                degree[v]++;
            }

            List<int> oddVertices = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (degree[i] % 2 != 0)
                {
                    oddVertices.Add(i);
                }
            }

            return oddVertices;
        }

        private static List<(int, int)> MinimumPerfectMatching(List<int> oddVertices, double[, ] distances)
        {
            List<(int, int)> bestMatching = new List<(int, int)>();
            double bestCost = double.MaxValue;
            MatchRecursive(oddVertices, new List<(int, int)>(), 0.0, distances, ref bestCost, ref bestMatching);
            return bestMatching;
        }

        private static void MatchRecursive(List<int> vertices, List<(int, int)> currentMatching, double currentCost, double[, ] distances, ref double bestCost, ref List<(int, int)> bestMatching)
        {
            if (vertices.Count == 0)
            {
                if (currentCost < bestCost)
                {
                    bestCost = currentCost;
                    bestMatching = new List<(int, int)>(currentMatching);
                }

                return;
            }

            int first = vertices[0];
            List<int> remaining = new List<int>(vertices);
            remaining.RemoveAt(0);
            for (int i = 0; i < remaining.Count; i++)
            {
                int second = remaining[i];
                double pairCost = distances[first, second];
                List<(int, int)> newMatching = new List<(int, int)>(currentMatching);
                newMatching.Add((first, second));
                List<int> newVertices = new List<int>(remaining);
                newVertices.RemoveAt(i);
                MatchRecursive(newVertices, newMatching, currentCost + pairCost, distances, ref bestCost, ref bestMatching);
            }
        }

        private static Dictionary<int, List<int>> BuildMultigraph(List<(int, int)> mstEdges, List<(int, int)> matchingEdges, int n)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }

            foreach ((int u, int v)in mstEdges)
            {
                graph[u].Add(v);
                graph[v].Add(u);
            }

            foreach ((int u, int v)in matchingEdges)
            {
                graph[u].Add(v);
                graph[v].Add(u);
            }

            return graph;
        }

        private static List<int> EulerianTour(Dictionary<int, List<int>> graph)
        {
            Stack<int> stack = new Stack<int>();
            List<int> circuit = new List<int>();
            int start = 0;
            stack.Push(start);
            while (stack.Count > 0)
            {
                int v = stack.Peek();
                if (graph[v].Count > 0)
                {
                    int u = graph[v][0];
                    stack.Push(u);
                    graph[v].RemoveAt(0);
                    graph[u].Remove(v);
                }
                else
                {
                    circuit.Add(v);
                    stack.Pop();
                }
            }

            circuit.Reverse();
            return circuit;
        }

        private static List<int> ShortcutEulerianTour(List<int> eulerianTour)
        {
            List<int> path = new List<int>();
            HashSet<int> visited = new HashSet<int>();
            foreach (int vertex in eulerianTour)
            {
                if (!visited.Contains(vertex))
                {
                    visited.Add(vertex);
                    path.Add(vertex);
                }
            }

            if (path.Count > 0 && path[0] != path[path.Count - 1])
            {
                path.Add(path[0]);
            }

            return path;
        }
    }
}