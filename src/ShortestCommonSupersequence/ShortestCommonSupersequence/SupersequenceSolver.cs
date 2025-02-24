using System;
using System.Text;

namespace ShortestCommonSupersequence
{
    public static class SupersequenceSolver
    {
        public static string ShortestCommonSupersequence(string sequence1, string sequence2)
        {
            if (sequence1 == null)
            {
                sequence1 = string.Empty;
            }

            if (sequence2 == null)
            {
                sequence2 = string.Empty;
            }

            string lcs = ComputeLCS(sequence1, sequence2);
            StringBuilder result = new StringBuilder();
            int i = 0;
            int j = 0;
            foreach (char c in lcs)
            {
                while (i < sequence1.Length && sequence1[i] != c)
                {
                    result.Append(sequence1[i]);
                    i++;
                }

                while (j < sequence2.Length && sequence2[j] != c)
                {
                    result.Append(sequence2[j]);
                    j++;
                }

                result.Append(c);
                i++;
                j++;
            }

            if (i < sequence1.Length)
            {
                result.Append(sequence1.Substring(i));
            }

            if (j < sequence2.Length)
            {
                result.Append(sequence2.Substring(j));
            }

            return result.ToString();
        }

        private static string ComputeLCS(string sequence1, string sequence2)
        {
            int m = sequence1.Length;
            int n = sequence2.Length;
            int[, ] dp = new int[m + 1, n + 1];
            for (int i = m; i >= 0; i--)
            {
                for (int j = n; j >= 0; j--)
                {
                    if (i == m || j == n)
                    {
                        dp[i, j] = 0;
                    }
                    else if (sequence1[i] == sequence2[j])
                    {
                        dp[i, j] = dp[i + 1, j + 1] + 1;
                    }
                    else
                    {
                        int val1 = dp[i + 1, j];
                        int val2 = dp[i, j + 1];
                        dp[i, j] = (val1 >= val2) ? val1 : val2;
                    }
                }
            }

            StringBuilder lcs = new StringBuilder();
            int x = 0;
            int y = 0;
            while (x < m && y < n)
            {
                if (sequence1[x] == sequence2[y])
                {
                    lcs.Append(sequence1[x]);
                    x++;
                    y++;
                }
                else if (dp[x + 1, y] >= dp[x, y + 1])
                {
                    x++;
                }
                else
                {
                    y++;
                }
            }

            return lcs.ToString();
        }

        public static string ShortestCommonSupersequence(params string[] sequences)
        {
            if (sequences == null || sequences.Length == 0)
            {
                return string.Empty;
            }

            string scs = sequences[0] ?? string.Empty;
            for (int index = 1; index < sequences.Length; index++)
            {
                string nextSequence = sequences[index] ?? string.Empty;
                scs = ShortestCommonSupersequence(scs, nextSequence);
            }

            return scs;
        }
    }
}