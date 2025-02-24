using System;
using System.Numerics;
using System.Security.Cryptography;

namespace DiffieHellmanExchange
{
    public class DiffieHellmanParticipant
    {
        public BigInteger Prime { get; private set; }
        public BigInteger Generator { get; private set; }
        public BigInteger PublicKey { get; private set; }

        private BigInteger _privateKey;
        public DiffieHellmanParticipant(BigInteger prime, BigInteger generator)
        {
            this.Prime = prime;
            this.Generator = generator;
            this._privateKey = GeneratePrivateKey(prime);
            this.PublicKey = BigInteger.ModPow(generator, this._privateKey, prime);
        }

        public DiffieHellmanParticipant() : this(DefaultPrime, DefaultGenerator)
        {
        }

        public static BigInteger DefaultPrime
        {
            get
            {
                return new BigInteger(23);
            }
        }

        public static BigInteger DefaultGenerator
        {
            get
            {
                return new BigInteger(5);
            }
        }

        private BigInteger GeneratePrivateKey(BigInteger prime)
        {
            BigInteger min = new BigInteger(2);
            BigInteger max = prime - 2;
            if (prime <= new BigInteger(4))
            {
                return new BigInteger(1);
            }

            if (max <= int.MaxValue)
            {
                Random random = new Random();
                int range = (int)(max - min + 1);
                int candidate = random.Next(range) + (int)min;
                return new BigInteger(candidate);
            }
            else
            {
                int byteCount = max.ToByteArray().Length;
                BigInteger result = min;
                while (result < min || result > max)
                {
                    byte[] bytes = new byte[byteCount];
                    RandomNumberGenerator.Fill(bytes);
                    result = new BigInteger(bytes);
                    if (result < 0)
                    {
                        result = -result;
                    }
                }

                return result;
            }
        }

        public BigInteger ComputeSharedSecret(BigInteger otherPublicKey)
        {
            return BigInteger.ModPow(otherPublicKey, this._privateKey, this.Prime);
        }
    }
}