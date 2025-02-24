using System.Collections.Generic;

namespace LongestCommonSubsequence
{
    public static class LongestCommonSubsequenceSolver
    {
        public static string ComputeLCS(IEnumerable<string> sequences)
        {
            if (sequences == null)
            {
                return string.Empty;
            }

            List<string> sequenceList = new List<string>(sequences);
            if (sequenceList.Count == 0)
            {
                return string.Empty;
            }

            string commonSubsequence = sequenceList[0] ?? "";
            for (int i = 1; i < sequenceList.Count; i++)
            {
                commonSubsequence = LCSForTwo(commonSubsequence, sequenceList[i] ?? "");
                if (commonSubsequence.Length == 0)
                {
                    return string.Empty;
                }
            }

            return commonSubsequence;
        }

        private static string LCSForTwo(string firstSequence, string secondSequence)
        {
            int firstLen = firstSequence.Length;
            int secondLen = secondSequence.Length;
            int[, ] dp = new int[firstLen + 1, secondLen + 1];
            for (int i = 0; i < firstLen; i++)
            {
                for (int j = 0; j < secondLen; j++)
                {
                    if (firstSequence[i] == secondSequence[j])
                    {
                        dp[i + 1, j + 1] = dp[i, j] + 1;
                    }
                    else
                    {
                        dp[i + 1, j + 1] = dp[i, j + 1] > dp[i + 1, j] ? dp[i, j + 1] : dp[i + 1, j];
                    }
                }
            }

            int index = dp[firstLen, secondLen];
            char[] lcs = new char[index];
            int a = firstLen;
            int b = secondLen;
            while (a > 0 && b > 0)
            {
                if (firstSequence[a - 1] == secondSequence[b - 1])
                {
                    lcs[index - 1] = firstSequence[a - 1];
                    a = a - 1;
                    b = b - 1;
                    index = index - 1;
                }
                else if (dp[a - 1, b] >= dp[a, b - 1])
                {
                    a = a - 1;
                }
                else
                {
                    b = b - 1;
                }
            }

            return new string (lcs);
        }
    }
}