using System;
using System.Collections.Generic;

namespace BidirectionalSearch
{
    class Program
    {
        public static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.AddEdge("A", "B");
            graph.AddEdge("A", "C");
            graph.AddEdge("B", "D");
            graph.AddEdge("C", "D");
            graph.AddEdge("D", "E");
            List<string> path = BidirectionalSearchAlgorithm.FindShortestPath(graph, "A", "E");
            if (path.Count == 0)
            {
                Console.WriteLine("No path found.");
            }
            else
            {
                Console.WriteLine("Shortest path: " + string.Join(" -> ", path));
            }
        }
    }
}