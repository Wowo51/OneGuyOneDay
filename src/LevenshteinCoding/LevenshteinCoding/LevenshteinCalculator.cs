using System;

namespace LevenshteinCoding
{
    public static class LevenshteinCalculator
    {
        public static int ComputeDistance(string? source, string? target)
        {
            if (source == null)
            {
                source = "";
            }

            if (target == null)
            {
                target = "";
            }

            int sourceLength = source.Length;
            int targetLength = target.Length;
            int[, ] dp = new int[sourceLength + 1, targetLength + 1];
            for (int i = 0; i <= sourceLength; i++)
            {
                dp[i, 0] = i;
            }

            for (int j = 0; j <= targetLength; j++)
            {
                dp[0, j] = j;
            }

            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
                }
            }

            return dp[sourceLength, targetLength];
        }
    }
}