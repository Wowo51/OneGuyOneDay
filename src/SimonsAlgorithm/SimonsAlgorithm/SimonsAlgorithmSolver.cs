using System;
using System.Collections.Generic;

namespace SimonsAlgorithm
{
    public static class SimonsAlgorithmSolver
    {
        private static readonly Random RandomGenerator = new Random();
        // Implements Simon's quantum algorithm simulation.
        // Instead of searching over all x (a classical 2^(n/2) collision‐search),
        // we simulate the quantum procedure by “measuring” the first register.
        // In Simon’s algorithm the measurement outcome y is uniformly distributed over
        // all n-bit strings satisfying (y • s) = 0 (mod 2), where s is the hidden secret.
        // Here we simulate one trial by:
        // 1. Picking a random x in [0, 2^n) and finding its unique collision partner,
        //    which yields s = x XOR (collision partner).
        // 2. Then returning a uniformly random y such that DotProduct(y, s) == 0.
        public static int FindSecret(int n, Func<int, int> f)
        {
            List<int> equations = new List<int>();
            while (ComputeRank(equations, n) < n - 1)
            {
                int measurement = SimulateMeasurement(n, f);
                if (!IsDependent(equations, measurement, n))
                {
                    equations.Add(measurement);
                }
            }

            int secret = SolveNullSpace(equations, n);
            return secret;
        }

        // Simulate a single measurement of the first register after the Hadamard transform.
        // The simulation follows the quantum steps:
        // - Prepare the uniform superposition over x.
        // - Apply the oracle f to create entanglement.
        // - Measure the second register to collapse the state to a pair {x, x XOR s}.
        // - After discarding the second register and applying Hadamard,
        //   the measurement outcome is uniformly random among y with (y • s)==0.
        private static int SimulateMeasurement(int n, Func<int, int> f)
        {
            int limit = 1 << n;
            // Choose a random x and find its paired element via the collision (since f is 2-to-1)
            int x = RandomGenerator.Next(0, limit);
            int fx = f(x);
            int paired = -1;
            for (int y = 0; y < limit; y++)
            {
                if (y != x && f(y) == fx)
                {
                    paired = y;
                    break;
                }
            }

            // Compute the secret candidate from the collision.
            int secretCandidate = x ^ paired;
            // The post-Hadamard measurement yields any y with DotProduct(y, secretCandidate) == 0.
            List<int> candidates = new List<int>();
            for (int r = 0; r < limit; r++)
            {
                if (DotProduct(r, secretCandidate, n) == 0)
                {
                    candidates.Add(r);
                }
            }

            int index = RandomGenerator.Next(0, candidates.Count);
            return candidates[index];
        }

        // Computes the dot product modulo 2 of two n-bit integers.
        private static int DotProduct(int a, int b, int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                int bitA = (a >> i) & 1;
                int bitB = (b >> i) & 1;
                sum += bitA * bitB;
            }

            return sum & 1;
        }

        // Checks if adding candidate to the current set of vectors
        // increases the rank (over GF(2)). If not, candidate is linearly dependent.
        private static bool IsDependent(List<int> vectors, int candidate, int n)
        {
            List<int> temp = new List<int>(vectors);
            temp.Add(candidate);
            int rankWithCandidate = ComputeRank(temp, n);
            int rankOriginal = ComputeRank(vectors, n);
            return rankWithCandidate == rankOriginal;
        }

        // Computes the rank over GF(2) of the list of n-bit vectors represented as ints.
        private static int ComputeRank(List<int> vectors, int n)
        {
            List<int> temp = new List<int>(vectors);
            int rank = 0;
            for (int bit = n - 1; bit >= 0; bit--)
            {
                int pivotIndex = -1;
                for (int i = rank; i < temp.Count; i++)
                {
                    if (((temp[i] >> bit) & 1) == 1)
                    {
                        pivotIndex = i;
                        break;
                    }
                }

                if (pivotIndex == -1)
                {
                    continue;
                }

                // Swap pivot into position.
                int tempVal = temp[rank];
                temp[rank] = temp[pivotIndex];
                temp[pivotIndex] = tempVal;
                int pivot = temp[rank];
                for (int i = 0; i < temp.Count; i++)
                {
                    if (i != rank && (((temp[i] >> bit) & 1) == 1))
                    {
                        temp[i] ^= pivot;
                    }
                }

                rank++;
            }

            return rank;
        }

        // Given a full set of measurement equations (each of which is a binary vector y satisfying y•s=0),
        // the nontrivial solution of A * s = 0 over GF(2) is unique.
        // This brute-force search returns the unique nonzero candidate s that satisfies all equations.
        private static int SolveNullSpace(List<int> equations, int n)
        {
            int limit = 1 << n;
            for (int candidate = 1; candidate < limit; candidate++)
            {
                bool valid = true;
                foreach (int eq in equations)
                {
                    if (DotProduct(candidate, eq, n) != 0)
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    return candidate;
                }
            }

            return 0;
        }
    }
}