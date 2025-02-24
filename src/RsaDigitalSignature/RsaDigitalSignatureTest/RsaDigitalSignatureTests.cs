using Microsoft.VisualStudio.TestTools.UnitTesting;
using RsaDigitalSignature;
using System;
using System.Text;

namespace RsaDigitalSignatureTest
{
    [TestClass]
    public class RsaDigitalSignatureTests
    {
        [TestMethod]
        public void TestKeyPairGeneration()
        {
            RsaKeyPair keyPair = DigitalSignatureService.GenerateKeyPair();
            Assert.IsNotNull(keyPair);
            Assert.IsFalse(string.IsNullOrEmpty(keyPair.PublicKey));
            Assert.IsFalse(string.IsNullOrEmpty(keyPair.PrivateKey));
        }

        [TestMethod]
        public void TestSignAndVerify()
        {
            RsaKeyPair keyPair = DigitalSignatureService.GenerateKeyPair();
            string message = "Hello, world!";
            string signature = DigitalSignatureService.SignData(message, keyPair.PrivateKey);
            bool valid = DigitalSignatureService.VerifySignature(message, signature, keyPair.PublicKey);
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void TestVerifyFailsForModifiedMessage()
        {
            RsaKeyPair keyPair = DigitalSignatureService.GenerateKeyPair();
            string message = "Test Message";
            string signature = DigitalSignatureService.SignData(message, keyPair.PrivateKey);
            string modifiedMessage = message + " extra";
            bool valid = DigitalSignatureService.VerifySignature(modifiedMessage, signature, keyPair.PublicKey);
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void TestVerifyFailsForTamperedSignature()
        {
            RsaKeyPair keyPair = DigitalSignatureService.GenerateKeyPair();
            string message = "Another Test Message";
            string signature = DigitalSignatureService.SignData(message, keyPair.PrivateKey);
            byte[] signatureBytes = Convert.FromBase64String(signature);
            signatureBytes[0] = (byte)(signatureBytes[0] ^ 0xFF);
            string tamperedSignature = Convert.ToBase64String(signatureBytes);
            bool valid = DigitalSignatureService.VerifySignature(message, tamperedSignature, keyPair.PublicKey);
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void TestWithLargeData()
        {
            RsaKeyPair keyPair = DigitalSignatureService.GenerateKeyPair();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                sb.Append("Data" + i.ToString());
            }

            string message = sb.ToString();
            string signature = DigitalSignatureService.SignData(message, keyPair.PrivateKey);
            bool valid = DigitalSignatureService.VerifySignature(message, signature, keyPair.PublicKey);
            Assert.IsTrue(valid);
        }
    }
}