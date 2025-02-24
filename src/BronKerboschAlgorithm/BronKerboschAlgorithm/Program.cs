using System;
using System.Collections.Generic;

namespace BronKerboschAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(1, 3);
            graph.AddEdge(4, 5);
            List<List<int>> cliques = BronKerbosch.FindMaximalCliques(graph);
            Console.WriteLine("Maximal Cliques:");
            foreach (List<int> clique in cliques)
            {
                Console.WriteLine(string.Join(", ", clique));
            }
        }
    }
}