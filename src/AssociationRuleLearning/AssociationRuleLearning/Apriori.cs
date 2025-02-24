using System;
using System.Collections.Generic;
using System.Linq;

namespace AssociationRuleLearning
{
    public class Apriori
    {
        public static List<AssociationRule> GenerateAssociationRules(List<List<string>> transactions, double minSupport, double minConfidence)
        {
            if (transactions == null)
            {
                return new List<AssociationRule>();
            }

            int totalTransactions = transactions.Count;
            Dictionary<string, int> allFrequent = new Dictionary<string, int>();
            List<List<string>> currentFrequent = new List<List<string>>();
            Dictionary<string, int> frequency1 = new Dictionary<string, int>();
            foreach (List<string> transaction in transactions)
            {
                HashSet<string> distinctItems = new HashSet<string>(transaction);
                foreach (string item in distinctItems)
                {
                    if (frequency1.ContainsKey(item))
                    {
                        frequency1[item] = frequency1[item] + 1;
                    }
                    else
                    {
                        frequency1[item] = 1;
                    }
                }
            }

            foreach (KeyValuePair<string, int> kvp in frequency1)
            {
                List<string> itemset = new List<string>
                {
                    kvp.Key
                };
                itemset.Sort();
                double support = (double)kvp.Value / totalTransactions;
                if (support >= minSupport)
                {
                    currentFrequent.Add(itemset);
                    string key = GetKey(itemset);
                    allFrequent[key] = kvp.Value;
                }
            }

            int k = 2;
            while (currentFrequent.Count > 0)
            {
                List<List<string>> candidates = GenerateCandidates(currentFrequent, k);
                List<List<string>> newFrequent = new List<List<string>>();
                foreach (List<string> candidate in candidates)
                {
                    int count = 0;
                    foreach (List<string> transaction in transactions)
                    {
                        if (IsSubset(candidate, transaction))
                        {
                            count = count + 1;
                        }
                    }

                    double support = (double)count / totalTransactions;
                    if (support >= minSupport)
                    {
                        newFrequent.Add(candidate);
                        string key = GetKey(candidate);
                        allFrequent[key] = count;
                    }
                }

                currentFrequent = newFrequent;
                k = k + 1;
            }

            List<AssociationRule> rules = new List<AssociationRule>();
            foreach (KeyValuePair<string, int> frequentKvp in allFrequent)
            {
                List<string> itemset = frequentKvp.Key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (itemset.Count < 2)
                {
                    continue;
                }

                int itemsetCount = frequentKvp.Value;
                List<List<string>> subsets = GetSubsets(itemset);
                foreach (List<string> antecedent in subsets)
                {
                    if (antecedent.Count == 0 || antecedent.Count == itemset.Count)
                    {
                        continue;
                    }

                    string antecedentKey = GetKey(antecedent);
                    if (!allFrequent.ContainsKey(antecedentKey))
                    {
                        continue;
                    }

                    int antecedentCount = allFrequent[antecedentKey];
                    double confidence = (double)itemsetCount / antecedentCount;
                    if (confidence >= minConfidence)
                    {
                        List<string> consequent = new List<string>(itemset);
                        foreach (string a in antecedent)
                        {
                            consequent.Remove(a);
                        }

                        AssociationRule rule = new AssociationRule
                        {
                            Antecedent = antecedent,
                            Consequent = consequent,
                            Support = (double)itemsetCount / totalTransactions,
                            Confidence = confidence
                        };
                        rules.Add(rule);
                    }
                }
            }

            return rules;
        }

        private static string GetKey(List<string> itemset)
        {
            return string.Join(",", itemset);
        }

        private static List<List<string>> GenerateCandidates(List<List<string>> prevFrequent, int k)
        {
            List<List<string>> candidates = new List<List<string>>();
            int count = prevFrequent.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    List<string> candidate = MergeItemsets(prevFrequent[i], prevFrequent[j], k);
                    if (candidate.Count == k && !candidates.Any(c => AreListsEqual(c, candidate)))
                    {
                        candidates.Add(candidate);
                    }
                }
            }

            return candidates;
        }

        private static List<string> MergeItemsets(List<string> first, List<string> second, int k)
        {
            List<string> candidate = new List<string>();
            bool canMerge = true;
            for (int i = 0; i < k - 2; i++)
            {
                if (first[i] != second[i])
                {
                    canMerge = false;
                    break;
                }
            }

            if (canMerge)
            {
                candidate = new List<string>(first);
                string elementToAdd = second[k - 2];
                if (!candidate.Contains(elementToAdd))
                {
                    candidate.Add(elementToAdd);
                }

                candidate.Sort();
            }

            return candidate;
        }

        private static bool AreListsEqual(List<string> list1, List<string> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsSubset(List<string> candidate, List<string> transaction)
        {
            foreach (string item in candidate)
            {
                if (!transaction.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        private static List<List<string>> GetSubsets(List<string> itemset)
        {
            List<List<string>> subsets = new List<List<string>>();
            int subsetCount = 1 << itemset.Count;
            for (int mask = 1; mask < subsetCount - 1; mask++)
            {
                List<string> subset = new List<string>();
                for (int i = 0; i < itemset.Count; i++)
                {
                    if (((mask >> i) & 1) == 1)
                    {
                        subset.Add(itemset[i]);
                    }
                }

                subsets.Add(subset);
            }

            return subsets;
        }
    }
}