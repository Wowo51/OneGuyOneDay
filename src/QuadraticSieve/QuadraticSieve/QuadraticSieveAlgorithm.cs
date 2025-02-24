using System;
using System.Collections.Generic;
using System.Numerics;

namespace QuadraticSieve
{
    public static class QuadraticSieveAlgorithm
    {
        public static List<BigInteger> Factor(BigInteger n)
        {
            if (n < 2)
            {
                return new List<BigInteger>
                {
                    n
                };
            }

            if (IsPrime(n))
            {
                return new List<BigInteger>
                {
                    n
                };
            }

            BigInteger sqrtN = Sqrt(n);
            if (sqrtN * sqrtN == n)
            {
                List<BigInteger> factors = Factor(sqrtN);
                factors.Add(sqrtN);
                return factors;
            }

            double logn = BigInteger.Log(n);
            double loglogn = Math.Log(logn);
            int bound = (int)Math.Exp(0.5 * Math.Sqrt(logn * loglogn));
            if (bound < 10)
            {
                bound = 10;
            }

            List<int> positiveFactorBase = new List<int>();
            foreach (int p in GetPrimesUpTo(bound))
            {
                if (LegendreSymbol(n, p) == 1)
                {
                    positiveFactorBase.Add(p);
                }
            }

            List<int> fullFactorBase = new List<int>();
            fullFactorBase.Add(-1);
            fullFactorBase.AddRange(positiveFactorBase);
            int requiredRelations = fullFactorBase.Count + 1;
            BigInteger a0 = Sqrt(n) + 1;
            List<Relation> relations = new List<Relation>();
            BigInteger x = a0;
            int attemptCount = 0;
            while (relations.Count < requiredRelations && attemptCount < 100000)
            {
                BigInteger q = x * x - n;
                BigInteger absq = q < 0 ? -q : q;
                Dictionary<int, int>? fac = FactorOverBase(absq, positiveFactorBase);
                if (fac != null)
                {
                    if (q < 0)
                    {
                        fac[-1] = 1;
                    }

                    List<int> expVector = new List<int>();
                    expVector.Add(q < 0 ? 1 : 0);
                    foreach (int p in positiveFactorBase)
                    {
                        int exp = fac.ContainsKey(p) ? fac[p] : 0;
                        expVector.Add(exp % 2);
                    }

                    relations.Add(new Relation { X = x, Q = q, Exponents = expVector, Factorization = fac });
                }

                x = x + 1;
                attemptCount = attemptCount + 1;
            }

            if (relations.Count < requiredRelations)
            {
                return new List<BigInteger>
                {
                    n
                };
            }

            List<List<int>> matrix = new List<List<int>>();
            foreach (Relation r in relations)
            {
                matrix.Add(new List<int>(r.Exponents));
            }

            List<int>? dependency = Mod2Solver.Solve(matrix);
            if (dependency == null)
            {
                return new List<BigInteger>
                {
                    n
                };
            }

            BigInteger aProduct = BigInteger.One;
            Dictionary<int, int> combinedExp = new Dictionary<int, int>();
            for (int i = 0; i < relations.Count; i++)
            {
                if (dependency[i] == 1)
                {
                    aProduct = aProduct * relations[i].X;
                    foreach (KeyValuePair<int, int> kv in relations[i].Factorization)
                    {
                        if (combinedExp.ContainsKey(kv.Key))
                        {
                            combinedExp[kv.Key] = combinedExp[kv.Key] + kv.Value;
                        }
                        else
                        {
                            combinedExp[kv.Key] = kv.Value;
                        }
                    }
                }
            }

            BigInteger rVal = BigInteger.One;
            foreach (KeyValuePair<int, int> kv in combinedExp)
            {
                rVal = rVal * BigInteger.Pow(kv.Key, kv.Value / 2);
            }

            BigInteger factorCandidate = BigInteger.GreatestCommonDivisor(aProduct - rVal, n);
            if (factorCandidate == 1 || factorCandidate == n)
            {
                factorCandidate = BigInteger.GreatestCommonDivisor(aProduct + rVal, n);
            }

            if (factorCandidate == 1 || factorCandidate == n)
            {
                return new List<BigInteger>
                {
                    n
                };
            }

            List<BigInteger> factors1 = Factor(factorCandidate);
            List<BigInteger> factors2 = Factor(n / factorCandidate);
            List<BigInteger> result = new List<BigInteger>();
            result.AddRange(factors1);
            result.AddRange(factors2);
            return result;
        }

        private static BigInteger Sqrt(BigInteger n)
        {
            if (n < 0)
            {
                return 0;
            }

            if (n == 0)
            {
                return 0;
            }

            BigInteger r = n;
            BigInteger prev = BigInteger.Zero;
            while (r != prev)
            {
                prev = r;
                r = (r + n / r) / 2;
            }

            return r;
        }

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

            BigInteger r = Sqrt(n);
            for (BigInteger i = 3; i <= r; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static List<int> GetPrimesUpTo(int limit)
        {
            bool[] sieve = new bool[limit + 1];
            for (int i = 2; i <= limit; i++)
            {
                sieve[i] = true;
            }

            for (int p = 2; p * p <= limit; p++)
            {
                if (sieve[p])
                {
                    for (int multiple = p * p; multiple <= limit; multiple += p)
                    {
                        sieve[multiple] = false;
                    }
                }
            }

            List<int> primes = new List<int>();
            for (int i = 2; i <= limit; i++)
            {
                if (sieve[i])
                {
                    primes.Add(i);
                }
            }

            return primes;
        }

        private static int LegendreSymbol(BigInteger n, int p)
        {
            BigInteger mod = n % p;
            if (mod < 0)
            {
                mod += p;
            }

            if (mod == 0)
            {
                return 0;
            }

            BigInteger exp = (p - 1) / 2;
            BigInteger result = BigInteger.ModPow(mod, exp, p);
            if (result == p - 1)
            {
                return -1;
            }

            return (int)result;
        }

        private static Dictionary<int, int>? FactorOverBase(BigInteger m, List<int> factorBase)
        {
            Dictionary<int, int> factors = new Dictionary<int, int>();
            BigInteger rem = m;
            foreach (int p in factorBase)
            {
                int count = 0;
                while (rem % p == 0)
                {
                    count = count + 1;
                    rem = rem / p;
                }

                if (count > 0)
                {
                    factors[p] = count;
                }
            }

            if (rem == 1)
            {
                return factors;
            }
            else
            {
                return null;
            }
        }

        private class Relation
        {
            public BigInteger X;
            public BigInteger Q;
            public List<int> Exponents = new List<int>();
            public Dictionary<int, int> Factorization = new Dictionary<int, int>();
        }
    }
}