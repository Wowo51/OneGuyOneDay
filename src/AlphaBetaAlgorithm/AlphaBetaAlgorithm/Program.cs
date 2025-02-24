using System;

namespace AlphaBetaAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double a = 3.0;
            double b = 4.0;
            double approx = AlphaBetaCalculator.ApproximateHypotenuse(a, b);
            Console.WriteLine("Approximation of hypotenuse for 3 and 4: " + approx);
        }
    }
}