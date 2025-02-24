using System;
using System.Collections.Generic;

namespace SingleLinkageClustering
{
    public class SingleLinkageClusterer<T>
    {
        public List<List<T>> ClusterPoints(IList<T> points, Func<T, T, double> distanceFunc, int desiredClusters)
        {
            if (points == null || distanceFunc == null)
            {
                return new List<List<T>>();
            }

            if (desiredClusters < 1)
            {
                desiredClusters = 1;
            }

            List<List<T>> clusters = new List<List<T>>();
            foreach (T point in points)
            {
                clusters.Add(new List<T> { point });
            }

            while (clusters.Count > desiredClusters)
            {
                double minDistance = double.MaxValue;
                int clusterIndexA = -1;
                int clusterIndexB = -1;
                for (int i = 0; i < clusters.Count; i++)
                {
                    for (int j = i + 1; j < clusters.Count; j++)
                    {
                        double distance = GetMinimumDistance(clusters[i], clusters[j], distanceFunc);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            clusterIndexA = i;
                            clusterIndexB = j;
                        }
                    }
                }

                if (clusterIndexA == -1 || clusterIndexB == -1)
                {
                    break;
                }

                List<T> mergedCluster = new List<T>();
                mergedCluster.AddRange(clusters[clusterIndexA]);
                mergedCluster.AddRange(clusters[clusterIndexB]);
                // Remove the clusters by removing the higher index first to avoid shifting.
                if (clusterIndexB > clusterIndexA)
                {
                    clusters.RemoveAt(clusterIndexB);
                    clusters.RemoveAt(clusterIndexA);
                }
                else
                {
                    clusters.RemoveAt(clusterIndexA);
                    clusters.RemoveAt(clusterIndexB);
                }

                clusters.Add(mergedCluster);
            }

            return clusters;
        }

        private double GetMinimumDistance(List<T> clusterA, List<T> clusterB, Func<T, T, double> distanceFunc)
        {
            double minDistance = double.MaxValue;
            foreach (T itemA in clusterA)
            {
                foreach (T itemB in clusterB)
                {
                    double distance = distanceFunc(itemA, itemB);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }
            }

            return minDistance;
        }
    }
}