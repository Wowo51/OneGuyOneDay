using System;

namespace LeastSlackTime
{
    public class ScheduledTask
    {
        public string Name { get; set; }
        public int ProcessingTime { get; set; }
        public int Deadline { get; set; }
        public int StartTime { get; set; }
        public int CompletionTime { get; set; }

        public int Slack
        {
            get
            {
                return Deadline - ProcessingTime;
            }
        }

        public ScheduledTask(string name, int processingTime, int deadline)
        {
            Name = name;
            ProcessingTime = processingTime;
            Deadline = deadline;
        }
    }
}