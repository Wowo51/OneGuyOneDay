using System.Numerics;

namespace FurerAlgorithm
{
    /// <summary>
    /// Provides multiplication of arbitrarily large integers using an FFT-based algorithm inspired by Fürer’s algorithm.
    /// </summary>
    public static class FurerMultiplier
    {
        /// <summary>
        /// Multiplies two large integers.
        /// </summary>
        /// <param name = "a">The first large integer.</param>
        /// <param name = "b">The second large integer.</param>
        /// <returns>The product of a and b.</returns>
        public static BigInteger Multiply(BigInteger a, BigInteger b)
        {
            return FFTMultiplier.Multiply(a, b);
        }
    }
}