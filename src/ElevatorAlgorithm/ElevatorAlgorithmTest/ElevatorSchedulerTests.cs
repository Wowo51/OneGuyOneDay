using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElevatorAlgorithm;

namespace ElevatorAlgorithmTest
{
    [TestClass]
    public class ElevatorSchedulerTests
    {
        [TestMethod]
        public void TestSchedule_NullRequests_ReturnsEmptyList()
        {
            ElevatorScheduler scheduler = new ElevatorScheduler();
            List<int> result = scheduler.Schedule(50, null, true, 100);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestSchedule_MovingUp_ReturnsCorrectOrder()
        {
            ElevatorScheduler scheduler = new ElevatorScheduler();
            List<int> requests = new List<int>
            {
                10,
                90,
                30,
                70,
                20,
                50
            };
            int initialPosition = 50;
            List<int> expected = new List<int>
            {
                50,
                70,
                90,
                30,
                20,
                10
            };
            List<int> result = scheduler.Schedule(initialPosition, requests, true, 100);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSchedule_MovingDown_ReturnsCorrectOrder()
        {
            ElevatorScheduler scheduler = new ElevatorScheduler();
            List<int> requests = new List<int>
            {
                10,
                90,
                30,
                70,
                20,
                50
            };
            int initialPosition = 50;
            List<int> expected = new List<int>
            {
                50,
                30,
                20,
                10,
                70,
                90
            };
            List<int> result = scheduler.Schedule(initialPosition, requests, false, 100);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSchedule_LargeDataset_MovingUp_And_MovingDown()
        {
            ElevatorScheduler scheduler = new ElevatorScheduler();
            const int dataSize = 1000;
            const int initialPosition = 500;
            List<int> requests = new List<int>();
            Random random = new Random(42);
            for (int i = 0; i < dataSize; i++)
            {
                requests.Add(random.Next(1, 1001));
            }

            List<int> sortedRequests = new List<int>(requests);
            sortedRequests.Sort();
            List<int> expectedUp = new List<int>();
            List<int> expectedDown = new List<int>();
            foreach (int x in sortedRequests)
            {
                if (x >= initialPosition)
                {
                    expectedUp.Add(x);
                }
                else
                {
                    expectedDown.Add(x);
                }
            }

            expectedDown.Reverse();
            List<int> expectedMovingUp = new List<int>();
            expectedMovingUp.AddRange(expectedUp);
            expectedMovingUp.AddRange(expectedDown);
            List<int> resultUp = scheduler.Schedule(initialPosition, requests, true, 1000);
            CollectionAssert.AreEqual(expectedMovingUp, resultUp);
            List<int> expectedDownList = new List<int>();
            List<int> expectedUpList = new List<int>();
            foreach (int x in sortedRequests)
            {
                if (x <= initialPosition)
                {
                    expectedDownList.Add(x);
                }
                else
                {
                    expectedUpList.Add(x);
                }
            }

            expectedDownList.Reverse();
            List<int> expectedMovingDown = new List<int>();
            expectedMovingDown.AddRange(expectedDownList);
            expectedMovingDown.AddRange(expectedUpList);
            List<int> resultDown = scheduler.Schedule(initialPosition, requests, false, 1000);
            CollectionAssert.AreEqual(expectedMovingDown, resultDown);
        }
    }
}