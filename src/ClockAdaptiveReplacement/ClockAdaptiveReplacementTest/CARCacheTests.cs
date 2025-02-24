using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClockAdaptiveReplacement;

namespace ClockAdaptiveReplacementTest
{
    [TestClass]
    public class CARCacheTests
    {
        [TestMethod]
        public void TestPutAndTryGetValue()
        {
            CARCache<int, string> cache = new CARCache<int, string>(3);
            cache.Put(1, "one");
            string value;
            bool found = cache.TryGetValue(1, out value);
            Assert.IsTrue(found, "Expected to find key 1 in the cache.");
            Assert.AreEqual("one", value, "The value for key 1 should be 'one'.");
        }

        [TestMethod]
        public void TestUpdateExistingKey()
        {
            CARCache<int, string> cache = new CARCache<int, string>(3);
            cache.Put(1, "one");
            cache.Put(1, "uno");
            string value;
            bool found = cache.TryGetValue(1, out value);
            Assert.IsTrue(found, "Expected to find key 1 after update.");
            Assert.AreEqual("uno", value, "The value for key 1 should have been updated to 'uno'.");
        }

        [TestMethod]
        public void TestEviction()
        {
            // Create a cache with capacity 2 to force eviction.
            CARCache<int, string> cache = new CARCache<int, string>(2);
            cache.Put(1, "one");
            cache.Put(2, "two");
            // Add a third element. This should trigger eviction.
            cache.Put(3, "three");
            string value;
            bool found1 = cache.TryGetValue(1, out value);
            bool found2 = cache.TryGetValue(2, out value);
            // At least one key should have been evicted.
            Assert.IsTrue((!found1) || (!found2), "One of the initial keys should have been evicted.");
            int countKeys = 0;
            IEnumerator<int> enumerator = cache.GetKeys().GetEnumerator();
            while (enumerator.MoveNext())
            {
                countKeys++;
            }

            Assert.AreEqual(2, countKeys, "The cache should contain 2 keys after eviction.");
        }

        [TestMethod]
        public void TestReferenceBitLogic()
        {
            // Insert three items. Access some keys to set their reference bits.
            CARCache<int, string> cache = new CARCache<int, string>(3);
            cache.Put(1, "one");
            cache.Put(2, "two");
            cache.Put(3, "three");
            string value;
            bool found = cache.TryGetValue(1, out value);
            Assert.IsTrue(found, "Key 1 should be retrievable.");
            found = cache.TryGetValue(2, out value);
            Assert.IsTrue(found, "Key 2 should be retrievable.");
            // Inserting a new item forces eviction; key 3 (not accessed) is expected to be evicted.
            cache.Put(4, "four");
            found = cache.TryGetValue(3, out value);
            Assert.IsFalse(found, "Key 3 should be evicted due to not being accessed.");
            found = cache.TryGetValue(1, out value);
            Assert.IsTrue(found, "Key 1 should still be in cache.");
            found = cache.TryGetValue(2, out value);
            Assert.IsTrue(found, "Key 2 should still be in cache.");
            found = cache.TryGetValue(4, out value);
            Assert.IsTrue(found, "Key 4 should be in cache.");
        }

        [TestMethod]
        public void TestClear()
        {
            CARCache<int, string> cache = new CARCache<int, string>(3);
            cache.Put(1, "one");
            cache.Put(2, "two");
            cache.Put(3, "three");
            cache.Clear();
            string value;
            bool found = cache.TryGetValue(1, out value);
            Assert.IsFalse(found, "Cache should be empty after clear.");
            found = cache.TryGetValue(2, out value);
            Assert.IsFalse(found, "Cache should be empty after clear.");
            found = cache.TryGetValue(3, out value);
            Assert.IsFalse(found, "Cache should be empty after clear.");
            int countKeys = 0;
            IEnumerator<int> enumerator = cache.GetKeys().GetEnumerator();
            while (enumerator.MoveNext())
            {
                countKeys++;
            }

            Assert.AreEqual(0, countKeys, "There should be no keys in the cache after clear.");
        }

        [TestMethod]
        public void TestGhostLists()
        {
            // With a capacity of 2, add keys to trigger ghost list updates.
            CARCache<int, string> cache = new CARCache<int, string>(2);
            cache.Put(1, "one");
            cache.Put(2, "two");
            // Adding a third element forces eviction.
            cache.Put(3, "three");
            bool ghost1 = cache.IsInGhostB1(1);
            bool ghost2 = cache.IsInGhostB1(2);
            bool ghostB1Updated = ghost1 || ghost2;
            bool ghostInB2 = cache.IsInGhostB2(1) || cache.IsInGhostB2(2);
            Assert.IsTrue(ghostB1Updated || ghostInB2, "At least one of the ghost lists should contain an evicted key.");
        }

        [TestMethod]
        public void TestT1AndT2Counts()
        {
            CARCache<int, string> cache = new CARCache<int, string>(3);
            cache.Put(1, "one");
            cache.Put(2, "two");
            cache.Put(3, "three");
            int t1 = cache.T1Count;
            int t2 = cache.T2Count;
            Assert.AreEqual(3, t1 + t2, "The total number of entries should equal the capacity.");
            string value;
            cache.TryGetValue(1, out value);
            int newT1 = cache.T1Count;
            int newT2 = cache.T2Count;
            Assert.IsTrue(newT1 <= t1, "Accessing an entry should potentially decrease the T1 count.");
        }

        [TestMethod]
        public void TestSyntheticDataLargeInput()
        {
            int capacity = 100;
            CARCache<int, string> cache = new CARCache<int, string>(capacity);
            // Insert items up to the capacity.
            for (int i = 1; i <= capacity; i++)
            {
                cache.Put(i, "Value" + i.ToString());
            }

            // Access alternate keys to set reference bits.
            for (int i = 1; i <= capacity; i += 2)
            {
                string value;
                cache.TryGetValue(i, out value);
            }

            // Add new items to force multiple evictions.
            for (int i = capacity + 1; i <= capacity + 50; i++)
            {
                cache.Put(i, "Value" + i.ToString());
            }

            int count = 0;
            IEnumerator<int> enumerator = cache.GetKeys().GetEnumerator();
            while (enumerator.MoveNext())
            {
                count++;
            }

            Assert.AreEqual(capacity, count, "The cache should maintain its capacity after multiple evictions.");
        }
    }
}