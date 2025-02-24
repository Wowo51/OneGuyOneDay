using System.Collections.Generic;

namespace ListScheduling
{
    public static class Scheduler
    {
        public static List<ScheduledTask> ListScheduling(List<Job> jobs, int machineCount)
        {
            List<ScheduledTask> scheduledTasks = new List<ScheduledTask>();
            if (jobs == null)
            {
                return scheduledTasks;
            }

            int[] machineFinishTimes = new int[machineCount];
            for (int i = 0; i < machineCount; i++)
            {
                machineFinishTimes[i] = 0;
            }

            foreach (Job currentJob in jobs)
            {
                int selectedMachine = 0;
                int minFinishTime = machineFinishTimes[0];
                for (int machineId = 1; machineId < machineCount; machineId++)
                {
                    if (machineFinishTimes[machineId] < minFinishTime)
                    {
                        selectedMachine = machineId;
                        minFinishTime = machineFinishTimes[machineId];
                    }
                }

                int startTime = machineFinishTimes[selectedMachine];
                int finishTime = startTime + currentJob.ProcessingTime;
                machineFinishTimes[selectedMachine] = finishTime;
                scheduledTasks.Add(new ScheduledTask(currentJob.Id, selectedMachine, startTime, finishTime));
            }

            return scheduledTasks;
        }
    }
}