using System;
using System.Collections.Generic;

namespace BoruvkasAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Graph graph = new Graph(4);
            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 6);
            graph.AddEdge(0, 3, 5);
            graph.AddEdge(1, 3, 15);
            graph.AddEdge(2, 3, 4);
            List<Edge> mst = Boruvka.ComputeMST(graph);
            Console.WriteLine("Edges in the Minimum Spanning Tree:");
            foreach (Edge edge in mst)
            {
                Console.WriteLine(edge.Source + " - " + edge.Destination + " (weight: " + edge.Weight + ")");
            }
        }
    }
}