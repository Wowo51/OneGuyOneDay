using System;
using System.Collections.Generic;

namespace MinimumDegreeAlgorithm
{
    public static class MinimumDegreeOrderer
    {
        public static int[] ComputeOrdering(List<HashSet<int>> graph)
        {
            if (graph == null)
            {
                return new int[0];
            }

            int nodeCount = graph.Count;
            bool[] eliminated = new bool[nodeCount];
            List<int> eliminationOrder = new List<int>();
            while (true)
            {
                int selectedVertex = -1;
                int minDegree = int.MaxValue;
                for (int i = 0; i < nodeCount; i++)
                {
                    if (!eliminated[i])
                    {
                        int degree = graph[i].Count;
                        if (degree < minDegree)
                        {
                            minDegree = degree;
                            selectedVertex = i;
                        }
                    }
                }

                if (selectedVertex == -1)
                {
                    break;
                }

                List<int> neighbors = new List<int>(graph[selectedVertex]);
                int neighborCount = neighbors.Count;
                for (int i = 0; i < neighborCount; i++)
                {
                    for (int j = i + 1; j < neighborCount; j++)
                    {
                        int ni = neighbors[i];
                        int nj = neighbors[j];
                        if (!graph[ni].Contains(nj))
                        {
                            graph[ni].Add(nj);
                        }

                        if (!graph[nj].Contains(ni))
                        {
                            graph[nj].Add(ni);
                        }
                    }
                }

                foreach (int neighbor in neighbors)
                {
                    graph[neighbor].Remove(selectedVertex);
                }

                eliminated[selectedVertex] = true;
                eliminationOrder.Add(selectedVertex);
                graph[selectedVertex].Clear();
            }

            return eliminationOrder.ToArray();
        }
    }
}