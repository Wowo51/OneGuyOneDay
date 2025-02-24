using System;
using System.Numerics;
using System.Collections.Generic;

namespace DixonAlgorithm
{
    public class Dixon
    {
        public static (BigInteger Factor1, BigInteger Factor2) Factor(BigInteger n)
        {
            if (n < new BigInteger(2))
            {
                return (n, BigInteger.One);
            }

            if (n % new BigInteger(2) == BigInteger.Zero)
            {
                return (new BigInteger(2), n / new BigInteger(2));
            }

            if (IsPrime(n))
            {
                return (n, BigInteger.One);
            }

            List<BigInteger> factorBase = GetFactorBase(n);
            // Quick trial division using the small primes in the factor base (skip -1).
            for (int i = 1; i < factorBase.Count; i++)
            {
                if (n % factorBase[i] == BigInteger.Zero)
                {
                    return (factorBase[i], n / factorBase[i]);
                }
            }

            List<Relation> relations = new List<Relation>();
            BigInteger sqrtN = Sqrt(n);
            BigInteger x = sqrtN + BigInteger.One;
            // Collect at least factorBase.Count + 1 relations.
            while (relations.Count < factorBase.Count + 1)
            {
                BigInteger Q = x * x - n;
                int[] fullExponents = new int[factorBase.Count];
                BigInteger temp = Q;
                if (temp < BigInteger.Zero)
                {
                    fullExponents[0] = 1;
                    temp = BigInteger.Abs(temp);
                }

                for (int i = 1; i < factorBase.Count; i++)
                {
                    BigInteger p = factorBase[i];
                    while (temp % p == BigInteger.Zero)
                    {
                        fullExponents[i]++;
                        temp /= p;
                    }
                }

                if (temp == BigInteger.One)
                {
                    int[] mod2Exponents = new int[factorBase.Count];
                    for (int i = 0; i < factorBase.Count; i++)
                    {
                        mod2Exponents[i] = fullExponents[i] % 2;
                    }

                    relations.Add(new Relation(x, fullExponents, mod2Exponents));
                }

                x = x + BigInteger.One;
            }

            List<int>? dependency = FindDependency(relations, factorBase.Count);
            if (dependency == null)
            {
                return (n, BigInteger.One);
            }

            BigInteger a = BigInteger.One;
            int[] combinedExponents = new int[factorBase.Count];
            for (int i = 0; i < dependency.Count; i++)
            {
                int index = dependency[i];
                Relation rel = relations[index];
                a = a * rel.X;
                for (int j = 0; j < factorBase.Count; j++)
                {
                    combinedExponents[j] += rel.FullExponents[j];
                }
            }

            BigInteger b = BigInteger.One;
            for (int i = 0; i < factorBase.Count; i++)
            {
                int halfExp = combinedExponents[i] / 2;
                if (halfExp > 0)
                {
                    b = b * BigInteger.Pow(factorBase[i], halfExp);
                }
            }

            BigInteger factorCandidate = BigInteger.GreatestCommonDivisor(a - b, n);
            if (factorCandidate == BigInteger.One || factorCandidate == n)
            {
                factorCandidate = BigInteger.GreatestCommonDivisor(a + b, n);
                if (factorCandidate == BigInteger.One || factorCandidate == n)
                {
                    return (n, BigInteger.One);
                }
                else
                {
                    return (factorCandidate, n / factorCandidate);
                }
            }

            return (factorCandidate, n / factorCandidate);
        }

        private static BigInteger Sqrt(BigInteger n)
        {
            if (n < BigInteger.Zero)
            {
                return -new BigInteger(1);
            }

            BigInteger low = BigInteger.Zero;
            BigInteger high = n;
            BigInteger mid = BigInteger.Zero;
            while (low <= high)
            {
                mid = (low + high) / new BigInteger(2);
                BigInteger midSquared = mid * mid;
                if (midSquared <= n && (mid + BigInteger.One) * (mid + BigInteger.One) > n)
                {
                    return mid;
                }
                else if (midSquared < n)
                {
                    low = mid + BigInteger.One;
                }
                else
                {
                    high = mid - BigInteger.One;
                }
            }

            return mid;
        }

        private static List<int>? FindDependency(List<Relation> relations, int vectorLength)
        {
            int relationCount = relations.Count;
            int totalSubsets = 1 << relationCount;
            for (int mask = 1; mask < totalSubsets; mask++)
            {
                int[] sumVector = new int[vectorLength];
                List<int> indices = new List<int>();
                for (int i = 0; i < relationCount; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        indices.Add(i);
                        for (int j = 0; j < vectorLength; j++)
                        {
                            sumVector[j] = (sumVector[j] + relations[i].Mod2Exponents[j]) % 2;
                        }
                    }
                }

                bool allZero = true;
                for (int j = 0; j < vectorLength; j++)
                {
                    if (sumVector[j] != 0)
                    {
                        allZero = false;
                        break;
                    }
                }

                if (allZero)
                {
                    return indices;
                }
            }

            return null;
        }

        private static bool IsPrime(BigInteger n)
        {
            if (n < new BigInteger(2))
            {
                return false;
            }

            if (n == new BigInteger(2))
            {
                return true;
            }

            if (n % new BigInteger(2) == BigInteger.Zero)
            {
                return false;
            }

            BigInteger sqrtN = Sqrt(n);
            for (BigInteger i = new BigInteger(3); i <= sqrtN; i = i + new BigInteger(2))
            {
                if (n % i == BigInteger.Zero)
                {
                    return false;
                }
            }

            return true;
        }

        private static List<BigInteger> GetFactorBase(BigInteger n)
        {
            List<BigInteger> factorBase = new List<BigInteger>();
            factorBase.Add(new BigInteger(-1));
            factorBase.Add(new BigInteger(2));
            factorBase.Add(new BigInteger(3));
            factorBase.Add(new BigInteger(5));
            factorBase.Add(new BigInteger(7));
            factorBase.Add(new BigInteger(11));
            factorBase.Add(new BigInteger(13));
            factorBase.Add(new BigInteger(17));
            factorBase.Add(new BigInteger(19));
            factorBase.Add(new BigInteger(23));
            return factorBase;
        }

        private class Relation
        {
            public BigInteger X { get; }
            public int[] FullExponents { get; }
            public int[] Mod2Exponents { get; }

            public Relation(BigInteger x, int[] fullExponents, int[] mod2Exponents)
            {
                X = x;
                FullExponents = fullExponents;
                Mod2Exponents = mod2Exponents;
            }
        }
    }
}