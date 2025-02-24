using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeastSlackTime;

namespace LeastSlackTimeTest
{
    [TestClass]
    public class LeastSlackTimeSchedulerTests
    {
        [TestMethod]
        public void TestSchedule_EmptyList_ReturnsEmptyList()
        {
            LeastSlackTimeScheduler scheduler = new LeastSlackTimeScheduler();
            List<ScheduledTask> emptyTasks = new List<ScheduledTask>();
            List<ScheduledTask> scheduled = scheduler.Schedule(emptyTasks);
            Assert.AreEqual(0, scheduled.Count);
        }

        [TestMethod]
        public void TestSchedule_SingleTask_CorrectTimes()
        {
            ScheduledTask task = new ScheduledTask("TaskA", 5, 10);
            List<ScheduledTask> tasks = new List<ScheduledTask>
            {
                task
            };
            LeastSlackTimeScheduler scheduler = new LeastSlackTimeScheduler();
            List<ScheduledTask> scheduled = scheduler.Schedule(tasks);
            Assert.AreEqual(1, scheduled.Count);
            ScheduledTask result = scheduled[0];
            Assert.AreEqual(0, result.StartTime);
            Assert.AreEqual(5, result.CompletionTime);
        }

        [TestMethod]
        public void TestSchedule_MultipleTasks_OrderAndTimes()
        {
            // Task slack values: Task1 -> 10-3=7, Task2 -> 7-2=5, Task3 -> 5-1=4.
            // Expected order: Task3, Task2, Task1.
            ScheduledTask task1 = new ScheduledTask("Task1", 3, 10);
            ScheduledTask task2 = new ScheduledTask("Task2", 2, 7);
            ScheduledTask task3 = new ScheduledTask("Task3", 1, 5);
            List<ScheduledTask> tasks = new List<ScheduledTask>
            {
                task1,
                task2,
                task3
            };
            LeastSlackTimeScheduler scheduler = new LeastSlackTimeScheduler();
            List<ScheduledTask> scheduled = scheduler.Schedule(tasks);
            Assert.AreEqual("Task3", scheduled[0].Name);
            Assert.AreEqual("Task2", scheduled[1].Name);
            Assert.AreEqual("Task1", scheduled[2].Name);
            int expectedStart = 0;
            foreach (ScheduledTask task in scheduled)
            {
                Assert.AreEqual(expectedStart, task.StartTime);
                expectedStart += task.ProcessingTime;
                Assert.AreEqual(task.StartTime + task.ProcessingTime, task.CompletionTime);
            }
        }

        [TestMethod]
        public void TestSchedule_LargeRandomDataSet_CorrectOrderAndCumulativeTimes()
        {
            const int numberOfTasks = 100;
            List<ScheduledTask> tasks = new List<ScheduledTask>();
            Random random = new Random(0);
            for (int i = 0; i < numberOfTasks; i++)
            {
                int processingTime = random.Next(1, 21);
                int slack = random.Next(1, 101);
                int deadline = processingTime + slack;
                tasks.Add(new ScheduledTask("Task" + i.ToString(), processingTime, deadline));
            }

            LeastSlackTimeScheduler scheduler = new LeastSlackTimeScheduler();
            List<ScheduledTask> scheduled = scheduler.Schedule(tasks);
            for (int i = 0; i < scheduled.Count - 1; i++)
            {
                int currentSlack = scheduled[i].Deadline - scheduled[i].ProcessingTime;
                int nextSlack = scheduled[i + 1].Deadline - scheduled[i + 1].ProcessingTime;
                Assert.IsTrue(currentSlack <= nextSlack);
            }

            int currentTime = 0;
            foreach (ScheduledTask task in scheduled)
            {
                Assert.AreEqual(currentTime, task.StartTime);
                currentTime += task.ProcessingTime;
                Assert.AreEqual(task.StartTime + task.ProcessingTime, task.CompletionTime);
            }
        }
    }
}