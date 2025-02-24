using System;
using System.Security.Cryptography;

namespace Ripemd160
{
    public class RIPEMD160Managed : HashAlgorithm
    {
        private const uint H0Init = 0x67452301U;
        private const uint H1Init = 0xEFCDAB89U;
        private const uint H2Init = 0x98BADCFEU;
        private const uint H3Init = 0x10325476U;
        private const uint H4Init = 0xC3D2E1F0U;
        private uint _h0, _h1, _h2, _h3, _h4;
        private byte[] _buffer;
        private long _count;
        private uint[] _blockDWords;
        public RIPEMD160Managed()
        {
            HashSizeValue = 160;
            _buffer = new byte[64];
            _blockDWords = new uint[16];
            Initialize();
        }

        public override void Initialize()
        {
            _h0 = H0Init;
            _h1 = H1Init;
            _h2 = H2Init;
            _h3 = H3Init;
            _h4 = H4Init;
            _count = 0;
            Array.Clear(_buffer, 0, _buffer.Length);
        }

        protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
        {
            int bufferOffset = (int)(_count % 64);
            _count += cbSize;
            int inputOffset = ibStart;
            int remaining = 64 - bufferOffset;
            if (cbSize < remaining)
            {
                Array.Copy(rgb, inputOffset, _buffer, bufferOffset, cbSize);
                return;
            }

            Array.Copy(rgb, inputOffset, _buffer, bufferOffset, remaining);
            Transform(_buffer, 0);
            inputOffset += remaining;
            cbSize -= remaining;
            while (cbSize >= 64)
            {
                Transform(rgb, inputOffset);
                inputOffset += 64;
                cbSize -= 64;
            }

            Array.Copy(rgb, inputOffset, _buffer, 0, cbSize);
        }

        protected override byte[] HashFinal()
        {
            byte[] pad;
            int padLength;
            long bitCount = _count * 8;
            int bufferOffset = (int)(_count % 64);
            padLength = (bufferOffset < 56) ? (56 - bufferOffset) : (120 - bufferOffset);
            pad = new byte[padLength + 8];
            pad[0] = 0x80;
            for (int i = 0; i < 8; i++)
            {
                pad[padLength + i] = (byte)(bitCount >> (8 * i));
            }

            HashCore(pad, 0, pad.Length);
            byte[] hash = new byte[20];
            WriteUInt32LE(_h0, hash, 0);
            WriteUInt32LE(_h1, hash, 4);
            WriteUInt32LE(_h2, hash, 8);
            WriteUInt32LE(_h3, hash, 12);
            WriteUInt32LE(_h4, hash, 16);
            base.HashValue = hash;
            return hash;
        }

        private static void WriteUInt32LE(uint value, byte[] buffer, int offset)
        {
            buffer[offset] = (byte)(value & 0xff);
            buffer[offset + 1] = (byte)((value >> 8) & 0xff);
            buffer[offset + 2] = (byte)((value >> 16) & 0xff);
            buffer[offset + 3] = (byte)((value >> 24) & 0xff);
        }

        private static uint Rol(uint value, int bits)
        {
            return (value << bits) | (value >> (32 - bits));
        }

        private void Transform(byte[] block, int offset)
        {
            for (int i = 0; i < 16; i++)
            {
                _blockDWords[i] = BitConverter.ToUInt32(block, offset + i * 4);
            }

            uint A1 = _h0, B1 = _h1, C1 = _h2, D1 = _h3, E1 = _h4;
            uint A2 = _h0, B2 = _h1, C2 = _h2, D2 = _h3, E2 = _h4;
            uint T;
            int[] r1 =
            {
                11,
                14,
                15,
                12,
                5,
                8,
                7,
                9,
                11,
                13,
                14,
                15,
                6,
                7,
                9,
                8
            };
            int[] r2 =
            {
                7,
                6,
                8,
                13,
                11,
                9,
                7,
                15,
                7,
                12,
                15,
                9,
                11,
                7,
                13,
                12
            };
            int[] r3 =
            {
                11,
                13,
                6,
                7,
                14,
                9,
                13,
                15,
                14,
                8,
                13,
                6,
                5,
                12,
                7,
                5
            };
            int[] r4 =
            {
                11,
                12,
                14,
                15,
                14,
                15,
                9,
                8,
                9,
                14,
                5,
                6,
                8,
                6,
                5,
                12
            };
            int[] r5 =
            {
                9,
                15,
                5,
                11,
                6,
                8,
                13,
                12,
                5,
                12,
                13,
                14,
                11,
                8,
                5,
                6
            };
            int[] indices1 =
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15
            };
            int[] indices2 =
            {
                7,
                4,
                13,
                1,
                10,
                6,
                15,
                3,
                12,
                0,
                9,
                5,
                2,
                14,
                11,
                8
            };
            int[] indices3 =
            {
                3,
                10,
                14,
                4,
                9,
                15,
                8,
                1,
                2,
                7,
                0,
                6,
                13,
                11,
                5,
                12
            };
            int[] indices4 =
            {
                1,
                9,
                11,
                10,
                0,
                8,
                12,
                4,
                13,
                3,
                7,
                15,
                14,
                5,
                6,
                2
            };
            int[] indices5 =
            {
                4,
                0,
                5,
                9,
                7,
                12,
                2,
                10,
                14,
                1,
                3,
                8,
                11,
                6,
                15,
                13
            };
            int[] rr1 =
            {
                8,
                9,
                9,
                11,
                13,
                15,
                15,
                5,
                7,
                7,
                8,
                11,
                14,
                14,
                12,
                6
            };
            int[] rr2 =
            {
                9,
                13,
                15,
                7,
                12,
                8,
                9,
                11,
                7,
                7,
                12,
                7,
                6,
                15,
                13,
                11
            };
            int[] rr3 =
            {
                9,
                7,
                15,
                11,
                8,
                6,
                6,
                14,
                12,
                13,
                5,
                14,
                13,
                13,
                7,
                5
            };
            int[] rr4 =
            {
                15,
                5,
                8,
                11,
                14,
                14,
                6,
                14,
                6,
                9,
                12,
                9,
                12,
                5,
                15,
                8
            };
            int[] rr5 =
            {
                8,
                5,
                12,
                9,
                12,
                5,
                14,
                6,
                8,
                13,
                6,
                5,
                15,
                13,
                11,
                11
            };
            int[] rIndices1 =
            {
                5,
                14,
                7,
                0,
                9,
                2,
                11,
                4,
                13,
                6,
                15,
                8,
                1,
                10,
                3,
                12
            };
            int[] rIndices2 =
            {
                6,
                11,
                3,
                7,
                0,
                13,
                5,
                10,
                14,
                15,
                8,
                12,
                4,
                9,
                1,
                2
            };
            int[] rIndices3 =
            {
                15,
                5,
                1,
                3,
                7,
                14,
                6,
                9,
                11,
                8,
                12,
                2,
                10,
                0,
                4,
                13
            };
            int[] rIndices4 =
            {
                8,
                6,
                4,
                1,
                3,
                11,
                15,
                0,
                5,
                12,
                2,
                13,
                9,
                7,
                10,
                14
            };
            int[] rIndices5 =
            {
                12,
                15,
                10,
                4,
                1,
                5,
                8,
                7,
                6,
                2,
                13,
                14,
                0,
                3,
                9,
                11
            };
            for (int j = 0; j < 16; j++)
            {
                T = Rol(A1 + F1(B1, C1, D1) + _blockDWords[indices1[j]], r1[j]) + E1;
                A1 = E1;
                E1 = D1;
                D1 = Rol(C1, 10);
                C1 = B1;
                B1 = T;
            }

            for (int j = 0; j < 16; j++)
            {
                T = Rol(A1 + F2(B1, C1, D1) + _blockDWords[indices2[j]] + 0x5A827999U, r2[j]) + E1;
                A1 = E1;
                E1 = D1;
                D1 = Rol(C1, 10);
                C1 = B1;
                B1 = T;
            }

            for (int j = 0; j < 16; j++)
            {
                T = Rol(A1 + F3(B1, C1, D1) + _blockDWords[indices3[j]] + 0x6ED9EBA1U, r3[j]) + E1;
                A1 = E1;
                E1 = D1;
                D1 = Rol(C1, 10);
                C1 = B1;
                B1 = T;
            }

            for (int j = 0; j < 16; j++)
            {
                T = Rol(A1 + F4(B1, C1, D1) + _blockDWords[indices4[j]] + 0x8F1BBCDCU, r4[j]) + E1;
                A1 = E1;
                E1 = D1;
                D1 = Rol(C1, 10);
                C1 = B1;
                B1 = T;
            }

            for (int j = 0; j < 16; j++)
            {
                T = Rol(A1 + F5(B1, C1, D1) + _blockDWords[indices5[j]] + 0xA953FD4EU, r5[j]) + E1;
                A1 = E1;
                E1 = D1;
                D1 = Rol(C1, 10);
                C1 = B1;
                B1 = T;
            }

            for (int j = 0; j < 16; j++)
            {
                T = Rol(A2 + F5(B2, C2, D2) + _blockDWords[rIndices1[j]] + 0x50A28BE6U, rr1[j]) + E2;
                A2 = E2;
                E2 = D2;
                D2 = Rol(C2, 10);
                C2 = B2;
                B2 = T;
            }

            for (int j = 0; j < 16; j++)
            {
                T = Rol(A2 + F4(B2, C2, D2) + _blockDWords[rIndices2[j]] + 0x5C4DD124U, rr2[j]) + E2;
                A2 = E2;
                E2 = D2;
                D2 = Rol(C2, 10);
                C2 = B2;
                B2 = T;
            }

            for (int j = 0; j < 16; j++)
            {
                T = Rol(A2 + F3(B2, C2, D2) + _blockDWords[rIndices3[j]] + 0x6D703EF3U, rr3[j]) + E2;
                A2 = E2;
                E2 = D2;
                D2 = Rol(C2, 10);
                C2 = B2;
                B2 = T;
            }

            for (int j = 0; j < 16; j++)
            {
                T = Rol(A2 + F2(B2, C2, D2) + _blockDWords[rIndices4[j]] + 0x7A6D76E9U, rr4[j]) + E2;
                A2 = E2;
                E2 = D2;
                D2 = Rol(C2, 10);
                C2 = B2;
                B2 = T;
            }

            for (int j = 0; j < 16; j++)
            {
                T = Rol(A2 + F1(B2, C2, D2) + _blockDWords[rIndices5[j]], rr5[j]) + E2;
                A2 = E2;
                E2 = D2;
                D2 = Rol(C2, 10);
                C2 = B2;
                B2 = T;
            }

            uint temp = _h1 + C1 + D2;
            _h1 = _h2 + D1 + E2;
            _h2 = _h3 + E1 + A2;
            _h3 = _h4 + A1 + B2;
            _h4 = _h0 + B1 + C2;
            _h0 = temp;
        }

        private static uint F1(uint x, uint y, uint z)
        {
            return x ^ y ^ z;
        }

        private static uint F2(uint x, uint y, uint z)
        {
            return (x & y) | (~x & z);
        }

        private static uint F3(uint x, uint y, uint z)
        {
            return (x | ~y) ^ z;
        }

        private static uint F4(uint x, uint y, uint z)
        {
            return (x & z) | (y & ~z);
        }

        private static uint F5(uint x, uint y, uint z)
        {
            return x ^ (y | ~z);
        }
    }
}