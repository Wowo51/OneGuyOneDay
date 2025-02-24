using System;
using System.Security.Cryptography;

namespace EllipticCurveDH
{
    public class ECDHKeyExchange : IDisposable
    {
        private ECDiffieHellman? _ecdh;
        public byte[] PublicKey { get; private set; }

        public ECDHKeyExchange()
        {
            _ecdh = ECDiffieHellman.Create();
            PublicKey = _ecdh.ExportSubjectPublicKeyInfo();
        }

        public byte[] DeriveSharedSecret(byte[] remotePublicKey)
        {
            if (remotePublicKey == null)
            {
                return new byte[0];
            }

            ECDiffieHellman temp = ECDiffieHellman.Create();
            int bytesRead = 0;
            temp.ImportSubjectPublicKeyInfo(remotePublicKey, out bytesRead);
            byte[] sharedSecret = _ecdh!.DeriveKeyMaterial(temp.PublicKey);
            temp.Dispose();
            return sharedSecret;
        }

        public void Dispose()
        {
            if (_ecdh != null)
            {
                _ecdh.Dispose();
                _ecdh = null;
            }
        }
    }
}