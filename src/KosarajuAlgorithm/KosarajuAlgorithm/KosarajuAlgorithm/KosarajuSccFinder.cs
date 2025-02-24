using System;
using System.Collections.Generic;

namespace KosarajuAlgorithm
{
    public static class KosarajuSccFinder
    {
        public static List<List<int>> FindStronglyConnectedComponents(Graph graph)
        {
            if (graph == null)
            {
                return new List<List<int>>();
            }

            List<int> finishOrder = new List<int>();
            HashSet<int> visited = new HashSet<int>();
            foreach (int vertex in graph.AdjacencyList.Keys)
            {
                if (!visited.Contains(vertex))
                {
                    DFS(graph, vertex, visited, finishOrder);
                }
            }

            Graph reversed = graph.Reverse();
            visited.Clear();
            List<List<int>> sccList = new List<List<int>>();
            for (int i = finishOrder.Count - 1; i >= 0; i--)
            {
                int vertex = finishOrder[i];
                if (!visited.Contains(vertex))
                {
                    List<int> component = new List<int>();
                    DFSCollect(reversed, vertex, visited, component);
                    sccList.Add(component);
                }
            }

            return sccList;
        }

        private static void DFS(Graph graph, int vertex, HashSet<int> visited, List<int> finishOrder)
        {
            visited.Add(vertex);
            if (graph.AdjacencyList.TryGetValue(vertex, out List<int>? neighbors) && neighbors != null)
            {
                foreach (int neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        DFS(graph, neighbor, visited, finishOrder);
                    }
                }
            }

            finishOrder.Add(vertex);
        }

        private static void DFSCollect(Graph graph, int vertex, HashSet<int> visited, List<int> component)
        {
            visited.Add(vertex);
            component.Add(vertex);
            if (graph.AdjacencyList.TryGetValue(vertex, out List<int>? neighbors) && neighbors != null)
            {
                foreach (int neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        DFSCollect(graph, neighbor, visited, component);
                    }
                }
            }
        }
    }
}