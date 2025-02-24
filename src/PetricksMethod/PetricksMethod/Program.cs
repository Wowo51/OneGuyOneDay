using System;
using System.Collections.Generic;

namespace PetricksMethod
{
    public class Program
    {
        public static int Main(string[] arguments)
        {
            IList<IList<string>> chart = new List<IList<string>>
            {
                new List<string>
                {
                    "A",
                    "B"
                },
                new List<string>
                {
                    "A",
                    "C"
                },
                new List<string>
                {
                    "B",
                    "C"
                }
            };
            IList<IList<string>> solutions = PetrickSimplifier.Simplify(chart);
            Console.WriteLine("Optimal solutions:");
            foreach (IList<string> solution in solutions)
            {
                Console.WriteLine(string.Join(", ", solution));
            }

            return 0;
        }
    }
}