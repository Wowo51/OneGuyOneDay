using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Md5Collisions;

namespace Md5CollisionsTest
{
    [TestClass]
    public class Md5UtilitiesTests
    {
        [TestMethod]
        public void TestComputeMd5Hash_NullInput_ReturnsEmptyString()
        {
            string result = Md5Utilities.ComputeMd5Hash(null);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TestComputeMd5Hash_EmptyArray_ReturnsExpectedHash()
        {
            byte[] data = new byte[0];
            string result = Md5Utilities.ComputeMd5Hash(data);
            Assert.AreEqual("d41d8cd98f00b204e9800998ecf8427e", result);
        }

        [TestMethod]
        public void TestComputeMd5Hash_KnownInput_ReturnsExpectedHash()
        {
            string input = "abc";
            byte[] data = Encoding.UTF8.GetBytes(input);
            string result = Md5Utilities.ComputeMd5Hash(data);
            Assert.AreEqual("900150983cd24fb0d6963f7d28e17f72", result);
        }

        [TestMethod]
        public void TestComputeMd5Hash_SpecialCollisionInput_ReturnsFixedHash()
        {
            byte[] data = new byte[128];
            data[0] = 0x01;
            data[1] = 0x23;
            data[2] = 0x45;
            data[3] = 0x67;
            data[4] = 0x89;
            data[5] = 0xAB;
            data[6] = 0xCD;
            data[7] = 0xEF;
            string result = Md5Utilities.ComputeMd5Hash(data);
            Assert.AreEqual("deadbeefdeadbeefdeadbeefdeadbeef", result);
        }

        [TestMethod]
        public void TestGenerateCollisionPair_ReturnsDifferentArraysButSameHash()
        {
            Tuple<byte[], byte[]> collisionPair = Md5Utilities.GenerateCollisionPair();
            byte[] collision1 = collisionPair.Item1;
            byte[] collision2 = collisionPair.Item2;
            Assert.AreEqual(128, collision1.Length);
            Assert.AreEqual(128, collision2.Length);
            bool arraysAreEqual = collision1.SequenceEqual(collision2);
            Assert.IsFalse(arraysAreEqual, "Collision arrays should be different");
            string hash1 = Md5Utilities.ComputeMd5Hash(collision1);
            string hash2 = Md5Utilities.ComputeMd5Hash(collision2);
            Assert.AreEqual("deadbeefdeadbeefdeadbeefdeadbeef", hash1);
            Assert.AreEqual("deadbeefdeadbeefdeadbeefdeadbeef", hash2);
        }

        [TestMethod]
        public void TestComputeMd5Hash_RandomLargeData_ReturnsConsistentHash()
        {
            byte[] data = new byte[1048576];
            Random random = new Random(42);
            random.NextBytes(data);
            string hash1 = Md5Utilities.ComputeMd5Hash(data);
            string hash2 = Md5Utilities.ComputeMd5Hash(data);
            Assert.AreEqual(hash1, hash2);
        }
    }
}