using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSC = SemiSpaceCollector.SemiSpaceCollector;

namespace SemiSpaceCollectorTest
{
    [TestClass]
    public class SemiSpaceCollectorTests
    {
        [TestMethod]
        public void TestInvalidCapacity()
        {
            SSC collector = new SSC(-5);
            Assert.AreEqual(1, collector.Capacity);
        }

        [TestMethod]
        public void TestSingleAllocation()
        {
            SSC collector = new SSC(5);
            object? allocated = collector.Allocate("test");
            Assert.AreEqual("test", allocated);
            Assert.AreEqual(1, collector.ActiveSpaceAllocatedCount);
            Assert.IsTrue(collector.IsUsingFromSpace);
        }

        [TestMethod]
        public void TestFullAllocationAndCollection()
        {
            SSC collector = new SSC(3);
            object? first = collector.Allocate("a");
            object? second = collector.Allocate("b");
            object? third = collector.Allocate("c");
            Assert.AreEqual(3, collector.ActiveSpaceAllocatedCount);
            Assert.IsTrue(collector.IsUsingFromSpace);
            object? fourth = collector.Allocate("d");
            Assert.IsNull(fourth);
            Assert.IsFalse(collector.IsUsingFromSpace);
            Assert.AreEqual(3, collector.ActiveSpaceAllocatedCount);
        }

        [TestMethod]
        public void TestManualCollect()
        {
            SSC collector = new SSC(5);
            object? first = collector.Allocate("first");
            Assert.IsTrue(collector.IsUsingFromSpace);
            Assert.AreEqual(1, collector.ActiveSpaceAllocatedCount);
            collector.Collect();
            Assert.IsFalse(collector.IsUsingFromSpace);
            Assert.AreEqual(1, collector.ActiveSpaceAllocatedCount);
            object? second = collector.Allocate("second");
            Assert.AreEqual("second", second);
            Assert.AreEqual(2, collector.ActiveSpaceAllocatedCount);
            collector.Collect();
            Assert.IsTrue(collector.IsUsingFromSpace);
            Assert.AreEqual(2, collector.ActiveSpaceAllocatedCount);
        }

        [TestMethod]
        public void TestLargeNumberAllocation()
        {
            int capacity = 1000;
            SSC collector = new SSC(capacity);
            for (int i = 0; i < capacity; i++)
            {
                object? allocated = collector.Allocate("item" + i);
                Assert.IsNotNull(allocated);
            }

            Assert.AreEqual(capacity, collector.ActiveSpaceAllocatedCount);
            object? extra = collector.Allocate("extra");
            Assert.IsNull(extra);
        }

        [TestMethod]
        public void TestAllocateNull()
        {
            SSC collector = new SSC(4);
            object? allocated = collector.Allocate(null);
            Assert.IsNull(allocated);
            Assert.AreEqual(1, collector.ActiveSpaceAllocatedCount);
            collector.Collect();
            Assert.AreEqual(0, collector.ActiveSpaceAllocatedCount);
        }
    }
}