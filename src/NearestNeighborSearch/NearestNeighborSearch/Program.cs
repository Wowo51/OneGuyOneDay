using System;
using System.Collections.Generic;

namespace NearestNeighborSearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[][] points = new double[][]
            {
                new double[]
                {
                    1.0,
                    2.0
                },
                new double[]
                {
                    2.0,
                    3.0
                },
                new double[]
                {
                    3.0,
                    1.0
                },
                new double[]
                {
                    4.0,
                    2.0
                }
            };
            double[] target = new double[]
            {
                3.1,
                2.1
            };
            NearestNeighborSearchAlgorithm searchAlgorithm = new NearestNeighborSearchAlgorithm();
            double[]? nearest = searchAlgorithm.FindNearestNeighbor(points, target);
            Console.WriteLine("Nearest neighbor to (" + string.Join(", ", target) + "):");
            if (nearest != null)
            {
                Console.WriteLine("(" + string.Join(", ", nearest) + ")");
            }
            else
            {
                Console.WriteLine("No valid nearest neighbor found.");
            }

            List<double[]> kNearest = searchAlgorithm.FindKNearestNeighbors(points, target, 2);
            Console.WriteLine("2 Nearest neighbors:");
            foreach (double[] point in kNearest)
            {
                Console.WriteLine("(" + string.Join(", ", point) + ")");
            }
        }
    }
}