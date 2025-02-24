using System;

namespace GraspAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            GraspSolver solver = new GraspSolver(100, 42, 0, 84);
            int bestSolution = solver.Solve();
            Console.WriteLine("Best solution found: " + bestSolution);
        }
    }
}