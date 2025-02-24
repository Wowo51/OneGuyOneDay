using System.Collections.Generic;

namespace ChandraTouegConsensus
{
    public class ConsensusManager
    {
        private readonly List<Process> processes;
        public ConsensusManager(List<Process> processes)
        {
            this.processes = processes;
        }

        public int? RunConsensus()
        {
            int maxRounds = 10;
            for (int round = 1; round <= maxRounds; round++)
            {
                Process? coordinator = ElectLeader();
                if (coordinator == null)
                {
                    return null;
                }

                // Phase 1: Coordinator collects current estimates from all alive processes.
                List<int> proposals = PhaseOne();
                // Choose a proposal. If proposals exist, use the first one; otherwise fallback to coordinator's estimate.
                int chosenValue = proposals.Count > 0 ? proposals[0] : coordinator.CurrentEstimate;
                coordinator.CurrentEstimate = chosenValue;
                // Phase 2: Broadcast the chosen value to all alive processes.
                PhaseTwo(chosenValue);
                if (ConsensusReached(chosenValue))
                {
                    Decide(chosenValue);
                    return chosenValue;
                }
            }

            return null;
        }

        private List<int> PhaseOne()
        {
            List<int> proposals = new List<int>();
            foreach (Process proc in processes)
            {
                if (proc.IsAlive)
                {
                    proposals.Add(proc.CurrentEstimate);
                }
            }

            return proposals;
        }

        private void PhaseTwo(int value)
        {
            foreach (Process proc in processes)
            {
                if (proc.IsAlive)
                {
                    proc.CurrentEstimate = value;
                }
            }
        }

        private Process? ElectLeader()
        {
            Process? leader = null;
            foreach (Process proc in processes)
            {
                if (proc.IsAlive)
                {
                    if (leader == null || proc.Id < leader.Id)
                    {
                        leader = proc;
                    }
                }
            }

            return leader;
        }

        private bool ConsensusReached(int proposal)
        {
            foreach (Process proc in processes)
            {
                if (proc.IsAlive && proc.CurrentEstimate != proposal)
                {
                    return false;
                }
            }

            return true;
        }

        private void Decide(int decision)
        {
            foreach (Process proc in processes)
            {
                if (proc.IsAlive)
                {
                    proc.ReceiveDecision(decision);
                }
            }
        }
    }
}