using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HopfieldNet;

namespace HopfieldNetTest
{
    [TestClass]
    public class HopfieldNetworkTests
    {
        [TestMethod]
        public void TestRecallWithValidPattern()
        {
            int size = 3;
            HopfieldNetwork network = new HopfieldNetwork(size);
            int[] pattern = new int[]
            {
                1,
                -1,
                1
            };
            List<int[]> patterns = new List<int[]>();
            patterns.Add(pattern);
            network.Train(patterns);
            int[] recalled = network.Recall(pattern);
            CollectionAssert.AreEqual(pattern, recalled, "Recall does not return the trained pattern.");
        }

        [TestMethod]
        public void TestRecallWithPerturbedPattern()
        {
            int size = 3;
            HopfieldNetwork network = new HopfieldNetwork(size);
            int[] pattern = new int[]
            {
                1,
                -1,
                1
            };
            List<int[]> patterns = new List<int[]>();
            patterns.Add(pattern);
            network.Train(patterns);
            int[] perturbed = new int[]
            {
                -1,
                -1,
                1
            };
            int[] recalled = network.Recall(perturbed);
            CollectionAssert.AreEqual(pattern, recalled, "Recall did not converge to the trained pattern from a perturbed input.");
        }

        [TestMethod]
        public void TestTrainWithNullPatterns()
        {
            int size = 4;
            HopfieldNetwork network = new HopfieldNetwork(size);
            network.Train(null);
            int[] recalled = network.Recall(null);
            int[] expected = new int[]
            {
                1,
                1,
                1,
                1
            };
            CollectionAssert.AreEqual(expected, recalled, "Recall with null input did not return default state of ones.");
        }

        [TestMethod]
        public void TestTrainWithInvalidPattern()
        {
            int size = 3;
            HopfieldNetwork network = new HopfieldNetwork(size);
            List<int[]> patterns = new List<int[]>();
            patterns.Add(new int[] { 1, -1 }); // Invalid pattern length
            network.Train(patterns);
            int[] input = new int[]
            {
                -1,
                -1,
                -1
            };
            int[] recalled = network.Recall(input);
            int[] expected = new int[]
            {
                1,
                1,
                1
            };
            CollectionAssert.AreEqual(expected, recalled, "Recall after training with invalid pattern did not default to ones.");
        }

        [TestMethod]
        public void TestFixedPointProperty()
        {
            int size = 3;
            HopfieldNetwork network = new HopfieldNetwork(size);
            int[] pattern = new int[]
            {
                1,
                -1,
                1
            };
            List<int[]> patterns = new List<int[]>();
            patterns.Add(pattern);
            network.Train(patterns);
            int[] recalled = network.Recall(pattern);
            int[] fixedPoint = network.Recall(recalled);
            CollectionAssert.AreEqual(recalled, fixedPoint, "Recall of the recalled state does not produce a fixed point.");
        }

        [TestMethod]
        public void TestLargeRandomPatterns()
        {
            int size = 10;
            HopfieldNetwork network = new HopfieldNetwork(size);
            List<int[]> patterns = new List<int[]>();
            Random random = new Random(0);
            for (int i = 0; i < 100; i++)
            {
                int[] pattern = new int[size];
                for (int j = 0; j < size; j++)
                {
                    pattern[j] = (random.Next(2) == 0) ? -1 : 1;
                }

                patterns.Add(pattern);
            }

            network.Train(patterns);
            int[] testState = new int[size];
            for (int j = 0; j < size; j++)
            {
                testState[j] = (random.Next(2) == 0) ? -1 : 1;
            }

            int[] recalled = network.Recall(testState);
            int[] fixedPoint = network.Recall(recalled);
            CollectionAssert.AreEqual(recalled, fixedPoint, "Large random patterns recall did not converge to a fixed point.");
            for (int i = 0; i < recalled.Length; i++)
            {
                Assert.IsTrue(recalled[i] == 1 || recalled[i] == -1, "Recalled state contains invalid values.");
            }
        }

        [TestMethod]
        public void TestRecallWithMultiplePatterns()
        {
            int size = 4;
            HopfieldNetwork network = new HopfieldNetwork(size);
            int[] pattern1 = new int[]
            {
                1,
                -1,
                1,
                -1
            };
            int[] pattern2 = new int[]
            {
                -1,
                1,
                -1,
                1
            };
            List<int[]> patterns = new List<int[]>();
            patterns.Add(pattern1);
            patterns.Add(pattern2);
            network.Train(patterns);
            int[] input = new int[]
            {
                1,
                -1,
                -1,
                -1
            }; // Perturbed version
            int[] recalled = network.Recall(input);
            bool equalToPattern1 = ArePatternsEqual(recalled, pattern1);
            bool equalToPattern2 = ArePatternsEqual(recalled, pattern2);
            Assert.IsTrue(equalToPattern1 || equalToPattern2, "Recall with multiple patterns did not return one of the trained patterns.");
        }

        private bool ArePatternsEqual(int[] array1, int[] array2)
        {
            if (array1 == null || array2 == null)
            {
                return false;
            }

            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}