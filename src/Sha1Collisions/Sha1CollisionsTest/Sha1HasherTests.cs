using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sha1Collisions;

namespace Sha1CollisionsTest
{
    [TestClass]
    public class Sha1HasherTests
    {
        [TestMethod]
        public void ComputeHash_WithNonNullInput_ReturnsExpectedHashForABC()
        {
            string input = "abc";
            string expectedHash = "a9993e364706816aba3e25717850c26c9cd0d89d";
            string actualHash = Sha1Hasher.ComputeHash(input);
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void ComputeHash_WithEmptyString_ReturnsExpectedHash()
        {
            string input = "";
            string expectedHash = "da39a3ee5e6b4b0d3255bfef95601890afd80709";
            string actualHash = Sha1Hasher.ComputeHash(input);
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void ComputeHash_WithNullInput_ReturnsHashOfEmptyString()
        {
            string expectedHash = "da39a3ee5e6b4b0d3255bfef95601890afd80709";
            string actualHash = Sha1Hasher.ComputeHash(null);
            Assert.AreEqual(expectedHash, actualHash);
        }

        [TestMethod]
        public void ComputeHash_IsConsistent()
        {
            string input = "The quick brown fox jumps over the lazy dog";
            string hash1 = Sha1Hasher.ComputeHash(input);
            string hash2 = Sha1Hasher.ComputeHash(input);
            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void ComputeHash_WithLargeInput_ReturnsHashOfLength40()
        {
            string input = new string ('a', 10000);
            string hash = Sha1Hasher.ComputeHash(input);
            Assert.IsNotNull(hash);
            Assert.AreEqual(40, hash.Length);
        }
    }
}