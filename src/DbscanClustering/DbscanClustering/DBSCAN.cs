using System;
using System.Collections.Generic;

namespace DbscanClustering
{
    public class DBSCAN
    {
        public List<List<Point>> Cluster(List<Point> points, double eps, int minPts)
        {
            if (points == null)
            {
                return new List<List<Point>>();
            }

            foreach (Point p in points)
            {
                p.Visited = false;
                p.ClusterId = 0;
            }

            List<List<Point>> clusters = new List<List<Point>>();
            int clusterId = 1;
            foreach (Point p in points)
            {
                if (p.Visited)
                {
                    continue;
                }

                p.Visited = true;
                List<Point> neighborPts = GetNeighbors(p, points, eps);
                if (neighborPts.Count < minPts)
                {
                    p.ClusterId = -1;
                }
                else
                {
                    List<Point> newCluster = new List<Point>();
                    ExpandCluster(p, neighborPts, newCluster, clusterId, points, eps, minPts);
                    clusters.Add(newCluster);
                    clusterId++;
                }
            }

            return clusters;
        }

        private void ExpandCluster(Point point, List<Point> neighborPts, List<Point> cluster, int clusterId, List<Point> points, double eps, int minPts)
        {
            point.ClusterId = clusterId;
            cluster.Add(point);
            int i = 0;
            while (i < neighborPts.Count)
            {
                Point currentPoint = neighborPts[i];
                if (!currentPoint.Visited)
                {
                    currentPoint.Visited = true;
                    List<Point> currentNeighbors = GetNeighbors(currentPoint, points, eps);
                    if (currentNeighbors.Count >= minPts)
                    {
                        foreach (Point q in currentNeighbors)
                        {
                            if (!neighborPts.Contains(q))
                            {
                                neighborPts.Add(q);
                            }
                        }
                    }
                }

                if (currentPoint.ClusterId == 0)
                {
                    currentPoint.ClusterId = clusterId;
                    cluster.Add(currentPoint);
                }

                i++;
            }
        }

        private List<Point> GetNeighbors(Point point, List<Point> points, double eps)
        {
            List<Point> neighbors = new List<Point>();
            foreach (Point potentialNeighbor in points)
            {
                if (EuclideanDistance(point, potentialNeighbor) <= eps)
                {
                    neighbors.Add(potentialNeighbor);
                }
            }

            return neighbors;
        }

        private double EuclideanDistance(Point a, Point b)
        {
            double sum = 0.0;
            int length = a.Coordinates.Length;
            for (int i = 0; i < length; i++)
            {
                double diff = a.Coordinates[i] - b.Coordinates[i];
                sum += diff * diff;
            }

            return System.Math.Sqrt(sum);
        }
    }
}