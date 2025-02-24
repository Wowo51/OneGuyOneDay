using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HashFunction;

namespace HashFunctionTest
{
    [TestClass]
    public class HasherTests
    {
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder(length);
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }

            return builder.ToString();
        }

        private static byte[] GenerateRandomByteArray(int length)
        {
            byte[] data = new byte[length];
            Random random = new Random();
            random.NextBytes(data);
            return data;
        }

        [TestMethod]
        public void TestComputeHash_NullString_ReturnsZero()
        {
            int result = Hasher.ComputeHash((string)null);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestComputeHash_EmptyString_ReturnsInitialValue()
        {
            int result = Hasher.ComputeHash("");
            Assert.AreEqual(17, result);
        }

        [TestMethod]
        public void TestComputeHash_SingleCharacter()
        {
            int result = Hasher.ComputeHash("a");
            Assert.AreEqual(624, result);
        }

        [TestMethod]
        public void TestComputeHash_TwoCharacters()
        {
            int result = Hasher.ComputeHash("ab");
            Assert.AreEqual(19442, result);
        }

        [TestMethod]
        public void TestComputeHash_Idempotence_String()
        {
            string input = GenerateRandomString(100);
            int first = Hasher.ComputeHash(input);
            int second = Hasher.ComputeHash(input);
            Assert.AreEqual(first, second);
        }

        [TestMethod]
        public void TestComputeHash_NullByteArray_ReturnsZero()
        {
            int result = Hasher.ComputeHash((byte[])null);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestComputeHash_EmptyByteArray_ReturnsInitialValue()
        {
            int result = Hasher.ComputeHash(new byte[0]);
            Assert.AreEqual(17, result);
        }

        [TestMethod]
        public void TestComputeHash_ByteArray_KnownValues()
        {
            byte[] data = new byte[]
            {
                1,
                2,
                3
            };
            int result = Hasher.ComputeHash(data);
            Assert.AreEqual(507473, result);
        }

        [TestMethod]
        public void TestComputeHash_Idempotence_ByteArray()
        {
            byte[] data = GenerateRandomByteArray(100);
            int first = Hasher.ComputeHash(data);
            int second = Hasher.ComputeHash(data);
            Assert.AreEqual(first, second);
        }

        [TestMethod]
        public void TestComputeHash_LargeRandomString()
        {
            string input = GenerateRandomString(1000);
            int first = Hasher.ComputeHash(input);
            int second = Hasher.ComputeHash(input);
            Assert.AreEqual(first, second);
            Assert.AreNotEqual(17, first);
        }

        [TestMethod]
        public void TestComputeHash_LargeRandomByteArray()
        {
            byte[] data = GenerateRandomByteArray(1000);
            int first = Hasher.ComputeHash(data);
            int second = Hasher.ComputeHash(data);
            Assert.AreEqual(first, second);
            Assert.AreNotEqual(17, first);
        }

        [TestMethod]
        public void TestComputeHash_NonAsciiCharacters()
        {
            string input = "ñöç";
            int result = Hasher.ComputeHash(input);
            Assert.AreEqual(745905, result);
        }
    }
}