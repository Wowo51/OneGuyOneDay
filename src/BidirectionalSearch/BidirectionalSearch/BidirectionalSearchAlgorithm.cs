using System;
using System.Collections.Generic;

namespace BidirectionalSearch
{
    public static class BidirectionalSearchAlgorithm
    {
        public static List<string> FindShortestPath(Graph graph, string start, string goal)
        {
            if (graph == null || start == null || goal == null)
            {
                return new List<string>();
            }

            if (start == goal)
            {
                return new List<string>
                {
                    start
                };
            }

            Queue<string> queueF = new Queue<string>();
            Queue<string> queueB = new Queue<string>();
            Dictionary<string, string?> parentF = new Dictionary<string, string?>();
            Dictionary<string, string?> parentB = new Dictionary<string, string?>();
            queueF.Enqueue(start);
            parentF[start] = null;
            queueB.Enqueue(goal);
            parentB[goal] = null;
            string? meetingNode = null;
            while (queueF.Count > 0 && queueB.Count > 0)
            {
                meetingNode = ExpandLevel(graph, queueF, parentF, parentB);
                if (meetingNode != null)
                {
                    return ConstructPath(meetingNode, parentF, parentB);
                }

                meetingNode = ExpandLevel(graph, queueB, parentB, parentF);
                if (meetingNode != null)
                {
                    return ConstructPath(meetingNode, parentF, parentB);
                }
            }

            return new List<string>();
        }

        private static string? ExpandLevel(Graph graph, Queue<string> queue, Dictionary<string, string?> currentParents, Dictionary<string, string?> otherParents)
        {
            int levelSize = queue.Count;
            for (int i = 0; i < levelSize; i++)
            {
                string current = queue.Dequeue();
                List<string> neighbors = graph.GetNeighbors(current);
                foreach (string neighbor in neighbors)
                {
                    if (!currentParents.ContainsKey(neighbor))
                    {
                        currentParents[neighbor] = current;
                        if (otherParents.ContainsKey(neighbor))
                        {
                            return neighbor;
                        }

                        queue.Enqueue(neighbor);
                    }
                }
            }

            return null;
        }

        private static List<string> ConstructPath(string meetingNode, Dictionary<string, string?> parentF, Dictionary<string, string?> parentB)
        {
            List<string> pathForward = new List<string>();
            string? node = meetingNode;
            while (node != null)
            {
                pathForward.Insert(0, node);
                node = parentF[node];
            }

            List<string> pathBackward = new List<string>();
            node = parentB[meetingNode];
            while (node != null)
            {
                pathBackward.Add(node);
                node = parentB[node];
            }

            List<string> fullPath = new List<string>(pathForward);
            fullPath.AddRange(pathBackward);
            return fullPath;
        }
    }
}