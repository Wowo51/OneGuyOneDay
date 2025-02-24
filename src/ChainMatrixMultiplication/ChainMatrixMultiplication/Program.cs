using System;

namespace ChainMatrixMultiplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] dims = new int[]
            {
                30,
                35,
                15,
                5,
                10,
                20,
                25
            };
            (int cost, string order) result = MatrixChainMultiplicationSolver.ComputeOptimalOrder(dims);
            Console.WriteLine("Minimum multiplication cost: " + result.cost);
            Console.WriteLine("Optimal parenthesization: " + result.order);
        }
    }
}