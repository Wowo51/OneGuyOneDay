using System;
using System.Collections.Generic;

namespace KnuthMorrisPratt
{
    public class KMPAlgorithm
    {
        public static List<int> Search(string text, string pattern)
        {
            if (text == null || pattern == null)
            {
                return new List<int>();
            }

            if (pattern.Length == 0)
            {
                return new List<int>();
            }

            List<int> indices = new List<int>();
            int[] lps = ComputeLPSArray(pattern);
            int i = 0;
            int j = 0;
            while (i < text.Length)
            {
                if (pattern[j] == text[i])
                {
                    i++;
                    j++;
                }

                if (j == pattern.Length)
                {
                    indices.Add(i - j);
                    j = lps[j - 1];
                }
                else if (i < text.Length && pattern[j] != text[i])
                {
                    if (j != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            return indices;
        }

        private static int[] ComputeLPSArray(string pattern)
        {
            int[] lps = new int[pattern.Length];
            int length = 0;
            lps[0] = 0;
            int i = 1;
            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[length])
                {
                    length++;
                    lps[i] = length;
                    i++;
                }
                else
                {
                    if (length != 0)
                    {
                        length = lps[length - 1];
                    }
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }

            return lps;
        }
    }
}