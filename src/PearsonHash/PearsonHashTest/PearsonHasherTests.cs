using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using PearsonHash;

namespace PearsonHashTest
{
    [TestClass]
    public class PearsonHasherTests
    {
        [TestMethod]
        public void TestComputeHash_NullByteArray()
        {
            byte expected = 0;
            byte actual = PearsonHasher.ComputeHash((byte[])null);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestComputeHash_NullString()
        {
            byte expected = 0;
            byte actual = PearsonHasher.ComputeHash((string)null);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestComputeHash_EmptyByteArray()
        {
            byte[] data = new byte[0];
            byte expected = 0;
            byte actual = PearsonHasher.ComputeHash(data);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestComputeHash_EmptyString()
        {
            string data = string.Empty;
            byte expected = 0;
            byte actual = PearsonHasher.ComputeHash(data);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestComputeHash_SimpleString()
        {
            string data = "a";
            // For "a", the hash is computed as T[0 XOR 0x61] = T[0x61]. From the PearsonHasher table T, T[97] is 0xEF.
            byte expected = 0xEF;
            byte actual = PearsonHasher.ComputeHash(data);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestComputeHash_MixedBytes()
        {
            byte[] data = new byte[]
            {
                1,
                2,
                3,
                4
            };
            // Calculation:
            // r0 = 0
            // r1 = T[0 XOR 1] = T[1] = 0x7C
            // r2 = T[0x7C XOR 2] = T[126] = 0xF3
            // r3 = T[0xF3 XOR 3] = T[240] = 0x8C
            // r4 = T[0x8C XOR 4] = T[136] = 0xC4
            byte expected = 0xC4;
            byte actual = PearsonHasher.ComputeHash(data);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestComputeHash_StringByteConsistency()
        {
            string data = "The quick brown fox jumps over the lazy dog";
            byte hashFromString = PearsonHasher.ComputeHash(data);
            byte hashFromBytes = PearsonHasher.ComputeHash(Encoding.UTF8.GetBytes(data));
            Assert.AreEqual(hashFromString, hashFromBytes);
        }

        [TestMethod]
        public void TestComputeHash_RandomDataConsistency()
        {
            Random random = new Random(12345);
            byte[] data = new byte[1024];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)random.Next(0, 256);
            }

            byte hash1 = PearsonHasher.ComputeHash(data);
            byte hash2 = PearsonHasher.ComputeHash(data);
            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void TestComputeHash_LargeRandomDataConsistency()
        {
            Random random = new Random(67890);
            byte[] data = new byte[10000];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)random.Next(0, 256);
            }

            byte hash1 = PearsonHasher.ComputeHash(data);
            byte hash2 = PearsonHasher.ComputeHash(data);
            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void TestComputeHash_LargeRandomStringConsistency()
        {
            Random random = new Random(54321);
            StringBuilder sb = new StringBuilder(10000);
            for (int i = 0; i < 10000; i++)
            {
                int ascii = random.Next(32, 127);
                sb.Append((char)ascii);
            }

            string data = sb.ToString();
            byte hashFromString = PearsonHasher.ComputeHash(data);
            byte hashFromBytes = PearsonHasher.ComputeHash(Encoding.UTF8.GetBytes(data));
            Assert.AreEqual(hashFromString, hashFromBytes);
        }
    }
}