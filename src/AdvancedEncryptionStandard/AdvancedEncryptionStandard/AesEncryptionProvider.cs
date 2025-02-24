using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AdvancedEncryptionStandard
{
    public class AesEncryptionProvider
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

        public AesEncryptionProvider(byte[] key, byte[] iv)
        {
            Key = new byte[key.Length];
            IV = new byte[iv.Length];
            Array.Copy(key, Key, key.Length);
            Array.Copy(iv, IV, iv.Length);
        }

        public string Encrypt(string plainText)
        {
            if (plainText == null)
            {
                return "";
            }

            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string cipherText)
        {
            if (cipherText == null)
            {
                return "";
            }

            string plaintext = "";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public static byte[] GenerateKey(int keySize)
        {
            byte[] key;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = keySize * 8;
                aesAlg.GenerateKey();
                key = aesAlg.Key;
            }

            return key;
        }

        public static byte[] GenerateIV()
        {
            byte[] iv;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV();
                iv = aesAlg.IV;
            }

            return iv;
        }
    }
}