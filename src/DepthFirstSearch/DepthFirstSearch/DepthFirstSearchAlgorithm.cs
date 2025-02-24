using System.Collections.Generic;

namespace DepthFirstSearch
{
    public class DepthFirstSearchAlgorithm
    {
        public static List<T> DFS<T>(Graph<T> graph, T start)
            where T : notnull
        {
            List<T> result = new List<T>();
            if (graph == null)
            {
                return result;
            }

            HashSet<T> visited = new HashSet<T>();
            DFSRecursive(graph, start, visited, result);
            return result;
        }

        private static void DFSRecursive<T>(Graph<T> graph, T current, HashSet<T> visited, List<T> result)
            where T : notnull
        {
            if (visited.Contains(current))
            {
                return;
            }

            visited.Add(current);
            result.Add(current);
            List<T> neighbors = graph.GetNeighbors(current);
            foreach (T neighbor in neighbors)
            {
                DFSRecursive(graph, neighbor, visited, result);
            }
        }
    }
}