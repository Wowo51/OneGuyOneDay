using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FisherYatesShuffle;

namespace FisherYatesShuffleTest
{
    [TestClass]
    public class FisherYatesShuffleTests
    {
        [TestMethod]
        public void Shuffle_NullList_DoesNotThrow()
        {
            IList<int>? list = null;
            ShuffleExtensions.Shuffle<int>(list);
        }

        [TestMethod]
        public void Shuffle_EmptyList_RemainsEmpty()
        {
            List<int> list = new List<int>();
            list.Shuffle();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Shuffle_SingleElement_RemainsUnchanged()
        {
            List<int> list = new List<int>
            {
                42
            };
            list.Shuffle();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(42, list[0]);
        }

        [TestMethod]
        public void Shuffle_MultipleElements_RandomPermutation()
        {
            List<int> list = Enumerable.Range(1, 100).ToList();
            List<int> originalOrder = new List<int>(list);
            bool orderChanged = false;
            for (int i = 0; i < 5; i++)
            {
                list.Shuffle();
                if (!Enumerable.SequenceEqual(originalOrder, list))
                {
                    orderChanged = true;
                    break;
                }
            }

            List<int> sortedOriginal = new List<int>(originalOrder);
            sortedOriginal.Sort();
            List<int> sortedShuffled = new List<int>(list);
            sortedShuffled.Sort();
            CollectionAssert.AreEqual(sortedOriginal, sortedShuffled);
            Assert.IsTrue(orderChanged, "Shuffle did not change the order after multiple attempts.");
        }

        [TestMethod]
        public void Shuffle_MultipleElements_GenericType()
        {
            List<string> list = new List<string>
            {
                "alpha",
                "beta",
                "gamma",
                "delta"
            };
            List<string> originalOrder = new List<string>(list);
            bool orderChanged = false;
            for (int i = 0; i < 5; i++)
            {
                list.Shuffle();
                if (!Enumerable.SequenceEqual(originalOrder, list))
                {
                    orderChanged = true;
                    break;
                }
            }

            CollectionAssert.AreEquivalent(originalOrder, list);
            Assert.IsTrue(orderChanged, "Shuffle did not change the order for a generic type after multiple attempts.");
        }

        [TestMethod]
        public void Shuffle_TwoElements_RandomOrder()
        {
            List<int> list = new List<int>
            {
                1,
                2
            };
            List<int> originalOrder = new List<int>(list);
            bool orderChanged = false;
            for (int i = 0; i < 10; i++)
            {
                list.Shuffle();
                if (!Enumerable.SequenceEqual(originalOrder, list))
                {
                    orderChanged = true;
                    break;
                }
            }

            CollectionAssert.AreEquivalent(originalOrder, list);
            Assert.IsTrue(orderChanged, "Shuffle did not change the order for a two-element list after multiple attempts.");
        }

        [TestMethod]
        public void Shuffle_LargeList_EfficiencyAndPermutationIntegrity()
        {
            List<int> list = Enumerable.Range(1, 10000).ToList();
            List<int> originalOrder = new List<int>(list);
            list.Shuffle();
            CollectionAssert.AreEquivalent(originalOrder, list);
        }
    }
}