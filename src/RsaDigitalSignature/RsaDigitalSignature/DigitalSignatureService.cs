using System;
using System.Security.Cryptography;
using System.Text;

namespace RsaDigitalSignature
{
    public class RsaKeyPair
    {
        public string PublicKey { get; set; } = string.Empty;
        public string PrivateKey { get; set; } = string.Empty;
    }

    public class DigitalSignatureService
    {
        public static RsaKeyPair GenerateKeyPair()
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.KeySize = 2048;
                byte[] publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();
                byte[] privateKeyBytes = rsa.ExportPkcs8PrivateKey();
                RsaKeyPair keyPair = new RsaKeyPair();
                keyPair.PublicKey = Convert.ToBase64String(publicKeyBytes);
                keyPair.PrivateKey = Convert.ToBase64String(privateKeyBytes);
                return keyPair;
            }
        }

        public static string SignData(string data, string privateKey)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] privateKeyBytes = Convert.FromBase64String(privateKey);
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportPkcs8PrivateKey(privateKeyBytes, out int bytesRead);
                byte[] signatureBytes = rsa.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                return Convert.ToBase64String(signatureBytes);
            }
        }

        public static bool VerifySignature(string data, string signature, string publicKey)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] signatureBytes = Convert.FromBase64String(signature);
            byte[] publicKeyBytes = Convert.FromBase64String(publicKey);
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out int bytesRead);
                return rsa.VerifyData(dataBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }
    }
}