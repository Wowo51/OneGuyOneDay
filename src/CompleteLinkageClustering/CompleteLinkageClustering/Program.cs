using System;
using System.Collections.Generic;

namespace CompleteLinkageClustering
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Int32> data = new List<Int32>
            {
                1,
                3,
                7,
                8,
                10
            };
            CompleteLinkageAlgorithm algorithm = new CompleteLinkageAlgorithm();
            List<List<Int32>> clusters = algorithm.Cluster(data, Distance, 3.0);
            Console.WriteLine("Clusters:");
            foreach (List<Int32> cluster in clusters)
            {
                Console.WriteLine(string.Join(", ", cluster));
            }
        }

        public static double Distance(Int32 a, Int32 b)
        {
            return Math.Abs(a - b);
        }
    }
}