using System;

namespace ChainMatrixMultiplication
{
    public class MatrixChainMultiplicationSolver
    {
        public static (int cost, string order) ComputeOptimalOrder(int[] p)
        {
            if (p == null || p.Length < 2)
            {
                return (0, string.Empty);
            }

            if (p.Length == 2)
            {
                return (0, "A1");
            }

            int n = p.Length - 1;
            int[, ] m = new int[n, n];
            int[, ] s = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                m[i, i] = 0;
            }

            for (int L = 2; L <= n; L++)
            {
                for (int i = 0; i <= n - L; i++)
                {
                    int j = i + L - 1;
                    m[i, j] = int.MaxValue;
                    for (int k = i; k < j; k++)
                    {
                        int q = m[i, k] + m[k + 1, j] + p[i] * p[k + 1] * p[j + 1];
                        if (q < m[i, j])
                        {
                            m[i, j] = q;
                            s[i, j] = k;
                        }
                    }
                }
            }

            string order = BuildOptimalOrder(s, 0, n - 1);
            return (m[0, n - 1], order);
        }

        private static string BuildOptimalOrder(int[, ] s, int i, int j)
        {
            if (i == j)
            {
                return "A" + (i + 1).ToString();
            }
            else
            {
                string left = BuildOptimalOrder(s, i, s[i, j]);
                string right = BuildOptimalOrder(s, s[i, j] + 1, j);
                return "(" + left + " x " + right + ")";
            }
        }
    }
}