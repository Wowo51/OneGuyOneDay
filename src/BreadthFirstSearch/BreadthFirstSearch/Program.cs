using System;
using System.Collections.Generic;

namespace BreadthFirstSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Graph<string> graph = new Graph<string>();
            graph.AddUndirectedEdge("A", "B");
            graph.AddUndirectedEdge("A", "C");
            graph.AddUndirectedEdge("B", "D");
            graph.AddUndirectedEdge("C", "D");
            graph.AddUndirectedEdge("D", "E");
            List<string>? result = graph.BreadthFirstSearchPath("A", "E");
            if (result != null)
            {
                Console.WriteLine("Shortest path:");
                for (int i = 0; i < result.Count; i++)
                {
                    Console.Write(result[i]);
                    if (i < result.Count - 1)
                    {
                        Console.Write(" -> ");
                    }
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No path found.");
            }
        }
    }
}