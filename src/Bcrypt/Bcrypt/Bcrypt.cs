using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Bcrypt
{
    public static class Bcrypt
    {
        // In standard bcrypt the salt is 16 bytes and the hash is 23 bytes,
        // which when encoded with the bcrypt Base64 yields respectively 22 and 31 characters.
        // The final hash string has the format: "$2a$" + cost (2 digits) + "$" + salt (22 chars) + hash (31 chars) = 60 chars.
        private const int SaltByteSize = 16;
        private const int HashByteSize = 23;
        private const int ExpectedHashStringLength = 60;
        private static readonly char[] Base64Code = "./ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        public static string HashPassword(string password, int cost = 10)
        {
            if (password == null)
            {
                return "";
            }

            byte[] salt = new byte[SaltByteSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            int iterations = 1 << cost;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashByteSize);
                string saltEncoded = EncodeBase64(salt, salt.Length);
                string hashEncoded = EncodeBase64(hash, hash.Length);
                return "$2a$" + cost.ToString("D2") + "$" + saltEncoded + hashEncoded;
            }
        }

        public static bool VerifyPassword(string password, string hashed)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashed) || hashed.Length != ExpectedHashStringLength)
            {
                return false;
            }

            try
            {
                // Extract cost, salt and hash from the bcrypt formatted string.
                string costStr = hashed.Substring(4, 2);
                int cost = int.Parse(costStr);
                string saltEncoded = hashed.Substring(7, 22);
                string hashEncoded = hashed.Substring(29, 31);
                byte[] salt = DecodeBase64(saltEncoded, SaltByteSize);
                int iterations = 1 << cost;
                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(HashByteSize);
                    string computedHashEncoded = EncodeBase64(hash, hash.Length);
                    string expected = "$2a$" + costStr + "$" + saltEncoded + computedHashEncoded;
                    return SlowEquals(hashed, expected);
                }
            }
            catch
            {
                return false;
            }
        }

        private static string EncodeBase64(byte[] input, int length)
        {
            int i = 0;
            StringBuilder sb = new StringBuilder();
            int c1, c2;
            while (i < length)
            {
                c1 = input[i++] & 0xff;
                sb.Append(Base64Code[c1 >> 2]);
                c1 = (c1 & 0x03) << 4;
                if (i >= length)
                {
                    sb.Append(Base64Code[c1]);
                    break;
                }

                c2 = input[i++] & 0xff;
                c1 |= (c2 >> 4) & 0x0F;
                sb.Append(Base64Code[c1]);
                c1 = (c2 & 0x0F) << 2;
                if (i >= length)
                {
                    sb.Append(Base64Code[c1]);
                    break;
                }

                c2 = input[i++] & 0xff;
                c1 |= (c2 >> 6) & 0x03;
                sb.Append(Base64Code[c1]);
                sb.Append(Base64Code[c2 & 0x3F]);
            }

            return sb.ToString();
        }

        private static byte[] DecodeBase64(string s, int maxLength)
        {
            List<byte> bytes = new List<byte>();
            int i = 0;
            int len = s.Length;
            while (i < len && bytes.Count < maxLength)
            {
                int c1 = Char64(s[i++]);
                int c2 = (i < len) ? Char64(s[i++]) : 0;
                if (c1 == -1 || c2 == -1)
                {
                    break;
                }

                int b = (c1 << 2) | ((c2 & 0x30) >> 4);
                bytes.Add((byte)b);
                if (bytes.Count >= maxLength || i >= len)
                {
                    break;
                }

                int c3 = Char64(s[i++]);
                if (c3 == -1)
                {
                    break;
                }

                b = ((c2 & 0x0F) << 4) | ((c3 & 0x3C) >> 2);
                bytes.Add((byte)b);
                if (bytes.Count >= maxLength || i >= len)
                {
                    break;
                }

                int c4 = Char64(s[i++]);
                if (c4 == -1)
                {
                    break;
                }

                b = ((c3 & 0x03) << 6) | c4;
                bytes.Add((byte)b);
            }

            return bytes.ToArray();
        }

        private static int Char64(char x)
        {
            if (x == '.')
            {
                return 0;
            }

            if (x == '/')
            {
                return 1;
            }

            int index = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".IndexOf(x);
            if (index >= 0)
            {
                return index + 2;
            }

            return -1;
        }

        private static bool SlowEquals(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }

            return diff == 0;
        }
    }
}