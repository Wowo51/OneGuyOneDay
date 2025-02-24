using System;
using System.Collections.Generic;

namespace HuangAlgorithm
{
    public class HuangTerminationDetector
    {
        public List<DistributedProcess> Processes;
        public HuangTerminationDetector()
        {
            Processes = new List<DistributedProcess>();
        }

        public void AddProcess(DistributedProcess process)
        {
            Processes.Add(process);
        }

        public bool CheckTermination()
        {
            int totalDifference = 0;
            bool allPassive = true;
            foreach (DistributedProcess process in Processes)
            {
                totalDifference += (process.SentMessages - process.ReceivedMessages);
                if (process.IsActive)
                {
                    allPassive = false;
                }
            }

            return (allPassive && (totalDifference == 0));
        }

        public void RunSimulation()
        {
            if (Processes.Count != 3)
            {
                return;
            }

            DistributedProcess P1 = Processes[0];
            DistributedProcess P2 = Processes[1];
            DistributedProcess P3 = Processes[2];
            P1.IsActive = true;
            P1.SendMessage(P2);
            P1.CompleteWork();
            P2.SendMessage(P3);
            P2.CompleteWork();
            P3.CompleteWork();
        }
    }
}