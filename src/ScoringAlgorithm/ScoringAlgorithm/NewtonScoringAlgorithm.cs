using System;

namespace ScoringAlgorithm
{
    public static class NewtonScoringAlgorithm
    {
        public static double Solve(Func<double, double> scoreFunc, Func<double, double> derivativeFunc, double initialGuess, double tolerance, int maxIterations)
        {
            if (scoreFunc == null || derivativeFunc == null)
            {
                return double.NaN;
            }

            double estimate = initialGuess;
            for (int i = 0; i < maxIterations; i++)
            {
                double score = scoreFunc(estimate);
                double derivative = derivativeFunc(estimate);
                if (Math.Abs(derivative) < tolerance)
                {
                    return double.NaN;
                }

                double newEstimate = estimate - score / derivative;
                if (Math.Abs(newEstimate - estimate) < tolerance)
                {
                    return newEstimate;
                }

                estimate = newEstimate;
            }

            return double.NaN;
        }
    }
}