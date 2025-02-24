using System;
using System.Collections.Generic;
using System.Linq;

namespace AprioriAlgorithm
{
    public class FrequentItemset<T>
        where T : notnull
    {
        public HashSet<T> Items { get; set; }
        public int Support { get; set; }

        public FrequentItemset(HashSet<T> items, int support)
        {
            Items = items ?? new HashSet<T>();
            Support = support;
        }
    }

    public class Apriori
    {
        public static List<FrequentItemset<T>> RunApriori<T>(List<HashSet<T>> transactions, double minSupport)
            where T : notnull
        {
            List<FrequentItemset<T>> allFrequentItemsets = new List<FrequentItemset<T>>();
            if (transactions == null)
            {
                return allFrequentItemsets;
            }

            int transactionCount = transactions.Count;
            int supportThreshold = (int)Math.Ceiling(minSupport * transactionCount);
            Dictionary<T, int> itemFrequencies = new Dictionary<T, int>();
            foreach (HashSet<T> transaction in transactions)
            {
                if (transaction != null)
                {
                    foreach (T item in transaction)
                    {
                        if (!itemFrequencies.ContainsKey(item))
                        {
                            itemFrequencies[item] = 0;
                        }

                        itemFrequencies[item] = itemFrequencies[item] + 1;
                    }
                }
            }

            List<FrequentItemset<T>> currentFrequentItemsets = new List<FrequentItemset<T>>();
            foreach (KeyValuePair<T, int> kvp in itemFrequencies)
            {
                if (kvp.Value >= supportThreshold)
                {
                    HashSet<T> items = new HashSet<T>();
                    items.Add(kvp.Key);
                    FrequentItemset<T> itemset = new FrequentItemset<T>(items, kvp.Value);
                    currentFrequentItemsets.Add(itemset);
                }
            }

            allFrequentItemsets.AddRange(currentFrequentItemsets);
            while (currentFrequentItemsets.Count > 0)
            {
                List<FrequentItemset<T>> candidateItemsets = new List<FrequentItemset<T>>();
                int currentSize = currentFrequentItemsets[0].Items.Count;
                for (int i = 0; i < currentFrequentItemsets.Count; i++)
                {
                    for (int j = i + 1; j < currentFrequentItemsets.Count; j++)
                    {
                        HashSet<T> unionItems = new HashSet<T>(currentFrequentItemsets[i].Items);
                        unionItems.UnionWith(currentFrequentItemsets[j].Items);
                        if (unionItems.Count == currentSize + 1)
                        {
                            bool alreadyExists = false;
                            foreach (FrequentItemset<T> candidate in candidateItemsets)
                            {
                                if (candidate.Items.SetEquals(unionItems))
                                {
                                    alreadyExists = true;
                                    break;
                                }
                            }

                            if (!alreadyExists)
                            {
                                int supportCount = 0;
                                foreach (HashSet<T> transaction in transactions)
                                {
                                    if (transaction != null && unionItems.IsSubsetOf(transaction))
                                    {
                                        supportCount = supportCount + 1;
                                    }
                                }

                                if (supportCount >= supportThreshold)
                                {
                                    FrequentItemset<T> candidateItemset = new FrequentItemset<T>(unionItems, supportCount);
                                    candidateItemsets.Add(candidateItemset);
                                }
                            }
                        }
                    }
                }

                currentFrequentItemsets = candidateItemsets;
                allFrequentItemsets.AddRange(currentFrequentItemsets);
            }

            return allFrequentItemsets;
        }
    }
}