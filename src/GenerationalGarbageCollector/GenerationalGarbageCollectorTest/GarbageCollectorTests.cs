using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenerationalGarbageCollector;
using System;
using System.Collections.Generic;

namespace GenerationalGarbageCollectorTest
{
    [TestClass]
    public class GarbageCollectorTests
    {
        [TestMethod]
        public void TestAllocateObject()
        {
            GenerationalGarbageCollector.GarbageCollector gc = new GenerationalGarbageCollector.GarbageCollector();
            int initialCount = gc.Generations[0].Objects.Count;
            GCObject obj = new GCObject("TestObject");
            gc.AllocateObject(obj);
            Assert.AreEqual(initialCount + 1, gc.Generations[0].Objects.Count, "Allocation failed for Gen0.");
        }

        [TestMethod]
        public void TestCollectGen0_Filtering()
        {
            GarbageCollector gc = new GarbageCollector();
            GCObject alive = new GCObject("Alive");
            GCObject dead = new GCObject("Dead");
            dead.IsReachable = false;
            gc.AllocateObject(alive);
            gc.AllocateObject(dead);
            gc.CollectGen0(o => o.IsReachable);
            Assert.AreEqual(0, gc.Generations[0].Objects.Count, "Gen0 should be empty after collection.");
            Assert.AreEqual(1, gc.Generations[1].Objects.Count, "Only alive object should be promoted to Gen1.");
        }

        [TestMethod]
        public void TestCollectGen1_Filtering()
        {
            GarbageCollector gc = new GarbageCollector();
            GCObject alive = new GCObject("Alive");
            GCObject dead = new GCObject("Dead");
            dead.IsReachable = false;
            gc.Generations[1].Objects.Add(alive);
            gc.Generations[1].Objects.Add(dead);
            gc.CollectGen1(o => o.IsReachable);
            Assert.AreEqual(0, gc.Generations[1].Objects.Count, "Gen1 should be empty after collection.");
            Assert.AreEqual(1, gc.Generations[2].Objects.Count, "Only alive object should be promoted to Gen2.");
        }

        [TestMethod]
        public void TestCollectGen2_Filtering()
        {
            GarbageCollector gc = new GarbageCollector();
            GCObject alive1 = new GCObject("Alive1");
            GCObject alive2 = new GCObject("Alive2");
            GCObject dead = new GCObject("Dead");
            dead.IsReachable = false;
            gc.Generations[2].Objects.Add(alive1);
            gc.Generations[2].Objects.Add(dead);
            gc.Generations[2].Objects.Add(alive2);
            gc.CollectGen2(o => o.IsReachable);
            Assert.AreEqual(2, gc.Generations[2].Objects.Count, "Gen2 should contain only alive objects after collection.");
        }

        [TestMethod]
        public void TestCollect_All_WithFiltering()
        {
            GarbageCollector gc = new GarbageCollector();
            GCObject obj1 = new GCObject("Alive1");
            GCObject obj2 = new GCObject("Alive2");
            GCObject obj3 = new GCObject("Dead");
            obj3.IsReachable = false;
            gc.AllocateObject(obj1);
            gc.AllocateObject(obj2);
            gc.AllocateObject(obj3);
            gc.Collect(o => o.IsReachable);
            Assert.AreEqual(0, gc.Generations[0].Objects.Count, "Gen0 should be empty after full collection.");
            Assert.AreEqual(0, gc.Generations[1].Objects.Count, "Gen1 should be empty after full collection.");
            Assert.AreEqual(2, gc.Generations[2].Objects.Count, "Gen2 should contain only alive objects after full collection.");
        }

        [TestMethod]
        public void TestCollect_All_WithNullLambda()
        {
            GarbageCollector gc = new GarbageCollector();
            GCObject obj1 = new GCObject("Obj1");
            GCObject obj2 = new GCObject("Obj2");
            obj2.IsReachable = false;
            gc.AllocateObject(obj1);
            gc.AllocateObject(obj2);
            gc.Collect(null);
            Assert.AreEqual(0, gc.Generations[0].Objects.Count, "Gen0 should be empty when null lambda is used.");
            Assert.AreEqual(0, gc.Generations[1].Objects.Count, "Gen1 should be empty when null lambda is used.");
            Assert.AreEqual(2, gc.Generations[2].Objects.Count, "Gen2 should contain all objects when null lambda is used.");
        }

        [TestMethod]
        public void TestSyntheticLargeDataSet()
        {
            GarbageCollector gc = new GarbageCollector();
            int totalObjects = 1000;
            List<GCObject> objects = new List<GCObject>();
            for (int i = 0; i < totalObjects; i++)
            {
                GCObject obj = new GCObject("Obj" + i);
                if ((i % 3) == 0)
                {
                    obj.IsReachable = false;
                }

                objects.Add(obj);
                gc.AllocateObject(obj);
            }

            gc.Collect(o => o.IsReachable);
            int expectedAlive = 0;
            for (int i = 0; i < totalObjects; i++)
            {
                if ((i % 3) != 0)
                {
                    expectedAlive++;
                }
            }

            Assert.AreEqual(0, gc.Generations[0].Objects.Count, "Gen0 should be empty after large dataset collection.");
            Assert.AreEqual(0, gc.Generations[1].Objects.Count, "Gen1 should be empty after large dataset collection.");
            Assert.AreEqual(expectedAlive, gc.Generations[2].Objects.Count, "Gen2 count mismatch for large dataset.");
        }

        [TestMethod]
        public void TestEmptyCollection()
        {
            GarbageCollector gc = new GarbageCollector();
            gc.Collect(o => o.IsReachable);
            Assert.AreEqual(0, gc.Generations[0].Objects.Count, "Gen0 should be empty when no objects allocated.");
            Assert.AreEqual(0, gc.Generations[1].Objects.Count, "Gen1 should be empty when no objects allocated.");
            Assert.AreEqual(0, gc.Generations[2].Objects.Count, "Gen2 should be empty when no objects allocated.");
        }

        [TestMethod]
        public void TestMultipleCollections()
        {
            GarbageCollector gc = new GarbageCollector();
            GCObject live = new GCObject("Live");
            GCObject dead = new GCObject("Dead");
            dead.IsReachable = false;
            gc.AllocateObject(live);
            gc.AllocateObject(dead);
            gc.Collect(o => o.IsReachable);
            Assert.AreEqual(0, gc.Generations[0].Objects.Count, "Gen0 should be empty after first collection.");
            Assert.AreEqual(0, gc.Generations[1].Objects.Count, "Gen1 should be empty after first collection.");
            Assert.AreEqual(1, gc.Generations[2].Objects.Count, "Gen2 should have one live object after first collection.");
            GCObject live2 = new GCObject("Live2");
            gc.AllocateObject(live2);
            gc.Collect(o => o.IsReachable);
            Assert.AreEqual(0, gc.Generations[0].Objects.Count, "Gen0 should be empty after second collection.");
            Assert.AreEqual(0, gc.Generations[1].Objects.Count, "Gen1 should be empty after second collection.");
            Assert.AreEqual(2, gc.Generations[2].Objects.Count, "Gen2 should have two live objects after second collection.");
        }
    }
}