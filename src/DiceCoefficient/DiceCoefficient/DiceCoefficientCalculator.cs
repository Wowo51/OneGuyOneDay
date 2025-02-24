using System;
using System.Collections.Generic;

namespace DiceCoefficient
{
    public static class DiceCoefficientCalculator
    {
        public static double Calculate(string s1, string s2)
        {
            if (s1 == null)
            {
                s1 = "";
            }

            if (s2 == null)
            {
                s2 = "";
            }

            if (s1.Length < 2 || s2.Length < 2)
            {
                if (s1.Equals(s2, StringComparison.Ordinal))
                {
                    return 1.0;
                }
                else
                {
                    return 0.0;
                }
            }

            Dictionary<string, int> bigrams1 = new Dictionary<string, int>();
            for (int index = 0; index < s1.Length - 1; index++)
            {
                string bigram = s1.Substring(index, 2);
                if (bigrams1.ContainsKey(bigram))
                {
                    bigrams1[bigram]++;
                }
                else
                {
                    bigrams1[bigram] = 1;
                }
            }

            Dictionary<string, int> bigrams2 = new Dictionary<string, int>();
            for (int index = 0; index < s2.Length - 1; index++)
            {
                string bigram = s2.Substring(index, 2);
                if (bigrams2.ContainsKey(bigram))
                {
                    bigrams2[bigram]++;
                }
                else
                {
                    bigrams2[bigram] = 1;
                }
            }

            int intersectionCount = 0;
            foreach (KeyValuePair<string, int> entry in bigrams1)
            {
                if (bigrams2.ContainsKey(entry.Key))
                {
                    intersectionCount += Math.Min(entry.Value, bigrams2[entry.Key]);
                }
            }

            int totalBigrams = (s1.Length - 1) + (s2.Length - 1);
            double diceCoefficient = (2.0 * intersectionCount) / totalBigrams;
            return diceCoefficient;
        }
    }
}