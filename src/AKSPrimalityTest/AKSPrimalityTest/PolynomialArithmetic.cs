using System.Numerics;

namespace AKSPrimalityTest
{
    public static class PolynomialArithmetic
    {
        public static BigInteger[] CreatePoly(int a, int r)
        {
            BigInteger[] poly = new BigInteger[r];
            poly[0] = a;
            if (r > 1)
            {
                poly[1] = 1;
            }

            return poly;
        }

        public static BigInteger[] Multiply(BigInteger[] poly1, BigInteger[] poly2, int r, BigInteger mod)
        {
            BigInteger[] result = new BigInteger[r];
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    int index = (i + j) % r;
                    result[index] = (result[index] + (poly1[i] * poly2[j]) % mod) % mod;
                }
            }

            return result;
        }

        public static BigInteger[] ModExp(BigInteger[] basePoly, BigInteger exponent, int r, BigInteger mod)
        {
            BigInteger[] result = new BigInteger[r];
            result[0] = 1;
            BigInteger[] baseCopy = basePoly;
            BigInteger exp = exponent;
            while (exp > 0)
            {
                if (exp % 2 == 1)
                {
                    result = Multiply(result, baseCopy, r, mod);
                }

                baseCopy = Multiply(baseCopy, baseCopy, r, mod);
                exp = exp >> 1;
            }

            return result;
        }

        public static bool AreEqual(BigInteger[] poly1, BigInteger[] poly2, BigInteger mod)
        {
            if (poly1.Length != poly2.Length)
            {
                return false;
            }

            for (int i = 0; i < poly1.Length; i++)
            {
                BigInteger coeff1 = ((poly1[i] % mod) + mod) % mod;
                BigInteger coeff2 = ((poly2[i] % mod) + mod) % mod;
                if (coeff1 != coeff2)
                {
                    return false;
                }
            }

            return true;
        }
    }
}