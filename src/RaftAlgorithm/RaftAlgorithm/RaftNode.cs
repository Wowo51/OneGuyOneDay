using System;
using System.Collections.Generic;

namespace RaftAlgorithm
{
    public class RaftNode
    {
        public string NodeId { get; private set; }
        public RaftState State { get; set; }
        public int CurrentTerm { get; set; }
        public string? VotedFor { get; set; }
        public List<RaftLogEntry> Log { get; private set; }

        public RaftNode(string nodeId)
        {
            NodeId = nodeId;
            State = RaftState.Follower;
            CurrentTerm = 0;
            VotedFor = null;
            Log = new List<RaftLogEntry>();
        }

        public void StartElection()
        {
            CurrentTerm = CurrentTerm + 1;
            State = RaftState.Candidate;
            VotedFor = NodeId;
        }

        public void AppendEntries(int term, string leaderId, ICollection<RaftLogEntry> entries)
        {
            if (term >= CurrentTerm)
            {
                CurrentTerm = term;
                State = RaftState.Follower;
                VotedFor = leaderId;
                Log.AddRange(entries);
            }
        }

        public void AddLogEntry(RaftLogEntry entry)
        {
            Log.Add(entry);
        }

        public bool RequestVote(int candidateTerm, string candidateNodeId)
        {
            if (candidateTerm > CurrentTerm)
            {
                CurrentTerm = candidateTerm;
                VotedFor = candidateNodeId;
                State = RaftState.Follower;
                return true;
            }
            else if (candidateTerm == CurrentTerm && (VotedFor == null || VotedFor == candidateNodeId))
            {
                VotedFor = candidateNodeId;
                return true;
            }

            return false;
        }
    }
}