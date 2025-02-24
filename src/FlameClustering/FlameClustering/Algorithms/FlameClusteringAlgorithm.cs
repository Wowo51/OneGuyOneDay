using System;
using System.Collections.Generic;
using FlameClustering.Models;

namespace FlameClustering.Algorithms
{
    public class FlameClusteringAlgorithm
    {
        public List<Cluster> Run(List<DataPoint>? dataPoints, double neighborhoodRadius, int minNeighbors)
        {
            List<Cluster> clusters = new List<Cluster>();
            if (dataPoints == null || dataPoints.Count == 0)
            {
                return clusters;
            }

            int clusterId = 0;
            List<DataPoint> corePoints = new List<DataPoint>();
            foreach (DataPoint dp in dataPoints)
            {
                int count = 0;
                foreach (DataPoint other in dataPoints)
                {
                    if (dp != other && dp.DistanceTo(other) <= neighborhoodRadius)
                    {
                        count += 1;
                    }
                }

                if (count >= minNeighbors)
                {
                    corePoints.Add(dp);
                    clusters.Add(new Cluster(clusterId, dp));
                    clusterId += 1;
                }
            }

            // Fuzzy membership assignment using exponential decay based on distance.
            foreach (DataPoint dp in dataPoints)
            {
                double totalMembership = 0.0;
                Dictionary<int, double> memberships = new Dictionary<int, double>();
                foreach (Cluster cluster in clusters)
                {
                    double distance = dp.DistanceTo(cluster.Center);
                    double membership = Math.Exp(-distance);
                    memberships[cluster.Id] = membership;
                    totalMembership += membership;
                }

                if (totalMembership > 0)
                {
                    foreach (Cluster cluster in clusters)
                    {
                        double normalized = memberships[cluster.Id] / totalMembership;
                        if (cluster.Memberships.ContainsKey(dp))
                        {
                            cluster.Memberships[dp] = normalized;
                        }
                        else
                        {
                            cluster.Memberships.Add(dp, normalized);
                        }
                    }
                }
            }

            return clusters;
        }
    }
}