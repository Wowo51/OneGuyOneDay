using System;
using System.Collections.Generic;

namespace KhopcaClusteringAlgorithm
{
    public class Node
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public List<Node> Neighbors { get; set; }
        public bool IsClusterHead { get; set; }
        public int ClusterId { get; set; }

        public Node(int id, int weight, double x, double y)
        {
            this.Id = id;
            this.Weight = weight;
            this.X = x;
            this.Y = y;
            this.Neighbors = new List<Node>();
            this.IsClusterHead = false;
            this.ClusterId = -1;
        }

        public double DistanceTo(Node other)
        {
            double deltaX = this.X - other.X;
            double deltaY = this.Y - other.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        public override string ToString()
        {
            return "Node " + this.Id + ": Weight=" + this.Weight + ", ClusterHead=" + this.IsClusterHead + ", ClusterId=" + this.ClusterId;
        }
    }
}