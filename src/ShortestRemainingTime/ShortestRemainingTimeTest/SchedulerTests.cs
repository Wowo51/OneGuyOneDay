using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortestRemainingTime;

namespace ShortestRemainingTimeTest
{
    [TestClass]
    public class SchedulerTests
    {
        [TestMethod]
        public void Test_NullInput_ReturnsEmptySchedule()
        {
            ShortestRemainingTimeScheduler scheduler = new ShortestRemainingTimeScheduler();
            List<ShortestRemainingTimeScheduler.ScheduleEntry> schedule = scheduler.GetSchedule(null);
            Assert.IsNotNull(schedule);
            Assert.AreEqual(0, schedule.Count);
        }

        [TestMethod]
        public void Test_EmptyInput_ReturnsEmptySchedule()
        {
            ShortestRemainingTimeScheduler scheduler = new ShortestRemainingTimeScheduler();
            List<Process> processes = new List<Process>();
            List<ShortestRemainingTimeScheduler.ScheduleEntry> schedule = scheduler.GetSchedule(processes);
            Assert.IsNotNull(schedule);
            Assert.AreEqual(0, schedule.Count);
        }

        [TestMethod]
        public void Test_SimpleSchedule()
        {
            // Two processes: Process1 with arrival=0, burst=5; Process2 with arrival=2, burst=3.
            Process process1 = new Process(1, 0, 5);
            Process process2 = new Process(2, 2, 3);
            List<Process> processes = new List<Process>
            {
                process1,
                process2
            };
            ShortestRemainingTimeScheduler scheduler = new ShortestRemainingTimeScheduler();
            List<ShortestRemainingTimeScheduler.ScheduleEntry> schedule = scheduler.GetSchedule(processes);
            // Expected: Process1 runs continuously from time 0 to 5, then Process2 runs from 5 to 8.
            Assert.AreEqual(2, schedule.Count);
            ShortestRemainingTimeScheduler.ScheduleEntry entry1 = schedule[0];
            ShortestRemainingTimeScheduler.ScheduleEntry entry2 = schedule[1];
            Assert.AreEqual(1, entry1.ProcessId);
            Assert.AreEqual(0, entry1.StartTime);
            Assert.AreEqual(5, entry1.EndTime);
            Assert.AreEqual(2, entry2.ProcessId);
            Assert.AreEqual(5, entry2.StartTime);
            Assert.AreEqual(8, entry2.EndTime);
        }

        [TestMethod]
        public void Test_PreemptiveSchedule()
        {
            // Test scenario to force preemption.
            // Process1: arrival=0, burst=6
            // Process2: arrival=2, burst=2
            // Process3: arrival=3, burst=1
            // Expected:
            // Process1 runs from 0 to 2,
            // Process2 runs from 2 to 4,
            // Process3 runs from 4 to 5,
            // Process1 resumes from 5 to 9.
            Process process1 = new Process(1, 0, 6);
            Process process2 = new Process(2, 2, 2);
            Process process3 = new Process(3, 3, 1);
            List<Process> processes = new List<Process>
            {
                process1,
                process2,
                process3
            };
            ShortestRemainingTimeScheduler scheduler = new ShortestRemainingTimeScheduler();
            List<ShortestRemainingTimeScheduler.ScheduleEntry> schedule = scheduler.GetSchedule(processes);
            Assert.AreEqual(4, schedule.Count);
            ShortestRemainingTimeScheduler.ScheduleEntry entry1 = schedule[0];
            ShortestRemainingTimeScheduler.ScheduleEntry entry2 = schedule[1];
            ShortestRemainingTimeScheduler.ScheduleEntry entry3 = schedule[2];
            ShortestRemainingTimeScheduler.ScheduleEntry entry4 = schedule[3];
            Assert.AreEqual(1, entry1.ProcessId);
            Assert.AreEqual(0, entry1.StartTime);
            Assert.AreEqual(2, entry1.EndTime);
            Assert.AreEqual(2, entry2.ProcessId);
            Assert.AreEqual(2, entry2.StartTime);
            Assert.AreEqual(4, entry2.EndTime);
            Assert.AreEqual(3, entry3.ProcessId);
            Assert.AreEqual(4, entry3.StartTime);
            Assert.AreEqual(5, entry3.EndTime);
            Assert.AreEqual(1, entry4.ProcessId);
            Assert.AreEqual(5, entry4.StartTime);
            Assert.AreEqual(9, entry4.EndTime);
        }

        [TestMethod]
        public void Test_LargeDataset()
        {
            // Generate a large synthetic dataset to thoroughly test the scheduler.
            List<Process> processes = new List<Process>();
            int numberOfProcesses = 1000;
            Random random = new Random(42);
            for (int i = 1; i <= numberOfProcesses; i++)
            {
                int arrival = random.Next(0, 100);
                int burst = random.Next(1, 20);
                processes.Add(new Process(i, arrival, burst));
            }

            ShortestRemainingTimeScheduler scheduler = new ShortestRemainingTimeScheduler();
            List<ShortestRemainingTimeScheduler.ScheduleEntry> schedule = scheduler.GetSchedule(processes);
            // Verify that the total executed time for each process equals its burst time.
            Dictionary<int, int> originalBurstTimes = new Dictionary<int, int>();
            foreach (Process process in processes)
            {
                if (!originalBurstTimes.ContainsKey(process.ProcessId))
                {
                    originalBurstTimes[process.ProcessId] = process.BurstTime;
                }
            }

            Dictionary<int, int> executedTimes = new Dictionary<int, int>();
            foreach (ShortestRemainingTimeScheduler.ScheduleEntry entry in schedule)
            {
                int duration = entry.EndTime - entry.StartTime;
                if (!executedTimes.ContainsKey(entry.ProcessId))
                {
                    executedTimes[entry.ProcessId] = 0;
                }

                executedTimes[entry.ProcessId] += duration;
            }

            foreach (KeyValuePair<int, int> kvp in originalBurstTimes)
            {
                int executed = executedTimes.ContainsKey(kvp.Key) ? executedTimes[kvp.Key] : 0;
                Assert.AreEqual(kvp.Value, executed);
            }
        }
    }
}