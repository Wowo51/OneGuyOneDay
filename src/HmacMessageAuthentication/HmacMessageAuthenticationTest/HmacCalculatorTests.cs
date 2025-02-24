using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using HmacMessageAuthentication;

namespace HmacMessageAuthenticationTest
{
    [TestClass]
    public class HmacCalculatorTests
    {
        [TestMethod]
        public void TestComputeHmacHex_WithKnownValues()
        {
            string keyString = "Jefe";
            string message = "what do ya want for nothing?";
            byte[] key = Encoding.ASCII.GetBytes(keyString);
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            string expected = "5bdcc146bf60754e6a042426089575c75a003f089d2739839dec58b964ec3843";
            string actual = HmacCalculator.ComputeHmacHex(key, messageBytes);
            Assert.AreEqual(expected, actual, "HMAC hex value mismatch.");
        }

        [TestMethod]
        public void TestComputeHmac_WithNullKey()
        {
            byte[] message = Encoding.ASCII.GetBytes("sample message");
            byte[] result = HmacCalculator.ComputeHmac(null, message);
            Assert.AreEqual(0, result.Length, "Expected empty array when key is null.");
        }

        [TestMethod]
        public void TestComputeHmac_WithNullMessage()
        {
            byte[] key = Encoding.ASCII.GetBytes("sample key");
            byte[] result = HmacCalculator.ComputeHmac(key, null);
            Assert.AreEqual(0, result.Length, "Expected empty array when message is null.");
        }

        [TestMethod]
        public void TestComputeHmac_WithBothNull()
        {
            byte[] result = HmacCalculator.ComputeHmac(null, null);
            Assert.AreEqual(0, result.Length, "Expected empty array when both key and message are null.");
        }

        [TestMethod]
        public void TestComputeHmac_RandomDataConsistency()
        {
            byte[] key = new byte[64];
            byte[] message = new byte[1024];
            Random random = new Random(42);
            random.NextBytes(key);
            random.NextBytes(message);
            byte[] result1 = HmacCalculator.ComputeHmac(key, message);
            byte[] result2 = HmacCalculator.ComputeHmac(key, message);
            Assert.AreEqual(32, result1.Length, "HMACSHA256 output must be 32 bytes.");
            for (int i = 0; i < result1.Length; i++)
            {
                Assert.AreEqual(result1[i], result2[i], "Mismatch at byte " + i);
            }
        }

        [TestMethod]
        public void TestComputeHmac_WithEmptyKeyAndEmptyMessage()
        {
            byte[] key = new byte[0];
            byte[] message = new byte[0];
            byte[] result = HmacCalculator.ComputeHmac(key, message);
            string hex = HmacCalculator.ComputeHmacHex(key, message);
            Assert.AreEqual(32, result.Length, "HMACSHA256 output must be 32 bytes for empty input.");
            Assert.AreEqual(64, hex.Length, "Hex string must be 64 characters long for empty input.");
        }

        [TestMethod]
        public void TestComputeHmac_LargeData()
        {
            byte[] key = new byte[256];
            byte[] message = new byte[10000];
            Random random = new Random(12345);
            random.NextBytes(key);
            random.NextBytes(message);
            byte[] result = HmacCalculator.ComputeHmac(key, message);
            string hex = HmacCalculator.ComputeHmacHex(key, message);
            Assert.AreEqual(32, result.Length, "HMACSHA256 output must be 32 bytes for large data.");
            Assert.AreEqual(64, hex.Length, "Hex string must be 64 characters long for large data.");
            byte[] result2 = HmacCalculator.ComputeHmac(key, message);
            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(result[i], result2[i], "Mismatch at byte " + i);
            }
        }
    }
}