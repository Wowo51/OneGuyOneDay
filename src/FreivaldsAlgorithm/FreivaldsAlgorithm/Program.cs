using System;

namespace FreivaldsAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[, ] A = new int[, ]
            {
                {
                    1,
                    2
                },
                {
                    3,
                    4
                }
            };
            int[, ] B = new int[, ]
            {
                {
                    5,
                    6
                },
                {
                    7,
                    8
                }
            };
            int[, ] C = new int[, ]
            {
                {
                    19,
                    22
                },
                {
                    43,
                    50
                }
            };
            bool result = Freivalds.VerifyMultiplication(A, B, C, 10);
            System.Console.WriteLine("Matrix multiplication verified: " + result);
        }
    }
}