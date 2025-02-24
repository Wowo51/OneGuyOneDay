using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NestedLoopJoin;

namespace NestedLoopJoinTest
{
    [TestClass]
    public class NestedLoopJoinTests
    {
        [TestMethod]
        public void TestJoin_BasicEquality()
        {
            List<int> left = new List<int>()
            {
                1,
                2,
                3
            };
            List<int> right = new List<int>()
            {
                2,
                3,
                4
            };
            IEnumerable<Tuple<int, int>> result = NestedLoopJoinAlgorithm.Join<int, int, Tuple<int, int>>(left, right, (int l, int r) => l == r, (int l, int r) => new Tuple<int, int>(l, r));
            List<Tuple<int, int>> resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.IsTrue(resultList.Exists(delegate (Tuple<int, int> item)
            {
                return item.Item1 == 2 && item.Item2 == 2;
            }));
            Assert.IsTrue(resultList.Exists(delegate (Tuple<int, int> item)
            {
                return item.Item1 == 3 && item.Item2 == 3;
            }));
        }

        [TestMethod]
        public void TestJoin_NoMatch()
        {
            List<int> left = new List<int>()
            {
                1,
                2,
                3
            };
            List<int> right = new List<int>()
            {
                4,
                5,
                6
            };
            IEnumerable<Tuple<int, int>> result = NestedLoopJoinAlgorithm.Join<int, int, Tuple<int, int>>(left, right, (int l, int r) => false, (int l, int r) => new Tuple<int, int>(l, r));
            List<Tuple<int, int>> resultList = result.ToList();
            Assert.AreEqual(0, resultList.Count);
        }

        [TestMethod]
        public void TestJoin_NullArguments()
        {
            List<int> left = new List<int>()
            {
                1,
                2,
                3
            };
            List<int> right = new List<int>()
            {
                4,
                5,
                6
            };
            IEnumerable<int> result1 = NestedLoopJoinAlgorithm.Join<int, int, int>(null !, right, (int l, int r) => true, (int l, int r) => l);
            Assert.AreEqual(0, result1.ToList().Count);
            IEnumerable<int> result2 = NestedLoopJoinAlgorithm.Join<int, int, int>(left, null !, (int l, int r) => true, (int l, int r) => l);
            Assert.AreEqual(0, result2.ToList().Count);
            IEnumerable<int> result3 = NestedLoopJoinAlgorithm.Join<int, int, int>(left, right, null !, (int l, int r) => l);
            Assert.AreEqual(0, result3.ToList().Count);
            IEnumerable<int> result4 = NestedLoopJoinAlgorithm.Join<int, int, int>(left, right, (int l, int r) => true, null !);
            Assert.AreEqual(0, result4.ToList().Count);
        }

        [TestMethod]
        public void TestJoin_LargeDataSet()
        {
            List<int> left = Enumerable.Range(0, 1000).ToList();
            List<int> right = Enumerable.Range(500, 1000).ToList();
            // Both lists contribute 500 even numbers; expected pairs: 500 * 500 = 250000.
            IEnumerable<int> result = NestedLoopJoinAlgorithm.Join<int, int, int>(left, right, (int l, int r) => (l % 2 == 0) && (r % 2 == 0), (int l, int r) => 1);
            List<int> resultList = result.ToList();
            Assert.AreEqual(250000, resultList.Count);
        }

        [TestMethod]
        public void TestJoin_CustomResultSelector()
        {
            List<string> left = new List<string>()
            {
                "apple",
                "banana"
            };
            List<string> right = new List<string>()
            {
                "crisp",
                "split",
                "pie"
            };
            // "apple" (5 letters) should match with "crisp" and "split" (5 letters each)
            IEnumerable<string> result = NestedLoopJoinAlgorithm.Join<string, string, string>(left, right, (string l, string r) => l.Length == r.Length, (string l, string r) => l + "-" + r);
            List<string> resultList = result.ToList();
            Assert.AreEqual(2, resultList.Count);
            Assert.IsTrue(resultList.Contains("apple-crisp"));
            Assert.IsTrue(resultList.Contains("apple-split"));
        }
    }
}