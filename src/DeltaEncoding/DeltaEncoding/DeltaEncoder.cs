using System;

namespace DeltaEncoding
{
    /// <summary>
    /// Provides methods for delta encoding and decoding of integer arrays.
    /// Delta encoding replaces each value with the difference from its predecessor,
    /// aiding compression when sequential data is frequent.
    /// </summary>
    public static class DeltaEncoder
    {
        public static int[] Encode(int[] input)
        {
            if (input == null || input.Length == 0)
            {
                return new int[0];
            }

            int[] encoded = new int[input.Length];
            encoded[0] = input[0];
            for (int i = 1; i < input.Length; i++)
            {
                encoded[i] = input[i] - input[i - 1];
            }

            return encoded;
        }

        public static int[] Decode(int[] encoded)
        {
            if (encoded == null || encoded.Length == 0)
            {
                return new int[0];
            }

            int[] decoded = new int[encoded.Length];
            decoded[0] = encoded[0];
            for (int i = 1; i < encoded.Length; i++)
            {
                decoded[i] = decoded[i - 1] + encoded[i];
            }

            return decoded;
        }
    }
}