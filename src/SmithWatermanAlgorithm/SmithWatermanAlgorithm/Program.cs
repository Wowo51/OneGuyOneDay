using System;

namespace SmithWatermanAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter first sequence:");
            string sequence1 = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter second sequence:");
            string sequence2 = Console.ReadLine() ?? string.Empty;
            int matchScore = 2;
            int mismatchPenalty = -1;
            int gapPenalty = 2;
            SmithWaterman sw = new SmithWaterman();
            AlignmentResult alignment = sw.Align(sequence1, sequence2, matchScore, mismatchPenalty, gapPenalty);
            Console.WriteLine("Alignment Score: " + alignment.Score);
            Console.WriteLine("Aligned Sequence 1: " + alignment.AlignedSequence1);
            Console.WriteLine("Aligned Sequence 2: " + alignment.AlignedSequence2);
        }
    }
}