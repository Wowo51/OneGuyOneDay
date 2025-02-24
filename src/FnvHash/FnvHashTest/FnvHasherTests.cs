using Microsoft.VisualStudio.TestTools.UnitTesting;
using FnvHash;
using System;
using System.Text;

namespace FnvHashTest
{
    [TestClass]
    public class FnvHasherTests
    {
        [TestMethod]
        public void ComputeHash_NullByteArray_ReturnsZero()
        {
            uint result = FnvHasher.ComputeHash((byte[])null);
            Assert.AreEqual(0u, result);
        }

        [TestMethod]
        public void ComputeHash_EmptyByteArray_ReturnsOffsetBasis()
        {
            byte[] data = new byte[0];
            uint result = FnvHasher.ComputeHash(data);
            Assert.AreEqual(2166136261u, result);
        }

        [TestMethod]
        public void ComputeHash_NullString_ReturnsHashOfEmpty()
        {
            string input = null;
            uint result = FnvHasher.ComputeHash(input);
            Assert.AreEqual(2166136261u, result);
        }

        [TestMethod]
        public void ComputeHash_EmptyString_ReturnsOffsetBasis()
        {
            string input = "";
            uint result = FnvHasher.ComputeHash(input);
            Assert.AreEqual(2166136261u, result);
        }

        [TestMethod]
        public void ComputeHash_HelloString_ReturnsExpectedHash()
        {
            string input = "hello";
            uint result = FnvHasher.ComputeHash(input);
            Assert.AreEqual(1335831723u, result);
        }

        [TestMethod]
        public void ComputeHash_StringVsByte_ReturnsSameResult()
        {
            string input = "Testing FnvHasher: string vs byte";
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            uint hashFromString = FnvHasher.ComputeHash(input);
            uint hashFromBytes = FnvHasher.ComputeHash(bytes);
            Assert.AreEqual(hashFromBytes, hashFromString);
        }

        [TestMethod]
        public void ComputeHash_LargeRandomArray_IsConsistent()
        {
            int size = 10000;
            byte[] data = new byte[size];
            Random random = new Random(42);
            for (int i = 0; i < size; i++)
            {
                data[i] = (byte)random.Next(0, 256);
            }

            uint hash1 = FnvHasher.ComputeHash(data);
            uint hash2 = FnvHasher.ComputeHash(data);
            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void ComputeHash_MultibyteString_Consistent()
        {
            string input = "你好, мир, hello 🌍";
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            uint hashFromString = FnvHasher.ComputeHash(input);
            uint hashFromBytes = FnvHasher.ComputeHash(bytes);
            Assert.AreEqual(hashFromString, hashFromBytes);
        }

        [TestMethod]
        public void ComputeHash_RandomString_IsConsistent()
        {
            int length = 5000;
            StringBuilder builder = new StringBuilder(length);
            Random random = new Random(100);
            for (int i = 0; i < length; i++)
            {
                char c = (char)random.Next(32, 127);
                builder.Append(c);
            }

            string input = builder.ToString();
            uint hash1 = FnvHasher.ComputeHash(input);
            uint hash2 = FnvHasher.ComputeHash(input);
            Assert.AreEqual(hash1, hash2);
        }
    }
}