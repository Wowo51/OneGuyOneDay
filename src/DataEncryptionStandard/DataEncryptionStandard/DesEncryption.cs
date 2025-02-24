using System;
using System.IO;
using System.Security.Cryptography;

namespace DataEncryptionStandard
{
    public static class DesEncryption
    {
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            if (data == null || key == null || iv == null || key.Length != 8 || iv.Length != 8)
            {
                return Array.Empty<byte>();
            }

            try
            {
                using (DES des = DES.Create())
                {
                    des.Key = key;
                    des.IV = iv;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(data, 0, data.Length);
                            cryptoStream.FlushFinalBlock();
                            return memoryStream.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Encryption Error: " + ex.ToString());
                return Array.Empty<byte>();
            }
        }

        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            if (data == null || key == null || iv == null || key.Length != 8 || iv.Length != 8)
            {
                return Array.Empty<byte>();
            }

            try
            {
                using (DES des = DES.Create())
                {
                    des.Key = key;
                    des.IV = iv;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(data, 0, data.Length);
                            cryptoStream.FlushFinalBlock();
                            return memoryStream.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Decryption Error: " + ex.ToString());
                return Array.Empty<byte>();
            }
        }
    }
}