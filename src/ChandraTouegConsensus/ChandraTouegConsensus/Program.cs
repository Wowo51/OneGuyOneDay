using System;
using System.Collections.Generic;

namespace ChandraTouegConsensus
{
    public static class Program
    {
        public static void Main()
        {
            List<Process> processes = new List<Process>
            {
                new Process(1, 3, true),
                new Process(2, 1, true),
                new Process(3, 2, false),
                new Process(4, 4, true)
            };
            ConsensusManager consensusManager = new ConsensusManager(processes);
            int? consensus = consensusManager.RunConsensus();
            if (consensus.HasValue)
            {
                Console.WriteLine("Consensus reached: " + consensus.Value);
            }
            else
            {
                Console.WriteLine("No live processes, consensus failed.");
            }
        }
    }
}