using System;
using System.Collections.Generic;

namespace ShortestJobScheduling
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            List<Job> jobs = new List<Job>
            {
                new Job(1, "Job A", 5),
                new Job(2, "Job B", 3),
                new Job(3, "Job C", 8),
                new Job(4, "Job D", 2)
            };
            IScheduler scheduler = new ShortestJobNextScheduler();
            List<Job> scheduledJobs = scheduler.Schedule(jobs);
            Console.WriteLine("Scheduled Job Order:");
            foreach (Job job in scheduledJobs)
            {
                Console.WriteLine("Job Id: " + job.Id + ", Name: " + job.Name + ", Duration: " + job.Duration);
            }
        }
    }
}