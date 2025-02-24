using System;

namespace VerhoeffAlgorithm
{
    public static class Verhoeff
    {
        private static readonly int[, ] d = new int[10, 10]
        {
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
                9
            },
            {
                1,
                2,
                3,
                4,
                0,
                6,
                7,
                8,
                9,
                5
            },
            {
                2,
                3,
                4,
                0,
                1,
                7,
                8,
                9,
                5,
                6
            },
            {
                3,
                4,
                0,
                1,
                2,
                8,
                9,
                5,
                6,
                7
            },
            {
                4,
                0,
                1,
                2,
                3,
                9,
                5,
                6,
                7,
                8
            },
            {
                5,
                9,
                8,
                7,
                6,
                0,
                4,
                3,
                2,
                1
            },
            {
                6,
                5,
                9,
                8,
                7,
                1,
                0,
                4,
                3,
                2
            },
            {
                7,
                6,
                5,
                9,
                8,
                2,
                1,
                0,
                4,
                3
            },
            {
                8,
                7,
                6,
                5,
                9,
                3,
                2,
                1,
                0,
                4
            },
            {
                9,
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1,
                0
            }
        };
        private static readonly int[, ] p = new int[8, 10]
        {
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
                9
            },
            {
                1,
                5,
                7,
                6,
                2,
                8,
                3,
                0,
                9,
                4
            },
            {
                5,
                8,
                0,
                3,
                7,
                9,
                6,
                1,
                4,
                2
            },
            {
                8,
                9,
                1,
                6,
                0,
                4,
                3,
                5,
                2,
                7
            },
            {
                9,
                4,
                5,
                3,
                1,
                2,
                6,
                8,
                7,
                0
            },
            {
                4,
                2,
                8,
                6,
                5,
                7,
                3,
                9,
                0,
                1
            },
            {
                2,
                7,
                9,
                3,
                8,
                0,
                6,
                4,
                1,
                5
            },
            {
                7,
                0,
                4,
                6,
                9,
                1,
                3,
                2,
                5,
                8
            }
        };
        private static readonly int[] inv = new int[10]
        {
            0,
            4,
            3,
            2,
            1,
            5,
            6,
            7,
            8,
            9
        };
        public static bool Validate(string number)
        {
            if (number == null)
            {
                return false;
            }

            int c = 0;
            int length = number.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = number[length - i - 1];
                if (ch < '0' || ch > '9')
                {
                    return false;
                }

                int digit = ch - '0';
                c = d[c, p[i % 8, digit]];
            }

            return (c == 0);
        }

        public static int ComputeCheckDigit(string number)
        {
            if (number == null)
            {
                return -1;
            }

            int c = 0;
            int length = number.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = number[length - i - 1];
                if (ch < '0' || ch > '9')
                {
                    return -1;
                }

                int digit = ch - '0';
                c = d[c, p[(i + 1) % 8, digit]];
            }

            return inv[c];
        }

        public static string AppendCheckDigit(string number)
        {
            int checkDigit = ComputeCheckDigit(number);
            if (checkDigit < 0)
            {
                return number;
            }

            return number + checkDigit.ToString();
        }
    }
}