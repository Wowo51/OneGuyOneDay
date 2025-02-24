using System;
using System.Numerics;

namespace KaratsubaAlgorithm
{
    public static class KaratsubaMultiplier
    {
        public static BigInteger Multiply(BigInteger x, BigInteger y)
        {
            // Base case: if either number is small, use conventional multiplication.
            if (x < 10 || y < 10)
            {
                return x * y;
            }

            // Determine the maximum number of digits between x and y.
            int n = Math.Max(x.ToString().Length, y.ToString().Length);
            int m = n / 2;
            BigInteger tenPower = BigInteger.Pow(10, m);
            // Split x and y into high and low parts.
            BigInteger a = x / tenPower;
            BigInteger b = x % tenPower;
            BigInteger c = y / tenPower;
            BigInteger d = y % tenPower;
            // Recursively compute three products.
            BigInteger ac = Multiply(a, c);
            BigInteger bd = Multiply(b, d);
            BigInteger adPlusbc = Multiply(a + b, c + d) - ac - bd;
            // Combine the three products according to Karatsuba's formula.
            return ac * BigInteger.Pow(10, 2 * m) + adPlusbc * tenPower + bd;
        }
    }
}