using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTRUEncrypt;

namespace NTRUEncryptTest
{
    [TestClass]
    public class NTRUEncryptorTests
    {
        [TestMethod]
        public void Test_GenerateKeyPair_NotNull()
        {
            NTRUEncryptor encryptor = new NTRUEncryptor();
            NTRUParameters parameters = new NTRUParameters();
            NTRUKeyPair keyPair = encryptor.GenerateKeyPair(parameters);
            Assert.IsNotNull(keyPair);
            Assert.IsNotNull(keyPair.PublicKey);
            Assert.IsNotNull(keyPair.PrivateKey);
            Assert.AreEqual(parameters.N, keyPair.PublicKey.Degree);
            Assert.AreEqual(parameters.N, keyPair.PrivateKey.Degree);
            // Generated key pair uses the same polynomial for both keys.
            CollectionAssert.AreEqual(keyPair.PublicKey.Coefficients, keyPair.PrivateKey.Coefficients);
        }

        [TestMethod]
        public void Test_Encrypt_NullInputs()
        {
            NTRUEncryptor encryptor = new NTRUEncryptor();
            NTRUParameters parameters = new NTRUParameters();
            NTRUKeyPair keyPair = encryptor.GenerateKeyPair(parameters);
            byte[] result1 = encryptor.Encrypt(null !, keyPair.PublicKey, parameters);
            Assert.AreEqual(0, result1.Length);
            byte[] message = new byte[]
            {
                1,
                2,
                3
            };
            byte[] result2 = encryptor.Encrypt(message, null !, parameters);
            Assert.AreEqual(0, result2.Length);
            byte[] result3 = encryptor.Encrypt(message, keyPair.PublicKey, null !);
            Assert.AreEqual(0, result3.Length);
        }

        [TestMethod]
        public void Test_Decrypt_NullInputs()
        {
            NTRUEncryptor encryptor = new NTRUEncryptor();
            NTRUParameters parameters = new NTRUParameters();
            NTRUKeyPair keyPair = encryptor.GenerateKeyPair(parameters);
            byte[] result1 = encryptor.Decrypt(null !, keyPair.PrivateKey, parameters);
            Assert.AreEqual(0, result1.Length);
            byte[] ciphertext = new byte[]
            {
                1,
                2,
                3
            };
            byte[] result2 = encryptor.Decrypt(ciphertext, null !, parameters);
            Assert.AreEqual(0, result2.Length);
            byte[] result3 = encryptor.Decrypt(ciphertext, keyPair.PrivateKey, null !);
            Assert.AreEqual(0, result3.Length);
        }

        [TestMethod]
        public void Test_EncryptionDecryption()
        {
            NTRUEncryptor encryptor = new NTRUEncryptor();
            NTRUParameters parameters = new NTRUParameters();
            byte[] originalMessage = new byte[parameters.N];
            for (int i = 0; i < parameters.N; i++)
            {
                originalMessage[i] = (byte)(i + 1);
            }

            NTRUKeyPair keyPair = encryptor.GenerateKeyPair(parameters);
            byte[] ciphertext = encryptor.Encrypt(originalMessage, keyPair.PublicKey, parameters);
            Assert.AreEqual(parameters.N, ciphertext.Length);
            byte[] decryptedMessage = encryptor.Decrypt(ciphertext, keyPair.PrivateKey, parameters);
            Assert.AreEqual(parameters.N, decryptedMessage.Length);
            byte[] expectedMessage = new byte[parameters.N];
            for (int i = 0; i < parameters.N; i++)
            {
                expectedMessage[i] = (byte)(originalMessage[i] % parameters.q);
            }

            CollectionAssert.AreEqual(expectedMessage, decryptedMessage);
        }

        [TestMethod]
        public void Test_RandomEncryptionDecryption()
        {
            NTRUEncryptor encryptor = new NTRUEncryptor();
            NTRUParameters parameters = new NTRUParameters();
            Random random = new Random();
            NTRUKeyPair keyPair = encryptor.GenerateKeyPair(parameters);
            for (int test = 0; test < 10; test++)
            {
                byte[] originalMessage = new byte[parameters.N];
                for (int i = 0; i < parameters.N; i++)
                {
                    originalMessage[i] = (byte)random.Next(0, 256);
                }

                byte[] ciphertext = encryptor.Encrypt(originalMessage, keyPair.PublicKey, parameters);
                byte[] decryptedMessage = encryptor.Decrypt(ciphertext, keyPair.PrivateKey, parameters);
                byte[] expectedMessage = new byte[parameters.N];
                for (int i = 0; i < parameters.N; i++)
                {
                    expectedMessage[i] = (byte)(originalMessage[i] % parameters.q);
                }

                CollectionAssert.AreEqual(expectedMessage, decryptedMessage);
            }
        }

        [TestMethod]
        public void Test_Polynomial_Addition_Subtraction()
        {
            int[] coeff1 = new int[]
            {
                1,
                2,
                3
            };
            int[] coeff2 = new int[]
            {
                3,
                2,
                1
            };
            Polynomial poly1 = new Polynomial(coeff1);
            Polynomial poly2 = new Polynomial(coeff2);
            int mod = 5;
            Polynomial sum = poly1.Add(poly2, mod);
            Polynomial diff = poly1.Subtract(poly2, mod);
            int[] expectedSum = new int[]
            {
                (1 + 3) % mod,
                (2 + 2) % mod,
                (3 + 1) % mod
            };
            int[] expectedDiff = new int[3];
            for (int i = 0; i < 3; i++)
            {
                int d = (coeff1[i] - coeff2[i]) % mod;
                if (d < 0)
                {
                    d += mod;
                }

                expectedDiff[i] = d;
            }

            CollectionAssert.AreEqual(expectedSum, sum.Coefficients);
            CollectionAssert.AreEqual(expectedDiff, diff.Coefficients);
        }

        [TestMethod]
        public void Test_EncryptEmptyMessage()
        {
            NTRUEncryptor encryptor = new NTRUEncryptor();
            NTRUParameters parameters = new NTRUParameters();
            NTRUKeyPair keyPair = encryptor.GenerateKeyPair(parameters);
            byte[] emptyMessage = new byte[0];
            byte[] ciphertext = encryptor.Encrypt(emptyMessage, keyPair.PublicKey, parameters);
            byte[] expected = keyPair.PublicKey.ToBytes(parameters.N, parameters.q);
            CollectionAssert.AreEqual(expected, ciphertext);
        }

        [TestMethod]
        public void Test_EncryptWithLongMessage()
        {
            NTRUEncryptor encryptor = new NTRUEncryptor();
            NTRUParameters parameters = new NTRUParameters();
            NTRUKeyPair keyPair = encryptor.GenerateKeyPair(parameters);
            Random random = new Random();
            int longLength = parameters.N * 2;
            byte[] longMessage = new byte[longLength];
            for (int i = 0; i < longLength; i++)
            {
                longMessage[i] = (byte)random.Next(0, 256);
            }

            byte[] ciphertext = encryptor.Encrypt(longMessage, keyPair.PublicKey, parameters);
            Assert.AreEqual(parameters.N, ciphertext.Length);
            byte[] expected = new byte[parameters.N];
            for (int i = 0; i < parameters.N; i++)
            {
                int messageVal = longMessage[i] % parameters.q;
                int sum = (messageVal + keyPair.PublicKey.Coefficients[i]) % parameters.q;
                if (sum < 0)
                {
                    sum += parameters.q;
                }

                expected[i] = (byte)sum;
            }

            CollectionAssert.AreEqual(expected, ciphertext);
        }
    }
}