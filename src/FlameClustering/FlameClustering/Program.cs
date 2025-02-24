using System;
using System.Collections.Generic;
using FlameClustering.Models;
using FlameClustering.Algorithms;

namespace FlameClustering
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<DataPoint> data = new List<DataPoint>();
            data.Add(new DataPoint(new double[] { 0, 0 }));
            data.Add(new DataPoint(new double[] { 1, 1 }));
            data.Add(new DataPoint(new double[] { 2, 2 }));
            data.Add(new DataPoint(new double[] { 10, 10 }));
            data.Add(new DataPoint(new double[] { 10, 11 }));
            FlameClusteringAlgorithm algorithm = new FlameClusteringAlgorithm();
            List<Cluster> clusters = algorithm.Run(data, 3.0, 1);
            Console.WriteLine("Clusters:");
            foreach (Cluster cluster in clusters)
            {
                Console.WriteLine("Cluster " + cluster.Id + " center: (" + string.Join(", ", cluster.Center.Coordinates) + ")");
                Console.WriteLine("Members:");
                foreach (KeyValuePair<DataPoint, double> entry in cluster.Memberships)
                {
                    Console.WriteLine(" DataPoint (" + string.Join(", ", entry.Key.Coordinates) + ") membership: " + entry.Value);
                }
            }
        }
    }
}