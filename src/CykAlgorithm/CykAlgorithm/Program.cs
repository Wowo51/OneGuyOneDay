using System;
using System.Collections.Generic;

namespace CykAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Define a simple grammar in Chomsky Normal Form.
            // Example grammar (for demonstration purposes):
            //   S -> A B
            //   A -> a
            //   B -> b
            List<Production> productions = new List<Production>
            {
                new Production("S", new List<string> { "A", "B" }),
                new Production("A", new List<string> { "a" }),
                new Production("B", new List<string> { "b" })
            };
            Grammar grammar = new Grammar("S", productions);
            CykParser parser = new CykParser();
            string input = "ab";
            bool result = parser.Parse(grammar, input);
            Console.WriteLine("Input \"" + input + "\" is " + (result ? "accepted" : "rejected") + " by the grammar.");
        }
    }
}