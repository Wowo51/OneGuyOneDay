using System;

namespace StringMetrics
{
    public static class StringMetricCalculator
    {
        public static int ComputeLevenshteinDistance(string s, string t)
        {
            if (s == null)
            {
                s = "";
            }

            if (t == null)
            {
                t = "";
            }

            int n = s.Length;
            int m = t.Length;
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            int[, ] distance = new int[n + 1, m + 1];
            for (int i = 0; i <= n; i++)
            {
                distance[i, 0] = i;
            }

            for (int j = 0; j <= m; j++)
            {
                distance[0, j] = j;
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    int insertion = distance[i, j - 1] + 1;
                    int deletion = distance[i - 1, j] + 1;
                    int substitution = distance[i - 1, j - 1] + cost;
                    distance[i, j] = Math.Min(Math.Min(insertion, deletion), substitution);
                }
            }

            return distance[n, m];
        }

        public static double ComputeNormalizedSimilarity(string s, string t)
        {
            if (s == null)
            {
                s = "";
            }

            if (t == null)
            {
                t = "";
            }

            int distance = ComputeLevenshteinDistance(s, t);
            int maxLength = Math.Max(s.Length, t.Length);
            if (maxLength == 0)
            {
                return 1.0;
            }

            return 1.0 - ((double)distance / (double)maxLength);
        }
    }
}