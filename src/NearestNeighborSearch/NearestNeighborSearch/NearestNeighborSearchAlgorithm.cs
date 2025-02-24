using System;
using System.Collections.Generic;

namespace NearestNeighborSearch
{
    public class NearestNeighborSearchAlgorithm
    {
        public double[]? FindNearestNeighbor(double[][] points, double[] target)
        {
            if (points == null || target == null)
            {
                return null;
            }

            int length = target.Length;
            double bestDistance = double.MaxValue;
            double[]? bestPoint = null;
            for (int i = 0; i < points.Length; i++)
            {
                double[] point = points[i];
                if (point == null || point.Length != length)
                {
                    continue;
                }

                double distance = EuclideanDistance(point, target);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestPoint = point;
                }
            }

            return bestPoint;
        }

        public List<double[]> FindKNearestNeighbors(double[][] points, double[] target, int k)
        {
            List<double[]> result = new List<double[]>();
            if (points == null || target == null || k <= 0)
            {
                return result;
            }

            int length = target.Length;
            List<KeyValuePair<double, double[]>> allDistances = new List<KeyValuePair<double, double[]>>();
            for (int i = 0; i < points.Length; i++)
            {
                double[] point = points[i];
                if (point == null || point.Length != length)
                {
                    continue;
                }

                double distance = EuclideanDistance(point, target);
                allDistances.Add(new KeyValuePair<double, double[]>(distance, point));
            }

            allDistances.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key));
            int count = Math.Min(k, allDistances.Count);
            for (int i = 0; i < count; i++)
            {
                result.Add(allDistances[i].Value);
            }

            return result;
        }

        private double EuclideanDistance(double[] a, double[] b)
        {
            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }

            return Math.Sqrt(sum);
        }
    }
}