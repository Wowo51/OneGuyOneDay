using System.Numerics;

namespace ExponentiatingBySquaring
{
    public static class Exponentiator
    {
        /// <summary>
        /// Computes the power of a base raised to an exponent using exponentiation by squaring.
        /// Negative exponents are not supported and will return 0.
        /// </summary>
        /// <param name = "baseValue">The base value.</param>
        /// <param name = "exponent">The exponent (must be non-negative).</param>
        /// <returns>The computed power as a BigInteger.</returns>
        public static BigInteger Power(BigInteger baseValue, BigInteger exponent)
        {
            if (exponent == 0)
            {
                return BigInteger.One;
            }

            if (exponent < 0)
            {
                return BigInteger.Zero;
            }

            BigInteger halfPower = Power(baseValue, exponent / 2);
            if (exponent % 2 == 0)
            {
                return halfPower * halfPower;
            }
            else
            {
                return baseValue * halfPower * halfPower;
            }
        }
    }
}