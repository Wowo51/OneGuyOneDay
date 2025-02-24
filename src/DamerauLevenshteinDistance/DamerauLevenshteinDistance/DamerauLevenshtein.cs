using System;

namespace DamerauLevenshteinDistance
{
    public static class DamerauLevenshtein
    {
        public static int ComputeDistance(string s1, string s2)
        {
            if (s1 == null)
            {
                s1 = "";
            }

            if (s2 == null)
            {
                s2 = "";
            }

            int len1 = s1.Length;
            int len2 = s2.Length;
            int[, ] distance = new int[len1 + 1, len2 + 1];
            for (int i = 0; i <= len1; i++)
            {
                distance[i, 0] = i;
            }

            for (int j = 0; j <= len2; j++)
            {
                distance[0, j] = j;
            }

            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
                    int deletion = distance[i - 1, j] + 1;
                    int insertion = distance[i, j - 1] + 1;
                    int substitution = distance[i - 1, j - 1] + cost;
                    distance[i, j] = Math.Min(Math.Min(deletion, insertion), substitution);
                    if (i > 1 && j > 1 && s1[i - 1] == s2[j - 2] && s1[i - 2] == s2[j - 1])
                    {
                        distance[i, j] = Math.Min(distance[i, j], distance[i - 2, j - 2] + 1);
                    }
                }
            }

            return distance[len1, len2];
        }
    }
}