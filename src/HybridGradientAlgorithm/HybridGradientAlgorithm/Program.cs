using System;

namespace HybridGradientAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[] initialGuess = new double[]
            {
                1.0,
                1.0
            };
            HybridConjugateGradient hybridAlgorithm = new HybridConjugateGradient();
            double[] solution = hybridAlgorithm.Solve(initialGuess);
            Console.WriteLine("Solution: " + string.Join(", ", solution));
        }
    }
}