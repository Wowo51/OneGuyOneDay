using System;
using DinicsAlgorithm;

namespace DinicsAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int nodeCount = 6;
            Dinic dinic = new Dinic(nodeCount);
            dinic.AddEdge(0, 1, 16);
            dinic.AddEdge(0, 2, 13);
            dinic.AddEdge(1, 2, 10);
            dinic.AddEdge(1, 3, 12);
            dinic.AddEdge(2, 1, 4);
            dinic.AddEdge(2, 4, 14);
            dinic.AddEdge(3, 2, 9);
            dinic.AddEdge(3, 5, 20);
            dinic.AddEdge(4, 3, 7);
            dinic.AddEdge(4, 5, 4);
            int maxFlow = dinic.MaxFlow(0, 5);
            Console.WriteLine("Maximum flow: " + maxFlow);
        }
    }
}