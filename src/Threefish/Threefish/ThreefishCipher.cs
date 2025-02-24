using System;

namespace Threefish
{
    public sealed class ThreefishCipher
    {
        private readonly UInt64[] _K;
        private readonly UInt64[] _T;
        private const UInt64 C240 = 0x1BD11BDAA9FC1A22UL;
        private const int Rounds = 72;
        private const int SubkeyCount = 19;
        private static readonly int[, ] RotationConstants = new int[, ]
        {
            {
                14,
                16
            },
            {
                52,
                57
            },
            {
                23,
                40
            },
            {
                5,
                37
            }
        };
        public ThreefishCipher(UInt64[] key, UInt64[] tweak)
        {
            UInt64[] localKey;
            if (key == null)
            {
                localKey = new UInt64[4];
            }
            else
            {
                localKey = new UInt64[4];
                for (int i = 0; i < 4; i++)
                {
                    if (i < key.Length)
                    {
                        localKey[i] = key[i];
                    }
                    else
                    {
                        localKey[i] = 0UL;
                    }
                }
            }

            UInt64[] localTweak;
            if (tweak == null)
            {
                localTweak = new UInt64[2];
            }
            else
            {
                localTweak = new UInt64[2];
                for (int i = 0; i < 2; i++)
                {
                    if (i < tweak.Length)
                    {
                        localTweak[i] = tweak[i];
                    }
                    else
                    {
                        localTweak[i] = 0UL;
                    }
                }
            }

            _K = new UInt64[5];
            _K[0] = localKey[0];
            _K[1] = localKey[1];
            _K[2] = localKey[2];
            _K[3] = localKey[3];
            _K[4] = C240 ^ _K[0] ^ _K[1] ^ _K[2] ^ _K[3];
            _T = new UInt64[3];
            _T[0] = localTweak[0];
            _T[1] = localTweak[1];
            _T[2] = localTweak[0] ^ localTweak[1];
        }

        public UInt64[] Encrypt(UInt64[] block)
        {
            UInt64[] X = new UInt64[4];
            if (block == null)
            {
                block = new UInt64[4];
            }

            for (int i = 0; i < 4; i++)
            {
                if (i < block.Length)
                {
                    X[i] = block[i];
                }
                else
                {
                    X[i] = 0UL;
                }
            }

            // Initial subkey injection (s = 0)
            for (int i = 0; i < 4; i++)
            {
                X[i] = unchecked(X[i] + _K[i]);
            }

            // 18 rounds (each round group consists of 4 rounds + subkey injection)
            for (int s = 1; s <= 18; s++)
            {
                for (int r = 0; r < 4; r++)
                {
                    Mix(ref X[0], ref X[1], RotationConstants[r, 0]);
                    Mix(ref X[2], ref X[3], RotationConstants[r, 1]);
                    if (r != 3)
                    {
                        Permute(ref X);
                    }
                }

                X[0] = unchecked(X[0] + _K[(s + 0) % 5]);
                X[1] = unchecked(X[1] + _K[(s + 1) % 5] + _T[s % 3]);
                X[2] = unchecked(X[2] + _K[(s + 2) % 5] + _T[(s + 1) % 3]);
                X[3] = unchecked(X[3] + _K[(s + 3) % 5] + (UInt64)s);
            }

            return X;
        }

        public UInt64[] Decrypt(UInt64[] block)
        {
            UInt64[] X = new UInt64[4];
            if (block == null)
            {
                block = new UInt64[4];
            }

            for (int i = 0; i < 4; i++)
            {
                if (i < block.Length)
                {
                    X[i] = block[i];
                }
                else
                {
                    X[i] = 0UL;
                }
            }

            // Reverse rounds: from s = 18 down to 1
            for (int s = 18; s >= 1; s--)
            {
                X[0] = unchecked(X[0] - _K[(s + 0) % 5]);
                X[1] = unchecked(X[1] - _K[(s + 1) % 5] - _T[s % 3]);
                X[2] = unchecked(X[2] - _K[(s + 2) % 5] - _T[(s + 1) % 3]);
                X[3] = unchecked(X[3] - _K[(s + 3) % 5] - (UInt64)s);
                for (int r = 3; r >= 0; r--)
                {
                    if (r != 3)
                    {
                        Permute(ref X);
                    }

                    InverseMix(ref X[0], ref X[1], RotationConstants[r, 0]);
                    InverseMix(ref X[2], ref X[3], RotationConstants[r, 1]);
                }
            }

            // Remove initial subkey injection (s = 0)
            for (int i = 0; i < 4; i++)
            {
                X[i] = unchecked(X[i] - _K[i]);
            }

            return X;
        }

        private static void Mix(ref UInt64 a, ref UInt64 b, int rotation)
        {
            UInt64 newA = unchecked(a + b);
            UInt64 newB = RotL(b, rotation) ^ newA;
            a = newA;
            b = newB;
        }

        private static void InverseMix(ref UInt64 a, ref UInt64 b, int rotation)
        {
            UInt64 x1 = RotR(b ^ a, rotation);
            UInt64 x0 = unchecked(a - x1);
            a = x0;
            b = x1;
        }

        private static UInt64 RotL(UInt64 x, int bits)
        {
            return (x << bits) | (x >> (64 - bits));
        }

        private static UInt64 RotR(UInt64 x, int bits)
        {
            return (x >> bits) | (x << (64 - bits));
        }

        private static void Permute(ref UInt64[] X)
        {
            UInt64 temp0 = X[0];
            UInt64 temp1 = X[3];
            UInt64 temp2 = X[2];
            UInt64 temp3 = X[1];
            X[0] = temp0;
            X[1] = temp1;
            X[2] = temp2;
            X[3] = temp3;
        }
    }
}