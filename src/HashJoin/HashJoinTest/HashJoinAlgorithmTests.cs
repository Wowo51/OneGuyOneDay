using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HashJoin;

namespace HashJoinTest
{
    [TestClass]
    public class HashJoinAlgorithmTests
    {
        [TestMethod]
        public void TestBasicJoin()
        {
            List<int> left = new List<int>
            {
                1,
                2,
                3
            };
            List<int> right = new List<int>
            {
                2,
                3,
                4
            };
            IEnumerable<Tuple<int, int>> joinResult = HashJoinAlgorithm.HashJoinMethod<int, int, int, Tuple<int, int>>(left, right, (int x) => x, (int y) => y, (int x, int y) => new Tuple<int, int>(x, y));
            List<Tuple<int, int>> resultList = joinResult.ToList();
            List<Tuple<int, int>> expected = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(2, 2),
                new Tuple<int, int>(3, 3)
            };
            Assert.AreEqual(expected.Count, resultList.Count, "Result count mismatch.");
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Item1, resultList[i].Item1);
                Assert.AreEqual(expected[i].Item2, resultList[i].Item2);
            }
        }

        [TestMethod]
        public void TestDuplicateKeysJoin()
        {
            List<int> left = new List<int>
            {
                1,
                1,
                2
            };
            List<int> right = new List<int>
            {
                1,
                2,
                1
            };
            IEnumerable<Tuple<int, int>> joinResult = HashJoinAlgorithm.HashJoinMethod<int, int, int, Tuple<int, int>>(left, right, (int x) => x, (int y) => y, (int x, int y) => new Tuple<int, int>(x, y));
            List<Tuple<int, int>> resultList = joinResult.ToList();
            List<Tuple<int, int>> expected = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(2, 2),
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(1, 1)
            };
            Assert.AreEqual(expected.Count, resultList.Count, "Result count mismatch.");
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Item1, resultList[i].Item1);
                Assert.AreEqual(expected[i].Item2, resultList[i].Item2);
            }
        }

        [TestMethod]
        public void TestNoMatchJoin()
        {
            List<int> left = new List<int>
            {
                1,
                2,
                3
            };
            List<int> right = new List<int>
            {
                4,
                5,
                6
            };
            IEnumerable<Tuple<int, int>> joinResult = HashJoinAlgorithm.HashJoinMethod<int, int, int, Tuple<int, int>>(left, right, (int x) => x, (int y) => y, (int x, int y) => new Tuple<int, int>(x, y));
            List<Tuple<int, int>> resultList = joinResult.ToList();
            Assert.AreEqual(0, resultList.Count, "Expected no matches.");
        }

        [TestMethod]
        public void TestNullParameters()
        {
            List<int> left = new List<int>
            {
                1,
                2,
                3
            };
            List<int> right = new List<int>
            {
                1,
                2,
                3
            };
            IEnumerable<Tuple<int, int>> resultNullLeft = HashJoinAlgorithm.HashJoinMethod<int, int, int, Tuple<int, int>>(null, right, (int x) => x, (int y) => y, (int x, int y) => new Tuple<int, int>(x, y));
            Assert.AreEqual(0, resultNullLeft.ToList().Count, "Null left should yield 0 results.");
            IEnumerable<Tuple<int, int>> resultNullRight = HashJoinAlgorithm.HashJoinMethod<int, int, int, Tuple<int, int>>(left, null, (int x) => x, (int y) => y, (int x, int y) => new Tuple<int, int>(x, y));
            Assert.AreEqual(0, resultNullRight.ToList().Count, "Null right should yield 0 results.");
            IEnumerable<Tuple<int, int>> resultNullLeftKey = HashJoinAlgorithm.HashJoinMethod<int, int, int, Tuple<int, int>>(left, right, null, (int y) => y, (int x, int y) => new Tuple<int, int>(x, y));
            Assert.AreEqual(0, resultNullLeftKey.ToList().Count, "Null leftKeySelector should yield 0 results.");
            IEnumerable<Tuple<int, int>> resultNullRightKey = HashJoinAlgorithm.HashJoinMethod<int, int, int, Tuple<int, int>>(left, right, (int x) => x, null, (int x, int y) => new Tuple<int, int>(x, y));
            Assert.AreEqual(0, resultNullRightKey.ToList().Count, "Null rightKeySelector should yield 0 results.");
            IEnumerable<Tuple<int, int>> resultNullResult = HashJoinAlgorithm.HashJoinMethod<int, int, int, Tuple<int, int>>(left, right, (int x) => x, (int y) => y, null);
            Assert.AreEqual(0, resultNullResult.ToList().Count, "Null resultSelector should yield 0 results.");
        }

        [TestMethod]
        public void TestLargeDataSetJoin()
        {
            List<int> left = new List<int>();
            for (int i = 500; i < 1000; i++)
            {
                left.Add(i);
                left.Add(i);
            }

            List<int> right = new List<int>();
            for (int i = 0; i < 1500; i++)
            {
                right.Add(i);
            }

            IEnumerable<Tuple<int, int>> joinResult = HashJoinAlgorithm.HashJoinMethod<int, int, int, Tuple<int, int>>(left, right, (int x) => x, (int y) => y, (int x, int y) => new Tuple<int, int>(x, y));
            List<Tuple<int, int>> resultList = joinResult.ToList();
            Assert.AreEqual(1000, resultList.Count, "Large dataset join count mismatch.");
            int count500 = resultList.Where((Tuple<int, int> x) => x.Item1 == 500 && x.Item2 == 500).Count();
            Assert.AreEqual(2, count500, "Key 500 should appear exactly twice.");
        }
    }
}