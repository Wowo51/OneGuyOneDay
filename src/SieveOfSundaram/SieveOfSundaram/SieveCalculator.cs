using System;
using System.Collections.Generic;

namespace SieveOfSundaram
{
    public static class SieveCalculator
    {
        public static List<int> GeneratePrimes(int max)
        {
            if (max < 2)
            {
                return new List<int>();
            }

            int n = (max % 2 == 0) ? (max - 2) / 2 : (max - 1) / 2;
            bool[] marked = new bool[n + 1];
            for (int i = 1; i <= n; i++)
            {
                for (int j = i; i + j + 2 * i * j <= n; j++)
                {
                    marked[i + j + 2 * i * j] = true;
                }
            }

            List<int> primes = new List<int>();
            if (max >= 2)
            {
                primes.Add(2);
            }

            for (int i = 1; i <= n; i++)
            {
                if (!marked[i])
                {
                    int primeCandidate = 2 * i + 1;
                    if (primeCandidate <= max)
                    {
                        primes.Add(primeCandidate);
                    }
                }
            }

            return primes;
        }
    }
}