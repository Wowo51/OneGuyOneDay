using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salsa20AndChaCha20;

namespace Salsa20AndChaCha20Test
{
    [TestClass]
    public class Salsa20Tests
    {
        [TestMethod]
        public void EncryptionDecryptionTest()
        {
            byte[] key = new byte[32];
            byte[] nonce = new byte[8];
            for (int i = 0; i < 32; i++)
            {
                key[i] = (byte)(i + 10);
            }

            for (int i = 0; i < 8; i++)
            {
                nonce[i] = (byte)(i + 20);
            }

            string originalText = "Sample text for Salsa20 encryption.";
            byte[] plaintext = Encoding.UTF8.GetBytes(originalText);
            byte[] ciphertext = new byte[plaintext.Length];
            Salsa20 salsa = new Salsa20(key, nonce);
            salsa.ProcessBytes(plaintext, 0, plaintext.Length, ciphertext, 0);
            byte[] decrypted = new byte[plaintext.Length];
            Salsa20 salsaDecrypt = new Salsa20(key, nonce);
            salsaDecrypt.ProcessBytes(ciphertext, 0, ciphertext.Length, decrypted, 0);
            CollectionAssert.AreEqual(plaintext, decrypted);
        }

        [TestMethod]
        public void ProcessBytesNullInputTest()
        {
            byte[] key = new byte[32];
            byte[] nonce = new byte[8];
            for (int i = 0; i < 32; i++)
            {
                key[i] = (byte)(i + 5);
            }

            for (int i = 0; i < 8; i++)
            {
                nonce[i] = (byte)(i + 3);
            }

            Salsa20 salsa = new Salsa20(key, nonce);
            byte[] output = new byte[15];
            byte[] originalOutput = new byte[15];
            System.Array.Copy(output, originalOutput, 15);
            salsa.ProcessBytes(null, 0, 0, output, 0);
            CollectionAssert.AreEqual(originalOutput, output);
            byte[] plaintext = new byte[7];
            salsa.ProcessBytes(plaintext, 0, plaintext.Length, null, 0);
        }

        [TestMethod]
        public void ProcessBytesInvalidOffsetTest()
        {
            byte[] key = new byte[32];
            byte[] nonce = new byte[8];
            for (int i = 0; i < 32; i++)
            {
                key[i] = (byte)i;
            }

            for (int i = 0; i < 8; i++)
            {
                nonce[i] = (byte)i;
            }

            Salsa20 salsa = new Salsa20(key, nonce);
            byte[] plaintext = Encoding.UTF8.GetBytes("Hello");
            byte[] output = new byte[plaintext.Length];
            byte[] originalOutput = new byte[plaintext.Length];
            System.Array.Copy(output, originalOutput, plaintext.Length);
            salsa.ProcessBytes(plaintext, -1, plaintext.Length, output, 0);
            CollectionAssert.AreEqual(originalOutput, output);
            salsa.ProcessBytes(plaintext, 0, plaintext.Length + 1, output, 0);
            CollectionAssert.AreEqual(originalOutput, output);
            salsa.ProcessBytes(plaintext, 0, plaintext.Length, output, -1);
            CollectionAssert.AreEqual(originalOutput, output);
            if (output.Length > 0)
            {
                salsa.ProcessBytes(plaintext, 0, plaintext.Length, output, output.Length);
            }
        }

        [TestMethod]
        public void SyntheticLargeDataTest()
        {
            byte[] key = new byte[32];
            byte[] nonce = new byte[8];
            for (int i = 0; i < 32; i++)
            {
                key[i] = (byte)(i * 2);
            }

            for (int i = 0; i < 8; i++)
            {
                nonce[i] = (byte)(i * 3);
            }

            int dataSize = 4096;
            byte[] plaintext = new byte[dataSize];
            System.Random random = new System.Random(100);
            for (int i = 0; i < dataSize; i++)
            {
                plaintext[i] = (byte)random.Next(0, 256);
            }

            byte[] ciphertext = new byte[dataSize];
            Salsa20 salsa = new Salsa20(key, nonce);
            salsa.ProcessBytes(plaintext, 0, dataSize, ciphertext, 0);
            byte[] decrypted = new byte[dataSize];
            Salsa20 salsaDecrypt = new Salsa20(key, nonce);
            salsaDecrypt.ProcessBytes(ciphertext, 0, dataSize, decrypted, 0);
            CollectionAssert.AreEqual(plaintext, decrypted);
        }
    }
}