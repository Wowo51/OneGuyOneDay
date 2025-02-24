using System;
using System.Numerics;

namespace BailliePSWTest
{
    public static class PrimalityTest
    {
        public static bool IsPrime(BigInteger n)
        {
            if (n < 2)
            {
                return false;
            }

            if (n == 2 || n == 3)
            {
                return true;
            }

            if (n % 2 == 0)
            {
                return false;
            }

            int[] smallPrimes = new int[]
            {
                3,
                5,
                7,
                11,
                13,
                17,
                19,
                23,
                29,
                31
            };
            foreach (int p in smallPrimes)
            {
                if (n == p)
                {
                    return true;
                }

                if (n % p == 0)
                {
                    return false;
                }
            }

            if (!MillerRabinTest(n, 2))
            {
                return false;
            }

            // Find D such that Jacobi(D, n) == -1, starting with D = 5 and alternating signs.
            BigInteger D = 5;
            while (Jacobi(D, n) != -1)
            {
                if (D > 0)
                {
                    D = -D - 2;
                }
                else
                {
                    D = -D + 2;
                }
            }

            BigInteger P = 1;
            BigInteger Q = (1 - D) / 4;
            // Write n+1 as d*2^s with d odd.
            BigInteger d = n + 1;
            int s = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            // Lucas U test: if U_d ≡ 0 mod n then n is probable prime.
            BigInteger U = LucasU(n, P, Q, d);
            if (U % n == 0)
            {
                return true;
            }

            // Otherwise check for some r with 0 ≤ r < s whether V_{d*2^r} ≡ 0 mod n.
            for (int r = 0; r < s; r++)
            {
                BigInteger exp = d * BigInteger.Pow(2, r);
                BigInteger V = LucasV(n, P, Q, exp);
                if (V % n == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool MillerRabinTest(BigInteger n, BigInteger baseValue)
        {
            BigInteger d = n - 1;
            int s = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            BigInteger x = BigInteger.ModPow(baseValue, d, n);
            if (x == 1 || x == n - 1)
            {
                return true;
            }

            for (int r = 1; r < s; r++)
            {
                x = (x * x) % n;
                if (x == n - 1)
                {
                    return true;
                }
            }

            return false;
        }

        public static int Jacobi(BigInteger a, BigInteger n)
        {
            if (n <= 0 || n % 2 == 0)
            {
                return 0;
            }

            a = a % n;
            int result = 1;
            while (a != 0)
            {
                while (a % 2 == 0)
                {
                    a /= 2;
                    BigInteger nMod8 = n % 8;
                    if (nMod8 == 3 || nMod8 == 5)
                    {
                        result = -result;
                    }
                }

                BigInteger temp = a;
                a = n;
                n = temp;
                if (a % 4 == 3 && n % 4 == 3)
                {
                    result = -result;
                }

                a %= n;
            }

            return (n == 1) ? result : 0;
        }

        private static BigInteger LucasU(BigInteger mod, BigInteger P, BigInteger Q, BigInteger k)
        {
            if (k == 0)
            {
                return 0;
            }

            if (k == 1)
            {
                return 1;
            }

            BigInteger[, ] M = new BigInteger[2, 2];
            M[0, 0] = P % mod;
            M[0, 1] = (-Q) % mod;
            if (M[0, 1] < 0)
            {
                M[0, 1] += mod;
            }

            M[1, 0] = 1;
            M[1, 1] = 0;
            BigInteger[, ] Mexp = MatrixPower(M, k - 1, mod);
            BigInteger U = Mexp[0, 0] % mod;
            if (U < 0)
            {
                U += mod;
            }

            return U;
        }

        private static BigInteger LucasV(BigInteger mod, BigInteger P, BigInteger Q, BigInteger k)
        {
            if (k == 0)
            {
                return 2 % mod;
            }

            BigInteger U_k = LucasU(mod, P, Q, k);
            BigInteger U_kPlus = LucasU(mod, P, Q, k + 1);
            BigInteger V = (2 * U_kPlus - P * U_k) % mod;
            if (V < 0)
            {
                V += mod;
            }

            return V;
        }

        private static BigInteger[, ] MatrixPower(BigInteger[, ] M, BigInteger exponent, BigInteger mod)
        {
            BigInteger[, ] result = new BigInteger[2, 2];
            result[0, 0] = 1;
            result[0, 1] = 0;
            result[1, 0] = 0;
            result[1, 1] = 1;
            while (exponent > 0)
            {
                if ((exponent & BigInteger.One) == BigInteger.One)
                {
                    result = MatrixMultiply(result, M, mod);
                }

                M = MatrixMultiply(M, M, mod);
                exponent /= 2;
            }

            return result;
        }

        private static BigInteger[, ] MatrixMultiply(BigInteger[, ] A, BigInteger[, ] B, BigInteger mod)
        {
            BigInteger[, ] result = new BigInteger[2, 2];
            result[0, 0] = (A[0, 0] * B[0, 0] + A[0, 1] * B[1, 0]) % mod;
            result[0, 1] = (A[0, 0] * B[0, 1] + A[0, 1] * B[1, 1]) % mod;
            result[1, 0] = (A[1, 0] * B[0, 0] + A[1, 1] * B[1, 0]) % mod;
            result[1, 1] = (A[1, 0] * B[0, 1] + A[1, 1] * B[1, 1]) % mod;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    result[i, j] %= mod;
                    if (result[i, j] < 0)
                    {
                        result[i, j] += mod;
                    }
                }
            }

            return result;
        }
    }
}