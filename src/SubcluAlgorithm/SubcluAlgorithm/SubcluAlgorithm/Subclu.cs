using System;
using System.Collections.Generic;

namespace SubcluAlgorithm
{
    public class Subclu
    {
        public List<SubspaceCluster> FindClusters(List<DataPoint> points, double epsilon, int minPoints)
        {
            List<SubspaceCluster> result = new List<SubspaceCluster>();
            if (points == null || points.Count == 0)
            {
                return result;
            }

            int dimensionCount = points[0].Coordinates.Length;
            int maxMask = 1 << dimensionCount;
            for (int mask = 1; mask < maxMask; mask++)
            {
                List<int> subspace = GetSubspaceFromMask(mask, dimensionCount);
                List<List<DataPoint>> clusters = DBSCAN(points, subspace, epsilon, minPoints);
                foreach (List<DataPoint> cluster in clusters)
                {
                    result.Add(new SubspaceCluster(new List<int>(subspace), new List<DataPoint>(cluster)));
                }
            }

            return result;
        }

        private List<int> GetSubspaceFromMask(int mask, int dimensionCount)
        {
            List<int> subspace = new List<int>();
            for (int i = 0; i < dimensionCount; i++)
            {
                if ((mask & (1 << i)) != 0)
                {
                    subspace.Add(i);
                }
            }

            return subspace;
        }

        private List<List<DataPoint>> DBSCAN(List<DataPoint> points, List<int> subspace, double epsilon, int minPoints)
        {
            List<List<DataPoint>> clusters = new List<List<DataPoint>>();
            int pointCount = points.Count;
            bool[] visited = new bool[pointCount];
            int[] clusterAssignment = new int[pointCount];
            for (int i = 0; i < pointCount; i++)
            {
                clusterAssignment[i] = -1;
            }

            int clusterId = 0;
            for (int i = 0; i < pointCount; i++)
            {
                if (visited[i])
                {
                    continue;
                }

                visited[i] = true;
                List<int> neighborIndices = RegionQuery(points, i, subspace, epsilon);
                if (neighborIndices.Count < minPoints)
                {
                    clusterAssignment[i] = -1; // mark as noise
                }
                else
                {
                    ExpandCluster(points, subspace, epsilon, minPoints, i, neighborIndices, clusterId, visited, clusterAssignment);
                    clusterId++;
                }
            }

            for (int id = 0; id < clusterId; id++)
            {
                List<DataPoint> clusterPoints = new List<DataPoint>();
                for (int j = 0; j < pointCount; j++)
                {
                    if (clusterAssignment[j] == id)
                    {
                        clusterPoints.Add(points[j]);
                    }
                }

                if (clusterPoints.Count > 0)
                {
                    clusters.Add(clusterPoints);
                }
            }

            return clusters;
        }

        private void ExpandCluster(List<DataPoint> points, List<int> subspace, double epsilon, int minPoints, int pointIndex, List<int> neighborIndices, int clusterId, bool[] visited, int[] clusterAssignment)
        {
            clusterAssignment[pointIndex] = clusterId;
            int currentIndex = 0;
            while (currentIndex < neighborIndices.Count)
            {
                int neighborIndex = neighborIndices[currentIndex];
                if (!visited[neighborIndex])
                {
                    visited[neighborIndex] = true;
                    List<int> neighborNeighborIndices = RegionQuery(points, neighborIndex, subspace, epsilon);
                    if (neighborNeighborIndices.Count >= minPoints)
                    {
                        for (int k = 0; k < neighborNeighborIndices.Count; k++)
                        {
                            int candidate = neighborNeighborIndices[k];
                            if (!neighborIndices.Contains(candidate))
                            {
                                neighborIndices.Add(candidate);
                            }
                        }
                    }
                }

                if (clusterAssignment[neighborIndex] == -1)
                {
                    clusterAssignment[neighborIndex] = clusterId;
                }

                currentIndex++;
            }
        }

        private List<int> RegionQuery(List<DataPoint> points, int pointIndex, List<int> subspace, double epsilon)
        {
            List<int> neighbors = new List<int>();
            DataPoint point = points[pointIndex];
            double epsilonSq = epsilon * epsilon;
            for (int i = 0; i < points.Count; i++)
            {
                double distanceSq = 0.0;
                for (int j = 0; j < subspace.Count; j++)
                {
                    int dim = subspace[j];
                    double diff = point.Coordinates[dim] - points[i].Coordinates[dim];
                    distanceSq += diff * diff;
                }

                if (distanceSq <= epsilonSq)
                {
                    neighbors.Add(i);
                }
            }

            return neighbors;
        }
    }
}