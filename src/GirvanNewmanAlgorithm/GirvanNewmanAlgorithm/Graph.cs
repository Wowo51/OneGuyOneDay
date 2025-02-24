using System;
using System.Collections.Generic;

namespace GirvanNewmanAlgorithm
{
    public class Graph
    {
        public Dictionary<string, List<string>> AdjacencyList { get; private set; }

        public Graph()
        {
            AdjacencyList = new Dictionary<string, List<string>>();
        }

        public void AddVertex(string vertex)
        {
            if (!AdjacencyList.ContainsKey(vertex))
            {
                AdjacencyList[vertex] = new List<string>();
            }
        }

        public void AddEdge(string vertex1, string vertex2)
        {
            AddVertex(vertex1);
            AddVertex(vertex2);
            if (!AdjacencyList[vertex1].Contains(vertex2))
            {
                AdjacencyList[vertex1].Add(vertex2);
            }

            if (!AdjacencyList[vertex2].Contains(vertex1))
            {
                AdjacencyList[vertex2].Add(vertex1);
            }
        }

        public void RemoveEdge(string vertex1, string vertex2)
        {
            if (AdjacencyList.ContainsKey(vertex1))
            {
                AdjacencyList[vertex1].Remove(vertex2);
            }

            if (AdjacencyList.ContainsKey(vertex2))
            {
                AdjacencyList[vertex2].Remove(vertex1);
            }
        }

        public List<List<string>> GetConnectedComponents()
        {
            List<List<string>> components = new List<List<string>>();
            HashSet<string> visited = new HashSet<string>();
            foreach (string vertex in AdjacencyList.Keys)
            {
                if (!visited.Contains(vertex))
                {
                    List<string> component = new List<string>();
                    Queue<string> queue = new Queue<string>();
                    queue.Enqueue(vertex);
                    visited.Add(vertex);
                    while (queue.Count > 0)
                    {
                        string current = queue.Dequeue();
                        component.Add(current);
                        foreach (string neighbor in AdjacencyList[current])
                        {
                            if (!visited.Contains(neighbor))
                            {
                                visited.Add(neighbor);
                                queue.Enqueue(neighbor);
                            }
                        }
                    }

                    components.Add(component);
                }
            }

            return components;
        }
    }
}