using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EllipticCurveDH;
using System.Security.Cryptography;

namespace EllipticCurveDHTest
{
    [TestClass]
    public class ECDHKeyExchangeTests
    {
        [TestMethod]
        public void TestPublicKeyNotNull()
        {
            using (ECDHKeyExchange ecdh = new ECDHKeyExchange())
            {
                Assert.IsNotNull(ecdh.PublicKey, "PublicKey should not be null.");
                Assert.IsTrue(ecdh.PublicKey.Length > 0, "PublicKey should not be empty.");
            }
        }

        [TestMethod]
        public void TestSharedSecretAgreement()
        {
            using (ECDHKeyExchange alice = new ECDHKeyExchange())
            using (ECDHKeyExchange bob = new ECDHKeyExchange())
            {
                byte[] alicePublic = alice.PublicKey;
                byte[] bobPublic = bob.PublicKey;
                byte[] aliceSecret = alice.DeriveSharedSecret(bobPublic);
                byte[] bobSecret = bob.DeriveSharedSecret(alicePublic);
                CollectionAssert.AreEqual(aliceSecret, bobSecret, "Shared secrets should match.");
            }
        }

        [TestMethod]
        public void TestDeriveSharedSecretNullInput()
        {
            using (ECDHKeyExchange keyExchange = new ECDHKeyExchange())
            {
                byte[] result = keyExchange.DeriveSharedSecret(null);
                Assert.IsNotNull(result, "Result should not be null.");
                Assert.AreEqual(0, result.Length, "Result should be empty for null input.");
            }
        }

        [TestMethod]
        public void TestDeriveSharedSecretInvalidInput()
        {
            using (ECDHKeyExchange keyExchange = new ECDHKeyExchange())
            {
                byte[] invalidKey = new byte[]
                {
                    0,
                    1,
                    2,
                    3
                };
                Assert.ThrowsException<CryptographicException>(() =>
                {
                    keyExchange.DeriveSharedSecret(invalidKey);
                }, "Invalid public key should throw CryptographicException.");
            }
        }

        [TestMethod]
        public void TestMultipleKeyExchanges()
        {
            for (int iteration = 0; iteration < 100; iteration++)
            {
                using (ECDHKeyExchange alice = new ECDHKeyExchange())
                using (ECDHKeyExchange bob = new ECDHKeyExchange())
                {
                    byte[] secretAlice = alice.DeriveSharedSecret(bob.PublicKey);
                    byte[] secretBob = bob.DeriveSharedSecret(alice.PublicKey);
                    CollectionAssert.AreEqual(secretAlice, secretBob, "Iteration " + iteration + ": Shared secrets do not match.");
                }
            }
        }

        [TestMethod]
        public void TestMultipleDisposeCalls()
        {
            ECDHKeyExchange keyExchange = new ECDHKeyExchange();
            keyExchange.Dispose();
            keyExchange.Dispose();
            Assert.IsTrue(true, "Multiple Dispose calls should not throw exceptions.");
        }

        [TestMethod]
        public void TestDeriveSharedSecretRandomInvalidInputs()
        {
            using (ECDHKeyExchange keyExchange = new ECDHKeyExchange())
            {
                Random random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    int length = random.Next(1, 10);
                    byte[] randomInvalid = new byte[length];
                    for (int j = 0; j < length; j++)
                    {
                        randomInvalid[j] = (byte)random.Next(0, 256);
                    }

                    Assert.ThrowsException<CryptographicException>(() =>
                    {
                        keyExchange.DeriveSharedSecret(randomInvalid);
                    }, "Random invalid public key should throw CryptographicException.");
                }
            }
        }

        [TestMethod]
        public void TestRepeatedDeriveSharedSecretSameInput()
        {
            using (ECDHKeyExchange alice = new ECDHKeyExchange())
            using (ECDHKeyExchange bob = new ECDHKeyExchange())
            {
                byte[] bobPublic = bob.PublicKey;
                byte[] secret1 = alice.DeriveSharedSecret(bobPublic);
                byte[] secret2 = alice.DeriveSharedSecret(bobPublic);
                CollectionAssert.AreEqual(secret1, secret2, "Repeated derivation with the same remote public key should yield the same secret.");
            }
        }
    }
}