using System;

namespace NeedlemanWunsch
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string sequence1 = "GATTACA";
            string sequence2 = "GCATGCU";
            AlignmentResult result = NeedlemanWunschAlgorithm.Align(sequence1, sequence2, 1, -1, -1);
            Console.WriteLine("Alignment Score: " + result.Score);
            Console.WriteLine("Aligned Sequence1: " + result.AlignedSequence1);
            Console.WriteLine("Aligned Sequence2: " + result.AlignedSequence2);
        }
    }
}