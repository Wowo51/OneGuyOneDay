using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SortingSignedReversals;

namespace SortingSignedReversalsTest
{
    [TestClass]
    public class SignedReversalSorterTests
    {
        [TestMethod]
        public void TestAlreadySortedPermutation()
        {
            List<int> permutation = new List<int>
            {
                1,
                2,
                3,
                4
            };
            List<string> steps = SignedReversalSorter.GreedySort(permutation);
            Assert.AreEqual(0, steps.Count);
        }

        [TestMethod]
        public void TestReversePermutation()
        {
            List<int> permutation = new List<int>
            {
                -4,
                -3,
                -2,
                -1
            };
            List<string> steps = SignedReversalSorter.GreedySort(permutation);
            Assert.AreEqual("1 2 3 4", steps[steps.Count - 1]);
        }

        [TestMethod]
        public void TestComplexPermutation()
        {
            List<int> permutation = new List<int>
            {
                3,
                4,
                -1,
                2
            };
            List<string> steps = SignedReversalSorter.GreedySort(permutation);
            Assert.AreEqual("1 2 3 4", steps[steps.Count - 1]);
        }

        [TestMethod]
        public void TestEmptyPermutation()
        {
            List<int> permutation = new List<int>();
            List<string> steps = SignedReversalSorter.GreedySort(permutation);
            Assert.AreEqual(0, steps.Count);
        }

        [TestMethod]
        public void TestSingleElementPermutation()
        {
            List<int> permutation = new List<int>
            {
                -1
            };
            List<string> steps = SignedReversalSorter.GreedySort(permutation);
            Assert.AreEqual("1", steps[steps.Count - 1]);
        }

        [TestMethod]
        public void TestSyntheticLargeRandomPermutation()
        {
            Random random = new Random(0);
            int n = 100;
            List<int> permutation = new List<int>();
            for (int i = 1; i <= n; i++)
            {
                permutation.Add(i);
            }

            for (int i = 0; i < n; i++)
            {
                int j = random.Next(0, n);
                int temp = permutation[i];
                permutation[i] = permutation[j];
                permutation[j] = temp;
            }

            for (int i = 0; i < n; i++)
            {
                if (random.NextDouble() < 0.5)
                {
                    permutation[i] = -permutation[i];
                }
            }

            List<string> steps = SignedReversalSorter.GreedySort(permutation);
            string finalStep = steps[steps.Count - 1];
            string[] parts = finalStep.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(n, parts.Length);
            for (int i = 0; i < n; i++)
            {
                int number = 0;
                bool parsed = Int32.TryParse(parts[i], out number);
                Assert.IsTrue(parsed);
                Assert.AreEqual(i + 1, number);
            }
        }
    }
}