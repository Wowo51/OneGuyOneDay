using System;
using System.Collections.Generic;

namespace MinimumDegreeAlgorithm
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            List<HashSet<int>> graph = new List<HashSet<int>>
            {
                new HashSet<int>
                {
                    1,
                    2
                },
                new HashSet<int>
                {
                    0,
                    2,
                    3
                },
                new HashSet<int>
                {
                    0,
                    1,
                    3
                },
                new HashSet<int>
                {
                    1,
                    2,
                    4
                },
                new HashSet<int>
                {
                    3
                }
            };
            int[] ordering = MinimumDegreeOrderer.ComputeOrdering(graph);
            Console.WriteLine("Elimination ordering:");
            for (int i = 0; i < ordering.Length; i++)
            {
                Console.Write(ordering[i] + " ");
            }

            Console.WriteLine();
        }
    }
}