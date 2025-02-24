using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AprioriAlgorithm;

namespace AprioriAlgorithmTest
{
    [TestClass]
    public class AprioriAlgorithmTests
    {
        [TestMethod]
        public void TestNullTransactions()
        {
            List<HashSet<string>>? transactions = null;
            List<FrequentItemset<string>> result = Apriori.RunApriori(transactions, 0.5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestEmptyTransactions()
        {
            List<HashSet<string>> transactions = new List<HashSet<string>>();
            List<FrequentItemset<string>> result = Apriori.RunApriori(transactions, 0.5);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestSingleTransaction()
        {
            HashSet<string> transaction = new HashSet<string>
            {
                "a",
                "b",
                "c"
            };
            List<HashSet<string>> transactions = new List<HashSet<string>>
            {
                transaction
            };
            List<FrequentItemset<string>> result = Apriori.RunApriori(transactions, 1.0);
            // With one transaction, expected frequent itemsets:
            // 3 singletons, 3 pairs, and 1 triple = 7 in total.
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Count);
            Int32 singletonCount = result.Where(fs => fs.Items.Count == 1).Count();
            Int32 pairCount = result.Where(fs => fs.Items.Count == 2).Count();
            Int32 tripleCount = result.Where(fs => fs.Items.Count == 3).Count();
            Assert.AreEqual(3, singletonCount);
            Assert.AreEqual(3, pairCount);
            Assert.AreEqual(1, tripleCount);
        }

        [TestMethod]
        public void TestMultipleTransactions()
        {
            List<HashSet<string>> transactions = new List<HashSet<string>>
            {
                new HashSet<string>
                {
                    "a",
                    "b"
                },
                new HashSet<string>
                {
                    "a",
                    "c"
                },
                new HashSet<string>
                {
                    "a",
                    "b",
                    "c"
                }
            };
            // With minSupport 0.66 in 3 transactions, threshold becomes Ceil(1.98) = 2.
            // Expected frequent itemsets are:
            // Singletons: {"a"} count 3, {"b"} count 2, {"c"} count 2 (3 total)
            // Pairs: {"a", "b"} and {"a", "c"} (2 total)
            // {"b", "c"} and {"a", "b", "c"} are not frequent.
            List<FrequentItemset<string>> result = Apriori.RunApriori(transactions, 0.66);
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count);
            Int32 singletonCount = result.Where(fs => fs.Items.Count == 1).Count();
            Int32 pairCount = result.Where(fs => fs.Items.Count == 2).Count();
            Assert.AreEqual(3, singletonCount);
            Assert.AreEqual(2, pairCount);
        }

        [TestMethod]
        public void TestLargeSyntheticData()
        {
            List<HashSet<string>> transactions = new List<HashSet<string>>();
            String[] items = new String[]
            {
                "X",
                "Y",
                "Z",
                "W",
                "V"
            };
            Random random = new Random(42);
            for (Int32 i = 0; i < 100; i++)
            {
                HashSet<string> transaction = new HashSet<string>();
                Int32 itemCount = random.Next(1, items.Length + 1);
                List<Int32> indices = Enumerable.Range(0, items.Length).ToList();
                for (Int32 j = 0; j < itemCount; j++)
                {
                    Int32 indexPos = random.Next(0, indices.Count);
                    Int32 index = indices[indexPos];
                    transaction.Add(items[index]);
                    indices.RemoveAt(indexPos);
                }

                transactions.Add(transaction);
            }

            // Using a minSupport of 0.5 so that only itemsets appearing in at least 50% of transactions are considered.
            List<FrequentItemset<string>> result = Apriori.RunApriori(transactions, 0.5);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }
    }
}