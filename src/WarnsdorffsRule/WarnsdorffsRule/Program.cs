using System;
using System.Collections.Generic;

namespace WarnsdorffsRule
{
    public class Program
    {
        public static void Main(string[] args)
        {
            KnightTourSolver solver = new KnightTourSolver();
            List<Coordinate> tour = solver.Solve(8, 0, 0);
            if (tour.Count == 0)
            {
                Console.WriteLine("No complete tour found.");
            }
            else
            {
                foreach (Coordinate coord in tour)
                {
                    Console.WriteLine("({0}, {1})", coord.X, coord.Y);
                }
            }
        }
    }
}