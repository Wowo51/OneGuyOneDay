using System;
using System.Collections.Generic;

namespace ListScheduling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Job> jobs = new List<Job>
            {
                new Job("Job1", 3),
                new Job("Job2", 2),
                new Job("Job3", 1),
                new Job("Job4", 4)
            };
            int machineCount = 2;
            List<ScheduledTask> schedule = Scheduler.ListScheduling(jobs, machineCount);
            Console.WriteLine("List Scheduling Results:");
            foreach (ScheduledTask scheduled in schedule)
            {
                Console.WriteLine(scheduled);
            }
        }
    }
}