using System;
using System.Collections.Generic;

namespace LloydsAlgorithm
{
    public class KMeans
    {
        public static KMeansResult Cluster(List<double[]> points, int k, int maxIterations)
        {
            if (points == null || points.Count == 0)
            {
                return new KMeansResult(new List<double[]>(), new int[0]);
            }

            int dimensions = points[0].Length;
            Random random = new Random();
            List<double[]> centroids = new List<double[]>();
            HashSet<int> chosenIndices = new HashSet<int>();
            while (centroids.Count < k)
            {
                int index = random.Next(points.Count);
                if (!chosenIndices.Contains(index))
                {
                    double[] centroid = new double[dimensions];
                    for (int i = 0; i < dimensions; i++)
                    {
                        centroid[i] = points[index][i];
                    }

                    centroids.Add(centroid);
                    chosenIndices.Add(index);
                }
            }

            int[] labels = new int[points.Count];
            bool changed = true;
            int iteration = 0;
            while (changed && iteration < maxIterations)
            {
                changed = false;
                for (int i = 0; i < points.Count; i++)
                {
                    double minDist = Double.MaxValue;
                    int bestCluster = 0;
                    for (int c = 0; c < centroids.Count; c++)
                    {
                        double dist = Distance(points[i], centroids[c]);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            bestCluster = c;
                        }
                    }

                    if (labels[i] != bestCluster)
                    {
                        labels[i] = bestCluster;
                        changed = true;
                    }
                }

                List<double[]> newCentroids = new List<double[]>();
                for (int c = 0; c < k; c++)
                {
                    List<double[]> clusterPoints = new List<double[]>();
                    for (int i = 0; i < points.Count; i++)
                    {
                        if (labels[i] == c)
                        {
                            clusterPoints.Add(points[i]);
                        }
                    }

                    double[] newCentroid = new double[dimensions];
                    if (clusterPoints.Count > 0)
                    {
                        for (int d = 0; d < dimensions; d++)
                        {
                            double sum = 0;
                            for (int i = 0; i < clusterPoints.Count; i++)
                            {
                                sum += clusterPoints[i][d];
                            }

                            newCentroid[d] = sum / clusterPoints.Count;
                        }
                    }
                    else
                    {
                        int randomIndex = random.Next(points.Count);
                        for (int d = 0; d < dimensions; d++)
                        {
                            newCentroid[d] = points[randomIndex][d];
                        }
                    }

                    newCentroids.Add(newCentroid);
                }

                centroids = newCentroids;
                iteration++;
            }

            return new KMeansResult(centroids, labels);
        }

        private static double Distance(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }

            return Math.Sqrt(sum);
        }
    }
}