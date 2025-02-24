using System;

namespace RandomSearch
{
    public class RandomSearchAlgorithm
    {
        public RandomSearchResult Optimize(double[] lowerBounds, double[] upperBounds, System.Func<double[], double> objective, int iterations)
        {
            if (lowerBounds == null || upperBounds == null || objective == null || lowerBounds.Length != upperBounds.Length)
            {
                return new RandomSearchResult(new double[0], double.PositiveInfinity);
            }

            System.Random random = new System.Random();
            double[] bestCandidate = new double[lowerBounds.Length];
            double bestValue = double.PositiveInfinity;
            for (int iteration = 0; iteration < iterations; iteration++)
            {
                double[] candidate = new double[lowerBounds.Length];
                for (int i = 0; i < candidate.Length; i++)
                {
                    double r = random.NextDouble();
                    candidate[i] = lowerBounds[i] + r * (upperBounds[i] - lowerBounds[i]);
                }

                double value = objective(candidate);
                if (value < bestValue)
                {
                    bestValue = value;
                    bestCandidate = (double[])candidate.Clone();
                }
            }

            return new RandomSearchResult(bestCandidate, bestValue);
        }
    }
}