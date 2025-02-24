using System;
using System.Collections.Generic;

namespace LongestCommonSubstring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Example inputs: modify or extend as needed.
            string[] inputs = new string[]
            {
                "ababc",
                "babca"
            };
            List<string> lcs = LongestCommonSubstringFinder.FindLongestCommonSubstrings(inputs);
            Console.WriteLine("Longest Common Substring(s):");
            foreach (string substring in lcs)
            {
                Console.WriteLine(substring);
            }
        }
    }
}