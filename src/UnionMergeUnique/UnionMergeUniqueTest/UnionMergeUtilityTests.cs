using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnionMergeUnique;

namespace UnionMergeUniqueTest
{
    [TestClass]
    public class UnionMergeUtilityTests
    {
        [TestMethod]
        public void UnionMerge_WithTwoNonNullCollections_ReturnsUnionWithoutDuplicates()
        {
            List<int> first = new List<int>
            {
                1,
                2,
                2,
                3
            };
            List<int> second = new List<int>
            {
                3,
                4,
                4,
                5
            };
            List<int> result = UnionMergeUtility.UnionMerge(first, second);
            Assert.IsNotNull(result);
            HashSet<int> expected = new HashSet<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            Assert.AreEqual(expected.Count, result.Count, "Union result count mismatch.");
            foreach (int value in result)
            {
                Assert.IsTrue(expected.Contains(value), "Unexpected value in union merge: " + value);
            }
        }

        [TestMethod]
        public void UnionMerge_WithNullFirstCollection_ReturnsUnionOfSecond()
        {
            List<int> second = new List<int>
            {
                10,
                20,
                20
            };
            List<int> result = UnionMergeUtility.UnionMerge<int>(null, second);
            Assert.IsNotNull(result);
            HashSet<int> expected = new HashSet<int>
            {
                10,
                20
            };
            Assert.AreEqual(expected.Count, result.Count, "Union result count mismatch.");
            foreach (int value in result)
            {
                Assert.IsTrue(expected.Contains(value), "Unexpected value in union merge: " + value);
            }
        }

        [TestMethod]
        public void UnionMerge_WithNullBoth_ReturnsEmptyList()
        {
            List<int> result = UnionMergeUtility.UnionMerge<int>(null, null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count, "Expected empty union merge list when both sources are null.");
        }

        [TestMethod]
        public void UnionMerge_WithLargeSyntheticData()
        {
            List<int> first = new List<int>();
            List<int> second = new List<int>();
            Random randomGenerator = new Random(42);
            for (int i = 0; i < 1000; i++)
            {
                first.Add(randomGenerator.Next(0, 501));
            }

            for (int j = 0; j < 1000; j++)
            {
                second.Add(randomGenerator.Next(250, 751));
            }

            List<int> result = UnionMergeUtility.UnionMerge(first, second);
            Assert.IsNotNull(result);
            HashSet<int> unionDistinct = new HashSet<int>(first);
            foreach (int number in second)
            {
                unionDistinct.Add(number);
            }

            Assert.AreEqual(unionDistinct.Count, result.Count, "Union merge count mismatch for synthetic data.");
            foreach (int value in result)
            {
                Assert.IsTrue(unionDistinct.Contains(value), "Union merge produced unexpected value: " + value);
            }
        }
    }
}