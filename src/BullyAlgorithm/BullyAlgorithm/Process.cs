using System;

namespace BullyAlgorithm
{
    public class Process
    {
        public int ProcessId { get; set; }
        public bool IsAlive { get; set; }
        public bool IsCoordinator { get; set; }

        public Process(int processId)
        {
            ProcessId = processId;
            IsAlive = true;
            IsCoordinator = false;
        }

        public Process? StartElection(BullyElection election)
        {
            return election.ElectCoordinator(this);
        }
    }
}