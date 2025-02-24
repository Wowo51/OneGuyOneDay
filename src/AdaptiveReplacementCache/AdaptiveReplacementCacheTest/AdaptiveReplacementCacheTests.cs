using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaptiveReplacementCache;

namespace AdaptiveReplacementCacheTest
{
    [TestClass]
    public class AdaptiveReplacementCacheTests
    {
        [TestMethod]
        public void TestBasicPutAndGet()
        {
            AdaptiveReplacementCache<int, string> cache = new AdaptiveReplacementCache<int, string>(3);
            cache.Put(1, "one");
            cache.Put(2, "two");
            cache.Put(3, "three");
            Assert.AreEqual("one", cache.Get(1), "Key 1 should return 'one'");
            Assert.AreEqual("two", cache.Get(2), "Key 2 should return 'two'");
            Assert.AreEqual("three", cache.Get(3), "Key 3 should return 'three'");
        }

        [TestMethod]
        public void TestOverwrite()
        {
            AdaptiveReplacementCache<int, string> cache = new AdaptiveReplacementCache<int, string>(2);
            cache.Put(1, "one");
            cache.Put(1, "uno");
            Assert.AreEqual("uno", cache.Get(1), "Key 1 should return 'uno' after overwrite");
        }

        [TestMethod]
        public void TestEvictionBehavior()
        {
            AdaptiveReplacementCache<int, string> cache = new AdaptiveReplacementCache<int, string>(2);
            cache.Put(1, "one");
            cache.Put(2, "two");
            cache.Put(3, "three");
            Assert.IsNull(cache.Get(1), "Key 1 should be evicted");
            Assert.AreEqual("two", cache.Get(2), "Key 2 should remain");
            Assert.AreEqual("three", cache.Get(3), "Key 3 should be inserted");
        }

        [TestMethod]
        public void TestPromotionFromT1ToT2()
        {
            AdaptiveReplacementCache<int, string> cache = new AdaptiveReplacementCache<int, string>(3);
            cache.Put(1, "one");
            cache.Put(2, "two");
            string? firstAccess = cache.Get(1); // Promote key 1 from T1 to T2
            Assert.IsNotNull(firstAccess, "Expected non-null value for key 1.");
            cache.Put(3, "three");
            Assert.AreEqual("one", firstAccess, "Initial retrieval of key 1 should return 'one'");
            Assert.AreEqual("one", cache.Get(1), "Subsequent retrieval of key 1 should return 'one' after promotion");
        }

        [TestMethod]
        public void TestCacheMiss()
        {
            AdaptiveReplacementCache<int, string> cache = new AdaptiveReplacementCache<int, string>(2);
            cache.Put(1, "one");
            Assert.IsNull(cache.Get(2), "Getting a non-existent key should return null");
        }

        [TestMethod]
        public void TestLargeDataSet()
        {
            int capacity = 100;
            AdaptiveReplacementCache<int, int> cache = new AdaptiveReplacementCache<int, int>(capacity);
            for (int i = 0; i < 1000; i++)
            {
                cache.Put(i, i * 10);
            }

            for (int i = 900; i < 1000; i++)
            {
                Assert.AreEqual(i * 10, cache.Get(i), $"Key {i} should return {i * 10}");
            }
        }

        [TestMethod]
        public void TestNegativeCapacity()
        {
            AdaptiveReplacementCache<int, string> cache = new AdaptiveReplacementCache<int, string>(-5);
            cache.Put(42, "forty-two");
            Assert.AreEqual("forty-two", cache.Get(42), "Cache should support capacity adjusted to 1 when negative value is provided.");
        }
    }
}