using System;
using System.Security.Cryptography;

namespace HmacMessageAuthentication
{
    public static class HmacCalculator
    {
        public static byte[] ComputeHmac(byte[] key, byte[] message)
        {
            if (key == null || message == null)
            {
                return new byte[0];
            }

            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(message);
            }
        }

        public static string ComputeHmacHex(byte[] key, byte[] message)
        {
            byte[] hash = ComputeHmac(key, message);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}