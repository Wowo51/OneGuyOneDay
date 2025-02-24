using System;

namespace CanopyClusteringAlgorithm
{
    public static class DistanceMetrics
    {
        public static double EuclideanDistance(double[] x, double[] y)
        {
            if (x == null || y == null || x.Length != y.Length)
            {
                return double.NaN;
            }

            double sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                double diff = x[i] - y[i];
                sum += diff * diff;
            }

            return Math.Sqrt(sum);
        }
    }
}