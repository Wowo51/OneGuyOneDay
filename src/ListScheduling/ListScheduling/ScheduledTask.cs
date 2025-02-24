namespace ListScheduling
{
    public class ScheduledTask
    {
        public string JobId { get; set; }
        public int MachineId { get; set; }
        public int StartTime { get; set; }
        public int FinishTime { get; set; }

        public ScheduledTask(string jobId, int machineId, int startTime, int finishTime)
        {
            JobId = jobId;
            MachineId = machineId;
            StartTime = startTime;
            FinishTime = finishTime;
        }

        public override string ToString()
        {
            return string.Format("Job {0} scheduled on machine {1} from {2} to {3}", JobId, MachineId, StartTime, FinishTime);
        }
    }
}