using System;
using System.Collections.Generic;
using System.Linq;

namespace LeastSlackTime
{
    public class LeastSlackTimeScheduler
    {
        public List<ScheduledTask> Schedule(List<ScheduledTask> tasks)
        {
            int currentTime = 0;
            List<ScheduledTask> scheduledTasks = new List<ScheduledTask>();
            // Sort tasks by their static slack (Deadline - ProcessingTime)
            List<ScheduledTask> sortedTasks = tasks.OrderBy(task => task.Deadline - task.ProcessingTime).ToList();
            foreach (ScheduledTask task in sortedTasks)
            {
                task.StartTime = currentTime;
                task.CompletionTime = currentTime + task.ProcessingTime;
                currentTime += task.ProcessingTime;
                scheduledTasks.Add(task);
            }

            return scheduledTasks;
        }
    }
}