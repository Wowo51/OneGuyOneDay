using System;

namespace FiniteDifferenceMethod
{
    public class FiniteDifferenceSolver
    {
        // Approximates the first derivative using the central difference method.
        public double ApproximateFirstDerivative(Func<double, double> f, double x, double h)
        {
            if (f == null)
            {
                return double.NaN;
            }

            if (h == 0.0)
            {
                return double.NaN;
            }

            return (f(x + h) - f(x - h)) / (2 * h);
        }

        // Approximates the second derivative using the central difference method.
        public double ApproximateSecondDerivative(Func<double, double> f, double x, double h)
        {
            if (f == null)
            {
                return double.NaN;
            }

            if (h == 0.0)
            {
                return double.NaN;
            }

            return (f(x + h) - 2 * f(x) + f(x - h)) / (h * h);
        }
    }
}