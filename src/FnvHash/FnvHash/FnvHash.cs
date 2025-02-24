using System;
using System.Text;

namespace FnvHash
{
    /// <summary>
    /// Provides methods to compute the Fowler–Noll–Vo (FNV-1a) hash function.
    /// </summary>
    public static class FnvHasher
    {
        private const uint OffsetBasis = 2166136261u;
        private const uint Prime = 16777619u;
        /// <summary>
        /// Computes the FNV-1a hash for the given byte array.
        /// </summary>
        /// <param name = "data">The byte array to hash.</param>
        /// <returns>A 32-bit hash value.</returns>
        public static uint ComputeHash(byte[] data)
        {
            if (data == null)
            {
                return 0u;
            }

            uint hash = OffsetBasis;
            for (int i = 0; i < data.Length; i = i + 1)
            {
                hash ^= data[i];
                hash *= Prime;
            }

            return hash;
        }

        /// <summary>
        /// Computes the FNV-1a hash for the given string using UTF8 encoding.
        /// </summary>
        /// <param name = "text">The string to hash.</param>
        /// <returns>A 32-bit hash value.</returns>
        public static uint ComputeHash(string text)
        {
            if (text == null)
            {
                text = string.Empty;
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return ComputeHash(textBytes);
        }
    }
}