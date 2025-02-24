using System.Collections.Generic;

namespace CanopyClusteringAlgorithm
{
    public static class CanopyClusterer
    {
        public static List<Canopy> Cluster(List<DataPoint> dataPoints, double thresholdLoose, double thresholdTight)
        {
            List<Canopy> canopies = new List<Canopy>();
            if (dataPoints == null || dataPoints.Count == 0)
            {
                return canopies;
            }

            List<DataPoint> points = new List<DataPoint>(dataPoints);
            while (points.Count > 0)
            {
                DataPoint currentCenter = points[0];
                Canopy canopy = new Canopy(currentCenter);
                List<DataPoint> toRemove = new List<DataPoint>();
                foreach (DataPoint point in points)
                {
                    double distance = DistanceMetrics.EuclideanDistance(currentCenter.Features, point.Features);
                    if (!double.IsNaN(distance))
                    {
                        if (distance < thresholdLoose)
                        {
                            canopy.Members.Add(point);
                        }

                        if (distance < thresholdTight)
                        {
                            toRemove.Add(point);
                        }
                    }
                }

                foreach (DataPoint removePoint in toRemove)
                {
                    points.Remove(removePoint);
                }

                canopies.Add(canopy);
            }

            return canopies;
        }
    }
}