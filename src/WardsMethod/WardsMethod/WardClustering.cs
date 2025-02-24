using System;
using System.Collections.Generic;

namespace WardsMethod
{
    public class WardClustering
    {
        public List<Cluster> Clusters { get; private set; }

        public WardClustering(List<double[]>? dataPoints)
        {
            if (dataPoints == null)
            {
                Clusters = new List<Cluster>();
                return;
            }

            Clusters = new List<Cluster>();
            foreach (double[] point in dataPoints)
            {
                List<double[]> pointList = new List<double[]>();
                pointList.Add(point);
                Cluster cluster = new Cluster(pointList);
                Clusters.Add(cluster);
            }
        }

        public void PerformClustering()
        {
            while (Clusters.Count > 1)
            {
                int mergeIndexA = 0;
                int mergeIndexB = 1;
                double minIncrease = double.MaxValue;
                for (int i = 0; i < Clusters.Count; i++)
                {
                    for (int j = i + 1; j < Clusters.Count; j++)
                    {
                        double increase = CalculateIncrease(Clusters[i], Clusters[j]);
                        if (increase < minIncrease)
                        {
                            minIncrease = increase;
                            mergeIndexA = i;
                            mergeIndexB = j;
                        }
                    }
                }

                Cluster merged = MergeClusters(Clusters[mergeIndexA], Clusters[mergeIndexB]);
                Clusters.RemoveAt(mergeIndexB);
                Clusters.RemoveAt(mergeIndexA);
                Clusters.Add(merged);
            }
        }

        public double CalculateIncrease(Cluster cluster1, Cluster cluster2)
        {
            List<double[]> mergedPoints = new List<double[]>();
            mergedPoints.AddRange(cluster1.DataPoints);
            mergedPoints.AddRange(cluster2.DataPoints);
            double[] mergedCentroid = Cluster.ComputeCentroid(mergedPoints);
            double ssBefore = SumOfSquares(cluster1, cluster1.Centroid) + SumOfSquares(cluster2, cluster2.Centroid);
            double ssAfter = SumOfSquares(cluster1, mergedCentroid) + SumOfSquares(cluster2, mergedCentroid);
            return ssAfter - ssBefore;
        }

        public Cluster MergeClusters(Cluster cluster1, Cluster cluster2)
        {
            List<double[]> mergedPoints = new List<double[]>();
            mergedPoints.AddRange(cluster1.DataPoints);
            mergedPoints.AddRange(cluster2.DataPoints);
            return new Cluster(mergedPoints);
        }

        public double SumOfSquares(Cluster cluster, double[] centroid)
        {
            double sum = 0.0;
            foreach (double[] point in cluster.DataPoints)
            {
                double squaredDistance = 0.0;
                for (int i = 0; i < point.Length; i++)
                {
                    double diff = point[i] - centroid[i];
                    squaredDistance += diff * diff;
                }

                sum += squaredDistance;
            }

            return sum;
        }
    }
}