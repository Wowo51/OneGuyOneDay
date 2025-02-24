using System;
using System.Collections.Generic;

namespace BoyerMooreAlgorithm
{
    public static class BoyerMoore
    {
        public static int Search(string text, string pattern)
        {
            if (text == null || pattern == null)
            {
                return -1;
            }

            int n = text.Length;
            int m = pattern.Length;
            if (m == 0)
            {
                return 0;
            }

            Dictionary<char, int> badChar = BuildBadCharacterTable(pattern);
            int[] goodSuffix = BuildGoodSuffixTable(pattern);
            int s = 0;
            while (s <= n - m)
            {
                int j = m - 1;
                while (j >= 0 && pattern[j] == text[s + j])
                {
                    j--;
                }

                if (j < 0)
                {
                    return s;
                }
                else
                {
                    int bcShift;
                    int occurrence;
                    if (badChar.TryGetValue(text[s + j], out occurrence))
                    {
                        bcShift = j - occurrence;
                    }
                    else
                    {
                        bcShift = j + 1;
                    }

                    if (bcShift < 1)
                    {
                        bcShift = 1;
                    }

                    int shift = Math.Max(bcShift, goodSuffix[j + 1]);
                    s += shift;
                }
            }

            return -1;
        }

        private static Dictionary<char, int> BuildBadCharacterTable(string pattern)
        {
            Dictionary<char, int> table = new Dictionary<char, int>();
            for (int i = 0; i < pattern.Length; i++)
            {
                table[pattern[i]] = i;
            }

            return table;
        }

        private static int[] BuildGoodSuffixTable(string pattern)
        {
            int m = pattern.Length;
            int[] shift = new int[m + 1];
            int[] border = new int[m + 1];
            int i = m;
            int j = m + 1;
            border[i] = j;
            while (i > 0)
            {
                while (j <= m && pattern[i - 1] != pattern[j - 1])
                {
                    if (shift[j] == 0)
                    {
                        shift[j] = j - i;
                    }

                    j = border[j];
                }

                i--;
                j--;
                border[i] = j;
            }

            j = border[0];
            for (i = 0; i <= m; i++)
            {
                if (shift[i] == 0)
                {
                    shift[i] = j;
                }

                if (i == j)
                {
                    j = border[j];
                }
            }

            return shift;
        }
    }
}