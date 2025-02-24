using System;

namespace ElserDifferenceMap
{
    public static class Program
    {
        public static void Main()
        {
            double[] initial = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            DifferenceMapSolver solver = new DifferenceMapSolver(new IdentityProjection(), new IdentityProjection(), -1.0, 100, 1e-6);
            double[] solution = solver.Solve(initial);
            Console.WriteLine("Solution: " + string.Join(", ", solution));
        }
    }
}