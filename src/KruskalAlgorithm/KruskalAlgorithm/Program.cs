using System;
using System.Collections.Generic;

namespace KruskalAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int vertices = 4;
            Graph graph = new Graph(vertices);
            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 6);
            graph.AddEdge(0, 3, 5);
            graph.AddEdge(1, 3, 15);
            graph.AddEdge(2, 3, 4);
            List<Edge> mst = KruskalMST.FindMinimumSpanningTree(graph);
            Console.WriteLine("Minimum Spanning Tree:");
            foreach (Edge edge in mst)
            {
                Console.WriteLine(edge.ToString());
            }
        }
    }
}