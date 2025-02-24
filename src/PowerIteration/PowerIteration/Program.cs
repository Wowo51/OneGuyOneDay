using System;

namespace PowerIteration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[, ] matrix = new double[, ]
            {
                {
                    4,
                    1
                },
                {
                    2,
                    3
                }
            };
            double[] initial = new double[]
            {
                1,
                1
            };
            double tolerance = 1E-6;
            int maxIterations = 1000;
            System.ValueTuple<double, double[]> result = PowerIterationAlgorithm.Compute(matrix, initial, tolerance, maxIterations);
            double eigenvalue = result.Item1;
            double[] eigenvector = result.Item2;
            Console.WriteLine("Dominant Eigenvalue: " + eigenvalue);
            Console.Write("Eigenvector: ");
            for (int i = 0; i < eigenvector.Length; i++)
            {
                Console.Write(eigenvector[i] + " ");
            }

            Console.WriteLine();
        }
    }
}