using System;

namespace GaussSeidelMethod
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[, ] matrix = new double[, ]
            {
                {
                    4,
                    1,
                    2
                },
                {
                    3,
                    5,
                    1
                },
                {
                    1,
                    1,
                    3
                }
            };
            double[] b = new double[]
            {
                4,
                7,
                3
            };
            double[] initial = new double[]
            {
                0,
                0,
                0
            };
            double tolerance = 1e-6;
            int maxIterations = 100;
            double[] solution = GaussSeidelSolver.Solve(matrix, b, initial, tolerance, maxIterations);
            Console.WriteLine("Solution:");
            for (int i = 0; i < solution.Length; i++)
            {
                Console.WriteLine("x[{0}] = {1}", i, solution[i]);
            }
        }
    }
}