using System.Collections.Generic;
using System.Linq;

namespace WACAClusteringAlgorithm
{
    public class WacaClusteringAlgorithm
    {
        public List<Cluster> ComputeClusters(List<Node> nodes, int maxHops)
        {
            List<Cluster> clusters = new List<Cluster>();
            HashSet<int> visited = new HashSet<int>();
            int clusterId = 0;
            foreach (Node node in nodes)
            {
                if (!visited.Contains(node.Id))
                {
                    Cluster cluster = new Cluster(clusterId++);
                    Queue<(Node, int)> queue = new Queue<(Node, int)>();
                    queue.Enqueue((node, 0));
                    visited.Add(node.Id);
                    while (queue.Count > 0)
                    {
                        (Node current, int hop) = queue.Dequeue();
                        cluster.Nodes.Add(current);
                        if (hop >= maxHops)
                        {
                            continue;
                        }

                        List<Node> neighbors = nodes.Where(n => current.Neighbors.Contains(n.Id) && !visited.Contains(n.Id)).ToList();
                        foreach (Node neighbor in neighbors)
                        {
                            visited.Add(neighbor.Id);
                            queue.Enqueue((neighbor, hop + 1));
                        }
                    }

                    clusters.Add(cluster);
                }
            }

            return clusters;
        }

        public List<List<Cluster>> ComputeHierarchicalClusters(List<Node> nodes, List<int> hopThresholds)
        {
            List<List<Cluster>> hierarchicalClusters = new List<List<Cluster>>();
            foreach (int threshold in hopThresholds)
            {
                List<Cluster> clusters = ComputeClusters(nodes, threshold);
                hierarchicalClusters.Add(clusters);
            }

            return hierarchicalClusters;
        }
    }
}