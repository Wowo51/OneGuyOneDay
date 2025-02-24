using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankersAlgorithm;
using System;
using System.Collections.Generic;

namespace BankersAlgorithmTest
{
    [TestClass]
    public class BankersAlgorithmTests
    {
        [TestMethod]
        public void ProcessNeed_CalculatesCorrectly()
        {
            int[] allocation = new int[]
            {
                1,
                2
            };
            int[] maximum = new int[]
            {
                3,
                4
            };
            Process process = new Process("P0", allocation, maximum);
            int[] expectedNeed = new int[]
            {
                2,
                2
            };
            int[] actualNeed = process.Need();
            Assert.AreEqual(expectedNeed.Length, actualNeed.Length, "Need array length mismatch.");
            for (int index = 0; index < expectedNeed.Length; index++)
            {
                Assert.AreEqual(expectedNeed[index], actualNeed[index], "Mismatch at index " + index);
            }
        }

        [TestMethod]
        public void FindSafeSequence_ReturnsSequence_WhenSafe()
        {
            int[] available = new int[]
            {
                3,
                3
            };
            Process[] processes = new Process[]
            {
                new Process("P0", new int[] { 0, 1 }, new int[] { 2, 2 }),
                new Process("P1", new int[] { 1, 0 }, new int[] { 2, 2 }),
                new Process("P2", new int[] { 1, 1 }, new int[] { 3, 3 })
            };
            List<string> safeSequence = BankersAlgorithmSolver.FindSafeSequence(available, processes);
            Assert.IsTrue(safeSequence.Count > 0, "Expected safeSequence to have elements.");
        }

        [TestMethod]
        public void FindSafeSequence_ReturnsEmpty_WhenUnsafe()
        {
            int[] available = new int[]
            {
                1,
                1
            };
            Process[] processes = new Process[]
            {
                new Process("P0", new int[] { 1, 0 }, new int[] { 3, 3 }),
                new Process("P1", new int[] { 0, 1 }, new int[] { 3, 3 })
            };
            List<string> safeSequence = BankersAlgorithmSolver.FindSafeSequence(available, processes);
            Assert.AreEqual(0, safeSequence.Count, "Expected safeSequence to be empty for unsafe state.");
        }

        [TestMethod]
        public void FindSafeSequence_LargeSyntheticTest()
        {
            int resourceCount = 5;
            int processCount = 50;
            int[] available = new int[resourceCount];
            for (int resourceIndex = 0; resourceIndex < resourceCount; resourceIndex++)
            {
                available[resourceIndex] = 10;
            }

            Process[] processes = new Process[processCount];
            Random random = new Random(12345);
            for (int processIndex = 0; processIndex < processCount; processIndex++)
            {
                int[] allocation = new int[resourceCount];
                int[] maximum = new int[resourceCount];
                for (int resourceIndex = 0; resourceIndex < resourceCount; resourceIndex++)
                {
                    int alloc = random.Next(0, 6);
                    allocation[resourceIndex] = alloc;
                    maximum[resourceIndex] = alloc + random.Next(0, 6);
                }

                processes[processIndex] = new Process("P" + processIndex.ToString(), allocation, maximum);
            }

            List<string> safeSequence = BankersAlgorithmSolver.FindSafeSequence(available, processes);
            if (safeSequence.Count > 0)
            {
                Assert.AreEqual(processCount, safeSequence.Count, "Expected safeSequence to cover all processes.");
            }
            else
            {
                Assert.AreEqual(0, safeSequence.Count, "Expected empty safeSequence when unsafe.");
            }
        }
    }
}