using System;

namespace TonelliShanksAlgorithm
{
    public static class TonelliShanks
    {
        public static long? SquareRootModuloPrime(long a, long p)
        {
            a = a % p;
            if (a < 0)
            {
                a += p;
            }

            if (a == 0)
            {
                return 0;
            }

            long ls = PowMod(a, (p - 1) / 2, p);
            if (ls != 1)
            {
                return null;
            }

            if (p % 4 == 3)
            {
                return PowMod(a, (p + 1) / 4, p);
            }

            long S = 0;
            long Q = p - 1;
            while ((Q & 1) == 0)
            {
                S++;
                Q /= 2;
            }

            long z = 2;
            while (PowMod(z, (p - 1) / 2, p) != p - 1)
            {
                z++;
            }

            long c = PowMod(z, Q, p);
            long r = PowMod(a, (Q + 1) / 2, p);
            long t = PowMod(a, Q, p);
            long M = S;
            while (t != 1)
            {
                long i = 0;
                long temp = t;
                while (temp != 1 && i < M)
                {
                    temp = (temp * temp) % p;
                    i++;
                }

                if (i == M)
                {
                    return null;
                }

                long b = PowMod(c, (1L << (int)(M - i - 1)), p);
                r = (r * b) % p;
                t = (t * b * b) % p;
                c = (b * b) % p;
                M = i;
            }

            return r;
        }

        private static long PowMod(long a, long exponent, long mod)
        {
            long result = 1;
            a = a % mod;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = (result * a) % mod;
                }

                exponent /= 2;
                a = (a * a) % mod;
            }

            return result;
        }
    }
}