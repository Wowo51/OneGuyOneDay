using System;

namespace DepthFirstSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Graph<int> graph = new Graph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 6);
            graph.AddEdge(3, 7);
            System.Collections.Generic.List<int> traversal = DepthFirstSearchAlgorithm.DFS(graph, 1);
            Console.WriteLine("Depth-First Search Traversal:");
            foreach (int node in traversal)
            {
                Console.Write(node + " ");
            }

            Console.WriteLine();
        }
    }
}