using System;
using System.Collections.Generic;

namespace CompleteLinkageClustering
{
    public class CompleteLinkageAlgorithm
    {
        public List<List<T>> Cluster<T>(IEnumerable<T>? items, Func<T, T, double>? distanceFunction, double threshold = Double.MaxValue)
        {
            if (items == null || distanceFunction == null)
            {
                return new List<List<T>>();
            }

            List<List<T>> clusters = new List<List<T>>();
            foreach (T item in items)
            {
                clusters.Add(new List<T> { item });
            }

            Boolean merged = true;
            while (merged)
            {
                merged = false;
                Double bestDistance = Double.MaxValue;
                Int32 bestI = -1;
                Int32 bestJ = -1;
                for (Int32 i = 0; i < clusters.Count; i++)
                {
                    for (Int32 j = i + 1; j < clusters.Count; j++)
                    {
                        Double clusterDistance = CalculateCompleteLinkageDistance(clusters[i], clusters[j], distanceFunction);
                        if (clusterDistance < bestDistance && clusterDistance <= threshold)
                        {
                            bestDistance = clusterDistance;
                            bestI = i;
                            bestJ = j;
                        }
                    }
                }

                if (bestI >= 0 && bestJ >= 0)
                {
                    clusters[bestI].AddRange(clusters[bestJ]);
                    clusters.RemoveAt(bestJ);
                    merged = true;
                }
            }

            return clusters;
        }

        private Double CalculateCompleteLinkageDistance<T>(List<T> clusterA, List<T> clusterB, Func<T, T, double> distanceFunction)
        {
            Double maxDistance = 0.0;
            for (Int32 i = 0; i < clusterA.Count; i++)
            {
                for (Int32 j = 0; j < clusterB.Count; j++)
                {
                    Double d = distanceFunction(clusterA[i], clusterB[j]);
                    if (d > maxDistance)
                    {
                        maxDistance = d;
                    }
                }
            }

            return maxDistance;
        }
    }
}