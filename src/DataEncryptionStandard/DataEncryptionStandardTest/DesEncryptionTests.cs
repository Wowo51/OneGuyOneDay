using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataEncryptionStandard;

namespace DataEncryptionStandardTest
{
    [TestClass]
    public class DesEncryptionTests
    {
        [TestMethod]
        public void TestEncryptDecrypt_ValidData()
        {
            byte[] plainText = Encoding.UTF8.GetBytes("Hello, World!");
            byte[] key = new byte[8]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            byte[] iv = new byte[8]
            {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            byte[] encrypted = DesEncryption.Encrypt(plainText, key, iv);
            Assert.IsNotNull(encrypted);
            Assert.AreNotEqual(0, encrypted.Length);
            byte[] decrypted = DesEncryption.Decrypt(encrypted, key, iv);
            Assert.IsNotNull(decrypted);
            CollectionAssert.AreEqual(plainText, decrypted);
        }

        [TestMethod]
        public void TestEncrypt_NullData_ReturnsEmpty()
        {
            byte[] key = new byte[8]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            byte[] iv = new byte[8]
            {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            byte[] result = DesEncryption.Encrypt(null !, key, iv);
            CollectionAssert.AreEqual(new byte[0], result);
        }

        [TestMethod]
        public void TestDecrypt_NullData_ReturnsEmpty()
        {
            byte[] key = new byte[8]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            byte[] iv = new byte[8]
            {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            byte[] result = DesEncryption.Decrypt(null !, key, iv);
            CollectionAssert.AreEqual(new byte[0], result);
        }

        [TestMethod]
        public void TestEncrypt_InvalidKey_ReturnsEmpty()
        {
            byte[] plainText = Encoding.UTF8.GetBytes("Hello");
            byte[] key = new byte[4]
            {
                1,
                2,
                3,
                4
            };
            byte[] iv = new byte[8]
            {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            byte[] result = DesEncryption.Encrypt(plainText, key, iv);
            CollectionAssert.AreEqual(new byte[0], result);
        }

        [TestMethod]
        public void TestDecrypt_InvalidIV_ReturnsEmpty()
        {
            byte[] plainText = Encoding.UTF8.GetBytes("Hello");
            byte[] key = new byte[8]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            byte[] ivInvalid = new byte[4]
            {
                8,
                7,
                6,
                5
            };
            byte[] validIV = new byte[8]
            {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            byte[] encrypted = DesEncryption.Encrypt(plainText, key, validIV);
            byte[] result = DesEncryption.Decrypt(encrypted, key, ivInvalid);
            CollectionAssert.AreEqual(new byte[0], result);
        }

        [TestMethod]
        public void TestEncryptDecrypt_EmptyData()
        {
            byte[] plainText = new byte[0];
            byte[] key = new byte[8]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            byte[] iv = new byte[8]
            {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            byte[] encrypted = DesEncryption.Encrypt(plainText, key, iv);
            byte[] decrypted = DesEncryption.Decrypt(encrypted, key, iv);
            CollectionAssert.AreEqual(plainText, decrypted);
        }

        [TestMethod]
        public void TestLargeDataEncryptionDecryption()
        {
            byte[] largeData = new byte[1024 * 1024];
            Random random = new Random(42);
            for (int i = 0; i < largeData.Length; i++)
            {
                largeData[i] = (byte)random.Next(0, 256);
            }

            byte[] key = new byte[8]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            byte[] iv = new byte[8]
            {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            byte[] encrypted = DesEncryption.Encrypt(largeData, key, iv);
            byte[] decrypted = DesEncryption.Decrypt(encrypted, key, iv);
            CollectionAssert.AreEqual(largeData, decrypted);
        }

        [TestMethod]
        public void TestEncrypt_NullKey_ReturnsEmpty()
        {
            byte[] plainText = Encoding.UTF8.GetBytes("Test message");
            byte[] iv = new byte[8]
            {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            byte[] result = DesEncryption.Encrypt(plainText, null !, iv);
            CollectionAssert.AreEqual(new byte[0], result);
        }

        [TestMethod]
        public void TestEncrypt_NullIV_ReturnsEmpty()
        {
            byte[] plainText = Encoding.UTF8.GetBytes("Test message");
            byte[] key = new byte[8]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            byte[] result = DesEncryption.Encrypt(plainText, key, null !);
            CollectionAssert.AreEqual(new byte[0], result);
        }

        [TestMethod]
        public void TestDecrypt_NullKey_ReturnsEmpty()
        {
            byte[] encrypted = Encoding.UTF8.GetBytes("Fake");
            byte[] iv = new byte[8]
            {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            byte[] result = DesEncryption.Decrypt(encrypted, null !, iv);
            CollectionAssert.AreEqual(new byte[0], result);
        }

        [TestMethod]
        public void TestDecrypt_NullIV_ReturnsEmpty()
        {
            byte[] encrypted = Encoding.UTF8.GetBytes("Fake");
            byte[] key = new byte[8]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            byte[] result = DesEncryption.Decrypt(encrypted, key, null !);
            CollectionAssert.AreEqual(new byte[0], result);
        }

        [TestMethod]
        public void TestDecrypt_CorruptData_ReturnsEmpty()
        {
            byte[] corruptData = new byte[8]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            byte[] key = new byte[8]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };
            byte[] iv = new byte[8]
            {
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1
            };
            byte[] result = DesEncryption.Decrypt(corruptData, key, iv);
            CollectionAssert.AreEqual(new byte[0], result);
        }
    }
}