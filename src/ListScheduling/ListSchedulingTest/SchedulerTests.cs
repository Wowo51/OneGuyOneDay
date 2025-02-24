using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ListScheduling;

namespace ListSchedulingTest
{
    [TestClass]
    public class SchedulerTests
    {
        [TestMethod]
        public void TestEmptyJobsList()
        {
            List<Job> jobs = new List<Job>();
            List<ScheduledTask> result = Scheduler.ListScheduling(jobs, 2);
            Assert.AreEqual(0, result.Count, "Result should be empty when no jobs are provided.");
        }

        [TestMethod]
        public void TestNullJobs()
        {
            List<ScheduledTask> result = Scheduler.ListScheduling(null, 2);
            Assert.AreEqual(0, result.Count, "Result should be empty when jobs list is null.");
        }

        [TestMethod]
        public void TestSingleJobWithOneMachine()
        {
            Job job = new Job("Job1", 5);
            List<Job> jobs = new List<Job>
            {
                job
            };
            List<ScheduledTask> result = Scheduler.ListScheduling(jobs, 1);
            Assert.AreEqual(1, result.Count, "Result should contain one scheduled task.");
            ScheduledTask scheduled = result[0];
            Assert.AreEqual("Job1", scheduled.JobId, "Job Id should match.");
            Assert.AreEqual(0, scheduled.MachineId, "Only machine id should be 0.");
            Assert.AreEqual(0, scheduled.StartTime, "Start time should be 0.");
            Assert.AreEqual(5, scheduled.FinishTime, "Finish time should equal processing time.");
        }

        [TestMethod]
        public void TestMultipleJobsWithMultipleMachines()
        {
            List<Job> jobs = new List<Job>
            {
                new Job("A", 3),
                new Job("B", 2),
                new Job("C", 1),
                new Job("D", 4)
            };
            List<ScheduledTask> result = Scheduler.ListScheduling(jobs, 2);
            Assert.AreEqual(4, result.Count, "Should schedule all jobs.");
            // Expected scheduling:
            // A: machine 0, start 0, finish 3
            // B: machine 1, start 0, finish 2
            // C: machine 1, start 2, finish 3
            // D: machine 0, start 3, finish 7
            ScheduledTask taskA = result[0];
            Assert.AreEqual("A", taskA.JobId);
            Assert.AreEqual(0, taskA.MachineId);
            Assert.AreEqual(0, taskA.StartTime);
            Assert.AreEqual(3, taskA.FinishTime);
            ScheduledTask taskB = result[1];
            Assert.AreEqual("B", taskB.JobId);
            Assert.AreEqual(1, taskB.MachineId);
            Assert.AreEqual(0, taskB.StartTime);
            Assert.AreEqual(2, taskB.FinishTime);
            ScheduledTask taskC = result[2];
            Assert.AreEqual("C", taskC.JobId);
            Assert.AreEqual(1, taskC.MachineId);
            Assert.AreEqual(2, taskC.StartTime);
            Assert.AreEqual(3, taskC.FinishTime);
            ScheduledTask taskD = result[3];
            Assert.AreEqual("D", taskD.JobId);
            Assert.AreEqual(0, taskD.MachineId);
            Assert.AreEqual(3, taskD.StartTime);
            Assert.AreEqual(7, taskD.FinishTime);
        }

        [TestMethod]
        public void TestSyntheticLargeDataSet()
        {
            int jobCount = 100;
            int machineCount = 5;
            List<Job> jobs = new List<Job>();
            System.Random random = new System.Random(42);
            for (int i = 0; i < jobCount; i++)
            {
                int processingTime = random.Next(1, 11);
                jobs.Add(new Job("Job" + i.ToString(), processingTime));
            }

            List<ScheduledTask> result = Scheduler.ListScheduling(jobs, machineCount);
            Assert.AreEqual(jobCount, result.Count, "Every job should be scheduled.");
            Dictionary<string, int> jobProcessingTimes = new Dictionary<string, int>();
            foreach (Job currentJob in jobs)
            {
                jobProcessingTimes.Add(currentJob.Id, currentJob.ProcessingTime);
            }

            foreach (ScheduledTask task in result)
            {
                int processingTime;
                Assert.IsTrue(jobProcessingTimes.TryGetValue(task.JobId, out processingTime), "Job id in scheduled task should exist in input jobs.");
                Assert.AreEqual(task.StartTime + processingTime, task.FinishTime, "Finish time should equal start time plus processing time.");
            }
        }
    }
}