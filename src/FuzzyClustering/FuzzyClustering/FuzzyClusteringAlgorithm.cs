using System;

namespace FuzzyClustering
{
    public class FuzzyClusteringAlgorithm
    {
        public FuzzyClusterResult Cluster(double[][] points, int clusterCount, double fuzziness, int maxIterations, double epsilon)
        {
            if (points == null)
            {
                points = new double[0][];
            }

            if (clusterCount <= 0)
            {
                return new FuzzyClusterResult(new double[0][], new double[0][]);
            }

            if (fuzziness <= 1.0)
            {
                fuzziness = 2.0;
            }

            int pointCount = points.Length;
            if (pointCount == 0)
            {
                return new FuzzyClusterResult(new double[0][], new double[0][]);
            }

            int dimension = points[0].Length;
            double[][] membership = new double[pointCount][];
            System.Random random = new System.Random();
            for (int i = 0; i < pointCount; i++)
            {
                membership[i] = new double[clusterCount];
                double sum = 0.0;
                for (int j = 0; j < clusterCount; j++)
                {
                    membership[i][j] = random.NextDouble();
                    sum += membership[i][j];
                }

                for (int j = 0; j < clusterCount; j++)
                {
                    membership[i][j] /= sum;
                }
            }

            double[][] centers = new double[clusterCount][];
            for (int j = 0; j < clusterCount; j++)
            {
                centers[j] = new double[dimension];
            }

            double maxDelta = double.MaxValue;
            int iteration = 0;
            while (iteration < maxIterations && maxDelta > epsilon)
            {
                // Update cluster centers
                for (int j = 0; j < clusterCount; j++)
                {
                    double[] numerator = new double[dimension];
                    double denominator = 0.0;
                    for (int i = 0; i < pointCount; i++)
                    {
                        double exponent = Math.Pow(membership[i][j], fuzziness);
                        for (int d = 0; d < dimension; d++)
                        {
                            numerator[d] += exponent * points[i][d];
                        }

                        denominator += exponent;
                    }

                    if (denominator != 0)
                    {
                        for (int d = 0; d < dimension; d++)
                        {
                            centers[j][d] = numerator[d] / denominator;
                        }
                    }
                }

                // Update membership matrix
                maxDelta = 0.0;
                for (int i = 0; i < pointCount; i++)
                {
                    int zeroIndex = -1;
                    for (int j = 0; j < clusterCount; j++)
                    {
                        double distance = Distance(points[i], centers[j]);
                        if (distance == 0.0)
                        {
                            zeroIndex = j;
                            break;
                        }
                    }

                    if (zeroIndex != -1)
                    {
                        for (int j = 0; j < clusterCount; j++)
                        {
                            double oldValue = membership[i][j];
                            double newValue = (j == zeroIndex ? 1.0 : 0.0);
                            membership[i][j] = newValue;
                            double delta = Math.Abs(newValue - oldValue);
                            if (delta > maxDelta)
                            {
                                maxDelta = delta;
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < clusterCount; j++)
                        {
                            double oldValue = membership[i][j];
                            double sumDenom = 0.0;
                            double dist_ij = Distance(points[i], centers[j]);
                            for (int k = 0; k < clusterCount; k++)
                            {
                                double dist_ik = Distance(points[i], centers[k]);
                                if (dist_ik == 0.0)
                                {
                                    dist_ik = 0.000001;
                                }

                                sumDenom += Math.Pow(dist_ij / dist_ik, 2.0 / (fuzziness - 1.0));
                            }

                            double newValue = 1.0 / sumDenom;
                            membership[i][j] = newValue;
                            double delta = Math.Abs(newValue - oldValue);
                            if (delta > maxDelta)
                            {
                                maxDelta = delta;
                            }
                        }
                    }
                }

                iteration++;
            }

            return new FuzzyClusterResult(centers, membership);
        }

        private double Distance(double[] point1, double[] point2)
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