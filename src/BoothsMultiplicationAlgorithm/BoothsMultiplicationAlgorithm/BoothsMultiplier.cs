using System;

namespace BoothsMultiplicationAlgorithm
{
    /// <summary>
    /// Implements Booth's multiplication algorithm for two's complement signed numbers.
    /// </summary>
    public class BoothsMultiplier
    {
        /// <summary>
        /// Multiplies two integers using Booth's multiplication algorithm.
        /// </summary>
        /// <param name = "multiplicand">The multiplicand.</param>
        /// <param name = "multiplier">The multiplier.</param>
        /// <returns>The product as a long integer.</returns>
        public long Multiply(int multiplicand, int multiplier)
        {
            int iterations = 32;
            int A = 0;
            int Q = multiplier;
            int Q1 = 0;
            for (int i = 0; i < iterations; i = i + 1)
            {
                int q0 = Q & 1;
                if (q0 == 0 && Q1 == 1)
                {
                    A = A + multiplicand;
                }
                else if (q0 == 1 && Q1 == 0)
                {
                    A = A - multiplicand;
                }

                int tempA = A;
                int tempQ = Q;
                A = A >> 1;
                Q = ((tempA & 1) << 31) | ((int)((uint)tempQ >> 1));
                Q1 = tempQ & 1;
            }

            return ((long)A << 32) | ((uint)Q);
        }
    }
}