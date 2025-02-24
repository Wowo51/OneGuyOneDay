using System;

namespace BisectionMethod
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double low = 0.0;
            double high = 2.0;
            double tolerance = 1e-6;
            double root = BisectionSolver.FindRoot((double x) => x * x - 2.0, low, high, tolerance);
            if (double.IsNaN(root))
            {
                Console.WriteLine("No root found in the interval.");
            }
            else
            {
                Console.WriteLine("Root found: " + root);
            }
        }
    }
}