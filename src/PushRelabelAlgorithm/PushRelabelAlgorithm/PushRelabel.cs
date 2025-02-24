using System;
using System.Collections.Generic;

namespace PushRelabelAlgorithm
{
    public static class PushRelabel
    {
        public static int GetMaxFlow(Graph graph, int source, int sink)
        {
            int n = graph.VertexCount;
            int[] height = new int[n];
            int[] excess = new int[n];
            for (int i = 0; i < n; i++)
            {
                height[i] = 0;
                excess[i] = 0;
            }

            height[source] = n;
            foreach (Edge e in graph.AdjacencyList[source])
            {
                e.Flow = e.Capacity;
                excess[e.To] += e.Flow;
                excess[source] -= e.Flow;
                e.Reverse!.Flow = -e.Flow;
            }

            bool progress = true;
            while (progress)
            {
                progress = false;
                for (int u = 0; u < n; u++)
                {
                    if (u != source && u != sink && excess[u] > 0)
                    {
                        bool pushed = false;
                        for (int i = 0; i < graph.AdjacencyList[u].Count; i++)
                        {
                            Edge e = graph.AdjacencyList[u][i];
                            if (e.ResidualCapacity() > 0 && height[u] == height[e.To] + 1)
                            {
                                int pushFlow = Math.Min(excess[u], e.ResidualCapacity());
                                e.Flow += pushFlow;
                                e.Reverse!.Flow -= pushFlow;
                                excess[e.To] += pushFlow;
                                excess[u] -= pushFlow;
                                pushed = true;
                                progress = true;
                                if (excess[u] == 0)
                                {
                                    break;
                                }
                            }
                        }

                        if (!pushed)
                        {
                            int minHeight = int.MaxValue;
                            foreach (Edge e in graph.AdjacencyList[u])
                            {
                                if (e.ResidualCapacity() > 0 && height[e.To] < minHeight)
                                {
                                    minHeight = height[e.To];
                                }
                            }

                            if (minHeight < int.MaxValue)
                            {
                                height[u] = minHeight + 1;
                                progress = true;
                            }
                        }
                    }
                }
            }

            return excess[sink];
        }
    }
}