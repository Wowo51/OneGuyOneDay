using System;
using System.Security.Cryptography;

namespace Whirlpool
{
    public class WhirlpoolHashAlgorithm : HashAlgorithm
    {
        private const int BlockSizeBytes = 64;
        private const int DigestSizeBytes = 64;
        private readonly byte[] buffer;
        private int bufferLength;
        private ulong totalMessageLength;
        private ulong[] hash;
        // Static lookup tables for Whirlpool transformation
        private static readonly byte[] SBox = new byte[256]
        {
            0x18,
            0x23,
            0xC6,
            0xE8,
            0x87,
            0xB8,
            0x01,
            0x4F,
            0x36,
            0xA6,
            0xD2,
            0xF5,
            0x79,
            0x6F,
            0x91,
            0x52,
            0x60,
            0xBC,
            0x9B,
            0x8E,
            0xA3,
            0x0C,
            0x7B,
            0x35,
            0x1D,
            0xE0,
            0xD7,
            0xC2,
            0x2E,
            0x4B,
            0xFE,
            0x57,
            0x15,
            0x77,
            0x37,
            0xE5,
            0x9F,
            0xF0,
            0x4A,
            0xDA,
            0x58,
            0xC9,
            0x29,
            0x0A,
            0xB1,
            0xA0,
            0x6B,
            0x85,
            0xBD,
            0x5D,
            0x10,
            0xF4,
            0xCB,
            0x3E,
            0x05,
            0x67,
            0xE4,
            0x27,
            0x41,
            0x8B,
            0xA7,
            0x7D,
            0x95,
            0xD8,
            0xFB,
            0xEE,
            0x7C,
            0x66,
            0xDD,
            0x17,
            0x47,
            0x9E,
            0xCA,
            0x2D,
            0xBF,
            0x07,
            0xAD,
            0x5A,
            0x83,
            0x33,
            0x63,
            0x02,
            0xAA,
            0x71,
            0xC8,
            0x19,
            0x49,
            0xD9,
            0xF2,
            0xE3,
            0x5B,
            0x88,
            0x9A,
            0x26,
            0x32,
            0xB0,
            0xE9,
            0x0F,
            0xD5,
            0x80,
            0xBE,
            0xCD,
            0x34,
            0x48,
            0xFF,
            0x7A,
            0x90,
            0x5F,
            0x20,
            0x68,
            0x1A,
            0xAE,
            0xB4,
            0x54,
            0x93,
            0x22,
            0x64,
            0xF1,
            0x73,
            0x12,
            0x40,
            0x08,
            0xC3,
            0xEC,
            0xDB,
            0xA1,
            0x8D,
            0x3D,
            0x97,
            0x00,
            0xCF,
            0x2B,
            0x76,
            0x82,
            0xD6,
            0x1B,
            0xB5,
            0xAF,
            0x6A,
            0x50,
            0x45,
            0xF3,
            0x30,
            0xEF,
            0x3F,
            0x55,
            0xA2,
            0xEA,
            0x65,
            0xBA,
            0x2F,
            0xC0,
            0xDE,
            0x1C,
            0xFD,
            0x4D,
            0x92,
            0x75,
            0x06,
            0x8A,
            0xB2,
            0xE6,
            0x0E,
            0x1F,
            0x62,
            0xD4,
            0xA8,
            0x96,
            0xF9,
            0xC5,
            0x25,
            0x59,
            0x84,
            0x72,
            0x39,
            0x4C,
            0x5E,
            0x78,
            0x38,
            0x8C,
            0xD1,
            0xA5,
            0xE2,
            0x61,
            0xB3,
            0x21,
            0x9C,
            0x1E,
            0x43,
            0xC7,
            0xFC,
            0x04,
            0x51,
            0x99,
            0x6D,
            0x0D,
            0xFA,
            0xDF,
            0x7E,
            0x24,
            0x3B,
            0xAB,
            0xCE,
            0x11,
            0x8F,
            0x4E,
            0xB7,
            0xEB,
            0x3C,
            0x81,
            0x94,
            0xF7,
            0xB9,
            0x13,
            0x2C,
            0xD3,
            0xE7,
            0x6E,
            0xC4,
            0x03,
            0x56,
            0x44,
            0x7F,
            0xA9,
            0x2A,
            0xBB,
            0xC1,
            0x53,
            0xDC,
            0x0B,
            0x9D,
            0x6C,
            0x31,
            0x74,
            0xF6,
            0x46,
            0xAC,
            0x89,
            0x14,
            0xE1,
            0x16,
            0x3A,
            0x69,
            0x09,
            0x70,
            0xB6,
            0xD0,
            0xED,
            0xCC,
            0x42,
            0x98,
            0xA4,
            0x28,
            0x5C,
            0xF8,
            0x86
        };
        private static readonly ulong[] T0 = new ulong[256];
        private static readonly ulong[] T1 = new ulong[256];
        private static readonly ulong[] T2 = new ulong[256];
        private static readonly ulong[] T3 = new ulong[256];
        private static readonly ulong[] T4 = new ulong[256];
        private static readonly ulong[] T5 = new ulong[256];
        private static readonly ulong[] T6 = new ulong[256];
        private static readonly ulong[] T7 = new ulong[256];
        private static readonly ulong[] RC = new ulong[10];
        static WhirlpoolHashAlgorithm()
        {
            int x;
            for (x = 0; x < 256; x++)
            {
                byte s = SBox[x];
                ulong t = ((ulong)GFMult(s, 1) << 56) ^ ((ulong)GFMult(s, 1) << 48) ^ ((ulong)GFMult(s, 4) << 40) ^ ((ulong)GFMult(s, 1) << 32) ^ ((ulong)GFMult(s, 8) << 24) ^ ((ulong)GFMult(s, 5) << 16) ^ ((ulong)GFMult(s, 2) << 8) ^ (ulong)GFMult(s, 9);
                T0[x] = t;
                T1[x] = RotateRight(t, 8);
                T2[x] = RotateRight(t, 16);
                T3[x] = RotateRight(t, 24);
                T4[x] = RotateRight(t, 32);
                T5[x] = RotateRight(t, 40);
                T6[x] = RotateRight(t, 48);
                T7[x] = RotateRight(t, 56);
            }

            int r, i;
            for (r = 0; r < 10; r++)
            {
                ulong rc = 0UL;
                for (i = 0; i < 8; i++)
                {
                    rc ^= GetT(i, 8 * r + i);
                }

                RC[r] = rc;
            }
        }

        private static ulong GetT(int tableIndex, int index)
        {
            switch (tableIndex)
            {
                case 0:
                    return T0[index];
                case 1:
                    return T1[index];
                case 2:
                    return T2[index];
                case 3:
                    return T3[index];
                case 4:
                    return T4[index];
                case 5:
                    return T5[index];
                case 6:
                    return T6[index];
                case 7:
                    return T7[index];
                default:
                    return 0UL;
            }
        }

        private static ulong RotateRight(ulong value, int bits)
        {
            return (value >> bits) | (value << (64 - bits));
        }

        private static byte GFMult(byte a, int b)
        {
            int result = 0;
            int aa = a;
            int multiplier = b;
            while (multiplier > 0)
            {
                if ((multiplier & 1) != 0)
                {
                    result ^= aa;
                }

                multiplier = multiplier >> 1;
                aa <<= 1;
                if (aa > 0xFF)
                {
                    aa ^= 0x11D;
                }
            }

            return (byte)(result & 0xFF);
        }

        public WhirlpoolHashAlgorithm()
        {
            this.HashSizeValue = DigestSizeBytes * 8;
            this.buffer = new byte[BlockSizeBytes];
            this.hash = new ulong[8];
            this.Initialize();
        }

        public override void Initialize()
        {
            int i;
            for (i = 0; i < 8; i++)
            {
                this.hash[i] = 0UL;
            }

            this.bufferLength = 0;
            this.totalMessageLength = 0UL;
            Array.Clear(this.buffer, 0, this.buffer.Length);
        }

        protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
        {
            int offset = ibStart;
            int bytesLeft = cbSize;
            while (bytesLeft > 0)
            {
                int space = BlockSizeBytes - this.bufferLength;
                int copyCount = (space < bytesLeft) ? space : bytesLeft;
                Array.Copy(rgb, offset, this.buffer, this.bufferLength, copyCount);
                this.bufferLength += copyCount;
                this.totalMessageLength += (ulong)copyCount;
                offset += copyCount;
                bytesLeft -= copyCount;
                if (this.bufferLength == BlockSizeBytes)
                {
                    ProcessBlock(this.buffer);
                    this.bufferLength = 0;
                }
            }
        }

        protected override byte[] HashFinal()
        {
            byte[] finalBlock = CreateFinalBlock();
            int offset = 0;
            while (offset < finalBlock.Length)
            {
                byte[] block = new byte[BlockSizeBytes];
                Array.Copy(finalBlock, offset, block, 0, BlockSizeBytes);
                ProcessBlock(block);
                offset += BlockSizeBytes;
            }

            byte[] result = new byte[DigestSizeBytes];
            int i;
            for (i = 0; i < 8; i++)
            {
                WriteUInt64ToBigEndian(this.hash[i], result, i * 8);
            }

            return result;
        }

        private byte[] CreateFinalBlock()
        {
            ulong totalBits = this.totalMessageLength * 8UL;
            int padLen;
            if (this.bufferLength < 32)
            {
                padLen = 32 - this.bufferLength;
            }
            else
            {
                padLen = 96 - this.bufferLength;
            }

            int finalBlockSize = this.bufferLength + padLen + 32;
            byte[] finalBlock = new byte[finalBlockSize];
            Array.Copy(this.buffer, 0, finalBlock, 0, this.bufferLength);
            if (padLen > 0)
            {
                finalBlock[this.bufferLength] = 0x80;
            }

            byte[] lengthBytes = new byte[32];
            int i;
            for (i = 0; i < 8; i++)
            {
                lengthBytes[31 - i] = (byte)(totalBits >> (8 * i));
            }

            Array.Copy(lengthBytes, 0, finalBlock, this.bufferLength + padLen, 32);
            return finalBlock;
        }

        private void WriteUInt64ToBigEndian(ulong value, byte[] destination, int offset)
        {
            int i;
            for (i = 0; i < 8; i++)
            {
                destination[offset + 7 - i] = (byte)(value >> (8 * i));
            }
        }

        private void ProcessBlock(byte[] block)
        {
            ulong[] blockWords = new ulong[8];
            int i;
            for (i = 0; i < 8; i++)
            {
                blockWords[i] = ((ulong)block[i * 8] << 56) | ((ulong)block[i * 8 + 1] << 48) | ((ulong)block[i * 8 + 2] << 40) | ((ulong)block[i * 8 + 3] << 32) | ((ulong)block[i * 8 + 4] << 24) | ((ulong)block[i * 8 + 5] << 16) | ((ulong)block[i * 8 + 6] << 8) | ((ulong)block[i * 8 + 7]);
            }

            ulong[] state = new ulong[8];
            ulong[] K = new ulong[8];
            for (i = 0; i < 8; i++)
            {
                K[i] = this.hash[i];
                state[i] = blockWords[i] ^ K[i];
            }

            int r;
            for (r = 0; r < 10; r++)
            {
                ulong[] newK = new ulong[8];
                for (i = 0; i < 8; i++)
                {
                    newK[i] = T0[(int)(K[i] >> 56) & 0xFF] ^ T1[(int)(K[(i + 7) & 7] >> 48) & 0xFF] ^ T2[(int)(K[(i + 6) & 7] >> 40) & 0xFF] ^ T3[(int)(K[(i + 5) & 7] >> 32) & 0xFF] ^ T4[(int)(K[(i + 4) & 7] >> 24) & 0xFF] ^ T5[(int)(K[(i + 3) & 7] >> 16) & 0xFF] ^ T6[(int)(K[(i + 2) & 7] >> 8) & 0xFF] ^ T7[(int)(K[(i + 1) & 7]) & 0xFF];
                }

                newK[0] ^= RC[r];
                ulong[] newState = new ulong[8];
                for (i = 0; i < 8; i++)
                {
                    newState[i] = newK[i] ^ (T0[(int)(state[i] >> 56) & 0xFF] ^ T1[(int)(state[(i + 7) & 7] >> 48) & 0xFF] ^ T2[(int)(state[(i + 6) & 7] >> 40) & 0xFF] ^ T3[(int)(state[(i + 5) & 7] >> 32) & 0xFF] ^ T4[(int)(state[(i + 4) & 7] >> 24) & 0xFF] ^ T5[(int)(state[(i + 3) & 7] >> 16) & 0xFF] ^ T6[(int)(state[(i + 2) & 7] >> 8) & 0xFF] ^ T7[(int)(state[(i + 1) & 7]) & 0xFF]);
                }

                K = newK;
                state = newState;
            }

            for (i = 0; i < 8; i++)
            {
                this.hash[i] ^= state[i] ^ blockWords[i];
            }
        }
    }
}