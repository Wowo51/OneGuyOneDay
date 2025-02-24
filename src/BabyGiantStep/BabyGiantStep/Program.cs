using System;
using System.Numerics;

namespace BabyGiantStep
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Numerics.BigInteger g = 2;
            System.Numerics.BigInteger h = 22;
            System.Numerics.BigInteger p = 29;
            System.Numerics.BigInteger x = DiscreteLogSolver.FindDiscreteLog(g, h, p);
            Console.WriteLine("Discrete log (x) such that 2^x ≡ 22 mod 29 is: " + x);
        }
    }
}