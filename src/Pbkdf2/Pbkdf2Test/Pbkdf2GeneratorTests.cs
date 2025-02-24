using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pbkdf2;

namespace Pbkdf2Test
{
    [TestClass]
    public class Pbkdf2GeneratorTests
    {
        [TestMethod]
        public void DeriveKey_ValidInput_ReturnsCorrectLength()
        {
            string password = "password";
            byte[] salt = new byte[]
            {
                1,
                2,
                3,
                4
            };
            int iterations = 1000;
            int keyLength = 32;
            byte[] derivedKey = Pbkdf2Generator.DeriveKey(password, salt, iterations, keyLength);
            Assert.AreEqual(keyLength, derivedKey.Length);
        }

        [TestMethod]
        public void DeriveKey_NullPassword_ReturnsConsistentResult()
        {
            string? password = null;
            byte[] salt = new byte[]
            {
                10,
                20,
                30,
                40
            };
            int iterations = 1000;
            int keyLength = 32;
            byte[] derivedKey = Pbkdf2Generator.DeriveKey(password, salt, iterations, keyLength);
            Assert.AreEqual(keyLength, derivedKey.Length);
        }

        [TestMethod]
        public void DeriveKey_NullSalt_ReturnsCorrectLength()
        {
            string password = "password";
            byte[]? salt = null;
            int iterations = 1000;
            int keyLength = 32;
            byte[] derivedKey = Pbkdf2Generator.DeriveKey(password, salt, iterations, keyLength);
            Assert.AreEqual(keyLength, derivedKey.Length);
        }

        [TestMethod]
        public void DeriveKey_NonPositiveParameters_UsesDefaults()
        {
            string password = "password";
            byte[] salt = new byte[]
            {
                5,
                5,
                5,
                5
            };
            int iterations = 0;
            int keyLength = 0;
            byte[] derivedKey = Pbkdf2Generator.DeriveKey(password, salt, iterations, keyLength);
            Assert.AreEqual(32, derivedKey.Length);
        }

        [TestMethod]
        public void DeriveKey_ConsistentResults()
        {
            string password = "consistentPassword";
            byte[] salt = new byte[]
            {
                1,
                2,
                3,
                4,
                5
            };
            int iterations = 1200;
            int keyLength = 40;
            byte[] first = Pbkdf2Generator.DeriveKey(password, salt, iterations, keyLength);
            byte[] second = Pbkdf2Generator.DeriveKey(password, salt, iterations, keyLength);
            CollectionAssert.AreEqual(first, second);
        }

        [TestMethod]
        public void DeriveKey_LargeSaltSyntheticData_ReturnsCorrectLength()
        {
            string password = "password";
            int saltLength = 1024;
            byte[] salt = new byte[saltLength];
            for (int i = 0; i < saltLength; i++)
            {
                salt[i] = (byte)(i % 256);
            }

            int iterations = 1500;
            int keyLength = 64;
            byte[] derivedKey = Pbkdf2Generator.DeriveKey(password, salt, iterations, keyLength);
            Assert.AreEqual(keyLength, derivedKey.Length);
        }
    }
}