using System;

namespace AlphaBetaAlgorithm
{
    public static class AlphaBetaCalculator
    {
        public static double ApproximateHypotenuse(double a, double b)
        {
            double absA = Math.Abs(a);
            double absB = Math.Abs(b);
            double maximum = (absA >= absB) ? absA : absB;
            double minimum = (absA < absB) ? absA : absB;
            return maximum + 0.41421356 * minimum;
        }
    }
}