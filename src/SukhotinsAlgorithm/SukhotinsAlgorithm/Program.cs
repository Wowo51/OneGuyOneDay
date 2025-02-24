using System;
using System.Collections.Generic;

namespace SukhotinsAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter text:");
            string input = Console.ReadLine() ?? "";
            ClassificationResult result = SukhotinsAlgorithm.Classify(input);
            Console.WriteLine("Vowels: " + string.Join(", ", result.Vowels));
            Console.WriteLine("Consonants: " + string.Join(", ", result.Consonants));
        }
    }
}