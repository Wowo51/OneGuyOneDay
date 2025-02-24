using System;
using System.Security.Cryptography;

namespace Sha2Variants
{
    public class SHA224 : HashAlgorithm
    {
        private const int BlockSize = 64;
        private uint[] _H;
        private byte[] _buffer;
        private long _count;
        private static readonly uint[] K = new uint[64]
        {
            0x428a2f98,
            0x71374491,
            0xb5c0fbcf,
            0xe9b5dba5,
            0x3956c25b,
            0x59f111f1,
            0x923f82a4,
            0xab1c5ed5,
            0xd807aa98,
            0x12835b01,
            0x243185be,
            0x550c7dc3,
            0x72be5d74,
            0x80deb1fe,
            0x9bdc06a7,
            0xc19bf174,
            0xe49b69c1,
            0xefbe4786,
            0x0fc19dc6,
            0x240ca1cc,
            0x2de92c6f,
            0x4a7484aa,
            0x5cb0a9dc,
            0x76f988da,
            0x983e5152,
            0xa831c66d,
            0xb00327c8,
            0xbf597fc7,
            0xc6e00bf3,
            0xd5a79147,
            0x06ca6351,
            0x14292967,
            0x27b70a85,
            0x2e1b2138,
            0x4d2c6dfc,
            0x53380d13,
            0x650a7354,
            0x766a0abb,
            0x81c2c92e,
            0x92722c85,
            0xa2bfe8a1,
            0xa81a664b,
            0xc24b8b70,
            0xc76c51a3,
            0xd192e819,
            0xd6990624,
            0xf40e3585,
            0x106aa070,
            0x19a4c116,
            0x1e376c08,
            0x2748774c,
            0x34b0bcb5,
            0x391c0cb3,
            0x4ed8aa4a,
            0x5b9cca4f,
            0x682e6ff3,
            0x748f82ee,
            0x78a5636f,
            0x84c87814,
            0x8cc70208,
            0x90befffa,
            0xa4506ceb,
            0xbef9a3f7,
            0xc67178f2
        };
        public SHA224()
        {
            HashSizeValue = 224;
            _buffer = new byte[BlockSize];
            _H = new uint[8];
            Initialize();
        }

        public override void Initialize()
        {
            _count = 0;
            _H[0] = 0xc1059ed8;
            _H[1] = 0x367cd507;
            _H[2] = 0x3070dd17;
            _H[3] = 0xf70e5939;
            _H[4] = 0xffc00b31;
            _H[5] = 0x68581511;
            _H[6] = 0x64f98fa7;
            _H[7] = 0xbefa4fa4;
            Array.Clear(_buffer, 0, _buffer.Length);
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            int offset = ibStart;
            int bufferOffset = (int)(_count % BlockSize);
            _count += cbSize;
            while (cbSize > 0)
            {
                int toCopy = BlockSize - bufferOffset;
                if (toCopy > cbSize)
                {
                    toCopy = cbSize;
                }

                Array.Copy(array, offset, _buffer, bufferOffset, toCopy);
                bufferOffset += toCopy;
                offset += toCopy;
                cbSize -= toCopy;
                if (bufferOffset == BlockSize)
                {
                    ProcessBlock(_buffer, 0);
                    bufferOffset = 0;
                }
            }
        }

        protected override byte[] HashFinal()
        {
            long bitCount = _count * 8;
            int bufferOffset = (int)(_count % BlockSize);
            _buffer[bufferOffset++] = 0x80;
            if (bufferOffset > 56)
            {
                for (; bufferOffset < BlockSize; bufferOffset++)
                {
                    _buffer[bufferOffset] = 0;
                }

                ProcessBlock(_buffer, 0);
                bufferOffset = 0;
            }

            for (; bufferOffset < 56; bufferOffset++)
            {
                _buffer[bufferOffset] = 0;
            }

            for (int i = 7; i >= 0; i--)
            {
                _buffer[56 + (7 - i)] = (byte)((bitCount >> (i * 8)) & 0xff);
            }

            ProcessBlock(_buffer, 0);
            byte[] hash = new byte[28];
            for (int i = 0; i < 7; i++)
            {
                hash[i * 4] = (byte)((_H[i] >> 24) & 0xff);
                hash[i * 4 + 1] = (byte)((_H[i] >> 16) & 0xff);
                hash[i * 4 + 2] = (byte)((_H[i] >> 8) & 0xff);
                hash[i * 4 + 3] = (byte)(_H[i] & 0xff);
            }

            return hash;
        }

        private static uint RotateRight(uint x, int n)
        {
            return (x >> n) | (x << (32 - n));
        }

        private static uint Sigma0(uint x)
        {
            return RotateRight(x, 2) ^ RotateRight(x, 13) ^ RotateRight(x, 22);
        }

        private static uint Sigma1(uint x)
        {
            return RotateRight(x, 6) ^ RotateRight(x, 11) ^ RotateRight(x, 25);
        }

        private static uint sigma0(uint x)
        {
            return RotateRight(x, 7) ^ RotateRight(x, 18) ^ (x >> 3);
        }

        private static uint sigma1(uint x)
        {
            return RotateRight(x, 17) ^ RotateRight(x, 19) ^ (x >> 10);
        }

        private static uint Ch(uint x, uint y, uint z)
        {
            return (x & y) ^ ((~x) & z);
        }

        private static uint Maj(uint x, uint y, uint z)
        {
            return (x & y) ^ (x & z) ^ (y & z);
        }

        private void ProcessBlock(byte[] block, int offset)
        {
            uint[] W = new uint[64];
            int i;
            for (i = 0; i < 16; i++)
            {
                W[i] = ((uint)block[offset + i * 4] << 24) | ((uint)block[offset + i * 4 + 1] << 16) | ((uint)block[offset + i * 4 + 2] << 8) | ((uint)block[offset + i * 4 + 3]);
            }

            for (i = 16; i < 64; i++)
            {
                W[i] = sigma1(W[i - 2]) + W[i - 7] + sigma0(W[i - 15]) + W[i - 16];
            }

            uint a = _H[0], b = _H[1], c = _H[2], d = _H[3];
            uint e = _H[4], f = _H[5], g = _H[6], h = _H[7];
            for (i = 0; i < 64; i++)
            {
                uint T1 = h + Sigma1(e) + Ch(e, f, g) + K[i] + W[i];
                uint T2 = Sigma0(a) + Maj(a, b, c);
                h = g;
                g = f;
                f = e;
                e = d + T1;
                d = c;
                c = b;
                b = a;
                a = T1 + T2;
            }

            _H[0] += a;
            _H[1] += b;
            _H[2] += c;
            _H[3] += d;
            _H[4] += e;
            _H[5] += f;
            _H[6] += g;
            _H[7] += h;
        }
    }
}