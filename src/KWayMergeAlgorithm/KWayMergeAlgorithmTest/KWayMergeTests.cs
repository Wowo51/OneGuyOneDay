using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using KWayMergeAlgorithm;

namespace KWayMergeAlgorithmTest
{
    [TestClass]
    public class KWayMergeTests
    {
        [TestMethod]
        public void Merge_NullInput_ReturnsEmpty()
        {
            IEnumerable<int> result = KWayMerge.Merge<int>(null);
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new List<int>(), result.ToList());
        }

        [TestMethod]
        public void Merge_AllEmptySequences_ReturnsEmpty()
        {
            List<List<int>> sequences = new List<List<int>>
            {
                new List<int>(),
                new List<int>()
            };
            IEnumerable<int> result = KWayMerge.Merge<int>(sequences);
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new List<int>(), result.ToList());
        }

        [TestMethod]
        public void Merge_SingleSequence_ReturnsSameSequence()
        {
            List<int> sequence = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            IEnumerable<int> result = KWayMerge.Merge<int>(new List<List<int>> { sequence });
            CollectionAssert.AreEqual(sequence, result.ToList());
        }

        [TestMethod]
        public void Merge_MultipleSortedSequences_ReturnsSortedMerge()
        {
            List<int> seq1 = new List<int>
            {
                1,
                4,
                7
            };
            List<int> seq2 = new List<int>
            {
                2,
                5,
                8
            };
            List<int> seq3 = new List<int>
            {
                3,
                6,
                9
            };
            IEnumerable<int> result = KWayMerge.Merge<int>(new List<List<int>> { seq1, seq2, seq3 });
            List<int> expected = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9
            };
            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public void Merge_SequencesWithDuplicates_ReturnsSortedMergeWithDuplicates()
        {
            List<int> seq1 = new List<int>
            {
                1,
                3,
                5
            };
            List<int> seq2 = new List<int>
            {
                1,
                3,
                4
            };
            IEnumerable<int> result = KWayMerge.Merge<int>(new List<List<int>> { seq1, seq2 });
            List<int> expected = new List<int>
            {
                1,
                1,
                3,
                3,
                4,
                5
            };
            CollectionAssert.AreEqual(expected, result.ToList());
        }

        [TestMethod]
        public void Merge_LargeSyntheticData_ReturnsSortedMerge()
        {
            List<List<int>> sequences = new List<List<int>>();
            Random random = new Random(0);
            for (int i = 0; i < 10; i++)
            {
                List<int> sequence = new List<int>();
                int baseValue = random.Next(1, 1000);
                for (int j = 0; j < 100; j++)
                {
                    sequence.Add(baseValue + j * random.Next(1, 5));
                }

                sequence.Sort();
                sequences.Add(sequence);
            }

            IEnumerable<int> result = KWayMerge.Merge<int>(sequences);
            List<int> mergedList = result.ToList();
            for (int i = 1; i < mergedList.Count; i++)
            {
                Assert.IsTrue(mergedList[i - 1] <= mergedList[i]);
            }

            int totalCount = sequences.Sum(seq => seq.Count);
            Assert.AreEqual(totalCount, mergedList.Count);
        }

        [TestMethod]
        public void Merge_ContainsNullSequence_IgnoresNullSequences()
        {
            List<List<int>> sequences = new List<List<int>>();
            sequences.Add(new List<int> { 10, 20, 30 });
            sequences.Add(null);
            IEnumerable<int> result = KWayMerge.Merge<int>(sequences);
            CollectionAssert.AreEqual(new List<int> { 10, 20, 30 }, result.ToList());
        }
    }
}