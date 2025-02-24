using System;
using System.Collections.Generic;

namespace ShortestRemainingTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Process> processes = new List<Process>
            {
                new Process(1, 0, 8),
                new Process(2, 1, 4),
                new Process(3, 2, 9),
                new Process(4, 3, 5)
            };
            ShortestRemainingTimeScheduler scheduler = new ShortestRemainingTimeScheduler();
            List<ShortestRemainingTimeScheduler.ScheduleEntry> schedule = scheduler.GetSchedule(processes);
            Console.WriteLine("Schedule:");
            foreach (ShortestRemainingTimeScheduler.ScheduleEntry entry in schedule)
            {
                Console.WriteLine("Process " + entry.ProcessId + " from " + entry.StartTime + " to " + entry.EndTime);
            }
        }
    }
}