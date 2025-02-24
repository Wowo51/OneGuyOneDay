using System;

namespace FordFulkersonAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Graph graph = new Graph(6);
            graph.AddEdge(0, 1, 16);
            graph.AddEdge(0, 2, 13);
            graph.AddEdge(1, 2, 10);
            graph.AddEdge(2, 1, 4);
            graph.AddEdge(1, 3, 12);
            graph.AddEdge(2, 4, 14);
            graph.AddEdge(3, 2, 9);
            graph.AddEdge(3, 5, 20);
            graph.AddEdge(4, 3, 7);
            graph.AddEdge(4, 5, 4);
            FordFulkerson solver = new FordFulkerson();
            int maxFlow = solver.ComputeMaxFlow(graph, 0, 5);
            Console.WriteLine("The maximum possible flow is " + maxFlow);
        }
    }
}