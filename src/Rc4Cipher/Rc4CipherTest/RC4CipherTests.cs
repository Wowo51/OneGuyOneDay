using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rc4Cipher;

namespace Rc4CipherTest
{
    [TestClass]
    public class RC4CipherTests
    {
        [TestMethod]
        public void Encrypt_NullData_ReturnsEmptyArray()
        {
            byte[] key = new byte[]
            {
                1,
                2,
                3
            };
            byte[] result = RC4Cipher.Encrypt(null, key);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Encrypt_NullKey_ReturnsEmptyArray()
        {
            byte[] data = new byte[]
            {
                1,
                2,
                3,
                4
            };
            byte[] result = RC4Cipher.Encrypt(data, null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Encrypt_EmptyKey_ReturnsEmptyArray()
        {
            byte[] data = new byte[]
            {
                5,
                6,
                7,
                8
            };
            byte[] key = new byte[0];
            byte[] result = RC4Cipher.Encrypt(data, key);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Encrypt_EmptyData_ReturnsEmptyArray()
        {
            byte[] data = new byte[0];
            byte[] key = new byte[]
            {
                1,
                2,
                3
            };
            byte[] result = RC4Cipher.Encrypt(data, key);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void EncryptDecrypt_SmallData_ReturnsOriginalData()
        {
            byte[] key = new byte[]
            {
                10,
                20,
                30
            };
            byte[] original = new byte[]
            {
                99,
                100,
                101,
                102,
                103
            };
            byte[] encrypted = RC4Cipher.Encrypt(original, key);
            Assert.IsNotNull(encrypted);
            Assert.AreNotEqual(0, encrypted.Length);
            byte[] decrypted = RC4Cipher.Decrypt(encrypted, key);
            CollectionAssert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void EncryptDecrypt_LargeData_ReturnsOriginalData()
        {
            byte[] key = new byte[]
            {
                1,
                2,
                3,
                4,
                5
            };
            int length = 10000;
            byte[] original = new byte[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                original[i] = (byte)random.Next(0, 256);
            }

            byte[] encrypted = RC4Cipher.Encrypt(original, key);
            Assert.IsNotNull(encrypted);
            Assert.AreEqual(original.Length, encrypted.Length);
            byte[] decrypted = RC4Cipher.Decrypt(encrypted, key);
            CollectionAssert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void EncryptDecrypt_SingleByteKey_ReturnsOriginalData()
        {
            byte[] key = new byte[]
            {
                7
            };
            byte[] original = new byte[]
            {
                42,
                43,
                44,
                45,
                46
            };
            byte[] encrypted = RC4Cipher.Encrypt(original, key);
            byte[] decrypted = RC4Cipher.Decrypt(encrypted, key);
            CollectionAssert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void EncryptDecrypt_KeyLongerThanData_ReturnsOriginalData()
        {
            byte[] key = new byte[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10
            };
            byte[] original = new byte[]
            {
                11,
                22,
                33
            };
            byte[] encrypted = RC4Cipher.Encrypt(original, key);
            byte[] decrypted = RC4Cipher.Decrypt(encrypted, key);
            CollectionAssert.AreEqual(original, decrypted);
        }
    }
}