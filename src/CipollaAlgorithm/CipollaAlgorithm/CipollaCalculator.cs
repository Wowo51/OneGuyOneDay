using System;

namespace CipollaAlgorithm
{
    public static class CipollaCalculator
    {
        public static int? ComputeSquareRoot(int n, int p)
        {
            n = n % p;
            if (n < 0)
            {
                n += p;
            }

            if (n == 0)
            {
                return 0;
            }

            if (LegendreSymbol(n, p) != 1)
            {
                return null;
            }

            int a = 0;
            int w = 0;
            for (int i = 1; i < p; i++)
            {
                int t = (i * i - n) % p;
                if (t < 0)
                {
                    t += p;
                }

                if (LegendreSymbol(t, p) == -1)
                {
                    a = i;
                    w = t;
                    break;
                }
            }

            (int r, int s) = Power((a, 1), (p + 1) / 2, w, p);
            return r;
        }

        private static int LegendreSymbol(int a, int p)
        {
            int r = ModExp(a, (p - 1) / 2, p);
            return (r == p - 1) ? -1 : r;
        }

        private static int ModExp(int baseValue, int exp, int mod)
        {
            int result = 1;
            int b = baseValue % mod;
            while (exp > 0)
            {
                if ((exp & 1) == 1)
                {
                    result = (result * b) % mod;
                }

                b = (b * b) % mod;
                exp >>= 1;
            }

            return result;
        }

        private static (int, int) Multiply((int, int) u, (int, int) v, int w, int p)
        {
            int term1 = (u.Item1 * v.Item1) % p;
            int term2 = (((u.Item2 * v.Item2) % p) * w) % p;
            int x = (term1 + term2) % p;
            if (x < 0)
            {
                x += p;
            }

            int y = (u.Item1 * v.Item2 + u.Item2 * v.Item1) % p;
            if (y < 0)
            {
                y += p;
            }

            return (x, y);
        }

        private static (int, int) Power((int, int) u, int exp, int w, int p)
        {
            (int, int) result = (1, 0);
            (int, int) baseVal = u;
            while (exp > 0)
            {
                if ((exp & 1) == 1)
                {
                    result = Multiply(result, baseVal, w, p);
                }

                baseVal = Multiply(baseVal, baseVal, w, p);
                exp >>= 1;
            }

            return result;
        }
    }
}