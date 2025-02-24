using System;

namespace TabuSearchAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Tabu Search Algorithm Demo");
            TabuSearch tabuSearch = new TabuSearch(100, 5);
            int initialSolution = 0;
            int bestSolution = tabuSearch.Execute(initialSolution);
            Console.WriteLine("Best solution found: " + bestSolution);
        }
    }
}