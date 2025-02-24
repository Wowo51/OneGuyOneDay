using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteinhausJohnsonTrotter;

namespace SteinhausJohnsonTrotterTest
{
    [TestClass]
    public class SteinhausJohnsonTrotterTests
    {
        [TestMethod]
        public void Generate_WithNEquals1()
        {
            IEnumerable<int[]> permutations = SteinhausJohnsonTrotterAlgorithm.Generate(1);
            List<int[]> list = permutations.ToList();
            Assert.AreEqual(1, list.Count);
            CollectionAssert.AreEqual(new int[] { 1 }, list[0]);
        }

        [TestMethod]
        public void Generate_WithNEquals3()
        {
            List<int[]> permutations = SteinhausJohnsonTrotterAlgorithm.Generate(3).ToList();
            Assert.AreEqual(6, permutations.Count);
            int[][] expected = new int[][]
            {
                new int[]
                {
                    1,
                    2,
                    3
                },
                new int[]
                {
                    1,
                    3,
                    2
                },
                new int[]
                {
                    3,
                    1,
                    2
                },
                new int[]
                {
                    3,
                    2,
                    1
                },
                new int[]
                {
                    2,
                    3,
                    1
                },
                new int[]
                {
                    2,
                    1,
                    3
                }
            };
            for (int i = 0; i < expected.Length; i++)
            {
                CollectionAssert.AreEqual(expected[i], permutations[i]);
            }
        }

        [TestMethod]
        public void Generate_WithNEquals0()
        {
            List<int[]> permutations = SteinhausJohnsonTrotterAlgorithm.Generate(0).ToList();
            Assert.AreEqual(1, permutations.Count);
            Assert.AreEqual(0, permutations[0].Length);
        }

        [TestMethod]
        public void Generate_WithNEquals4_CountTest()
        {
            int n = 4;
            List<int[]> permutations = SteinhausJohnsonTrotterAlgorithm.Generate(n).ToList();
            Assert.AreEqual(24, permutations.Count);
            foreach (int[] permutation in permutations)
            {
                int[] sortedPermutation = (int[])permutation.Clone();
                Array.Sort(sortedPermutation);
                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, sortedPermutation);
            }
        }

        [TestMethod]
        public void Main_PrintsOutput()
        {
            StringWriter output = new StringWriter();
            TextWriter originalOut = Console.Out;
            Console.SetOut(output);
            try
            {
                SteinhausJohnsonTrotter.Program.Main();
                string result = output.ToString();
                Assert.IsTrue(result.Contains("Steinhaus-Johnson-Trotter Permutations:"));
                string[] lines = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                Assert.IsTrue(lines.Length >= 2);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}