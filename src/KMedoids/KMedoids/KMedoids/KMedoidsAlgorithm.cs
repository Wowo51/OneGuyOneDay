using System;
using System.Collections.Generic;
using System.Linq;

namespace KMedoids
{
    public class KMedoidsAlgorithm
    {
        public KMedoidsResult Cluster(IList<DataPoint> points, int k)
        {
            if (points == null || points.Count == 0 || k <= 0 || k > points.Count)
            {
                return new KMedoidsResult();
            }

            // Initialize medoids with first k indices.
            List<int> medoidIndices = new List<int>();
            for (int i = 0; i < k; i++)
            {
                medoidIndices.Add(i);
            }

            bool improved = true;
            while (improved)
            {
                improved = false;
                double currentCost = ComputeTotalCost(points, medoidIndices);
                for (int mIdx = 0; mIdx < medoidIndices.Count; mIdx++)
                {
                    int medoidIndex = medoidIndices[mIdx];
                    for (int i = 0; i < points.Count; i++)
                    {
                        if (medoidIndices.Contains(i))
                        {
                            continue;
                        }

                        List<int> newMedoids = new List<int>(medoidIndices);
                        newMedoids[mIdx] = i;
                        double newCost = ComputeTotalCost(points, newMedoids);
                        if (newCost < currentCost)
                        {
                            medoidIndices = newMedoids;
                            currentCost = newCost;
                            improved = true;
                        }
                    }

                    if (improved)
                    {
                        break;
                    }
                }
            }

            KMedoidsResult result = new KMedoidsResult();
            foreach (int idx in medoidIndices)
            {
                result.Medoids.Add(points[idx]);
            }

            Dictionary<int, List<int>> clustersIndices = GetClusters(points, medoidIndices);
            foreach (KeyValuePair<int, List<int>> entry in clustersIndices)
            {
                DataPoint medoid = points[entry.Key];
                List<DataPoint> clusterPoints = new List<DataPoint>();
                foreach (int pointIndex in entry.Value)
                {
                    clusterPoints.Add(points[pointIndex]);
                }

                result.Clusters[medoid] = clusterPoints;
            }

            return result;
        }

        private double ComputeTotalCost(IList<DataPoint> points, List<int> medoidIndices)
        {
            double totalCost = 0.0;
            foreach (DataPoint point in points)
            {
                double minDist = double.MaxValue;
                foreach (int medoidIndex in medoidIndices)
                {
                    double distance = point.DistanceTo(points[medoidIndex]);
                    if (distance < minDist)
                    {
                        minDist = distance;
                    }
                }

                totalCost += minDist;
            }

            return totalCost;
        }

        private Dictionary<int, List<int>> GetClusters(IList<DataPoint> points, List<int> medoidIndices)
        {
            Dictionary<int, List<int>> clusters = new Dictionary<int, List<int>>();
            foreach (int medoidIndex in medoidIndices)
            {
                clusters[medoidIndex] = new List<int>();
            }

            for (int i = 0; i < points.Count; i++)
            {
                double minDistance = double.MaxValue;
                int assignedMedoid = medoidIndices[0];
                foreach (int medoidIndex in medoidIndices)
                {
                    double distance = points[i].DistanceTo(points[medoidIndex]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        assignedMedoid = medoidIndex;
                    }
                }

                clusters[assignedMedoid].Add(i);
            }

            return clusters;
        }
    }
}