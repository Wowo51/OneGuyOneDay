using System;
using System.Collections.Generic;

namespace AStarSearch
{
    public static class AStarAlgorithm
    {
        public static List<T> FindPath<T>(T start, T goal, Func<T, IEnumerable<T>> getNeighbors, Func<T, T, double> cost, Func<T, T, double> heuristic)
            where T : notnull
        {
            if (getNeighbors == null || cost == null || heuristic == null)
            {
                return new List<T>();
            }

            Dictionary<T, T> cameFrom = new Dictionary<T, T>();
            Dictionary<T, double> costSoFar = new Dictionary<T, double>();
            PriorityQueue<T, double> frontier = new PriorityQueue<T, double>();
            frontier.Enqueue(start, 0.0);
            costSoFar[start] = 0.0;
            while (frontier.Count > 0)
            {
                T current = frontier.Dequeue();
                if (EqualityComparer<T>.Default.Equals(current, goal))
                {
                    break;
                }

                foreach (T neighbor in getNeighbors(current))
                {
                    double newCost = costSoFar[current] + cost(current, neighbor);
                    if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                    {
                        costSoFar[neighbor] = newCost;
                        double priority = newCost + heuristic(neighbor, goal);
                        frontier.Enqueue(neighbor, priority);
                        cameFrom[neighbor] = current;
                    }
                }
            }

            if (!EqualityComparer<T>.Default.Equals(start, goal) && !cameFrom.ContainsKey(goal))
            {
                return new List<T>();
            }

            List<T> path = new List<T>();
            T currentNode = goal;
            path.Add(currentNode);
            while (!EqualityComparer<T>.Default.Equals(currentNode, start))
            {
                if (!cameFrom.TryGetValue(currentNode, out T? previous) || previous == null)
                {
                    return new List<T>();
                }

                currentNode = previous;
                path.Insert(0, currentNode);
            }

            return path;
        }
    }
}