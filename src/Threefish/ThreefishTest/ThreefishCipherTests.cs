using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Threefish;

namespace ThreefishTest
{
    [TestClass]
    public class ThreefishCipherTests
    {
        [TestMethod]
        public void EncryptDecrypt_SimpleTest()
        {
            UInt64[] key = new UInt64[4]
            {
                0x0123456789ABCDEFUL,
                0x0FEDCBA987654321UL,
                0x0011223344556677UL,
                0x8899AABBCCDDEEFFUL
            };
            UInt64[] tweak = new UInt64[2]
            {
                0x0102030405060708UL,
                0x0807060504030201UL
            };
            ThreefishCipher cipher = new ThreefishCipher(key, tweak);
            UInt64[] plaintext = new UInt64[4]
            {
                0x1111111111111111UL,
                0x2222222222222222UL,
                0x3333333333333333UL,
                0x4444444444444444UL
            };
            UInt64[] ciphertext = cipher.Encrypt(plaintext);
            UInt64[] decrypted = cipher.Decrypt(ciphertext);
            CollectionAssert.AreEqual(plaintext, decrypted);
        }

        [TestMethod]
        public void EncryptDecrypt_NullBlockTest()
        {
            UInt64[] key = new UInt64[4]
            {
                0xFEDCBA9876543210UL,
                0x0123456789ABCDEFUL,
                0x9988776655443322UL,
                0x2233445566778899UL
            };
            UInt64[] tweak = new UInt64[2]
            {
                0x0F0E0D0C0B0A0908UL,
                0x08090A0B0C0D0E0FUL
            };
            ThreefishCipher cipher = new ThreefishCipher(key, tweak);
            UInt64[] ciphertext = cipher.Encrypt(null);
            UInt64[] decrypted = cipher.Decrypt(ciphertext);
            UInt64[] expected = new UInt64[4]
            {
                0UL,
                0UL,
                0UL,
                0UL
            };
            CollectionAssert.AreEqual(expected, decrypted);
        }

        [TestMethod]
        public void EncryptDecrypt_RandomDataTest()
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                UInt64[] key = new UInt64[4];
                for (int j = 0; j < 4; j++)
                {
                    byte[] buffer = new byte[8];
                    random.NextBytes(buffer);
                    key[j] = BitConverter.ToUInt64(buffer, 0);
                }

                UInt64[] tweak = new UInt64[2];
                for (int j = 0; j < 2; j++)
                {
                    byte[] buffer = new byte[8];
                    random.NextBytes(buffer);
                    tweak[j] = BitConverter.ToUInt64(buffer, 0);
                }

                ThreefishCipher cipher = new ThreefishCipher(key, tweak);
                UInt64[] plaintext = new UInt64[4];
                for (int j = 0; j < 4; j++)
                {
                    byte[] buffer = new byte[8];
                    random.NextBytes(buffer);
                    plaintext[j] = BitConverter.ToUInt64(buffer, 0);
                }

                UInt64[] ciphertext = cipher.Encrypt(plaintext);
                UInt64[] decrypted = cipher.Decrypt(ciphertext);
                CollectionAssert.AreEqual(plaintext, decrypted, "Failed random test iteration " + i);
            }
        }

        [TestMethod]
        public void EncryptDecrypt_ShortKeyAndTweakTest()
        {
            UInt64[] key = new UInt64[2]
            {
                0xDEADBEEFDEADBEEFUL,
                0xFEEDFACEFEEDFACEUL
            };
            UInt64[] tweak = new UInt64[1]
            {
                0xCAFEBABECAFEBABEUL
            };
            ThreefishCipher cipher = new ThreefishCipher(key, tweak);
            UInt64[] plaintext = new UInt64[4]
            {
                0UL,
                1UL,
                2UL,
                3UL
            };
            UInt64[] ciphertext = cipher.Encrypt(plaintext);
            UInt64[] decrypted = cipher.Decrypt(ciphertext);
            CollectionAssert.AreEqual(plaintext, decrypted);
        }

        [TestMethod]
        public void EncryptDecrypt_ExcessKeyAndTweakTest()
        {
            UInt64[] key = new UInt64[6]
            {
                0x1111111111111111UL,
                0x2222222222222222UL,
                0x3333333333333333UL,
                0x4444444444444444UL,
                0x5555555555555555UL,
                0x6666666666666666UL
            };
            UInt64[] tweak = new UInt64[3]
            {
                0x7777777777777777UL,
                0x8888888888888888UL,
                0x9999999999999999UL
            };
            ThreefishCipher cipher = new ThreefishCipher(key, tweak);
            UInt64[] plaintext = new UInt64[4]
            {
                0xAAAAAAAAAAAAAAAAUL,
                0xBBBBBBBBBBBBBBBBUL,
                0xCCCCCCCCCCCCCCCCUL,
                0xDDDDDDDDDDDDDDDDUL
            };
            UInt64[] ciphertext = cipher.Encrypt(plaintext);
            UInt64[] decrypted = cipher.Decrypt(ciphertext);
            CollectionAssert.AreEqual(plaintext, decrypted);
        }

        [TestMethod]
        public void EncryptDecrypt_ShortPlaintextTest()
        {
            UInt64[] key = new UInt64[4]
            {
                0x1234567812345678UL,
                0x2345678923456789UL,
                0x3456789A3456789AUL,
                0x456789AB456789ABUL
            };
            UInt64[] tweak = new UInt64[2]
            {
                0x56789ABC56789ABCUL,
                0x6789ABCD6789ABCDUL
            };
            ThreefishCipher cipher = new ThreefishCipher(key, tweak);
            UInt64[] shortPlaintext = new UInt64[2]
            {
                0xAAAAAAAAAAAAAAAAUL,
                0xBBBBBBBBBBBBBBBBUL
            };
            UInt64[] expected = new UInt64[4]
            {
                0xAAAAAAAAAAAAAAAAUL,
                0xBBBBBBBBBBBBBBBBUL,
                0UL,
                0UL
            };
            UInt64[] ciphertext = cipher.Encrypt(shortPlaintext);
            UInt64[] decrypted = cipher.Decrypt(ciphertext);
            CollectionAssert.AreEqual(expected, decrypted);
        }
    }
}