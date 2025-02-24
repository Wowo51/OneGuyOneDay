using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShortestSeekFirst;

namespace ShortestSeekFirstTest
{
    [TestClass]
    public class SSTFSchedulerTests
    {
        [TestMethod]
        public void TestNullRequests()
        {
            List<int> result = SSTFScheduler.Schedule(null, 50);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestEmptyRequests()
        {
            List<int> requests = new List<int>();
            List<int> result = SSTFScheduler.Schedule(requests, 50);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestSingleRequest()
        {
            List<int> requests = new List<int>
            {
                75
            };
            List<int> result = SSTFScheduler.Schedule(requests, 50);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(75, result[0]);
        }

        [TestMethod]
        public void TestMultipleRequests()
        {
            List<int> requests = new List<int>
            {
                10,
                30,
                75,
                90
            };
            List<int> result = SSTFScheduler.Schedule(requests, 50);
            List<int> expected = new List<int>
            {
                30,
                10,
                75,
                90
            };
            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i], "Mismatch at index " + i);
            }
        }

        [TestMethod]
        public void TestTwoRequestsTie_OrderOne()
        {
            List<int> requests = new List<int>
            {
                55,
                45
            };
            List<int> result = SSTFScheduler.Schedule(requests, 50);
            List<int> expected = new List<int>
            {
                55,
                45
            };
            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i], "Mismatch at index " + i);
            }
        }

        [TestMethod]
        public void TestTwoRequestsTie_OrderTwo()
        {
            List<int> requests = new List<int>
            {
                45,
                55
            };
            List<int> result = SSTFScheduler.Schedule(requests, 50);
            List<int> expected = new List<int>
            {
                45,
                55
            };
            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i], "Mismatch at index " + i);
            }
        }

        [TestMethod]
        public void TestSyntheticLargeDataset()
        {
            Random random = new Random(42);
            int datasetSize = 100;
            List<int> requests = new List<int>();
            for (int i = 0; i < datasetSize; i++)
            {
                int randValue = random.Next(0, 1001);
                requests.Add(randValue);
            }

            int head = random.Next(0, 1001);
            List<int> result = SSTFScheduler.Schedule(requests, head);
            Assert.AreEqual(requests.Count, result.Count);
            List<int> sortedRequests = new List<int>(requests);
            sortedRequests.Sort();
            List<int> sortedResult = new List<int>(result);
            sortedResult.Sort();
            for (int i = 0; i < sortedRequests.Count; i++)
            {
                Assert.AreEqual(sortedRequests[i], sortedResult[i], "Permutation mismatch at index " + i);
            }

            int currentPosition = head;
            List<int> remaining = new List<int>(requests);
            for (int i = 0; i < result.Count; i++)
            {
                int chosen = result[i];
                int minDistance = Math.Abs(chosen - currentPosition);
                foreach (int request in remaining)
                {
                    int distance = Math.Abs(request - currentPosition);
                    Assert.IsTrue(minDistance <= distance, "Chosen value is not the nearest at step " + i);
                }

                currentPosition = chosen;
                remaining.Remove(chosen);
            }
        }

        [TestMethod]
        public void TestDuplicateRequests()
        {
            List<int> requests = new List<int>
            {
                50,
                50,
                60,
                60
            };
            List<int> result = SSTFScheduler.Schedule(requests, 50);
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(50, result[0]);
            Assert.AreEqual(50, result[1]);
            Assert.AreEqual(60, result[2]);
            Assert.AreEqual(60, result[3]);
        }

        [TestMethod]
        public void TestNegativeHead()
        {
            List<int> requests = new List<int>
            {
                0,
                10,
                20
            };
            List<int> result = SSTFScheduler.Schedule(requests, -10);
            List<int> expected = new List<int>
            {
                0,
                10,
                20
            };
            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }
    }
}