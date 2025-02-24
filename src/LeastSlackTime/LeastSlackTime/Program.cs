using System;
using System.Collections.Generic;

namespace LeastSlackTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<ScheduledTask> tasks = new List<ScheduledTask>
            {
                new ScheduledTask("Task1", 3, 10),
                new ScheduledTask("Task2", 2, 7),
                new ScheduledTask("Task3", 1, 5)
            };
            LeastSlackTimeScheduler scheduler = new LeastSlackTimeScheduler();
            List<ScheduledTask> scheduledTasks = scheduler.Schedule(tasks);
            Console.WriteLine("Scheduled Tasks Order:");
            foreach (ScheduledTask task in scheduledTasks)
            {
                Console.WriteLine("{0}: Start {1}, Complete {2}, Deadline {3}", task.Name, task.StartTime, task.CompletionTime, task.Deadline);
            }
        }
    }
}