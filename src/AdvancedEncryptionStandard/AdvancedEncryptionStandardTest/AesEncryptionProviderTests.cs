using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdvancedEncryptionStandard;

namespace AdvancedEncryptionStandardTest
{
    [TestClass]
    public class AesEncryptionProviderTests
    {
        private string GenerateRandomString(int length)
        {
            StringBuilder builder = new StringBuilder(length);
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (int i = 0; i < length; i++)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }

            return builder.ToString();
        }

        [TestMethod]
        public void TestEncryptDecrypt_ValidText()
        {
            int keySize = 16;
            byte[] key = AesEncryptionProvider.GenerateKey(keySize);
            byte[] iv = AesEncryptionProvider.GenerateIV();
            AesEncryptionProvider provider = new AesEncryptionProvider(key, iv);
            string original = "Hello world!";
            string encrypted = provider.Encrypt(original);
            Assert.IsFalse(string.IsNullOrEmpty(encrypted));
            string decrypted = provider.Decrypt(encrypted);
            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void TestEncryptNull_ReturnsEmpty()
        {
            int keySize = 16;
            byte[] key = AesEncryptionProvider.GenerateKey(keySize);
            byte[] iv = AesEncryptionProvider.GenerateIV();
            AesEncryptionProvider provider = new AesEncryptionProvider(key, iv);
            string result = provider.Encrypt(null);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TestDecryptNull_ReturnsEmpty()
        {
            int keySize = 16;
            byte[] key = AesEncryptionProvider.GenerateKey(keySize);
            byte[] iv = AesEncryptionProvider.GenerateIV();
            AesEncryptionProvider provider = new AesEncryptionProvider(key, iv);
            string result = provider.Decrypt(null);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TestEncryptDecrypt_EmptyString()
        {
            int keySize = 16;
            byte[] key = AesEncryptionProvider.GenerateKey(keySize);
            byte[] iv = AesEncryptionProvider.GenerateIV();
            AesEncryptionProvider provider = new AesEncryptionProvider(key, iv);
            string original = string.Empty;
            string encrypted = provider.Encrypt(original);
            string decrypted = provider.Decrypt(encrypted);
            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        public void TestGenerateKeyLength()
        {
            int keySize = 16;
            byte[] key = AesEncryptionProvider.GenerateKey(keySize);
            Assert.AreEqual(keySize, key.Length);
        }

        [TestMethod]
        public void TestGenerateIVLength()
        {
            byte[] iv = AesEncryptionProvider.GenerateIV();
            Assert.AreEqual(16, iv.Length);
        }

        [TestMethod]
        public void TestEncryptDecrypt_LargeText()
        {
            int keySize = 16;
            byte[] key = AesEncryptionProvider.GenerateKey(keySize);
            byte[] iv = AesEncryptionProvider.GenerateIV();
            AesEncryptionProvider provider = new AesEncryptionProvider(key, iv);
            string original = GenerateRandomString(10000);
            string encrypted = provider.Encrypt(original);
            Assert.IsFalse(string.IsNullOrEmpty(encrypted));
            string decrypted = provider.Decrypt(encrypted);
            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDecrypt_InvalidBase64_ThrowsFormatException()
        {
            int keySize = 16;
            byte[] key = AesEncryptionProvider.GenerateKey(keySize);
            byte[] iv = AesEncryptionProvider.GenerateIV();
            AesEncryptionProvider provider = new AesEncryptionProvider(key, iv);
            string invalidCipherText = "NotAValidBase64";
            provider.Decrypt(invalidCipherText);
        }

        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void TestDecrypt_WithWrongKey_ThrowsCryptographicException()
        {
            int keySize = 16;
            byte[] key1 = AesEncryptionProvider.GenerateKey(keySize);
            byte[] key2 = AesEncryptionProvider.GenerateKey(keySize);
            byte[] iv = AesEncryptionProvider.GenerateIV();
            AesEncryptionProvider providerEncrypt = new AesEncryptionProvider(key1, iv);
            string original = "Test message";
            string encrypted = providerEncrypt.Encrypt(original);
            AesEncryptionProvider providerDecrypt = new AesEncryptionProvider(key2, iv);
            providerDecrypt.Decrypt(encrypted);
        }

        [TestMethod]
        public void TestEncryptDecrypt_SpecialCharacters()
        {
            int keySize = 16;
            byte[] key = AesEncryptionProvider.GenerateKey(keySize);
            byte[] iv = AesEncryptionProvider.GenerateIV();
            AesEncryptionProvider provider = new AesEncryptionProvider(key, iv);
            string original = "こんにちは、世界！ Привет, мир! 😀";
            string encrypted = provider.Encrypt(original);
            string decrypted = provider.Decrypt(encrypted);
            Assert.AreEqual(original, decrypted);
        }
    }
}