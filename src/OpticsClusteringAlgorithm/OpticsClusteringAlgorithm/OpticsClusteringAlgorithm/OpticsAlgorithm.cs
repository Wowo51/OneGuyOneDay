using System;
using System.Collections.Generic;

namespace OpticsClusteringAlgorithm
{
    public class OpticsAlgorithm
    {
        public List<PointEntry> Run(List<Point> points, double epsilon, int minPoints)
        {
            if (points == null)
            {
                return new List<PointEntry>();
            }

            List<PointEntry> pointEntries = new List<PointEntry>();
            foreach (Point point in points)
            {
                pointEntries.Add(new PointEntry(point));
            }

            List<PointEntry> ordering = new List<PointEntry>();
            foreach (PointEntry entry in pointEntries)
            {
                if (!entry.Processed)
                {
                    List<PointEntry> neighbors = GetNeighbors(entry, pointEntries, epsilon);
                    entry.Processed = true;
                    ordering.Add(entry);
                    if (neighbors.Count >= minPoints)
                    {
                        entry.CoreDistance = CalculateCoreDistance(entry, neighbors, minPoints);
                        List<PointEntry> seeds = new List<PointEntry>();
                        UpdateSeeds(seeds, entry, neighbors, epsilon);
                        while (seeds.Count > 0)
                        {
                            seeds.Sort(delegate (PointEntry a, PointEntry b)
                            {
                                double aVal = a.ReachabilityDistance ?? double.MaxValue;
                                double bVal = b.ReachabilityDistance ?? double.MaxValue;
                                return aVal.CompareTo(bVal);
                            });
                            PointEntry current = seeds[0];
                            seeds.RemoveAt(0);
                            List<PointEntry> currentNeighbors = GetNeighbors(current, pointEntries, epsilon);
                            current.Processed = true;
                            ordering.Add(current);
                            if (currentNeighbors.Count >= minPoints)
                            {
                                current.CoreDistance = CalculateCoreDistance(current, currentNeighbors, minPoints);
                                UpdateSeeds(seeds, current, currentNeighbors, epsilon);
                            }
                        }
                    }
                }
            }

            return ordering;
        }

        private double CalculateCoreDistance(PointEntry entry, List<PointEntry> neighbors, int minPoints)
        {
            List<double> distances = new List<double>();
            foreach (PointEntry neighbor in neighbors)
            {
                distances.Add(CalculateDistance(entry.Value, neighbor.Value));
            }

            distances.Sort();
            if (distances.Count >= minPoints)
            {
                return distances[minPoints - 1];
            }

            return double.NaN;
        }

        private void UpdateSeeds(List<PointEntry> seeds, PointEntry center, List<PointEntry> neighbors, double epsilon)
        {
            foreach (PointEntry neighbor in neighbors)
            {
                if (!neighbor.Processed)
                {
                    double distance = CalculateDistance(center.Value, neighbor.Value);
                    double newReachability = (center.CoreDistance != null) ? Math.Max(center.CoreDistance.GetValueOrDefault(), distance) : distance;
                    if (neighbor.ReachabilityDistance == null)
                    {
                        neighbor.ReachabilityDistance = newReachability;
                        seeds.Add(neighbor);
                    }
                    else if (newReachability < (neighbor.ReachabilityDistance ?? double.MaxValue))
                    {
                        neighbor.ReachabilityDistance = newReachability;
                    }
                }
            }
        }

        private List<PointEntry> GetNeighbors(PointEntry entry, List<PointEntry> allEntries, double epsilon)
        {
            List<PointEntry> neighbors = new List<PointEntry>();
            foreach (PointEntry e in allEntries)
            {
                double distance = CalculateDistance(entry.Value, e.Value);
                if (distance <= epsilon)
                {
                    neighbors.Add(e);
                }
            }

            return neighbors;
        }

        private double CalculateDistance(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}