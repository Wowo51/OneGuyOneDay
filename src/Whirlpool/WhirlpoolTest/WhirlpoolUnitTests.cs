using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;
using System.Text;
using Whirlpool;

namespace WhirlpoolTest
{
    [TestClass]
    public class WhirlpoolUnitTests
    {
        [TestMethod]
        public void TestEmptyInput()
        {
            WhirlpoolHashAlgorithm whirlpool1 = new WhirlpoolHashAlgorithm();
            byte[] input = new byte[0];
            byte[] hash1 = whirlpool1.ComputeHash(input);
            Assert.IsNotNull(hash1);
            Assert.AreEqual(64, hash1.Length);
            WhirlpoolHashAlgorithm whirlpool2 = new WhirlpoolHashAlgorithm();
            byte[] hash2 = whirlpool2.ComputeHash(input);
            CollectionAssert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void TestDifferentInputs()
        {
            string input1 = "hello";
            string input2 = "world";
            byte[] data1 = Encoding.UTF8.GetBytes(input1);
            byte[] data2 = Encoding.UTF8.GetBytes(input2);
            WhirlpoolHashAlgorithm whirlpool = new WhirlpoolHashAlgorithm();
            byte[] hash1 = whirlpool.ComputeHash(data1);
            byte[] hash2 = whirlpool.ComputeHash(data2);
            CollectionAssert.AreNotEqual(hash1, hash2, "Hashes for different inputs should differ");
        }

        [TestMethod]
        public void TestConsistency()
        {
            string input = "The quick brown fox jumps over the lazy dog";
            byte[] data = Encoding.UTF8.GetBytes(input);
            WhirlpoolHashAlgorithm whirlpool1 = new WhirlpoolHashAlgorithm();
            byte[] hash1 = whirlpool1.ComputeHash(data);
            WhirlpoolHashAlgorithm whirlpool2 = new WhirlpoolHashAlgorithm();
            byte[] hash2 = whirlpool2.ComputeHash(data);
            CollectionAssert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void TestIncrementalHashing()
        {
            string input = "Incremental hashing test input";
            byte[] data = Encoding.UTF8.GetBytes(input);
            WhirlpoolHashAlgorithm alg = new WhirlpoolHashAlgorithm();
            int mid = data.Length / 2;
            alg.Initialize();
            alg.TransformBlock(data, 0, mid, data, 0);
            alg.TransformFinalBlock(data, mid, data.Length - mid);
            byte[] hashIncremental = alg.Hash!; // null-forgiving operator to suppress CS8600 warning
            WhirlpoolHashAlgorithm alg2 = new WhirlpoolHashAlgorithm();
            byte[] fullHash = alg2.ComputeHash(data);
            CollectionAssert.AreEqual(fullHash, hashIncremental);
        }

        [TestMethod]
        public void TestLargeRandomInput()
        {
            int size = 1024 * 1024;
            byte[] largeData = new byte[size];
            Random random = new Random(12345);
            random.NextBytes(largeData);
            WhirlpoolHashAlgorithm alg1 = new WhirlpoolHashAlgorithm();
            byte[] hash1 = alg1.ComputeHash(largeData);
            WhirlpoolHashAlgorithm alg2 = new WhirlpoolHashAlgorithm();
            byte[] hash2 = alg2.ComputeHash(largeData);
            CollectionAssert.AreEqual(hash1, hash2, "Large random input hash mismatch");
            Assert.AreEqual(64, hash1.Length);
        }
    }
}