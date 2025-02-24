using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salsa20AndChaCha20;

namespace Salsa20AndChaCha20Test
{
    [TestClass]
    public class ChaCha20Tests
    {
        [TestMethod]
        public void EncryptionDecryptionTest()
        {
            byte[] key = new byte[32];
            byte[] nonce = new byte[12];
            for (int i = 0; i < 32; i++)
            {
                key[i] = (byte)i;
            }

            for (int i = 0; i < 12; i++)
            {
                nonce[i] = (byte)(i + 1);
            }

            string originalText = "The quick brown fox jumps over the lazy dog";
            byte[] plaintext = Encoding.UTF8.GetBytes(originalText);
            byte[] ciphertext = new byte[plaintext.Length];
            ChaCha20 chacha = new ChaCha20(key, nonce);
            chacha.ProcessBytes(plaintext, 0, plaintext.Length, ciphertext, 0);
            byte[] decrypted = new byte[plaintext.Length];
            ChaCha20 chachaDecrypt = new ChaCha20(key, nonce);
            chachaDecrypt.ProcessBytes(ciphertext, 0, ciphertext.Length, decrypted, 0);
            CollectionAssert.AreEqual(plaintext, decrypted);
        }

        [TestMethod]
        public void ProcessBytesNullInputTest()
        {
            byte[] key = new byte[32];
            byte[] nonce = new byte[12];
            for (int i = 0; i < 32; i++)
            {
                key[i] = (byte)(i + 1);
            }

            for (int i = 0; i < 12; i++)
            {
                nonce[i] = (byte)(i + 2);
            }

            ChaCha20 chacha = new ChaCha20(key, nonce);
            byte[] output = new byte[10];
            byte[] originalOutput = new byte[10];
            System.Array.Copy(output, originalOutput, 10);
            chacha.ProcessBytes(null, 0, 0, output, 0);
            CollectionAssert.AreEqual(originalOutput, output);
            byte[] plaintext = new byte[5];
            chacha.ProcessBytes(plaintext, 0, plaintext.Length, null, 0);
        }

        [TestMethod]
        public void ProcessBytesInvalidOffsetTest()
        {
            byte[] key = new byte[32];
            byte[] nonce = new byte[12];
            for (int i = 0; i < 32; i++)
            {
                key[i] = (byte)i;
            }

            for (int i = 0; i < 12; i++)
            {
                nonce[i] = (byte)i;
            }

            ChaCha20 chacha = new ChaCha20(key, nonce);
            byte[] plaintext = Encoding.UTF8.GetBytes("Test");
            byte[] output = new byte[plaintext.Length];
            byte[] originalOutput = new byte[plaintext.Length];
            System.Array.Copy(output, originalOutput, plaintext.Length);
            chacha.ProcessBytes(plaintext, -1, plaintext.Length, output, 0);
            CollectionAssert.AreEqual(originalOutput, output);
            chacha.ProcessBytes(plaintext, 0, plaintext.Length + 1, output, 0);
            CollectionAssert.AreEqual(originalOutput, output);
            chacha.ProcessBytes(plaintext, 0, plaintext.Length, output, -1);
            CollectionAssert.AreEqual(originalOutput, output);
            if (output.Length > 0)
            {
                chacha.ProcessBytes(plaintext, 0, plaintext.Length, output, output.Length);
            }
        }

        [TestMethod]
        public void SyntheticLargeDataTest()
        {
            byte[] key = new byte[32];
            byte[] nonce = new byte[12];
            for (int i = 0; i < 32; i++)
            {
                key[i] = (byte)(i * 3);
            }

            for (int i = 0; i < 12; i++)
            {
                nonce[i] = (byte)(i * 5);
            }

            int dataSize = 4096;
            byte[] plaintext = new byte[dataSize];
            System.Random random = new System.Random(42);
            for (int i = 0; i < dataSize; i++)
            {
                plaintext[i] = (byte)random.Next(0, 256);
            }

            byte[] ciphertext = new byte[dataSize];
            ChaCha20 chacha = new ChaCha20(key, nonce);
            chacha.ProcessBytes(plaintext, 0, dataSize, ciphertext, 0);
            byte[] decrypted = new byte[dataSize];
            ChaCha20 chachaDecrypt = new ChaCha20(key, nonce);
            chachaDecrypt.ProcessBytes(ciphertext, 0, dataSize, decrypted, 0);
            CollectionAssert.AreEqual(plaintext, decrypted);
        }
    }
}