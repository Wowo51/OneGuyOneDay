using System;
using System.Collections.Generic;

namespace KargersAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<int> vertices = new List<int>
            {
                1,
                2,
                3,
                4
            };
            List<Edge> edges = new List<Edge>
            {
                new Edge(1, 2),
                new Edge(1, 3),
                new Edge(1, 4),
                new Edge(2, 3),
                new Edge(2, 4),
                new Edge(3, 4)
            };
            Graph graph = new Graph(vertices, edges);
            int iterations = 100;
            int minCut = KargerAlgorithm.FindMinCut(graph, iterations);
            Console.WriteLine("Estimated minimum cut: " + minCut);
        }
    }
}