using System;
using System.Collections.Generic;

namespace KhopcaClusteringAlgorithm
{
    public class KHOPCAAlgorithm
    {
        private List<Node> _nodes;
        public int K { get; set; }

        public KHOPCAAlgorithm(List<Node> nodes)
        {
            _nodes = nodes;
            K = 3;
        }

        public void RunIteration()
        {
            // Phase 1: Immediate neighbor update
            foreach (Node node in _nodes)
            {
                int localMaxWeight = node.Weight;
                int localMaxId = node.Id;
                node.IsClusterHead = false;
                node.ClusterId = node.Id;
                foreach (Node neighbor in node.Neighbors)
                {
                    if (neighbor.Weight > localMaxWeight)
                    {
                        localMaxWeight = neighbor.Weight;
                        localMaxId = neighbor.Id;
                    }
                }

                if (node.Weight >= localMaxWeight)
                {
                    node.IsClusterHead = true;
                    node.ClusterId = node.Id;
                }
                else
                {
                    node.IsClusterHead = false;
                    node.ClusterId = localMaxId;
                }
            }

            // Build a lookup dictionary for nodes by their Id
            Dictionary<int, Node> nodeMap = new Dictionary<int, Node>();
            foreach (Node node in _nodes)
            {
                nodeMap.Add(node.Id, node);
            }

            // Phase 2: Propagate the best cluster head candidate over K hops
            for (int hop = 0; hop < K; hop++)
            {
                foreach (Node node in _nodes)
                {
                    int currentCandidateId = node.ClusterId;
                    int currentCandidateWeight = nodeMap[currentCandidateId].Weight;
                    foreach (Node neighbor in node.Neighbors)
                    {
                        int neighborCandidateId = neighbor.ClusterId;
                        int neighborCandidateWeight = nodeMap[neighborCandidateId].Weight;
                        if (neighborCandidateWeight > currentCandidateWeight)
                        {
                            currentCandidateWeight = neighborCandidateWeight;
                            currentCandidateId = neighborCandidateId;
                        }
                    }

                    node.ClusterId = currentCandidateId;
                    node.IsClusterHead = (node.Id == currentCandidateId);
                }
            }
        }
    }
}