using System;
using System.Collections.Generic;

namespace QuineMcCluskeyAlgorithm
{
    public class QuineMcCluskey
    {
        public static List<string> Simplify(List<int> minterms, List<int> dontCares, int variableCount)
        {
            if (minterms == null)
            {
                minterms = new List<int>();
            }

            if (dontCares == null)
            {
                dontCares = new List<int>();
            }

            List<int> allTerms = new List<int>();
            for (int i = 0; i < minterms.Count; i++)
            {
                allTerms.Add(minterms[i]);
            }

            for (int i = 0; i < dontCares.Count; i++)
            {
                if (!allTerms.Contains(dontCares[i]))
                {
                    allTerms.Add(dontCares[i]);
                }
            }

            allTerms.Sort();
            List<BooleanTerm> terms = new List<BooleanTerm>();
            for (int i = 0; i < allTerms.Count; i++)
            {
                int termValue = allTerms[i];
                string binary = ToBinaryString(termValue, variableCount);
                BooleanTerm bt = new BooleanTerm(binary, new List<int> { termValue });
                terms.Add(bt);
            }

            List<BooleanTerm> primeImplicants = new List<BooleanTerm>();
            List<BooleanTerm> currentTerms = terms;
            bool combinationOccurred = false;
            do
            {
                combinationOccurred = false;
                List<BooleanTerm> combinedThisRound = new List<BooleanTerm>();
                var groups = new Dictionary<int, List<BooleanTerm>>();
                for (int i = 0; i < currentTerms.Count; i++)
                {
                    int onesCount = currentTerms[i].CountOnes();
                    if (!groups.ContainsKey(onesCount))
                    {
                        groups[onesCount] = new List<BooleanTerm>();
                    }

                    groups[onesCount].Add(currentTerms[i]);
                }

                List<int> sortedKeys = new List<int>(groups.Keys);
                sortedKeys.Sort();
                for (int i = 0; i < sortedKeys.Count - 1; i++)
                {
                    if (!groups.ContainsKey(sortedKeys[i]) || !groups.ContainsKey(sortedKeys[i + 1]))
                    {
                        continue;
                    }

                    List<BooleanTerm> group1 = groups[sortedKeys[i]];
                    List<BooleanTerm> group2 = groups[sortedKeys[i + 1]];
                    for (int j = 0; j < group1.Count; j++)
                    {
                        for (int k = 0; k < group2.Count; k++)
                        {
                            BooleanTerm combinedTerm = group1[j].Combine(group2[k]);
                            if (combinedTerm != null)
                            {
                                BooleanTermComparer comparer = new BooleanTermComparer();
                                if (!combinedThisRound.Contains(combinedTerm, comparer))
                                {
                                    combinedThisRound.Add(combinedTerm);
                                }

                                group1[j].MarkCombined();
                                group2[k].MarkCombined();
                                combinationOccurred = true;
                            }
                        }
                    }
                }

                for (int i = 0; i < currentTerms.Count; i++)
                {
                    if (!currentTerms[i].IsCombined)
                    {
                        BooleanTermComparer comparer = new BooleanTermComparer();
                        if (!primeImplicants.Contains(currentTerms[i], comparer))
                        {
                            primeImplicants.Add(currentTerms[i]);
                        }
                    }
                }

                currentTerms = combinedThisRound;
            }
            while (combinationOccurred && currentTerms.Count > 0);
            List<int> uncoveredMinterms = new List<int>();
            for (int i = 0; i < minterms.Count; i++)
            {
                uncoveredMinterms.Add(minterms[i]);
            }

            List<BooleanTerm> essentialPrimeImplicants = new List<BooleanTerm>();
            var chart = new Dictionary<int, List<BooleanTerm>>();
            for (int i = 0; i < uncoveredMinterms.Count; i++)
            {
                int m = uncoveredMinterms[i];
                chart[m] = new List<BooleanTerm>();
                for (int j = 0; j < primeImplicants.Count; j++)
                {
                    if (primeImplicants[j].Minterms.Contains(m))
                    {
                        chart[m].Add(primeImplicants[j]);
                    }
                }
            }

            bool chartChanged = true;
            while (chartChanged)
            {
                chartChanged = false;
                List<int> mintermsToRemove = new List<int>();
                foreach (int m in new List<int>(chart.Keys))
                {
                    if (chart[m].Count == 1)
                    {
                        BooleanTerm essential = chart[m][0];
                        BooleanTermComparer comparer = new BooleanTermComparer();
                        if (!essentialPrimeImplicants.Contains(essential, comparer))
                        {
                            essentialPrimeImplicants.Add(essential);
                            chartChanged = true;
                        }

                        for (int i = 0; i < essential.Minterms.Count; i++)
                        {
                            int covered = essential.Minterms[i];
                            if (chart.ContainsKey(covered))
                            {
                                mintermsToRemove.Add(covered);
                            }
                        }
                    }
                }

                for (int i = 0; i < mintermsToRemove.Count; i++)
                {
                    int key = mintermsToRemove[i];
                    if (chart.ContainsKey(key))
                    {
                        chart.Remove(key);
                    }
                }

                foreach (int key in chart.Keys)
                {
                    chart[key].RemoveAll(delegate (BooleanTerm bt)
                    {
                        BooleanTermComparer comparer = new BooleanTermComparer();
                        return essentialPrimeImplicants.Contains(bt, comparer);
                    });
                }
            }

            while (chart.Count > 0)
            {
                int bestCoverage = -1;
                BooleanTerm bestTerm = null;
                for (int i = 0; i < primeImplicants.Count; i++)
                {
                    int coverage = 0;
                    foreach (int m in chart.Keys)
                    {
                        if (primeImplicants[i].Minterms.Contains(m))
                        {
                            coverage++;
                        }
                    }

                    if (coverage > bestCoverage)
                    {
                        bestCoverage = coverage;
                        bestTerm = primeImplicants[i];
                    }
                }

                if (bestTerm != null)
                {
                    essentialPrimeImplicants.Add(bestTerm);
                    List<int> keysToRemove = new List<int>();
                    foreach (int m in new List<int>(chart.Keys))
                    {
                        if (bestTerm.Minterms.Contains(m))
                        {
                            keysToRemove.Add(m);
                        }
                    }

                    for (int i = 0; i < keysToRemove.Count; i++)
                    {
                        int key = keysToRemove[i];
                        if (chart.ContainsKey(key))
                        {
                            chart.Remove(key);
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            List<string> result = new List<string>();
            for (int i = 0; i < essentialPrimeImplicants.Count; i++)
            {
                result.Add(essentialPrimeImplicants[i].Term);
            }

            return result;
        }

        public static string ToBinaryString(int value, int length)
        {
            char[] bits = new char[length];
            for (int i = length - 1; i >= 0; i--)
            {
                bits[i] = (value % 2 == 1) ? '1' : '0';
                value = value / 2;
            }

            return new string (bits);
        }
    }
}