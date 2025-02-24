using System;

namespace GoldschmidtDivision
{
    /// <summary>
    /// Provides methods to perform division using Goldschmidt's iterative algorithm based on multiplicative updates.
    /// </summary>
    public static class GoldschmidtDivider
    {
        /// <summary>
        /// Divides the specified dividend by the divisor using the Goldschmidt division algorithm.
        /// </summary>
        /// <param name = "dividend">The dividend value.</param>
        /// <param name = "divisor">The divisor value. If zero, returns NaN.</param>
        /// <param name = "tolerance">The tolerance for convergence.</param>
        /// <param name = "maxIterations">The maximum allowed iterations.</param>
        /// <returns>The division result computed iteratively, or NaN if the divisor is zero.</returns>
        public static double Divide(double dividend, double divisor, double tolerance, int maxIterations)
        {
            if (divisor == 0.0)
            {
                return double.NaN;
            }

            double sign = Math.Sign(dividend) * Math.Sign(divisor);
            double a = Math.Abs(dividend);
            double b = Math.Abs(divisor);
            // Normalize b to the range [0.5, 1] using scaling.
            double scale = Math.Pow(2.0, Math.Ceiling(Math.Log(b, 2.0)));
            double x = a / scale;
            double y = b / scale;
            int iteration = 0;
            while (Math.Abs(y - 1.0) > tolerance && iteration < maxIterations)
            {
                double factor = 2.0 - y;
                x = x * factor;
                y = y * factor;
                iteration++;
            }

            return sign * x;
        }

        /// <summary>
        /// Divides the specified dividend by the divisor using default tolerance (1e-10) and maximum iterations (20).
        /// </summary>
        /// <param name = "dividend">The dividend value.</param>
        /// <param name = "divisor">The divisor value.</param>
        /// <returns>The division result, or NaN if the divisor is zero.</returns>
        public static double Divide(double dividend, double divisor)
        {
            return Divide(dividend, divisor, 1e-10, 20);
        }
    }
}