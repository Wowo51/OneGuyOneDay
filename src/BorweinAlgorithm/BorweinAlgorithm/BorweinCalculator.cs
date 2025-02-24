using System;

namespace BorweinAlgorithm
{
    public static class BorweinCalculator
    {
        /// <summary>
        /// Calculates an approximation of 1/π using Borwein's iterative algorithm.
        /// </summary>
        /// <param name = "iterations">The number of iterations to perform. Must be non-negative.</param>
        /// <returns>An approximation of 1/π.</returns>
        public static double CalculateReciprocalPi(int iterations)
        {
            if (iterations < 0)
            {
                iterations = 0;
            }

            double y = Math.Sqrt(2.0) - 1.0;
            double a = 6.0 - 4.0 * Math.Sqrt(2.0);
            for (int i = 0; i < iterations; i++)
            {
                double y4 = y * y * y * y;
                double root = Math.Pow(1.0 - y4, 0.25);
                double yNext = (1.0 - root) / (1.0 + root);
                double factor = Math.Pow(1.0 + yNext, 4);
                double twoExponent = Math.Pow(2.0, (2 * i + 3));
                double aNext = a * factor - twoExponent * yNext * (1.0 + yNext + yNext * yNext);
                y = yNext;
                a = aNext;
            }

            return a;
        }
    }
}