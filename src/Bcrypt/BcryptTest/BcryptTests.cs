using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using Bcrypt;

namespace BcryptTest
{
    [TestClass]
    public class BcryptTests
    {
        [TestMethod]
        public void TestHashAndVerify()
        {
            string password = "TestPassword123";
            string hash = Bcrypt.Bcrypt.HashPassword(password);
            Assert.IsFalse(string.IsNullOrEmpty(hash));
            Assert.AreEqual(60, hash.Length);
            Assert.IsTrue(Bcrypt.Bcrypt.VerifyPassword(password, hash));
        }

        [TestMethod]
        public void TestInvalidPassword()
        {
            string password = "TestPassword123";
            string wrongPassword = "WrongPassword";
            string hash = Bcrypt.Bcrypt.HashPassword(password);
            Assert.IsFalse(Bcrypt.Bcrypt.VerifyPassword(wrongPassword, hash));
        }

        [TestMethod]
        public void TestHashUnique()
        {
            string password = "TestPasswordUnique";
            string hash1 = Bcrypt.Bcrypt.HashPassword(password);
            string hash2 = Bcrypt.Bcrypt.HashPassword(password);
            Assert.AreNotEqual(hash1, hash2);
            Assert.IsTrue(Bcrypt.Bcrypt.VerifyPassword(password, hash1));
            Assert.IsTrue(Bcrypt.Bcrypt.VerifyPassword(password, hash2));
        }

        [TestMethod]
        public void TestNullPasswordHash()
        {
            string hash = Bcrypt.Bcrypt.HashPassword(null);
            Assert.AreEqual("", hash);
        }

        [TestMethod]
        public void TestNullOrEmptyVerify()
        {
            string password = "password";
            string hash = Bcrypt.Bcrypt.HashPassword(password);
            Assert.IsFalse(Bcrypt.Bcrypt.VerifyPassword(null, hash));
            Assert.IsFalse(Bcrypt.Bcrypt.VerifyPassword(password, ""));
        }

        [TestMethod]
        public void TestEmptyPassword()
        {
            string hash = Bcrypt.Bcrypt.HashPassword("");
            Assert.IsFalse(Bcrypt.Bcrypt.VerifyPassword("", hash));
        }

        [TestMethod]
        public void TestInvalidHashFormat()
        {
            string password = "password";
            Assert.IsFalse(Bcrypt.Bcrypt.VerifyPassword(password, "invalidhash"));
        }

        [TestMethod]
        public void TestCustomCost()
        {
            string password = "CustomCostPassword!";
            int cost = 12;
            string hash = Bcrypt.Bcrypt.HashPassword(password, cost);
            Assert.IsFalse(string.IsNullOrEmpty(hash));
            Assert.AreEqual(60, hash.Length);
            Assert.IsTrue(Bcrypt.Bcrypt.VerifyPassword(password, hash));
        }

        [TestMethod]
        public void TestSpecialCharacters()
        {
            string password = "!@#$%^&*()_+[]{}|;':,.<>?";
            string hash = Bcrypt.Bcrypt.HashPassword(password);
            Assert.IsFalse(string.IsNullOrEmpty(hash));
            Assert.AreEqual(60, hash.Length);
            Assert.IsTrue(Bcrypt.Bcrypt.VerifyPassword(password, hash));
        }

        [TestMethod]
        public void TestSyntheticLargeData()
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                string password = GenerateRandomString(random, 20);
                string hash = Bcrypt.Bcrypt.HashPassword(password);
                Assert.IsFalse(string.IsNullOrEmpty(hash));
                Assert.AreEqual(60, hash.Length);
                Assert.IsTrue(Bcrypt.Bcrypt.VerifyPassword(password, hash));
            }
        }

        private static string GenerateRandomString(Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}