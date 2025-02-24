using System.Collections.Generic;

namespace BreadthFirstSearch
{
    public class Graph<T>
        where T : notnull
    {
        private readonly Dictionary<T, List<T>> _adjacencyList = new Dictionary<T, List<T>>();
        public void AddUndirectedEdge(T source, T target)
        {
            if (!_adjacencyList.ContainsKey(source))
            {
                _adjacencyList[source] = new List<T>();
            }

            if (!_adjacencyList.ContainsKey(target))
            {
                _adjacencyList[target] = new List<T>();
            }

            _adjacencyList[source].Add(target);
            _adjacencyList[target].Add(source);
        }

        public List<T>? BreadthFirstSearchPath(T start, T goal)
        {
            if (!_adjacencyList.ContainsKey(start) || !_adjacencyList.ContainsKey(goal))
            {
                return null;
            }

            Queue<T> queue = new Queue<T>();
            Dictionary<T, T> previous = new Dictionary<T, T>();
            HashSet<T> visited = new HashSet<T>();
            queue.Enqueue(start);
            visited.Add(start);
            while (queue.Count > 0)
            {
                T current = queue.Dequeue();
                if (current.Equals(goal))
                {
                    List<T> path = new List<T>();
                    T temp = goal;
                    while (!temp.Equals(start))
                    {
                        path.Add(temp);
                        temp = previous[temp];
                    }

                    path.Add(start);
                    path.Reverse();
                    return path;
                }

                if (_adjacencyList.TryGetValue(current, out List<T>? neighbors))
                {
                    foreach (T neighbor in neighbors)
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            previous[neighbor] = current;
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            return null;
        }
    }
}