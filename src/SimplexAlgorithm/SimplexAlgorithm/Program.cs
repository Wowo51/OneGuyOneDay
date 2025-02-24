using System;

namespace SimplexAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[, ] A = new double[, ]
            {
                {
                    1,
                    2
                },
                {
                    1,
                    1
                }
            };
            double[] b = new double[]
            {
                6,
                4
            };
            double[] c = new double[]
            {
                3,
                2
            };
            double[] solution = SimplexSolver.Solve(A, b, c);
            System.Console.WriteLine("Solution:");
            for (int i = 0; i < solution.Length; i++)
            {
                System.Console.WriteLine("x" + (i + 1) + " = " + solution[i]);
            }
        }
    }
}