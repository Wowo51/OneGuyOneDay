using System;

namespace BellmanFordAlgorithm
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Graph graph = new Graph(5);
            graph.AddEdge(0, 1, 6);
            graph.AddEdge(0, 2, 7);
            graph.AddEdge(1, 2, 8);
            graph.AddEdge(1, 3, 5);
            graph.AddEdge(1, 4, -4);
            graph.AddEdge(2, 3, -3);
            graph.AddEdge(2, 4, 9);
            graph.AddEdge(3, 1, -2);
            graph.AddEdge(4, 3, 7);
            graph.AddEdge(4, 0, 2);
            int[] distances;
            int[] predecessors;
            bool success = BellmanFordSolver.Solve(graph, 0, out distances, out predecessors);
            if (success)
            {
                Console.WriteLine("Vertex\tDistance\tPredecessor");
                for (int i = 0; i < distances.Length; i++)
                {
                    string pred = (predecessors[i] == -1 ? "None" : predecessors[i].ToString());
                    Console.WriteLine("{0}\t{1}\t\t{2}", i, distances[i], pred);
                }
            }
            else
            {
                Console.WriteLine("Graph contains a negative weight cycle.");
            }
        }
    }
}