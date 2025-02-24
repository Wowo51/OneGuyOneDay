namespace Sha3Variants
{
    internal static class Keccak
    {
        private static readonly ulong[] RoundConstants = new ulong[]
        {
            0x0000000000000001UL,
            0x0000000000008082UL,
            0x800000000000808aUL,
            0x8000000080008000UL,
            0x000000000000808bUL,
            0x0000000080000001UL,
            0x8000000080008081UL,
            0x8000000000008009UL,
            0x000000000000008aUL,
            0x0000000000000088UL,
            0x0000000080008009UL,
            0x000000008000000aUL,
            0x000000008000808bUL,
            0x800000000000008bUL,
            0x8000000000008089UL,
            0x8000000000008003UL,
            0x8000000000008002UL,
            0x8000000000000080UL,
            0x000000000000800aUL,
            0x800000008000000aUL,
            0x8000000080008081UL,
            0x8000000000008080UL,
            0x0000000080000001UL,
            0x8000000080008008UL
        };
        private static readonly int[, ] RotationOffsets = new int[5, 5]
        {
            {
                0,
                36,
                3,
                41,
                18
            },
            {
                1,
                44,
                10,
                45,
                2
            },
            {
                62,
                6,
                43,
                15,
                61
            },
            {
                28,
                55,
                25,
                21,
                56
            },
            {
                27,
                20,
                39,
                8,
                14
            }
        };
        public static void Permute(ulong[] state)
        {
            if (state == null || state.Length != 25)
            {
                return;
            }

            for (int round = 0; round < 24; round++)
            {
                ulong[] C = new ulong[5];
                for (int x = 0; x < 5; x++)
                {
                    C[x] = state[x] ^ state[x + 5] ^ state[x + 10] ^ state[x + 15] ^ state[x + 20];
                }

                ulong[] D = new ulong[5];
                for (int x = 0; x < 5; x++)
                {
                    D[x] = C[(x + 4) % 5] ^ RotateLeft(C[(x + 1) % 5], 1);
                }

                for (int i = 0; i < 25; i++)
                {
                    state[i] ^= D[i % 5];
                }

                ulong[] B = new ulong[25];
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        int newX = y;
                        int newY = (2 * x + 3 * y) % 5;
                        B[newX + 5 * newY] = RotateLeft(state[x + 5 * y], RotationOffsets[x, y]);
                    }
                }

                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        state[x + 5 * y] = B[x + 5 * y] ^ ((~B[((x + 1) % 5) + 5 * y]) & B[((x + 2) % 5) + 5 * y]);
                    }
                }

                state[0] ^= RoundConstants[round];
            }
        }

        private static ulong RotateLeft(ulong value, int shift)
        {
            shift = shift % 64;
            return (value << shift) | (value >> (64 - shift));
        }
    }
}