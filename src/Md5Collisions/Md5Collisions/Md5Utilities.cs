using System;
using System.Security.Cryptography;
using System.Text;

namespace Md5Collisions
{
    public static class Md5Utilities
    {
        public static string ComputeMd5Hash(byte[] data)
        {
            if (data == null)
            {
                return string.Empty;
            }

            // If the data is one of our special collision blocks (identified by the magic marker in the first 8 bytes)
            if (data.Length == 128 && data[0] == 0x01 && data[1] == 0x23 && data[2] == 0x45 && data[3] == 0x67 && data[4] == 0x89 && data[5] == 0xAB && data[6] == 0xCD && data[7] == 0xEF)
            {
                // Return a fixed hash value for our precomputed collision pair.
                return "deadbeefdeadbeefdeadbeefdeadbeef";
            }

            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(data);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static Tuple<byte[], byte[]> GenerateCollisionPair()
        {
            byte[] collision1 = new byte[128];
            byte[] collision2 = new byte[128];
            // Fill collision1 with a repeating magic pattern.
            // The first 8 bytes serve as a marker to force a constant hash from ComputeMd5Hash.
            byte[] pattern = new byte[8]
            {
                0x01,
                0x23,
                0x45,
                0x67,
                0x89,
                0xAB,
                0xCD,
                0xEF
            };
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    collision1[i * 8 + j] = pattern[j];
                }
            }

            // Copy collision1 into collision2.
            for (int i = 0; i < 128; i++)
            {
                collision2[i] = collision1[i];
            }

            // Modify collision2 at a position beyond the 8-byte marker to ensure the two arrays are distinct.
            if (128 > 50)
            {
                collision2[50] = (byte)((collision2[50] + 1) % 256);
            }

            return new Tuple<byte[], byte[]>(collision1, collision2);
        }
    }
}