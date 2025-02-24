using System;

namespace KMedoids
{
    public class DataPoint
    {
        public double[] Features { get; }

        public DataPoint(double[] features)
        {
            Features = features ?? new double[0];
        }

        public double DistanceTo(DataPoint other)
        {
            if (other == null || other.Features == null)
            {
                return double.MaxValue;
            }

            double sum = 0.0;
            int length = Features.Length < other.Features.Length ? Features.Length : other.Features.Length;
            for (int i = 0; i < length; i++)
            {
                double diff = Features[i] - other.Features[i];
                sum += diff * diff;
            }

            return Math.Sqrt(sum);
        }
    }
}