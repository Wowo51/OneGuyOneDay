using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockLoopJoin;

namespace BlockLoopJoinTest
{
    [TestClass]
    public class BlockLoopJoinUnitTests
    {
        [TestMethod]
        public void Test_NullLeft_ReturnsNoResults()
        {
            IEnumerable<int>? left = null;
            List<int> right = new List<int>()
            {
                1,
                2,
                3
            };
            IEnumerable<(int, int)> result = BlockNestedLoopJoin.Join<int, int, int>(left, right, (int x) => x, (int x) => x, 2);
            List<(int, int)> resultList = new List<(int, int)>(result);
            Assert.AreEqual(0, resultList.Count);
        }

        [TestMethod]
        public void Test_NullRight_ReturnsNoResults()
        {
            List<int> left = new List<int>()
            {
                1,
                2,
                3
            };
            IEnumerable<int>? right = null;
            IEnumerable<(int, int)> result = BlockNestedLoopJoin.Join<int, int, int>(left, right, (int x) => x, (int x) => x, 2);
            List<(int, int)> resultList = new List<(int, int)>(result);
            Assert.AreEqual(0, resultList.Count);
        }

        [TestMethod]
        public void Test_NullKeySelectors_ReturnsNoResults()
        {
            List<int> left = new List<int>()
            {
                1,
                2,
                3
            };
            List<int> right = new List<int>()
            {
                1,
                2,
                3
            };
            IEnumerable<(int, int)> result1 = BlockNestedLoopJoin.Join<int, int, int>(left, right, null, (int x) => x, 2);
            List<(int, int)> resultList1 = new List<(int, int)>(result1);
            Assert.AreEqual(0, resultList1.Count);
            IEnumerable<(int, int)> result2 = BlockNestedLoopJoin.Join<int, int, int>(left, right, (int x) => x, null, 2);
            List<(int, int)> resultList2 = new List<(int, int)>(result2);
            Assert.AreEqual(0, resultList2.Count);
        }

        [TestMethod]
        public void Test_JoinMatchingElements_ReturnsPairs()
        {
            List<int> left = new List<int>()
            {
                1,
                2,
                3,
                4
            };
            List<int> right = new List<int>()
            {
                3,
                4,
                5
            };
            IEnumerable<(int, int)> result = BlockNestedLoopJoin.Join<int, int, int>(left, right, (int x) => x, (int x) => x, 2);
            List<(int, int)> resultList = new List<(int, int)>(result);
            List<(int, int)> expected = new List<(int, int)>()
            {
                (3, 3),
                (4, 4)
            };
            Assert.AreEqual(expected.Count, resultList.Count);
            foreach ((int expectedLeft, int expectedRight)in expected)
            {
                Assert.IsTrue(resultList.Contains((expectedLeft, expectedRight)));
            }
        }

        [TestMethod]
        public void Test_JoinNonMatchingElements_ReturnsEmpty()
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
            IEnumerable<(int, int)> result = BlockNestedLoopJoin.Join<int, int, int>(left, right, (int x) => x, (int x) => x, 2);
            List<(int, int)> resultList = new List<(int, int)>(result);
            Assert.AreEqual(0, resultList.Count);
        }

        [TestMethod]
        public void Test_BlockSizeLessThanOne_UsesOneBlockSize()
        {
            List<int> left = new List<int>()
            {
                1,
                2,
                2,
                3
            };
            List<int> right = new List<int>()
            {
                2
            };
            // blockSize = 0 should be corrected to 1 internally.
            IEnumerable<(int, int)> result = BlockNestedLoopJoin.Join<int, int, int>(left, right, (int x) => x, (int x) => x, 0);
            List<(int, int)> resultList = new List<(int, int)>(result);
            List<(int, int)> expected = new List<(int, int)>()
            {
                (2, 2),
                (2, 2)
            };
            Assert.AreEqual(expected.Count, resultList.Count);
            foreach ((int expectedLeft, int expectedRight)in expected)
            {
                Assert.IsTrue(resultList.Contains((expectedLeft, expectedRight)));
            }
        }

        [TestMethod]
        public void Test_BlockSizeLargerThanLeftCount()
        {
            List<int> left = new List<int>()
            {
                1,
                2,
                3
            };
            List<int> right = new List<int>()
            {
                1,
                2,
                3
            };
            // blockSize greater than the left count.
            IEnumerable<(int, int)> result = BlockNestedLoopJoin.Join<int, int, int>(left, right, (int x) => x, (int x) => x, 10);
            List<(int, int)> resultList = new List<(int, int)>(result);
            List<(int, int)> expected = new List<(int, int)>()
            {
                (1, 1),
                (2, 2),
                (3, 3)
            };
            Assert.AreEqual(expected.Count, resultList.Count);
            foreach ((int expectedLeft, int expectedRight)in expected)
            {
                Assert.IsTrue(resultList.Contains((expectedLeft, expectedRight)));
            }
        }

        [TestMethod]
        public void Test_MultipleMatches_DuplicateEntries()
        {
            List<int> left = new List<int>()
            {
                1,
                2,
                2,
                3,
                3,
                3
            };
            List<int> right = new List<int>()
            {
                2,
                3,
                3
            };
            IEnumerable<(int, int)> result = BlockNestedLoopJoin.Join<int, int, int>(left, right, (int x) => x, (int x) => x, 2);
            List<(int, int)> resultList = new List<(int, int)>(result);
            // For key 2: two occurrences in left and one in right => 2 matches.
            // For key 3: three occurrences in left and two in right => 6 matches.
            int expectedCount = 2 + 6;
            Assert.AreEqual(expectedCount, resultList.Count);
        }

        [TestMethod]
        public void Test_LargeDataSet()
        {
            const int size = 1000;
            List<int> left = new List<int>();
            List<int> right = new List<int>();
            for (int i = 0; i < size; i++)
            {
                left.Add(i);
                right.Add(i + (size / 2));
            }

            IEnumerable<(int, int)> result = BlockNestedLoopJoin.Join<int, int, int>(left, right, (int x) => x, (int x) => x, 50);
            List<(int, int)> resultList = new List<(int, int)>(result);
            List<(int, int)> expectedMatches = new List<(int, int)>();
            for (int i = 0; i < size; i++)
            {
                int key = i;
                if (right.Contains(key))
                {
                    expectedMatches.Add((key, key));
                }
            }

            Assert.AreEqual(expectedMatches.Count, resultList.Count);
            foreach ((int expectedLeft, int expectedRight)in expectedMatches)
            {
                Assert.IsTrue(resultList.Contains((expectedLeft, expectedRight)));
            }
        }
    }
}