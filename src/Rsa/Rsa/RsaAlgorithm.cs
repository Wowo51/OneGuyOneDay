using System;
using System.Numerics;

namespace Rsa
{
    public class RsaAlgorithm
    {
        public BigInteger PublicKey { get; private set; }
        public BigInteger PrivateKey { get; private set; }
        public BigInteger Modulus { get; private set; }

        public RsaAlgorithm(BigInteger p, BigInteger q, BigInteger e)
        {
            if (p <= 1 || q <= 1)
            {
                PublicKey = 0;
                PrivateKey = 0;
                Modulus = 0;
                return;
            }

            BigInteger n = BigInteger.Multiply(p, q);
            BigInteger phi = BigInteger.Multiply(p - 1, q - 1);
            if (BigInteger.GreatestCommonDivisor(phi, e) != 1)
            {
                PublicKey = 0;
                PrivateKey = 0;
                Modulus = 0;
                return;
            }

            PublicKey = e;
            Modulus = n;
            PrivateKey = ModInverse(e, phi);
        }

        public BigInteger Encrypt(BigInteger message)
        {
            if (Modulus == 0 || PublicKey == 0)
            {
                return 0;
            }

            return BigInteger.ModPow(message, PublicKey, Modulus);
        }

        public BigInteger Decrypt(BigInteger cipher)
        {
            if (Modulus == 0 || PrivateKey == 0)
            {
                return 0;
            }

            return BigInteger.ModPow(cipher, PrivateKey, Modulus);
        }

        public BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger x0 = 0;
            BigInteger x1 = 1;
            if (m == 1)
            {
                return 0;
            }

            while (a > 1)
            {
                BigInteger q = a / m;
                BigInteger t = m;
                m = a % m;
                a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0)
            {
                x1 += m0;
            }

            return x1;
        }
    }
}