using System;
using System.Text;

namespace HirschbergsAlgorithm
{
    public class AlignmentResult
    {
        public string AlignedSequence1 { get; set; } = "";
        public string AlignedSequence2 { get; set; } = "";
        public int Cost { get; set; }
    }

    public static class HirschbergsAlgorithm
    {
        public static AlignmentResult Align(string seq1, string seq2)
        {
            (string alignedSeq1, string alignedSeq2) = ComputeHirschberg(seq1, seq2);
            int cost = CalculateCost(alignedSeq1, alignedSeq2);
            return new AlignmentResult
            {
                AlignedSequence1 = alignedSeq1,
                AlignedSequence2 = alignedSeq2,
                Cost = cost
            };
        }

        private static int CalculateCost(string s1, string s2)
        {
            int cost = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    cost++;
                }
            }

            return cost;
        }

        private static (string, string) ComputeHirschberg(string A, string B)
        {
            if (A.Length == 0)
            {
                return (new string ('-', B.Length), B);
            }

            if (B.Length == 0)
            {
                return (A, new string ('-', A.Length));
            }

            if (A.Length == 1 || B.Length == 1)
            {
                return NeedlemanWunsch(A, B);
            }

            int i = A.Length / 2;
            string A_left = A.Substring(0, i);
            string A_right = A.Substring(i);
            int[] leftCosts = ComputeCost(A_left, B);
            int[] rightCosts = ComputeCostReverse(A_right, B);
            int m = B.Length;
            int minCost = int.MaxValue;
            int partition = 0;
            for (int j = 0; j <= m; j++)
            {
                int costValue = leftCosts[j] + rightCosts[m - j];
                if (costValue < minCost)
                {
                    minCost = costValue;
                    partition = j;
                }
            }

            (string leftAlignedA, string leftAlignedB) = ComputeHirschberg(A_left, B.Substring(0, partition));
            (string rightAlignedA, string rightAlignedB) = ComputeHirschberg(A_right, B.Substring(partition));
            return (leftAlignedA + rightAlignedA, leftAlignedB + rightAlignedB);
        }

        private static int[] ComputeCost(string A, string B)
        {
            int m = B.Length;
            int[] dp = new int[m + 1];
            for (int j = 0; j <= m; j++)
            {
                dp[j] = j;
            }

            for (int i = 1; i <= A.Length; i++)
            {
                int previous = dp[0];
                dp[0] = i;
                for (int j = 1; j <= m; j++)
                {
                    int temp = dp[j];
                    int cost = (A[i - 1] == B[j - 1]) ? 0 : 1;
                    dp[j] = Math.Min(dp[j - 1] + 1, Math.Min(previous + cost, dp[j] + 1));
                    previous = temp;
                }
            }

            return dp;
        }

        private static int[] ComputeCostReverse(string A, string B)
        {
            string rA = Reverse(A);
            string rB = Reverse(B);
            int[] revCosts = ComputeCost(rA, rB);
            return revCosts;
        }

        private static (string, string) NeedlemanWunsch(string A, string B)
        {
            int n = A.Length;
            int m = B.Length;
            int[, ] dp = new int[n + 1, m + 1];
            for (int i = 0; i <= n; i++)
            {
                dp[i, 0] = i;
            }

            for (int j = 0; j <= m; j++)
            {
                dp[0, j] = j;
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (A[i - 1] == B[j - 1]) ? 0 : 1;
                    int deletion = dp[i - 1, j] + 1;
                    int insertion = dp[i, j - 1] + 1;
                    int diagonal = dp[i - 1, j - 1] + cost;
                    dp[i, j] = Math.Min(deletion, Math.Min(insertion, diagonal));
                }
            }

            StringBuilder alignedA = new StringBuilder();
            StringBuilder alignedB = new StringBuilder();
            int iPos = n;
            int jPos = m;
            while (iPos > 0 && jPos > 0)
            {
                int current = dp[iPos, jPos];
                int diag = dp[iPos - 1, jPos - 1];
                int up = dp[iPos - 1, jPos];
                int left = dp[iPos, jPos - 1];
                int cost = (A[iPos - 1] == B[jPos - 1]) ? 0 : 1;
                if (iPos > 0 && jPos > 0 && current == diag + cost)
                {
                    alignedA.Append(A[iPos - 1]);
                    alignedB.Append(B[jPos - 1]);
                    iPos--;
                    jPos--;
                }
                else if (iPos > 0 && current == up + 1)
                {
                    alignedA.Append(A[iPos - 1]);
                    alignedB.Append('-');
                    iPos--;
                }
                else
                {
                    alignedA.Append('-');
                    alignedB.Append(B[jPos - 1]);
                    jPos--;
                }
            }

            while (iPos > 0)
            {
                alignedA.Append(A[iPos - 1]);
                alignedB.Append('-');
                iPos--;
            }

            while (jPos > 0)
            {
                alignedA.Append('-');
                alignedB.Append(B[jPos - 1]);
                jPos--;
            }

            char[] arrA = alignedA.ToString().ToCharArray();
            char[] arrB = alignedB.ToString().ToCharArray();
            Array.Reverse(arrA);
            Array.Reverse(arrB);
            return (new string (arrA), new string (arrB));
        }

        private static string Reverse(string s)
        {
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string (array);
        }
    }
}