using System.Collections.Generic;

namespace WACAClusteringAlgorithm
{
    public class Cluster
    {
        public int ClusterId { get; set; }
        public List<Node> Nodes { get; set; }

        public Cluster(int clusterId)
        {
            ClusterId = clusterId;
            Nodes = new List<Node>();
        }
    }
}