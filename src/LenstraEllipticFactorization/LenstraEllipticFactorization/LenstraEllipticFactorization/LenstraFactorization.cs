using System;
using System.Numerics;

namespace LenstraEllipticFactorization
{
    public static class LenstraFactorization
    {
        public static BigInteger Factor(BigInteger n)
        {
            if (n % 2 == 0)
            {
                return 2;
            }

            BigInteger B = 10000;
            int[] primes = new int[]
            {
                2,
                3,
                5,
                7,
                11,
                13,
                17,
                19,
                23,
                29,
                31,
                37,
                41,
                43,
                47,
                53,
                59,
                61,
                67,
                71,
                73,
                79,
                83,
                89,
                97
            };
            Random random = new Random();
            for (int attempt = 0; attempt < 50; attempt++)
            {
                BigInteger x = new BigInteger(random.Next(2, 100));
                BigInteger y = new BigInteger(random.Next(2, 100));
                BigInteger a = new BigInteger(random.Next(1, 100));
                BigInteger b = (BigInteger.Pow(y, 2) - BigInteger.Pow(x, 3) - a * x) % n;
                if (b < 0)
                {
                    b += n;
                }

                EllipticCurve curve = new EllipticCurve(n, a, b);
                EllipticCurvePoint P = new EllipticCurvePoint(x, y);
                BigInteger factor = 0;
                for (int i = 0; i < primes.Length; i++)
                {
                    int p = primes[i];
                    int exp = (int)Math.Floor(Math.Log((double)B) / Math.Log(p));
                    BigInteger multiplier = BigInteger.Pow(p, exp);
                    P = curve.Multiply(P, multiplier, out factor);
                    if (factor != 0 && factor != 1 && factor != n)
                    {
                        return factor;
                    }
                }
            }

            return n;
        }
    }
}