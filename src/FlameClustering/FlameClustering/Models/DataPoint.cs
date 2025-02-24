using System;

namespace FlameClustering.Models
{
    public class DataPoint
    {
        public double[] Coordinates { get; }

        public DataPoint(double[]? coordinates)
        {
            if (coordinates == null)
            {
                coordinates = new double[0];
            }

            this.Coordinates = coordinates;
        }

        public double DistanceTo(DataPoint? other)
        {
            if (other == null)
            {
                return double.MaxValue;
            }

            double sum = 0.0;
            int length = System.Math.Min(this.Coordinates.Length, other.Coordinates.Length);
            for (int i = 0; i < length; i++)
            {
                double diff = this.Coordinates[i] - other.Coordinates[i];
                sum += diff * diff;
            }

            return System.Math.Sqrt(sum);
        }
    }
}