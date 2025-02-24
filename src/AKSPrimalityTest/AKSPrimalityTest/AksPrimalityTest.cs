using System;
using System.Numerics;

namespace AKSPrimalityTest
{
    public static class AksPrimalityTest
    {
        public static bool IsPrime(BigInteger number)
        {
            if (number < 2)
            {
                return false;
            }

            if (number == 2 || number == 3)
            {
                return true;
            }

            if (IsPerfectPower(number))
            {
                return false;
            }

            int r = FindSuitableR(number);
            for (int a = 2; a <= r; a++)
            {
                BigInteger gcd = BigInteger.GreatestCommonDivisor(number, new BigInteger(a));
                if (gcd > 1 && gcd < number)
                {
                    return false;
                }
            }

            if (number <= r)
            {
                return true;
            }

            int totient = EulerTotient(r);
            double limit = Math.Floor(Math.Sqrt(totient) * BigInteger.Log(number));
            for (int a = 1; a <= limit; a++)
            {
                BigInteger[] poly = PolynomialArithmetic.ModExp(PolynomialArithmetic.CreatePoly(a, r), number, r, number);
                BigInteger[] expected = new BigInteger[r];
                int expIndex = (int)(number % r);
                expected[expIndex] = 1;
                expected[0] = (expected[0] + a) % number;
                if (!PolynomialArithmetic.AreEqual(poly, expected, number))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsPerfectPower(BigInteger number)
        {
            int maxExponent = (int)Math.Ceiling(BigInteger.Log(number, 2));
            for (int b = 2; b <= maxExponent; b++)
            {
                BigInteger root = KthRoot(number, b);
                if (BigInteger.Pow(root, b) == number || BigInteger.Pow(root + 1, b) == number)
                {
                    return true;
                }
            }

            return false;
        }

        private static int FindSuitableR(BigInteger number)
        {
            double log2n = BigInteger.Log(number, 2);
            double limit = log2n * log2n;
            int r = 2;
            while (true)
            {
                if (BigInteger.GreatestCommonDivisor(number, new BigInteger(r)) == 1)
                {
                    int order = MultiplicativeOrder(number, r);
                    if (order > limit)
                    {
                        break;
                    }
                }

                r++;
            }

            return r;
        }

        private static int MultiplicativeOrder(BigInteger number, int r)
        {
            int order = 1;
            BigInteger result = number % r;
            while (result != 1)
            {
                result = (result * number) % r;
                order++;
                if (order > r)
                {
                    break;
                }
            }

            return order;
        }

        private static BigInteger KthRoot(BigInteger number, int k)
        {
            BigInteger low = BigInteger.One;
            BigInteger high = number;
            while (low <= high)
            {
                BigInteger mid = (low + high) / 2;
                BigInteger midPow = BigInteger.Pow(mid, k);
                if (midPow == number)
                {
                    return mid;
                }
                else if (midPow < number)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return high;
        }

        private static int EulerTotient(int r)
        {
            int totient = r;
            int temp = r;
            for (int p = 2; p * p <= temp; p++)
            {
                if (temp % p == 0)
                {
                    while (temp % p == 0)
                    {
                        temp /= p;
                    }

                    totient -= totient / p;
                }
            }

            if (temp > 1)
            {
                totient -= totient / temp;
            }

            return totient;
        }
    }
}