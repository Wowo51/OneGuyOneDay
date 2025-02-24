using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortestJobScheduling;

namespace ShortestJobSchedulingTest
{
    [TestClass]
    public class ShortestJobNextSchedulerTests
    {
        [TestMethod]
        public void Test_Schedule_WithValidJobs()
        {
            List<Job> jobs = new List<Job>
            {
                new Job(1, "Job A", 5),
                new Job(2, "Job B", 3),
                new Job(3, "Job C", 8),
                new Job(4, "Job D", 2)
            };
            ShortestJobNextScheduler scheduler = new ShortestJobNextScheduler();
            List<Job> scheduledJobs = scheduler.Schedule(jobs);
            Assert.AreEqual(4, scheduledJobs.Count);
            Assert.IsTrue(scheduledJobs[0].Duration <= scheduledJobs[1].Duration);
            Assert.IsTrue(scheduledJobs[1].Duration <= scheduledJobs[2].Duration);
            Assert.IsTrue(scheduledJobs[2].Duration <= scheduledJobs[3].Duration);
            Assert.AreEqual(4, scheduledJobs[0].Id);
            Assert.AreEqual(2, scheduledJobs[1].Id);
            Assert.AreEqual(1, scheduledJobs[2].Id);
            Assert.AreEqual(3, scheduledJobs[3].Id);
        }

        [TestMethod]
        public void Test_Schedule_WithEmptyJobList()
        {
            List<Job> jobs = new List<Job>();
            ShortestJobNextScheduler scheduler = new ShortestJobNextScheduler();
            List<Job> scheduledJobs = scheduler.Schedule(jobs);
            Assert.AreEqual(0, scheduledJobs.Count);
        }

        [TestMethod]
        public void Test_Schedule_WithNullJobList()
        {
            ShortestJobNextScheduler scheduler = new ShortestJobNextScheduler();
            List<Job> scheduledJobs = scheduler.Schedule(null);
            Assert.IsNotNull(scheduledJobs);
            Assert.AreEqual(0, scheduledJobs.Count);
        }

        [TestMethod]
        public void Test_Schedule_WithLargeRandomJobList()
        {
            System.Random random = new System.Random(42);
            List<Job> jobs = new List<Job>();
            for (int i = 1; i <= 1000; i++)
            {
                int duration = random.Next(1, 1001);
                jobs.Add(new Job(i, "Job " + i.ToString(), duration));
            }

            ShortestJobNextScheduler scheduler = new ShortestJobNextScheduler();
            List<Job> scheduledJobs = scheduler.Schedule(jobs);
            Assert.AreEqual(1000, scheduledJobs.Count);
            for (int i = 0; i < scheduledJobs.Count - 1; i++)
            {
                Assert.IsTrue(scheduledJobs[i].Duration <= scheduledJobs[i + 1].Duration, "Jobs are not sorted correctly.");
            }
        }

        [TestMethod]
        public void Test_Schedule_WithDuplicateDurations()
        {
            List<Job> jobs = new List<Job>
            {
                new Job(1, "Job A", 5),
                new Job(2, "Job B", 5),
                new Job(3, "Job C", 3),
                new Job(4, "Job D", 3)
            };
            ShortestJobNextScheduler scheduler = new ShortestJobNextScheduler();
            List<Job> scheduledJobs = scheduler.Schedule(jobs);
            Assert.AreEqual(4, scheduledJobs.Count);
            for (int i = 0; i < scheduledJobs.Count - 1; i++)
            {
                Assert.IsTrue(scheduledJobs[i].Duration <= scheduledJobs[i + 1].Duration, "Jobs are not sorted correctly.");
            }

            Assert.IsTrue(scheduledJobs[0].Duration == 3 && scheduledJobs[1].Duration == 3);
            Assert.IsTrue(scheduledJobs[2].Duration == 5 && scheduledJobs[3].Duration == 5);
        }
    }
}