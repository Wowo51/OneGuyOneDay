using System;
using System.Numerics;

namespace MullersMethod
{
    public class Program
    {
        public static void Main()
        {
            Func<System.Numerics.Complex, System.Numerics.Complex> f = delegate (System.Numerics.Complex x)
            {
                return x * x - 2;
            };
            System.Numerics.Complex x0 = new System.Numerics.Complex(1, 0);
            System.Numerics.Complex x1 = new System.Numerics.Complex(1.5, 0);
            System.Numerics.Complex x2 = new System.Numerics.Complex(2, 0);
            System.Numerics.Complex root = MullersMethodSolver.FindRoot(f, x0, x1, x2);
            Console.WriteLine("Root: " + root);
        }
    }
}