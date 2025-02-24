using System;

namespace NthRootAlgorithm
{
    public static class NthRootCalculator
    {
        public static double NthRoot(double a, int n, double tolerance = 1e-10, int maxIterations = 1000)
        {
            if (n <= 0)
            {
                return double.NaN;
            }

            double sign = 1.0;
            double absA = a;
            if (a < 0)
            {
                if (n % 2 == 0)
                {
                    return double.NaN;
                }

                sign = -1.0;
                absA = -a;
            }

            if (absA == 0)
            {
                return 0;
            }

            double current = absA;
            for (int i = 0; i < maxIterations; i++)
            {
                double previous = current;
                double denominator = (n - 1) * previous + absA / Math.Pow(previous, n - 1);
                current = denominator / n;
                if (Math.Abs(current - previous) < tolerance)
                {
                    break;
                }
            }

            return sign * current;
        }
    }
}