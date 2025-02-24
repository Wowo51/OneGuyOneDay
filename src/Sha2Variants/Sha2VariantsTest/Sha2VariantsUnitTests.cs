using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sha2Variants;

namespace Sha2VariantsTest
{
    [TestClass]
    public class Sha2VariantsUnitTests
    {
        private static string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.Append(b.ToString("x2"));
            }

            return hex.ToString();
        }

        [TestMethod]
        public void TestSha224_KnownVector()
        {
            byte[] input = Encoding.ASCII.GetBytes("abc");
            byte[] hash = Sha2.ComputeSha224(input);
            string expected = "23097d223405d8228642a477bda255b32aadbce4bda0b3f7e36c9da7";
            Assert.AreEqual(expected, ByteArrayToHexString(hash));
        }

        [TestMethod]
        public void TestSha256_KnownVector()
        {
            byte[] input = Encoding.ASCII.GetBytes("abc");
            byte[] hash = Sha2.ComputeSha256(input);
            string expected = "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad";
            Assert.AreEqual(expected, ByteArrayToHexString(hash));
        }

        [TestMethod]
        public void TestSha384_KnownVector()
        {
            byte[] input = Encoding.ASCII.GetBytes("abc");
            byte[] hash = Sha2.ComputeSha384(input);
            string expected = "cb00753f45a35e8bb5a03d699ac65007272c32ab0eded1631a8b605a43ff5bed8086072ba1e7cc2358baeca134c825a7";
            Assert.AreEqual(expected, ByteArrayToHexString(hash));
        }

        [TestMethod]
        public void TestSha512_KnownVector()
        {
            byte[] input = Encoding.ASCII.GetBytes("abc");
            byte[] hash = Sha2.ComputeSha512(input);
            string expected = "ddaf35a193617abacc417349ae20413112e6fa4e89a97ea20a9eeee64b55d39a2192992a274fc1a836ba3c23a3feebbd454d4423643ce80e2a9ac94fa54ca49f";
            Assert.AreEqual(expected, ByteArrayToHexString(hash));
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            byte[] input = new byte[0];
            byte[] hash224 = Sha2.ComputeSha224(input);
            string expected224 = "d14a028c2a3a2bc9476102bb288234c415a2b01f828ea62ac5b3e42f";
            Assert.AreEqual(expected224, ByteArrayToHexString(hash224));
            byte[] hash256 = Sha2.ComputeSha256(input);
            string expected256 = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
            Assert.AreEqual(expected256, ByteArrayToHexString(hash256));
            byte[] hash384 = Sha2.ComputeSha384(input);
            string expected384 = "38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b";
            Assert.AreEqual(expected384, ByteArrayToHexString(hash384));
            byte[] hash512 = Sha2.ComputeSha512(input);
            string expected512 = "cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e";
            Assert.AreEqual(expected512, ByteArrayToHexString(hash512));
        }

        [TestMethod]
        public void TestLargeRandomData()
        {
            int size = 1024 * 1024;
            byte[] data = new byte[size];
            Random random = new Random(12345);
            random.NextBytes(data);
            byte[] hash224 = Sha2.ComputeSha224(data);
            Assert.AreEqual(28, hash224.Length);
            byte[] hash256 = Sha2.ComputeSha256(data);
            Assert.AreEqual(32, hash256.Length);
            byte[] hash384 = Sha2.ComputeSha384(data);
            Assert.AreEqual(48, hash384.Length);
            byte[] hash512 = Sha2.ComputeSha512(data);
            Assert.AreEqual(64, hash512.Length);
        }

        [TestMethod]
        public void TestConsistentResults()
        {
            byte[] input = Encoding.ASCII.GetBytes("TestConsistency");
            byte[] hash1 = Sha2.ComputeSha256(input);
            byte[] hash2 = Sha2.ComputeSha256(input);
            CollectionAssert.AreEqual(hash1, hash2);
            byte[] hashA = Sha2.ComputeSha224(input);
            byte[] hashB = Sha2.ComputeSha224(input);
            CollectionAssert.AreEqual(hashA, hashB);
        }
    }
}