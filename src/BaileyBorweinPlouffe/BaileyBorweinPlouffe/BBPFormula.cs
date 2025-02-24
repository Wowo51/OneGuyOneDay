using System;

namespace BaileyBorweinPlouffe
{
    public static class BBPFormula
    {
        public static double Series(int j, int n)
        {
            double sum = 0.0;
            for (int k = 0; k <= n; k++)
            {
                int d = 8 * k + j;
                long p = ModPow(16, n - k, d);
                double term = (double)p / d;
                sum += term;
                sum -= Math.Floor(sum);
            }

            int k2 = n + 1;
            while (true)
            {
                int d = 8 * k2 + j;
                double term = Math.Pow(16, n - k2) / d;
                if (term < 1e-15)
                {
                    break;
                }

                sum += term;
                sum -= Math.Floor(sum);
                k2++;
            }

            return sum;
        }

        public static long ModPow(long baseValue, int exponent, int mod)
        {
            long result = 1;
            long baseMod = baseValue % mod;
            int exp = exponent;
            while (exp > 0)
            {
                if ((exp & 1) == 1)
                {
                    result = (result * baseMod) % mod;
                }

                baseMod = (baseMod * baseMod) % mod;
                exp = exp >> 1;
            }

            return result;
        }

        public static int ComputeHexDigit(int n)
        {
            int nAdjusted = n - 1;
            double s1 = Series(1, nAdjusted);
            double s4 = Series(4, nAdjusted);
            double s5 = Series(5, nAdjusted);
            double s6 = Series(6, nAdjusted);
            double piFraction = 4.0 * s1 - 2.0 * s4 - s5 - s6;
            piFraction -= Math.Floor(piFraction);
            int hexDigit = (int)(piFraction * 16.0);
            return hexDigit;
        }

        public static int ComputeBinaryDigit(int n)
        {
            int hexIndex = ((n - 1) / 4) + 1;
            int hexDigit = ComputeHexDigit(hexIndex);
            int offset = (n - 1) % 4;
            int binaryDigit = (hexDigit >> (3 - offset)) & 1;
            return binaryDigit;
        }
    }
}