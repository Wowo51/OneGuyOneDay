using System;

namespace BestBinFirst
{
    public class Point
    {
        public double[] Coordinates;
        public Point(double[] coordinates)
        {
            if (coordinates == null)
            {
                Coordinates = new double[0];
            }
            else
            {
                Coordinates = coordinates;
            }
        }

        public int Dimension
        {
            get
            {
                return Coordinates.Length;
            }
        }

        public double DistanceSquared(Point other)
        {
            if (other == null || other.Coordinates == null || other.Dimension != this.Dimension)
            {
                return double.MaxValue;
            }

            double sum = 0;
            for (int i = 0; i < Dimension; i++)
            {
                double diff = Coordinates[i] - other.Coordinates[i];
                sum += diff * diff;
            }

            return sum;
        }
    }
}