using System;
using System.Collections.Generic;

namespace KMeansPlusPlus
{
    public class KMeansPlusPlusAlgorithm
    {
        public int MaxIterations { get; set; }
        public double Tolerance { get; set; }
        public List<double[]> Centroids { get; private set; }

        public KMeansPlusPlusAlgorithm()
        {
            this.MaxIterations = 300;
            this.Tolerance = 1e-4;
            this.Centroids = new List<double[]>();
        }

        public int[] Fit(double[][] data, int k)
        {
            if (data == null || data.Length == 0 || k <= 0 || k > data.Length)
            {
                return new int[0];
            }

            System.Random random = new System.Random();
            int n = data.Length;
            int dimensions = data[0].Length;
            this.Centroids.Clear();
            // k‑means++ initialization: choose first centroid randomly.
            int firstIndex = random.Next(0, n);
            double[] firstCentroid = new double[dimensions];
            for (int d = 0; d < dimensions; d++)
            {
                firstCentroid[d] = data[firstIndex][d];
            }

            this.Centroids.Add(firstCentroid);
            double[] distances = new double[n];
            for (int centroidCount = 1; centroidCount < k; centroidCount++)
            {
                double sumDistances = 0.0;
                for (int i = 0; i < n; i++)
                {
                    double nearestDistance = Double.MaxValue;
                    foreach (double[] centroid in this.Centroids)
                    {
                        double dist = ComputeDistanceSquared(data[i], centroid);
                        if (dist < nearestDistance)
                        {
                            nearestDistance = dist;
                        }
                    }

                    distances[i] = nearestDistance;
                    sumDistances += nearestDistance;
                }

                if (sumDistances == 0.0)
                {
                    int randomIndex = random.Next(0, n);
                    double[] newCentroid = new double[dimensions];
                    for (int d = 0; d < dimensions; d++)
                    {
                        newCentroid[d] = data[randomIndex][d];
                    }

                    this.Centroids.Add(newCentroid);
                }
                else
                {
                    double randomValue = random.NextDouble() * sumDistances;
                    double cumulative = 0.0;
                    int selectedIndex = 0;
                    for (int i = 0; i < n; i++)
                    {
                        cumulative += distances[i];
                        if (cumulative >= randomValue)
                        {
                            selectedIndex = i;
                            break;
                        }
                    }

                    double[] selectedCentroid = new double[dimensions];
                    for (int d = 0; d < dimensions; d++)
                    {
                        selectedCentroid[d] = data[selectedIndex][d];
                    }

                    this.Centroids.Add(selectedCentroid);
                }
            }

            // Main k‑means iteration: assignment and update steps.
            int[] assignments = new int[n];
            for (int iter = 0; iter < this.MaxIterations; iter++)
            {
                bool anyChange = false;
                // Assignment step.
                for (int i = 0; i < n; i++)
                {
                    double minDistance = Double.MaxValue;
                    int clusterIndex = -1;
                    for (int j = 0; j < this.Centroids.Count; j++)
                    {
                        double distance = ComputeDistanceSquared(data[i], this.Centroids[j]);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            clusterIndex = j;
                        }
                    }

                    if (assignments[i] != clusterIndex)
                    {
                        assignments[i] = clusterIndex;
                        anyChange = true;
                    }
                }

                // Update step.
                List<double[]> newCentroids = new List<double[]>();
                for (int j = 0; j < k; j++)
                {
                    List<double[]> clusterPoints = new List<double[]>();
                    for (int i = 0; i < n; i++)
                    {
                        if (assignments[i] == j)
                        {
                            clusterPoints.Add(data[i]);
                        }
                    }

                    if (clusterPoints.Count > 0)
                    {
                        newCentroids.Add(ComputeCentroid(clusterPoints));
                    }
                    else
                    {
                        newCentroids.Add(this.Centroids[j]);
                    }
                }

                // Check for convergence.
                double maxShift = 0.0;
                for (int j = 0; j < k; j++)
                {
                    double shift = Math.Sqrt(ComputeDistanceSquared(this.Centroids[j], newCentroids[j]));
                    if (shift > maxShift)
                    {
                        maxShift = shift;
                    }
                }

                this.Centroids = newCentroids;
                if (!anyChange || maxShift < this.Tolerance)
                {
                    break;
                }
            }

            return assignments;
        }

        private double ComputeDistanceSquared(double[] point1, double[] point2)
        {
            if (point1 == null || point2 == null || point1.Length != point2.Length)
            {
                return 0.0;
            }

            double sum = 0.0;
            for (int i = 0; i < point1.Length; i++)
            {
                double diff = point1[i] - point2[i];
                sum += diff * diff;
            }

            return sum;
        }

        private double[] ComputeCentroid(List<double[]> points)
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

            for (int i = 0; i < points.Count; i++)
            {
                double[] point = points[i];
                for (int d = 0; d < dimensions; d++)
                {
                    centroid[d] += point[d];
                }
            }

            for (int d = 0; d < dimensions; d++)
            {
                centroid[d] /= points.Count;
            }

            return centroid;
        }
    }
}