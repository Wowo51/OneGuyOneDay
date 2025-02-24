using System;

namespace NewtonRaphsonDivision
{
    public static class NewtonRaphsonDivisionCalculator
    {
        public static double Divide(double numerator, double divisor, double tolerance = 1e-10, int maxIterations = 20)
        {
            if (double.IsNaN(numerator) || double.IsNaN(divisor))
            {
                return double.NaN;
            }

            if (divisor == 0.0)
            {
                if (numerator == 0.0)
                {
                    return double.NaN;
                }
                else if (numerator > 0.0)
                {
                    return double.PositiveInfinity;
                }
                else
                {
                    return double.NegativeInfinity;
                }
            }

            double reciprocal;
            if (Math.Abs(divisor) > 1.0)
            {
                // For better convergence when divisor is large,
                // use an initial guess computed by the reciprocal.
                reciprocal = 1.0 / divisor;
            }
            else
            {
                // Use a signed constant initial guess when |divisor| <= 1.
                reciprocal = (divisor >= 0.0 ? 1.0 : -1.0);
            }

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                double previous = reciprocal;
                reciprocal = reciprocal * (2.0 - divisor * reciprocal);
                if (Math.Abs(reciprocal - previous) < tolerance)
                {
                    break;
                }
            }

            double quotient = numerator * reciprocal;
            return quotient;
        }
    }
}