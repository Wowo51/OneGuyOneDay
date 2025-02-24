using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sha3Variants;

namespace Sha3VariantsTest
{
    [TestClass]
    public class Sha3VariantsTests
    {
        private static byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            Random random = new Random(42);
            for (int i = 0; i < length; i++)
            {
                bytes[i] = (byte)random.Next(0, 256);
            }

            return bytes;
        }

        private static void AssertArraysEqual(byte[] expected, byte[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length, "Array lengths differ.");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i], $"Arrays differ at index {i}.");
            }
        }

        [TestMethod]
        public void Sha3_224_EmptyInputTest()
        {
            Sha3_224 sha3 = new Sha3_224();
            byte[] hashFromEmpty = sha3.ComputeHash(new byte[0]);
            byte[] hashFromNull = sha3.ComputeHash(null);
            Assert.AreEqual(28, hashFromEmpty.Length, "Sha3_224 hash length should be 28 bytes.");
            CollectionAssert.AreEqual(hashFromEmpty, hashFromNull, "Empty input hashing should be consistent.");
        }

        [TestMethod]
        public void Sha3_256_RandomInputTest()
        {
            Sha3_256 sha3 = new Sha3_256();
            byte[] message = GenerateRandomBytes(128);
            byte[] hash1 = sha3.ComputeHash(message);
            byte[] hash2 = sha3.ComputeHash(message);
            Assert.AreEqual(32, hash1.Length, "Sha3_256 hash length should be 32 bytes.");
            CollectionAssert.AreEqual(hash1, hash2, "Hashing the same input should produce the same output.");
        }

        [TestMethod]
        public void Sha3_384_RandomInputTest()
        {
            Sha3_384 sha3 = new Sha3_384();
            byte[] message = GenerateRandomBytes(256);
            byte[] hash = sha3.ComputeHash(message);
            Assert.AreEqual(48, hash.Length, "Sha3_384 hash length should be 48 bytes.");
        }

        [TestMethod]
        public void Sha3_512_RandomInputTest()
        {
            Sha3_512 sha3 = new Sha3_512();
            byte[] message = GenerateRandomBytes(512);
            byte[] hash = sha3.ComputeHash(message);
            Assert.AreEqual(64, hash.Length, "Sha3_512 hash length should be 64 bytes.");
        }

        [TestMethod]
        public void DifferentInputProducesDifferentHashTest()
        {
            Sha3_256 sha3 = new Sha3_256();
            byte[] message1 = GenerateRandomBytes(64);
            byte[] message2 = new byte[64];
            for (int i = 0; i < 64; i++)
            {
                message2[i] = message1[i];
            }

            // Modify one byte
            message2[0] = (byte)(message2[0] ^ 0xFF);
            byte[] hash1 = sha3.ComputeHash(message1);
            byte[] hash2 = sha3.ComputeHash(message2);
            bool areEqual = true;
            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                {
                    areEqual = false;
                    break;
                }
            }

            Assert.IsFalse(areEqual, "Different inputs should produce different hashes for Sha3_256.");
        }

        [TestMethod]
        public void Shake128_EmptyInputTest()
        {
            Shake128 shake = new Shake128();
            byte[] hashFromEmpty = shake.ComputeHash(new byte[0], 64);
            byte[] hashFromNull = shake.ComputeHash(null, 64);
            Assert.AreEqual(64, hashFromEmpty.Length, "Shake128 output length should match the requested length.");
            CollectionAssert.AreEqual(hashFromEmpty, hashFromNull, "Empty input hashing with Shake128 should be consistent.");
        }

        [TestMethod]
        public void Shake256_RandomInputTest()
        {
            Shake256 shake = new Shake256();
            byte[] message = GenerateRandomBytes(200);
            byte[] hash1 = shake.ComputeHash(message, 128);
            byte[] hash2 = shake.ComputeHash(message, 128);
            Assert.AreEqual(128, hash1.Length, "Shake256 output length should match the requested length.");
            CollectionAssert.AreEqual(hash1, hash2, "Hashing the same input with Shake256 should produce the same output.");
        }

        [TestMethod]
        public void ShakeDifferentInputProducesDifferentHashTest()
        {
            Shake256 shake = new Shake256();
            byte[] message1 = GenerateRandomBytes(64);
            byte[] message2 = new byte[64];
            for (int i = 0; i < 64; i++)
            {
                message2[i] = message1[i];
            }

            message2[0] = (byte)(message2[0] ^ 0xFF);
            byte[] hash1 = shake.ComputeHash(message1, 128);
            byte[] hash2 = shake.ComputeHash(message2, 128);
            bool areEqual = true;
            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                {
                    areEqual = false;
                    break;
                }
            }

            Assert.IsFalse(areEqual, "Different inputs should produce different hashes for Shake256.");
        }

        [TestMethod]
        public void LargeInputConsistencyTest()
        {
            byte[] message = GenerateRandomBytes(10000);
            Sha3_256 sha3_256 = new Sha3_256();
            byte[] hash1 = sha3_256.ComputeHash(message);
            byte[] hash2 = sha3_256.ComputeHash(message);
            CollectionAssert.AreEqual(hash1, hash2, "Large input consistency test for Sha3_256 failed.");
            Shake128 shake128 = new Shake128();
            byte[] shake1 = shake128.ComputeHash(message, 256);
            byte[] shake2 = shake128.ComputeHash(message, 256);
            CollectionAssert.AreEqual(shake1, shake2, "Large input consistency test for Shake128 failed.");
        }
    }
}