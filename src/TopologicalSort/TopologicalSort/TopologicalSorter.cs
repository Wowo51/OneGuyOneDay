using System.Collections.Generic;

namespace TopologicalSort
{
    public static class TopologicalSorter
    {
        // Attempts a topological sort on the dependencyGraph.
        // The dependencyGraph maps a node to the collection of nodes it depends on.
        // Returns true and a sorted list of nodes if successful.
        // If a cycle is detected, returns false and the sorted list is incomplete.
        public static bool TrySort<T>(IDictionary<T, IEnumerable<T>> dependencyGraph, out List<T> sorted)
            where T : notnull
        {
            if (dependencyGraph == null)
            {
                sorted = new List<T>();
                return false;
            }

            Dictionary<T, int> inDegree = new Dictionary<T, int>();
            Dictionary<T, List<T>> dependents = new Dictionary<T, List<T>>();
            // Initialize each node and calculate in-degrees.
            foreach (KeyValuePair<T, IEnumerable<T>> pair in dependencyGraph)
            {
                T node = pair.Key;
                if (!inDegree.ContainsKey(node))
                {
                    inDegree[node] = 0;
                }

                if (pair.Value != null)
                {
                    foreach (T prerequisite in pair.Value)
                    {
                        if (!inDegree.ContainsKey(prerequisite))
                        {
                            inDegree[prerequisite] = 0;
                        }

                        inDegree[node] = inDegree[node] + 1;
                        if (!dependents.ContainsKey(prerequisite))
                        {
                            dependents[prerequisite] = new List<T>();
                        }

                        dependents[prerequisite].Add(node);
                    }
                }
            }

            Queue<T> queue = new Queue<T>();
            foreach (KeyValuePair<T, int> entry in inDegree)
            {
                if (entry.Value == 0)
                {
                    queue.Enqueue(entry.Key);
                }
            }

            sorted = new List<T>();
            while (queue.Count > 0)
            {
                T current = queue.Dequeue();
                sorted.Add(current);
                if (dependents.ContainsKey(current))
                {
                    foreach (T dependent in dependents[current])
                    {
                        inDegree[dependent] = inDegree[dependent] - 1;
                        if (inDegree[dependent] == 0)
                        {
                            queue.Enqueue(dependent);
                        }
                    }
                }
            }

            // If not all nodes were sorted, a cycle exists.
            if (sorted.Count != inDegree.Count)
            {
                return false;
            }

            return true;
        }

        // Returns a sorted list of nodes based on their dependencies.
        // If a cycle is detected, an empty list is returned.
        public static List<T> Sort<T>(IDictionary<T, IEnumerable<T>> dependencyGraph)
            where T : notnull
        {
            List<T> sorted;
            bool success = TrySort<T>(dependencyGraph, out sorted);
            if (!success)
            {
                sorted = new List<T>();
            }

            return sorted;
        }
    }
}