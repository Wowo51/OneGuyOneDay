using System;
using System.Collections.Generic;

namespace ShortestRemainingTime
{
    public class ShortestRemainingTimeScheduler
    {
        public class ScheduleEntry
        {
            public int ProcessId { get; set; }
            public int StartTime { get; set; }
            public int EndTime { get; set; }
        }

        public List<ScheduleEntry> GetSchedule(List<Process> processes)
        {
            if (processes == null)
            {
                return new List<ScheduleEntry>();
            }

            List<Process> localProcesses = new List<Process>(processes);
            List<ScheduleEntry> schedule = new List<ScheduleEntry>();
            int time = 0;
            int completed = 0;
            int n = localProcesses.Count;
            ScheduleEntry? currentEntry = null;
            while (completed < n)
            {
                Process? selectedProcess = null;
                foreach (Process process in localProcesses)
                {
                    if (process.ArrivalTime <= time && process.RemainingTime > 0)
                    {
                        if (selectedProcess == null || process.RemainingTime < selectedProcess.RemainingTime)
                        {
                            selectedProcess = process;
                        }
                    }
                }

                if (selectedProcess == null)
                {
                    time++;
                    continue;
                }

                if (currentEntry == null || currentEntry.ProcessId != selectedProcess.ProcessId)
                {
                    if (currentEntry != null)
                    {
                        schedule.Add(currentEntry);
                    }

                    currentEntry = new ScheduleEntry
                    {
                        ProcessId = selectedProcess.ProcessId,
                        StartTime = time,
                        EndTime = time
                    };
                }

                selectedProcess.RemainingTime--;
                time++;
                currentEntry.EndTime = time;
                if (selectedProcess.RemainingTime == 0)
                {
                    completed++;
                }
            }

            if (currentEntry != null)
            {
                schedule.Add(currentEntry);
            }

            return schedule;
        }
    }
}