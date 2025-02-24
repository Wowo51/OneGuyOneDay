using System;

namespace NTRUEncrypt
{
    public class NTRUKeyPair
    {
        public Polynomial PublicKey { get; }
        public Polynomial PrivateKey { get; }

        public NTRUKeyPair(Polynomial publicKey, Polynomial privateKey)
        {
            PublicKey = publicKey ?? new Polynomial(new int[0]);
            PrivateKey = privateKey ?? new Polynomial(new int[0]);
        }
    }
}