using System;
using System.Text;
using System.Security.Cryptography;

namespace Sha1Collisions
{
    /// <summary>
    /// Provides functionality to compute SHA‑1 hashes.
    /// Note: SHA‑1 is considered insecure because collisions exist.
    /// </summary>
    public static class Sha1Hasher
    {
        /// <summary>
        /// Computes the SHA‑1 hash of the specified string and returns it as a lowercase hexadecimal string.
        /// </summary>
        /// <param name = "input">The input string. If null, an empty string is used.</param>
        /// <returns>A lowercase hexadecimal string representing the SHA‑1 hash.</returns>
        public static string ComputeHash(string? input)
        {
            string text = (input != null) ? input : string.Empty;
            byte[] data = Encoding.UTF8.GetBytes(text);
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(data);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}