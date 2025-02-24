using System;
using System.IO;
using System.Security.Cryptography;

namespace YarrowAlgorithm
{
    public class YarrowGenerator
    {
        private YarrowRandom prng;
        private MemoryStream entropyPool;
        private int reseedThreshold;
        public YarrowGenerator() : this(null)
        {
        }

        public YarrowGenerator(byte[] seed)
        {
            if (seed == null || seed.Length == 0)
            {
                seed = CreateSeed();
            }

            this.prng = new YarrowRandom(seed);
            this.entropyPool = new MemoryStream();
            this.reseedThreshold = 32;
        }

        private byte[] CreateSeed()
        {
            byte[] timeBytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
            byte[] guidBytes = Guid.NewGuid().ToByteArray();
            byte[] seed = new byte[timeBytes.Length + guidBytes.Length];
            Buffer.BlockCopy(timeBytes, 0, seed, 0, timeBytes.Length);
            Buffer.BlockCopy(guidBytes, 0, seed, timeBytes.Length, guidBytes.Length);
            return seed;
        }

        public void AddEntropy(byte[] entropy)
        {
            if (entropy == null || entropy.Length == 0)
            {
                return;
            }

            this.entropyPool.Write(entropy, 0, entropy.Length);
            if (this.entropyPool.Length >= this.reseedThreshold)
            {
                byte[] poolData = this.entropyPool.ToArray();
                byte[] newSeed = SHA256.HashData(poolData);
                this.prng.Reseed(newSeed);
                this.entropyPool.SetLength(0);
            }
        }

        public void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                return;
            }

            this.prng.NextBytes(buffer);
        }

        public int NextInt32()
        {
            return this.prng.NextInt32();
        }
    }
}