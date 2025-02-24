namespace LongestIncreasingSubsequence
{
    public static class LongestIncreasingSubsequenceCalculator
    {
        public static int[] FindLIS(int[] sequence)
        {
            if (sequence == null)
            {
                return new int[0];
            }

            int n = sequence.Length;
            if (n == 0)
            {
                return new int[0];
            }

            int[] dp = new int[n];
            int[] prev = new int[n];
            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
                prev[i] = -1;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (sequence[j] < sequence[i] && dp[j] + 1 > dp[i])
                    {
                        dp[i] = dp[j] + 1;
                        prev[i] = j;
                    }
                }
            }

            int maxLength = dp[0];
            int maxIndex = 0;
            for (int i = 1; i < n; i++)
            {
                if (dp[i] > maxLength)
                {
                    maxLength = dp[i];
                    maxIndex = i;
                }
            }

            int[] lis = new int[maxLength];
            int k = maxLength - 1;
            int index = maxIndex;
            while (index != -1)
            {
                lis[k] = sequence[index];
                k--;
                index = prev[index];
            }

            return lis;
        }
    }
}