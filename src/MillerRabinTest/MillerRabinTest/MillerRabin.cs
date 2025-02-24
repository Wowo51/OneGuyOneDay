using System;
using System.Numerics;

namespace MillerRabinTest
{
    public static class MillerRabin
    {
        public static bool IsProbablyPrime(BigInteger number, int iterations)
        {
            if (number < 2)
            {
                return false;
            }

            if (number == 2 || number == 3)
            {
                return true;
            }

            if (number % 2 == 0)
            {
                return false;
            }

            BigInteger d = number - 1;
            int s = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            Random random = new Random();
            for (int i = 0; i < iterations; i++)
            {
                BigInteger a = RandomBigInteger(2, number - 2, random);
                BigInteger x = BigInteger.ModPow(a, d, number);
                if (x == 1 || x == number - 1)
                {
                    continue;
                }

                bool continueOuter = false;
                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, number);
                    if (x == 1)
                    {
                        return false;
                    }

                    if (x == number - 1)
                    {
                        continueOuter = true;
                        break;
                    }
                }

                if (!continueOuter)
                {
                    return false;
                }
            }

            return true;
        }

        private static BigInteger RandomBigInteger(BigInteger min, BigInteger max, Random rng)
        {
            BigInteger diff = max - min + 1;
            int byteLength = (int)Math.Ceiling(BigInteger.Log(diff, 2) / 8.0);
            if (byteLength <= 0)
            {
                byteLength = 1;
            }

            BigInteger result;
            byte[] bytes = new byte[byteLength];
            do
            {
                rng.NextBytes(bytes);
                result = new BigInteger(bytes, isUnsigned: true, isBigEndian: false);
            }
            while (result >= diff);
            return result + min;
        }
    }
}