using System;
using System.Collections.Generic;

namespace AverageLinkageClustering
{
    public class AverageLinkageClusterer<T>
    {
        public List<Cluster<T>> ClusterData(List<T> items, Func<T, T, double> distance, int desiredClusterCount)
        {
            if (items == null)
            {
                return new List<Cluster<T>>();
            }

            if (distance == null)
            {
                return new List<Cluster<T>>();
            }

            if (desiredClusterCount < 1)
            {
                desiredClusterCount = 1;
            }

            List<Cluster<T>> clusters = new List<Cluster<T>>();
            for (int index = 0; index < items.Count; index++)
            {
                T currentItem = items[index];
                clusters.Add(new Cluster<T>(currentItem));
            }

            while (clusters.Count > desiredClusterCount)
            {
                double bestDistance = double.MaxValue;
                int bestI = -1;
                int bestJ = -1;
                for (int i = 0; i < clusters.Count - 1; i++)
                {
                    for (int j = i + 1; j < clusters.Count; j++)
                    {
                        double currentDistance = ComputeAverageLinkageDistance(clusters[i], clusters[j], distance);
                        if (currentDistance < bestDistance)
                        {
                            bestDistance = currentDistance;
                            bestI = i;
                            bestJ = j;
                        }
                    }
                }

                if (bestI == -1 || bestJ == -1)
                {
                    break;
                }

                Cluster<T> merged = Cluster<T>.Merge(clusters[bestI], clusters[bestJ]);
                if (bestJ > bestI)
                {
                    clusters.RemoveAt(bestJ);
                    clusters.RemoveAt(bestI);
                }
                else
                {
                    clusters.RemoveAt(bestI);
                    clusters.RemoveAt(bestJ);
                }

                clusters.Add(merged);
            }

            return clusters;
        }

        private double ComputeAverageLinkageDistance(Cluster<T> cluster1, Cluster<T> cluster2, Func<T, T, double> distance)
        {
            if (cluster1 == null || cluster2 == null || distance == null)
            {
                return double.MaxValue;
            }

            List<T> members1 = cluster1.Members;
            List<T> members2 = cluster2.Members;
            if (members1.Count == 0 || members2.Count == 0)
            {
                return double.MaxValue;
            }

            double sum = 0;
            int count = 0;
            for (int i = 0; i < members1.Count; i++)
            {
                for (int j = 0; j < members2.Count; j++)
                {
                    sum = sum + distance(members1[i], members2[j]);
                    count = count + 1;
                }
            }

            return sum / count;
        }
    }
}