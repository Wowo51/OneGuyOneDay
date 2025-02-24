using System;

namespace ChandraTouegConsensus
{
    public class Process
    {
        public int Id { get; }
        public int ProposedValue { get; }
        public bool IsAlive { get; set; }
        public int? DecidedValue { get; private set; }
        // The current estimate used during the consensus rounds.
        public int CurrentEstimate { get; set; }

        public Process(int id, int proposedValue, bool isAlive)
        {
            this.Id = id;
            this.ProposedValue = proposedValue;
            this.IsAlive = isAlive;
            this.DecidedValue = null;
            this.CurrentEstimate = proposedValue;
        }

        public void ReceiveDecision(int value)
        {
            this.DecidedValue = value;
        }
    }
}