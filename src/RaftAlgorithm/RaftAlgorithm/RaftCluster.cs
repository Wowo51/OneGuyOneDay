using System;
using System.Collections.Generic;

namespace RaftAlgorithm
{
    public class RaftCluster
    {
        private List<RaftNode> _nodes;
        public RaftCluster()
        {
            _nodes = new List<RaftNode>();
        }

        public void AddNode(RaftNode node)
        {
            _nodes.Add(node);
        }

        public IList<RaftNode> Nodes
        {
            get
            {
                return _nodes.AsReadOnly();
            }
        }

        public void StartElection()
        {
            if (_nodes.Count == 0)
            {
                return;
            }

            // Choose the first node as candidate and let it vote for itself.
            RaftNode candidate = _nodes[0];
            candidate.StartElection();
            int votes = 1;
            for (int i = 1; i < _nodes.Count; i++)
            {
                bool voteGranted = _nodes[i].RequestVote(candidate.CurrentTerm, candidate.NodeId);
                if (voteGranted)
                {
                    votes++;
                }
            }

            int majority = (_nodes.Count / 2) + 1;
            if (votes >= majority)
            {
                candidate.State = RaftState.Leader;
            }
            else
            {
                candidate.State = RaftState.Follower;
                candidate.VotedFor = null;
            }
        }

        public RaftNode GetLeader()
        {
            foreach (RaftNode candidate in _nodes)
            {
                if (candidate.State == RaftState.Leader)
                {
                    return candidate;
                }
            }

            return _nodes[0];
        }
    }
}