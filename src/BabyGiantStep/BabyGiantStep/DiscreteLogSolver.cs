using System;
using System.Collections.Generic;
using System.Numerics;

namespace BabyGiantStep
{
    public static class DiscreteLogSolver
    {
        public static BigInteger FindDiscreteLog(BigInteger g, BigInteger h, BigInteger p)
        {
            BigInteger m = BigIntegerExtensions.CeilingSqrt(p - 1);
            Dictionary<BigInteger, BigInteger> babySteps = new Dictionary<BigInteger, BigInteger>();
            BigInteger baby = 1;
            for (BigInteger j = 0; j < m; j++)
            {
                if (!babySteps.ContainsKey(baby))
                {
                    babySteps.Add(baby, j);
                }

                baby = (baby * g) % p;
            }

            BigInteger factor = ModPow(g, m, p);
            factor = InverseMod(factor, p);
            BigInteger giant = h;
            for (BigInteger i = 0; i < m; i++)
            {
                if (babySteps.ContainsKey(giant))
                {
                    return i * m + babySteps[giant];
                }

                giant = (giant * factor) % p;
            }

            return -1;
        }

        public static BigInteger ModPow(BigInteger baseValue, BigInteger exponent, BigInteger modulus)
        {
            BigInteger result = 1;
            baseValue = baseValue % modulus;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = (result * baseValue) % modulus;
                }

                exponent = exponent >> 1;
                baseValue = (baseValue * baseValue) % modulus;
            }

            return result;
        }

        public static BigInteger InverseMod(BigInteger a, BigInteger mod)
        {
            BigInteger m0 = mod;
            BigInteger x0 = 0;
            BigInteger x1 = 1;
            BigInteger q;
            BigInteger temp;
            while (a > 1 && mod != 0)
            {
                q = a / mod;
                temp = mod;
                mod = a % mod;
                a = temp;
                temp = x0;
                x0 = x1 - q * x0;
                x1 = temp;
            }

            if (x1 < 0)
            {
                x1 += m0;
            }

            return x1;
        }

        public static BigInteger Sqrt(BigInteger n)
        {
            if (n < 0)
            {
                return -1;
            }

            if (n == 0)
            {
                return 0;
            }

            BigInteger r = n;
            BigInteger r1 = (r + n / r) >> 1;
            while (r1 < r)
            {
                r = r1;
                r1 = (r + n / r) >> 1;
            }

            return r;
        }
    }

    public static class BigIntegerExtensions
    {
        public static BigInteger Sqrt(BigInteger n)
        {
            return DiscreteLogSolver.Sqrt(n);
        }

        public static BigInteger CeilingSqrt(BigInteger n)
        {
            BigInteger s = Sqrt(n);
            if (s * s < n)
            {
                return s + 1;
            }

            return s;
        }
    }
}