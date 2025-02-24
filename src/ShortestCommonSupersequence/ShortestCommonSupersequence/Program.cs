using System;

namespace ShortestCommonSupersequence
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: <program> <sequence1> <sequence2> [...]");
                return;
            }

            string result = SupersequenceSolver.ShortestCommonSupersequence(args);
            Console.WriteLine(result);
        }
    }
}