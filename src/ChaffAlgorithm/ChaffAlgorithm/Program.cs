using System;
using System.Collections.Generic;

namespace ChaffAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Chaff Algorithm SAT Solver");
            // Sample CNF: (x1 OR not x2) AND (not x1 OR x2 OR x3)
            List<List<int>> clauses = new List<List<int>>();
            clauses.Add(new List<int> { 1, -2 });
            clauses.Add(new List<int> { -1, 2, 3 });
            ChaffSolver solver = new ChaffSolver();
            Dictionary<int, bool>? solution = solver.Solve(clauses);
            if (solution != null)
            {
                Console.WriteLine("SATISFIABLE with assignment:");
                foreach (KeyValuePair<int, bool> entry in solution)
                {
                    Console.WriteLine("Variable {0} = {1}", entry.Key, entry.Value);
                }
            }
            else
            {
                Console.WriteLine("UNSATISFIABLE.");
            }
        }
    }
}