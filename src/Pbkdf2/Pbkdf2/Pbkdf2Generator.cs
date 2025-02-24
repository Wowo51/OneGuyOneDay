using System;
using System.Security.Cryptography;
using System.Text;

namespace Pbkdf2
{
    public static class Pbkdf2Generator
    {
        public static byte[] DeriveKey(string password, byte[] salt, int iterations, int keyLength)
        {
            if (password == null)
            {
                password = string.Empty;
            }

            if (salt == null)
            {
                salt = new byte[0];
            }

            if (iterations <= 0)
            {
                iterations = 1000;
            }

            if (keyLength <= 0)
            {
                keyLength = 32;
            }

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            using (HMACSHA256 hmac = new HMACSHA256(passwordBytes))
            {
                int hashLength = hmac.HashSize / 8;
                int totalBlocks = (int)Math.Ceiling((double)keyLength / hashLength);
                byte[] output = new byte[totalBlocks * hashLength];
                for (int i = 1; i <= totalBlocks; i++)
                {
                    byte[] intBuffer = GetBigEndianBytes(i);
                    byte[] saltAndIndex = new byte[salt.Length + intBuffer.Length];
                    Buffer.BlockCopy(salt, 0, saltAndIndex, 0, salt.Length);
                    Buffer.BlockCopy(intBuffer, 0, saltAndIndex, salt.Length, intBuffer.Length);
                    byte[] U_prev = hmac.ComputeHash(saltAndIndex);
                    byte[] T = new byte[hashLength];
                    Buffer.BlockCopy(U_prev, 0, T, 0, hashLength);
                    for (int j = 1; j < iterations; j++)
                    {
                        U_prev = hmac.ComputeHash(U_prev);
                        for (int k = 0; k < hashLength; k++)
                        {
                            T[k] ^= U_prev[k];
                        }
                    }

                    Buffer.BlockCopy(T, 0, output, (i - 1) * hashLength, hashLength);
                }

                byte[] derivedKey = new byte[keyLength];
                Buffer.BlockCopy(output, 0, derivedKey, 0, keyLength);
                return derivedKey;
            }
        }

        private static byte[] GetBigEndianBytes(int number)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)((number >> 24) & 0xff);
            bytes[1] = (byte)((number >> 16) & 0xff);
            bytes[2] = (byte)((number >> 8) & 0xff);
            bytes[3] = (byte)(number & 0xff);
            return bytes;
        }
    }
}