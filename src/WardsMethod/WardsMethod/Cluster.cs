using System;
using System.Collections.Generic;

namespace WardsMethod
{
    public class Cluster
    {
        public List<double[]> DataPoints { get; set; }
        public double[] Centroid { get; set; }

        public Cluster(List<double[]> dataPoints)
        {
            DataPoints = dataPoints ?? new List<double[]>();
            Centroid = ComputeCentroid(DataPoints);
        }

        public static double[] ComputeCentroid(List<double[]> points)
        {
            if (points == null || points.Count == 0)
            {
                return new double[0];
            }

            int dimensions = points[0].Length;
            double[] centroid = new double[dimensions];
            for (int i = 0; i < dimensions; i++)
            {
                centroid[i] = 0.0;
            }

            foreach (double[] point in points)
            {
                for (int i = 0; i < dimensions; i++)
                {
                    centroid[i] += point[i];
                }
            }

            for (int i = 0; i < dimensions; i++)
            {
                centroid[i] /= points.Count;
            }

            return centroid;
        }
    }
}