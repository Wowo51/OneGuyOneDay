using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rsa;
using System;
using System.Numerics;

namespace RsaTest
{
    [TestClass]
    public class RsaUnitTests
    {
        [TestMethod]
        public void TestEncryptionDecryption()
        {
            BigInteger p = new BigInteger(61);
            BigInteger q = new BigInteger(53);
            BigInteger e = new BigInteger(17);
            RsaAlgorithm rsa = new RsaAlgorithm(p, q, e);
            BigInteger original = new BigInteger(65);
            BigInteger cipher = rsa.Encrypt(original);
            BigInteger decrypted = rsa.Decrypt(cipher);
            Assert.AreEqual(original, decrypted, "Decrypted message should match the original.");
        }

        [TestMethod]
        public void TestInvalidPrimes()
        {
            BigInteger p = new BigInteger(1);
            BigInteger q = new BigInteger(53);
            BigInteger e = new BigInteger(17);
            RsaAlgorithm rsa = new RsaAlgorithm(p, q, e);
            BigInteger cipher = rsa.Encrypt(new BigInteger(123));
            Assert.AreEqual(BigInteger.Zero, cipher, "Encryption should fail and return 0 for invalid primes.");
        }

        [TestMethod]
        public void TestInvalidExponent()
        {
            BigInteger p = new BigInteger(61);
            BigInteger q = new BigInteger(53);
            // 60 is not coprime with phi(61-1 * 53-1 = 3120)
            BigInteger e = new BigInteger(60);
            RsaAlgorithm rsa = new RsaAlgorithm(p, q, e);
            BigInteger cipher = rsa.Encrypt(new BigInteger(123));
            Assert.AreEqual(BigInteger.Zero, cipher, "Encryption should fail and return 0 for invalid exponent.");
        }

        [TestMethod]
        public void TestEncryptionDecryptionSynthetic()
        {
            BigInteger p = new BigInteger(61);
            BigInteger q = new BigInteger(53);
            BigInteger e = new BigInteger(17);
            RsaAlgorithm rsa = new RsaAlgorithm(p, q, e);
            BigInteger modulus = rsa.Modulus;
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int randomValue = rnd.Next(0, (int)modulus);
                BigInteger message = new BigInteger(randomValue);
                BigInteger cipher = rsa.Encrypt(message);
                BigInteger decrypted = rsa.Decrypt(cipher);
                Assert.AreEqual(message, decrypted, "Synthetic test failed for message: " + randomValue);
            }
        }

        [TestMethod]
        public void TestEncryptionDecryptionAllMessages()
        {
            BigInteger p = new BigInteger(61);
            BigInteger q = new BigInteger(53);
            BigInteger e = new BigInteger(17);
            RsaAlgorithm rsa = new RsaAlgorithm(p, q, e);
            // Test a range of messages to cover a variety of cases.
            for (int i = 0; i < 100; i++)
            {
                BigInteger message = new BigInteger(i);
                BigInteger cipher = rsa.Encrypt(message);
                BigInteger decrypted = rsa.Decrypt(cipher);
                Assert.AreEqual(message, decrypted, "Message " + i + " failed encryption-decryption cycle.");
            }
        }
    }
}