using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortMergeJoin;

namespace SortMergeJoinTest
{
    [TestClass]
    public class SortMergeJoinerTests
    {
        [TestMethod]
        public void TestJoin_NormalCase()
        {
            List<int> left = new List<int>
            {
                1,
                2,
                2,
                3,
                5,
                7,
                7
            };
            List<int> right = new List<int>
            {
                2,
                2,
                4,
                7,
                8
            };
            List<Tuple<int, int>> result = SortMergeJoiner.Join<int, int, int>(left, right, delegate (int x)
            {
                return x;
            }, delegate (int x)
            {
                return x;
            }, Comparer<int>.Default);
            Assert.AreEqual(6, result.Count, "Expected 6 pairs for matching keys.");
            foreach (Tuple<int, int> pair in result)
            {
                Assert.AreEqual(pair.Item1, pair.Item2, "Joined values must be equal.");
                Assert.IsTrue(pair.Item1 == 2 || pair.Item1 == 7, "Joined key should be 2 or 7.");
            }
        }

        [TestMethod]
        public void TestJoin_Disjoint()
        {
            List<int> left = new List<int>
            {
                1,
                3,
                5
            };
            List<int> right = new List<int>
            {
                2,
                4,
                6
            };
            List<Tuple<int, int>> result = SortMergeJoiner.Join<int, int, int>(left, right, delegate (int x)
            {
                return x;
            }, delegate (int x)
            {
                return x;
            }, Comparer<int>.Default);
            Assert.AreEqual(0, result.Count, "No join pairs expected for disjoint sets.");
        }

        [TestMethod]
        public void TestJoin_EmptyInput()
        {
            List<int> left = new List<int>();
            List<int> right = new List<int>
            {
                1,
                2,
                3
            };
            List<Tuple<int, int>> result1 = SortMergeJoiner.Join<int, int, int>(left, right, delegate (int x)
            {
                return x;
            }, delegate (int x)
            {
                return x;
            }, Comparer<int>.Default);
            Assert.AreEqual(0, result1.Count, "No join pairs expected when left is empty.");
            left = new List<int>
            {
                1,
                2,
                3
            };
            right = new List<int>();
            List<Tuple<int, int>> result2 = SortMergeJoiner.Join<int, int, int>(left, right, delegate (int x)
            {
                return x;
            }, delegate (int x)
            {
                return x;
            }, Comparer<int>.Default);
            Assert.AreEqual(0, result2.Count, "No join pairs expected when right is empty.");
        }

        [TestMethod]
        public void TestJoin_NullParameters()
        {
            List<int> nonNullList = new List<int>
            {
                1,
                2,
                3
            };
            List<Tuple<int, int>> result;
            result = SortMergeJoiner.Join<int, int, int>(null, nonNullList, delegate (int x)
            {
                return x;
            }, delegate (int x)
            {
                return x;
            }, Comparer<int>.Default);
            Assert.AreEqual(0, result.Count, "Expected empty result if left is null.");
            result = SortMergeJoiner.Join<int, int, int>(nonNullList, null, delegate (int x)
            {
                return x;
            }, delegate (int x)
            {
                return x;
            }, Comparer<int>.Default);
            Assert.AreEqual(0, result.Count, "Expected empty result if right is null.");
            result = SortMergeJoiner.Join<int, int, int>(nonNullList, nonNullList, null, delegate (int x)
            {
                return x;
            }, Comparer<int>.Default);
            Assert.AreEqual(0, result.Count, "Expected empty result if leftKeySelector is null.");
            result = SortMergeJoiner.Join<int, int, int>(nonNullList, nonNullList, delegate (int x)
            {
                return x;
            }, null, Comparer<int>.Default);
            Assert.AreEqual(0, result.Count, "Expected empty result if rightKeySelector is null.");
            result = SortMergeJoiner.Join<int, int, int>(nonNullList, nonNullList, delegate (int x)
            {
                return x;
            }, delegate (int x)
            {
                return x;
            }, null);
            Assert.AreEqual(0, result.Count, "Expected empty result if comparer is null.");
        }

        [TestMethod]
        public void TestJoin_LargeDataSet()
        {
            List<int> left = new List<int>();
            List<int> right = new List<int>();
            for (int number = 1; number <= 100; number++)
            {
                for (int i = 0; i < 10; i++)
                {
                    left.Add(number);
                }
            }

            for (int number = 1; number <= 100; number++)
            {
                for (int i = 0; i < 10; i++)
                {
                    right.Add(number);
                }
            }

            List<Tuple<int, int>> result = SortMergeJoiner.Join<int, int, int>(left, right, delegate (int x)
            {
                return x;
            }, delegate (int x)
            {
                return x;
            }, Comparer<int>.Default);
            int expected = 100 * 100;
            Assert.AreEqual(expected, result.Count, "Expected 10000 pairs for large dataset.");
            foreach (Tuple<int, int> pair in result)
            {
                Assert.IsTrue(pair.Item1 >= 1 && pair.Item1 <= 100, "Left value in expected range.");
                Assert.AreEqual(pair.Item1, pair.Item2, "Joined pair should have equal values.");
            }
        }
    }
}