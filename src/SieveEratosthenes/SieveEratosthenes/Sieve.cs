using System;
using System.Collections.Generic;

namespace SieveEratosthenes
{
    public class Sieve
    {
        public static List<int> GetPrimes(int bound)
        {
            List<int> primes = new List<int>();
            if (bound < 2)
            {
                return primes;
            }

            bool[] isPrime = new bool[bound + 1];
            for (int i = 0; i <= bound; i++)
            {
                isPrime[i] = true;
            }

            isPrime[0] = false;
            isPrime[1] = false;
            int limit = (int)Math.Sqrt(bound);
            for (int i = 2; i <= limit; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= bound; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            for (int i = 2; i <= bound; i++)
            {
                if (isPrime[i])
                {
                    primes.Add(i);
                }
            }

            return primes;
        }
    }
}