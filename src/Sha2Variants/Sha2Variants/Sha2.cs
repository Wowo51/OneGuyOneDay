using System;
using System.Security.Cryptography;

namespace Sha2Variants
{
    public static class Sha2
    {
        public static byte[] ComputeSha224(byte[] data)
        {
            Sha2Variants.SHA224 sha224 = new Sha2Variants.SHA224();
            sha224.Initialize();
            sha224.TransformFinalBlock(data, 0, data.Length);
            return sha224.Hash!;
        }

        public static byte[] ComputeSha256(byte[] data)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] result = sha256.ComputeHash(data);
            sha256.Dispose();
            return result;
        }

        public static byte[] ComputeSha384(byte[] data)
        {
            SHA384 sha384 = SHA384.Create();
            byte[] result = sha384.ComputeHash(data);
            sha384.Dispose();
            return result;
        }

        public static byte[] ComputeSha512(byte[] data)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] result = sha512.ComputeHash(data);
            sha512.Dispose();
            return result;
        }
    }
}