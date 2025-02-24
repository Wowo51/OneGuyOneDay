using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EarliestDeadlineFirst;

namespace EarliestDeadlineFirstTest
{
    [TestClass]
    public class EdfSchedulerTests
    {
        [TestMethod]
        public void TestAddTask()
        {
            EdfScheduler scheduler = new EdfScheduler();
            ScheduledTask task = new ScheduledTask("Task1", DateTime.Now.AddSeconds(10), () =>
            {
            });
            List<ScheduledTask> tasks = scheduler.GetScheduledTasks();
            Assert.AreEqual(0, tasks.Count);
            scheduler.AddTask(task);
            tasks = scheduler.GetScheduledTasks();
            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("Task1", tasks[0].Name);
        }

        [TestMethod]
        public void TestRemoveTask()
        {
            EdfScheduler scheduler = new EdfScheduler();
            ScheduledTask task1 = new ScheduledTask("Task1", DateTime.Now.AddSeconds(10), () =>
            {
            });
            ScheduledTask task2 = new ScheduledTask("Task2", DateTime.Now.AddSeconds(5), () =>
            {
            });
            scheduler.AddTask(task1);
            scheduler.AddTask(task2);
            scheduler.RemoveTask(task1);
            List<ScheduledTask> tasks = scheduler.GetScheduledTasks();
            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("Task2", tasks[0].Name);
        }

        [TestMethod]
        public void TestGetScheduledTasksSorting()
        {
            DateTime now = DateTime.Now;
            ScheduledTask task1 = new ScheduledTask("Task1", now.AddSeconds(10), () =>
            {
            });
            ScheduledTask task2 = new ScheduledTask("Task2", now.AddSeconds(5), () =>
            {
            });
            ScheduledTask task3 = new ScheduledTask("Task3", now.AddSeconds(15), () =>
            {
            });
            EdfScheduler scheduler = new EdfScheduler();
            scheduler.AddTask(task1);
            scheduler.AddTask(task2);
            scheduler.AddTask(task3);
            List<ScheduledTask> tasks = scheduler.GetScheduledTasks();
            Assert.AreEqual(3, tasks.Count);
            Assert.AreEqual("Task2", tasks[0].Name);
            Assert.AreEqual("Task1", tasks[1].Name);
            Assert.AreEqual("Task3", tasks[2].Name);
        }

        [TestMethod]
        public void TestExecuteAllTasks()
        {
            List<string> executionList = new List<string>();
            EdfScheduler scheduler = new EdfScheduler();
            DateTime now = DateTime.Now;
            ScheduledTask task1 = new ScheduledTask("Task1", now.AddSeconds(3), () =>
            {
                executionList.Add("Task1");
            });
            ScheduledTask task2 = new ScheduledTask("Task2", now.AddSeconds(1), () =>
            {
                executionList.Add("Task2");
            });
            ScheduledTask task3 = new ScheduledTask("Task3", now.AddSeconds(2), () =>
            {
                executionList.Add("Task3");
            });
            scheduler.AddTask(task1);
            scheduler.AddTask(task2);
            scheduler.AddTask(task3);
            scheduler.ExecuteAllTasks();
            Assert.AreEqual(3, executionList.Count);
            Assert.AreEqual("Task2", executionList[0]);
            Assert.AreEqual("Task3", executionList[1]);
            Assert.AreEqual("Task1", executionList[2]);
        }

        [TestMethod]
        public void TestLargeNumberOfTasks()
        {
            EdfScheduler scheduler = new EdfScheduler();
            int numTasks = 1000;
            List<ScheduledTask> addedTasks = new List<ScheduledTask>();
            Random random = new Random(0);
            DateTime baseTime = DateTime.Now;
            for (int i = 0; i < numTasks; i++)
            {
                int offsetSeconds = random.Next(0, 10000);
                DateTime deadline = baseTime.AddSeconds(offsetSeconds);
                ScheduledTask task = new ScheduledTask("Task" + i.ToString(), deadline, () =>
                {
                });
                scheduler.AddTask(task);
                addedTasks.Add(task);
            }

            List<ScheduledTask> sortedTasks = scheduler.GetScheduledTasks();
            Assert.AreEqual(numTasks, sortedTasks.Count);
            for (int i = 1; i < sortedTasks.Count; i++)
            {
                Assert.IsTrue(sortedTasks[i - 1].Deadline <= sortedTasks[i].Deadline);
            }
        }

        [TestMethod]
        public void TestAddNullTaskDoesNothing()
        {
            EdfScheduler scheduler = new EdfScheduler();
            scheduler.AddTask(null);
            List<ScheduledTask> tasks = scheduler.GetScheduledTasks();
            Assert.IsNotNull(tasks);
            Assert.AreEqual(0, tasks.Count);
        }

        [TestMethod]
        public void TestRemoveNullTaskDoesNothing()
        {
            EdfScheduler scheduler = new EdfScheduler();
            scheduler.RemoveTask(null);
            List<ScheduledTask> tasks = scheduler.GetScheduledTasks();
            Assert.IsNotNull(tasks);
        }

        [TestMethod]
        public void TestScheduledTaskNullNameSetsEmpty()
        {
            DateTime now = DateTime.Now;
            ScheduledTask task = new ScheduledTask(null, now, () =>
            {
            });
            Assert.AreEqual("", task.Name);
        }

        [TestMethod]
        public void TestExecuteTaskWithNullActionDoesNotThrow()
        {
            EdfScheduler scheduler = new EdfScheduler();
            ScheduledTask task = new ScheduledTask("TaskWithNullAction", DateTime.Now.AddSeconds(2), null);
            scheduler.AddTask(task);
            scheduler.ExecuteAllTasks();
            Assert.IsTrue(true);
        }
    }
}