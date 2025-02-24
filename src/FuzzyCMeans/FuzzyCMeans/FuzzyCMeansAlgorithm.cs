using System;
using System.Collections.Generic;

namespace FuzzyCMeans
{
    public class FuzzyCMeansAlgorithm
    {
        public static FuzzyCMeansResult Compute(List<double[]> dataPoints, int clusters, double m, double tolerance, int maxIterations)
        {
            if (dataPoints == null)
            {
                return new FuzzyCMeansResult(new List<double[]>(), new double[0, 0]);
            }

            int n = dataPoints.Count;
            if (n == 0)
            {
                return new FuzzyCMeansResult(new List<double[]>(), new double[0, 0]);
            }

            int dimension = dataPoints[0].Length;
            double[, ] memberships = new double[n, clusters];
            double[, ] newMemberships = new double[n, clusters];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < clusters; j++)
                {
                    double randomValue = random.NextDouble();
                    memberships[i, j] = randomValue;
                    sum += randomValue;
                }

                for (int j = 0; j < clusters; j++)
                {
                    memberships[i, j] = memberships[i, j] / sum;
                }
            }

            List<double[]> centroids = new List<double[]>();
            for (int j = 0; j < clusters; j++)
            {
                double[] centroid = new double[dimension];
                for (int d = 0; d < dimension; d++)
                {
                    centroid[d] = 0.0;
                }

                centroids.Add(centroid);
            }

            int iteration = 0;
            double maxDifference = double.MaxValue;
            while (iteration < maxIterations && maxDifference > tolerance)
            {
                // Update centroids
                for (int j = 0; j < clusters; j++)
                {
                    double[] numerator = new double[dimension];
                    double denominator = 0.0;
                    for (int i = 0; i < n; i++)
                    {
                        double u_ij_m = Math.Pow(memberships[i, j], m);
                        for (int d = 0; d < dimension; d++)
                        {
                            numerator[d] += u_ij_m * dataPoints[i][d];
                        }

                        denominator += u_ij_m;
                    }

                    if (denominator != 0)
                    {
                        for (int d = 0; d < dimension; d++)
                        {
                            centroids[j][d] = numerator[d] / denominator;
                        }
                    }
                    else
                    {
                        for (int d = 0; d < dimension; d++)
                        {
                            centroids[j][d] = 0.0;
                        }
                    }
                }

                // Update memberships
                maxDifference = 0.0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < clusters; j++)
                    {
                        double distanceToCentroid = EuclideanDistance(dataPoints[i], centroids[j]);
                        if (distanceToCentroid == 0)
                        {
                            newMemberships[i, j] = 1.0;
                            for (int k = 0; k < clusters; k++)
                            {
                                if (k != j)
                                {
                                    newMemberships[i, k] = 0.0;
                                }
                            }

                            break;
                        }
                        else
                        {
                            double sumTerm = 0.0;
                            for (int k = 0; k < clusters; k++)
                            {
                                double distanceToOther = EuclideanDistance(dataPoints[i], centroids[k]);
                                if (distanceToOther == 0)
                                {
                                    sumTerm += 1.0;
                                }
                                else
                                {
                                    double ratio = distanceToCentroid / distanceToOther;
                                    sumTerm += Math.Pow(ratio, 2.0 / (m - 1.0));
                                }
                            }

                            newMemberships[i, j] = 1.0 / sumTerm;
                        }
                    }
                }

                // Calculate convergence
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < clusters; j++)
                    {
                        double diff = Math.Abs(newMemberships[i, j] - memberships[i, j]);
                        if (diff > maxDifference)
                        {
                            maxDifference = diff;
                        }

                        memberships[i, j] = newMemberships[i, j];
                    }
                }

                iteration++;
            }

            return new FuzzyCMeansResult(centroids, memberships);
        }

        private static double EuclideanDistance(double[] point1, double[] point2)
        {
            double sum = 0.0;
            for (int i = 0; i < point1.Length; i++)
            {
                double diff = point1[i] - point2[i];
                sum += diff * diff;
            }

            return Math.Sqrt(sum);
        }
    }
}