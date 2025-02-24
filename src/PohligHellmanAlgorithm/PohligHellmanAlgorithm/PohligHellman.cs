using System;
using System.Collections.Generic;
using System.Numerics;

namespace PohligHellmanAlgorithm
{
    public class PohligHellman
    {
        public static BigInteger ComputeDiscreteLog(BigInteger g, BigInteger h, BigInteger p)
        {
            BigInteger n = p - 1;
            Dictionary<BigInteger, int> factors = Factorize(n);
            List<Tuple<BigInteger, BigInteger>> congruences = new List<Tuple<BigInteger, BigInteger>>();
            foreach (KeyValuePair<BigInteger, int> factor in factors)
            {
                BigInteger q = factor.Key;
                int exponent = factor.Value;
                BigInteger modulus = BigInteger.Pow(q, exponent);
                BigInteger subSolution = SolveSubproblem(g, h, p, q, exponent);
                if (subSolution == -1)
                {
                    return -1;
                }

                congruences.Add(new Tuple<BigInteger, BigInteger>(modulus, subSolution));
            }

            BigInteger result = CRT(congruences);
            return result;
        }

        private static Dictionary<BigInteger, int> Factorize(BigInteger n)
        {
            Dictionary<BigInteger, int> factors = new Dictionary<BigInteger, int>();
            int count = 0;
            while (n % 2 == 0)
            {
                count++;
                n /= 2;
            }

            if (count > 0)
            {
                factors.Add(2, count);
            }

            for (BigInteger d = 3; d * d <= n; d += 2)
            {
                count = 0;
                while (n % d == 0)
                {
                    count++;
                    n /= d;
                }

                if (count > 0)
                {
                    factors.Add(d, count);
                }
            }

            if (n > 1)
            {
                factors.Add(n, 1);
            }

            return factors;
        }

        private static BigInteger SolveSubproblem(BigInteger g, BigInteger h, BigInteger p, BigInteger q, int e)
        {
            BigInteger xPartial = 0;
            for (int j = 0; j < e; j++)
            {
                BigInteger exp = (p - 1) / BigInteger.Pow(q, j + 1);
                // Compute g^(xPartial) mod p and its modular inverse.
                BigInteger gXPow = BigInteger.ModPow(g, xPartial, p);
                BigInteger gXPowInv = ModInverse(gXPow, p);
                // Adjust h by removing the contribution of already found digits.
                BigInteger temp = (h * gXPowInv) % p;
                BigInteger expr = BigInteger.ModPow(temp, exp, p);
                BigInteger baseVal = BigInteger.ModPow(g, (p - 1) / q, p);
                int dDigit = -1;
                int intQ = (int)q;
                for (int d = 0; d < intQ; d++)
                {
                    if (BigInteger.ModPow(baseVal, d, p) == expr)
                    {
                        dDigit = d;
                        break;
                    }
                }

                if (dDigit == -1)
                {
                    return -1;
                }

                xPartial += dDigit * BigInteger.Pow(q, j);
            }

            return xPartial;
        }

        private static BigInteger CRT(List<Tuple<BigInteger, BigInteger>> congruences)
        {
            BigInteger M = 1;
            foreach (Tuple<BigInteger, BigInteger> congruence in congruences)
            {
                M *= congruence.Item1;
            }

            BigInteger result = 0;
            foreach (Tuple<BigInteger, BigInteger> congruence in congruences)
            {
                BigInteger m = congruence.Item1;
                BigInteger a = congruence.Item2;
                BigInteger Mi = M / m;
                BigInteger inv = ModInverse(Mi, m);
                result += a * Mi * inv;
            }

            result %= M;
            return result;
        }

        private static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            Tuple<BigInteger, BigInteger, BigInteger> eg = ExtendedGcd(a, m);
            BigInteger gcd = eg.Item1;
            BigInteger x = eg.Item2;
            if (gcd != 1)
            {
                return 0;
            }

            x %= m;
            if (x < 0)
            {
                x += m;
            }

            return x;
        }

        private static Tuple<BigInteger, BigInteger, BigInteger> ExtendedGcd(BigInteger a, BigInteger b)
        {
            if (b == 0)
            {
                return new Tuple<BigInteger, BigInteger, BigInteger>(a, 1, 0);
            }

            Tuple<BigInteger, BigInteger, BigInteger> rec = ExtendedGcd(b, a % b);
            BigInteger gcd = rec.Item1;
            BigInteger x1 = rec.Item2;
            BigInteger y1 = rec.Item3;
            BigInteger x = y1;
            BigInteger y = x1 - (a / b) * y1;
            return new Tuple<BigInteger, BigInteger, BigInteger>(gcd, x, y);
        }
    }
}