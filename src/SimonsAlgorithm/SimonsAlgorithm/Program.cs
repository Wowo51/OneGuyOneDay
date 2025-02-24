using System;

namespace SimonsAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 4;
            int secret = 6;
            System.Func<int, int> f = delegate (int x)
            {
                int xXorSecret = x ^ secret;
                int result = (x < xXorSecret) ? x : xXorSecret;
                return result;
            };
            int foundSecret = SimonsAlgorithmSolver.FindSecret(n, f);
            Console.WriteLine("Expected Secret: " + secret);
            Console.WriteLine("Found Secret: " + foundSecret);
        }
    }
}