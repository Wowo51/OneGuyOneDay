using System;
using System.Collections.Generic;

namespace KhopcaClusteringAlgorithm
{
    public class NetworkSimulator
    {
        public List<Node> Nodes { get; private set; }
        public double CommunicationRange { get; set; }

        private Random _random;
        public NetworkSimulator(int numNodes, double commRange)
        {
            CommunicationRange = commRange;
            Nodes = new List<Node>();
            _random = new Random();
            for (int i = 0; i < numNodes; i++)
            {
                double x = _random.NextDouble() * 100.0;
                double y = _random.NextDouble() * 100.0;
                int weight = _random.Next(1, 101);
                Node node = new Node(i, weight, x, y);
                Nodes.Add(node);
            }
        }

        public void UpdateNeighbors()
        {
            foreach (Node node in Nodes)
            {
                node.Neighbors.Clear();
                foreach (Node other in Nodes)
                {
                    if (other.Id != node.Id && node.DistanceTo(other) <= CommunicationRange)
                    {
                        node.Neighbors.Add(other);
                    }
                }
            }
        }
    }
}