using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntersectionClockSynchronization;

namespace IntersectionClockSynchronizationTest
{
    [TestClass]
    public class IntersectionClockSynchronizationTests
    {
        [TestMethod]
        public void Test_EmptyIntervals()
        {
            List<TimeInterval?> intervals = new List<TimeInterval?>();
            IntersectionSynchronizer synchronizer = new IntersectionSynchronizer();
            TimeInterval? result = synchronizer.ComputeIntersection(intervals);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_SingleInterval()
        {
            List<TimeInterval?> intervals = new List<TimeInterval?>
            {
                new TimeInterval(5.0, 10.0)
            };
            IntersectionSynchronizer synchronizer = new IntersectionSynchronizer();
            TimeInterval? result = synchronizer.ComputeIntersection(intervals);
            Assert.IsNotNull(result);
            Assert.AreEqual(5.0, result!.Start);
            Assert.AreEqual(10.0, result!.End);
        }

        [TestMethod]
        public void Test_TwoOverlappingIntervals()
        {
            List<TimeInterval?> intervals = new List<TimeInterval?>
            {
                new TimeInterval(1.0, 10.0),
                new TimeInterval(3.0, 8.0)
            };
            IntersectionSynchronizer synchronizer = new IntersectionSynchronizer();
            TimeInterval? result = synchronizer.ComputeIntersection(intervals);
            Assert.IsNotNull(result);
            Assert.AreEqual(3.0, result!.Start);
            Assert.AreEqual(8.0, result!.End);
        }

        [TestMethod]
        public void Test_TwoNonOverlappingIntervals()
        {
            List<TimeInterval?> intervals = new List<TimeInterval?>
            {
                new TimeInterval(1.0, 3.0),
                new TimeInterval(4.0, 6.0)
            };
            IntersectionSynchronizer synchronizer = new IntersectionSynchronizer();
            TimeInterval? result = synchronizer.ComputeIntersection(intervals);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_ListWithNullValues()
        {
            List<TimeInterval?> intervals = new List<TimeInterval?>
            {
                new TimeInterval(2.0, 10.0),
                null,
                new TimeInterval(3.0, 8.0)
            };
            IntersectionSynchronizer synchronizer = new IntersectionSynchronizer();
            TimeInterval? result = synchronizer.ComputeIntersection(intervals);
            Assert.IsNotNull(result);
            Assert.AreEqual(3.0, result!.Start);
            Assert.AreEqual(8.0, result!.End);
        }

        [TestMethod]
        public void Test_MultipleOverlappingIntervals()
        {
            List<TimeInterval?> intervals = new List<TimeInterval?>
            {
                new TimeInterval(0, 100),
                new TimeInterval(10, 90),
                new TimeInterval(20, 80),
                new TimeInterval(30, 70),
                new TimeInterval(40, 60)
            };
            IntersectionSynchronizer synchronizer = new IntersectionSynchronizer();
            TimeInterval? result = synchronizer.ComputeIntersection(intervals);
            Assert.IsNotNull(result);
            Assert.AreEqual(40, result!.Start);
            Assert.AreEqual(60, result!.End);
        }

        [TestMethod]
        public void Test_SyntheticLargeDataset()
        {
            List<TimeInterval?> intervals = new List<TimeInterval?>();
            for (int index = 0; index < 50; index++)
            {
                intervals.Add(new TimeInterval(index, 100 - index));
            }

            IntersectionSynchronizer synchronizer = new IntersectionSynchronizer();
            TimeInterval? result = synchronizer.ComputeIntersection(intervals);
            Assert.IsNotNull(result);
            Assert.AreEqual(49, result!.Start);
            Assert.AreEqual(51, result!.End);
        }
    }
}