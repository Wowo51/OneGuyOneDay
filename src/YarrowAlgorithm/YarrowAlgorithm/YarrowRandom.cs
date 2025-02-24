using System;
using System.Security.Cryptography;

namespace YarrowAlgorithm
{
    public class YarrowRandom
    {
        private HMACSHA256 hmac = null !;
        private ulong counter;
        public YarrowRandom()
        {
            byte[] seed = CreateSeed();
            this.Reseed(seed);
        }

        public YarrowRandom(byte[] seed)
        {
            if (seed == null || seed.Length == 0)
            {
                seed = CreateSeed();
            }

            this.Reseed(seed);
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

        public void Reseed(byte[] seed)
        {
            if (seed == null || seed.Length == 0)
            {
                seed = CreateSeed();
            }

            byte[] key = SHA256.HashData(seed);
            hmac = new HMACSHA256(key);
            counter = 0;
        }

        public void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                return;
            }

            int offset = 0;
            while (offset < buffer.Length)
            {
                byte[] counterBytes = BitConverter.GetBytes(counter);
                byte[] hash = hmac.ComputeHash(counterBytes);
                int bytesToCopy = buffer.Length - offset;
                if (bytesToCopy > hash.Length)
                {
                    bytesToCopy = hash.Length;
                }

                Array.Copy(hash, 0, buffer, offset, bytesToCopy);
                offset += bytesToCopy;
                counter++;
            }
        }

        public int NextInt32()
        {
            byte[] bytes = new byte[4];
            NextBytes(bytes);
            int result = BitConverter.ToInt32(bytes, 0);
            return result;
        }
    }
}