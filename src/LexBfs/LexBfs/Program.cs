using System;
using System.Collections.Generic;

namespace LexBfs
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Graph<int> graph = new Graph<int>();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            List<int> ordering = LexBfsAlgorithm.LexBfs(graph);
            Console.WriteLine("LexBFS ordering:");
            foreach (int vertex in ordering)
            {
                Console.Write(vertex + " ");
            }

            Console.WriteLine();
        }
    }
}