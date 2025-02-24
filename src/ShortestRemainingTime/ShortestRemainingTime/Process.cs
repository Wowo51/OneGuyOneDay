using System;

namespace ShortestRemainingTime
{
    public class Process
    {
        public int ProcessId { get; set; }
        public int ArrivalTime { get; set; }
        public int BurstTime { get; set; }
        public int RemainingTime { get; set; }

        public Process(int processId, int arrivalTime, int burstTime)
        {
            this.ProcessId = processId;
            this.ArrivalTime = arrivalTime;
            this.BurstTime = burstTime;
            this.RemainingTime = burstTime;
        }
    }
}