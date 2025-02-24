using System;

namespace SequiturAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter string to compress:");
            string input = Console.ReadLine() ?? "";
            Sequitur sequitur = new Sequitur();
            Grammar grammar = sequitur.Compress(input);
            Console.WriteLine("Compressed grammar:");
            Console.WriteLine(grammar.ToString());
        }
    }
}