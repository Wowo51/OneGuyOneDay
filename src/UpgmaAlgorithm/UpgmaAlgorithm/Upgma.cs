using System;
using System.Collections.Generic;

namespace UpgmaAlgorithm
{
    public static class Upgma
    {
        public static TreeNode? BuildTree(double[, ] matrix, string[] labels)
        {
            if (matrix == null || labels == null)
            {
                return null;
            }

            int numberOfTaxa = matrix.GetLength(0);
            if (numberOfTaxa != labels.Length || matrix.GetLength(1) != numberOfTaxa)
            {
                return null;
            }

            List<ClusterWrapper> clusters = new List<ClusterWrapper>();
            for (int i = 0; i < numberOfTaxa; i++)
            {
                TreeNode leaf = new TreeNode(labels[i]);
                ClusterWrapper cw = new ClusterWrapper(leaf, i);
                clusters.Add(cw);
            }

            Dictionary<ClusterPair, double> distanceMap = new Dictionary<ClusterPair, double>();
            for (int i = 0; i < numberOfTaxa; i++)
            {
                for (int j = i + 1; j < numberOfTaxa; j++)
                {
                    ClusterPair pair = new ClusterPair(clusters[i].Id, clusters[j].Id);
                    distanceMap[pair] = matrix[i, j];
                }
            }

            int nextId = numberOfTaxa;
            while (clusters.Count > 1)
            {
                ClusterPair? minPair = null;
                double minDistance = Double.MaxValue;
                foreach (KeyValuePair<ClusterPair, double> entry in distanceMap)
                {
                    if (entry.Value < minDistance)
                    {
                        minDistance = entry.Value;
                        minPair = entry.Key;
                    }
                }

                if (minPair == null)
                {
                    break;
                }

                ClusterWrapper? cluster1 = clusters.Find(delegate (ClusterWrapper x)
                {
                    return x.Id == minPair.First;
                });
                ClusterWrapper? cluster2 = clusters.Find(delegate (ClusterWrapper x)
                {
                    return x.Id == minPair.Second;
                });
                if (cluster1 == null || cluster2 == null)
                {
                    break;
                }

                double newHeight = minDistance / 2.0;
                TreeNode newNode = new TreeNode(null)
                {
                    Height = newHeight,
                    Left = cluster1.Node,
                    Right = cluster2.Node,
                    ClusterSize = cluster1.Node.ClusterSize + cluster2.Node.ClusterSize
                };
                ClusterWrapper newCluster = new ClusterWrapper(newNode, nextId);
                nextId++;
                List<ClusterWrapper> remaining = new List<ClusterWrapper>();
                foreach (ClusterWrapper cw in clusters)
                {
                    if (cw.Id != cluster1.Id && cw.Id != cluster2.Id)
                    {
                        remaining.Add(cw);
                    }
                }

                Dictionary<int, double> newDistancesForNewCluster = new Dictionary<int, double>();
                foreach (ClusterWrapper other in remaining)
                {
                    double distance1 = 0.0;
                    ClusterPair pair1 = new ClusterPair(cluster1.Id, other.Id);
                    if (!distanceMap.TryGetValue(pair1, out distance1))
                    {
                        distance1 = 0.0;
                    }

                    double distance2 = 0.0;
                    ClusterPair pair2 = new ClusterPair(cluster2.Id, other.Id);
                    if (!distanceMap.TryGetValue(pair2, out distance2))
                    {
                        distance2 = 0.0;
                    }

                    double weightedDistance = (cluster1.Node.ClusterSize * distance1 + cluster2.Node.ClusterSize * distance2) / (cluster1.Node.ClusterSize + cluster2.Node.ClusterSize);
                    newDistancesForNewCluster[other.Id] = weightedDistance;
                }

                List<ClusterPair> keysToRemove = new List<ClusterPair>();
                foreach (ClusterPair key in new List<ClusterPair>(distanceMap.Keys))
                {
                    if (key.First == cluster1.Id || key.Second == cluster1.Id || key.First == cluster2.Id || key.Second == cluster2.Id)
                    {
                        keysToRemove.Add(key);
                    }
                }

                foreach (ClusterPair key in keysToRemove)
                {
                    distanceMap.Remove(key);
                }

                foreach (ClusterWrapper other in remaining)
                {
                    ClusterPair newPair = new ClusterPair(newCluster.Id, other.Id);
                    distanceMap[newPair] = newDistancesForNewCluster[other.Id];
                }

                clusters.RemoveAll(delegate (ClusterWrapper x)
                {
                    return x.Id == cluster1.Id || x.Id == cluster2.Id;
                });
                clusters.Add(newCluster);
            }

            if (clusters.Count == 1)
            {
                return clusters[0].Node;
            }

            return null;
        }

        internal class ClusterWrapper
        {
            public TreeNode Node { get; set; }
            public int Id { get; set; }

            public ClusterWrapper(TreeNode node, int id)
            {
                this.Node = node;
                this.Id = id;
            }
        }

        internal record ClusterPair
        {
            public int First { get; init; }
            public int Second { get; init; }

            public ClusterPair(int a, int b)
            {
                if (a < b)
                {
                    First = a;
                    Second = b;
                }
                else
                {
                    First = b;
                    Second = a;
                }
            }
        }
    }
}