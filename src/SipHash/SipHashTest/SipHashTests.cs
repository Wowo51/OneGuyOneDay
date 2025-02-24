using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SipHash;

namespace SipHashTest
{
    [TestClass]
    public class SipHashTests
    {
        [TestMethod]
        public void ComputeHash_NullData_UlongKeys_ReturnsZero()
        {
            ulong hash = SipHasher.ComputeHash(null, 0UL, 0UL);
            Assert.AreEqual(0UL, hash, "Expected hash 0 when data is null");
        }

        [TestMethod]
        public void ComputeHash_NullData_ByteKey_ReturnsZero()
        {
            byte[] key = new byte[16];
            ulong hash = SipHasher.ComputeHash(null, key);
            Assert.AreEqual(0UL, hash, "Expected hash 0 when data is null");
        }

        [TestMethod]
        public void ComputeHash_InvalidByteKey_ReturnsZero()
        {
            byte[] data = GenerateTestData(10);
            byte[] shortKey = new byte[8]; // invalid: length not equal to 16
            ulong hash = SipHasher.ComputeHash(data, shortKey);
            Assert.AreEqual(0UL, hash, "Expected hash 0 when key length is not 16");
            byte[] longKey = new byte[20]; // invalid: length not equal to 16
            hash = SipHasher.ComputeHash(data, longKey);
            Assert.AreEqual(0UL, hash, "Expected hash 0 when key length is not 16");
        }

        [TestMethod]
        public void ComputeHash_EmptyData_ReturnsConsistentHash()
        {
            byte[] data = new byte[0];
            ulong key0 = 0x0706050403020100UL;
            ulong key1 = 0x0F0E0D0C0B0A0908UL;
            ulong hash1 = SipHasher.ComputeHash(data, key0, key1);
            ulong hash2 = SipHasher.ComputeHash(data, key0, key1);
            Assert.AreEqual(hash1, hash2, "Hash should be consistent for empty array");
        }

        [TestMethod]
        public void ComputeHash_Consistency_WithRandomData()
        {
            System.Random random = new System.Random();
            for (int i = 0; i < 10; i++)
            {
                int length = random.Next(0, 1024);
                byte[] data = new byte[length];
                random.NextBytes(data);
                ulong key0 = (((ulong)(uint)random.Next()) << 32) | ((ulong)(uint)random.Next());
                ulong key1 = (((ulong)(uint)random.Next()) << 32) | ((ulong)(uint)random.Next());
                ulong hash1 = SipHasher.ComputeHash(data, key0, key1);
                ulong hash2 = SipHasher.ComputeHash(data, key0, key1);
                Assert.AreEqual(hash1, hash2, "Hash should be consistent for same input and keys");
            }
        }

        [TestMethod]
        public void ComputeHash_DifferentKeys_ProduceDifferentHashes()
        {
            byte[] data = GenerateTestData(64);
            ulong key0A = 0x0102030405060708UL;
            ulong key1A = 0x1112131415161718UL;
            ulong key0B = 0x8102030405060708UL; // slightly modified key
            ulong key1B = 0x9112131415161718UL;
            ulong hashA = SipHasher.ComputeHash(data, key0A, key1A);
            ulong hashB = SipHasher.ComputeHash(data, key0B, key1B);
            Assert.AreNotEqual(hashA, hashB, "Different keys should produce different hashes for the same data");
        }

        [TestMethod]
        public void ComputeHash_OverloadedMethods_AreConsistent()
        {
            byte[] data = GenerateTestData(100);
            ulong key0 = 0x1122334455667788UL;
            ulong key1 = 0x99AABBCCDDEEFF00UL;
            byte[] key = new byte[16];
            Array.Copy(BitConverter.GetBytes(key0), 0, key, 0, 8);
            Array.Copy(BitConverter.GetBytes(key1), 0, key, 8, 8);
            ulong hashUlong = SipHasher.ComputeHash(data, key0, key1);
            ulong hashByteKey = SipHasher.ComputeHash(data, key);
            Assert.AreEqual(hashUlong, hashByteKey, "Hash computed by both overloads should match");
        }

        private static byte[] GenerateTestData(int length)
        {
            byte[] array = new byte[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = (byte)(i % 256);
            }

            return array;
        }
    }
}