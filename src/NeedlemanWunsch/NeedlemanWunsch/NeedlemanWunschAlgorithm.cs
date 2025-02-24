using System;
using System.Text;

namespace NeedlemanWunsch
{
    public class NeedlemanWunschAlgorithm
    {
        public static AlignmentResult Align(string sequence1, string sequence2, int match, int mismatch, int gap)
        {
            if (sequence1 == null)
            {
                sequence1 = string.Empty;
            }

            if (sequence2 == null)
            {
                sequence2 = string.Empty;
            }

            int length1 = sequence1.Length;
            int length2 = sequence2.Length;
            int[, ] score = new int[length1 + 1, length2 + 1];
            for (int i = 0; i <= length1; i++)
            {
                score[i, 0] = i * gap;
            }

            for (int j = 0; j <= length2; j++)
            {
                score[0, j] = j * gap;
            }

            for (int i = 1; i <= length1; i++)
            {
                for (int j = 1; j <= length2; j++)
                {
                    int diagonal = score[i - 1, j - 1] + ((sequence1[i - 1] == sequence2[j - 1]) ? match : mismatch);
                    int up = score[i - 1, j] + gap;
                    int left = score[i, j - 1] + gap;
                    score[i, j] = Math.Max(Math.Max(diagonal, up), left);
                }
            }

            int iIndex = length1;
            int jIndex = length2;
            StringBuilder aligned1 = new StringBuilder();
            StringBuilder aligned2 = new StringBuilder();
            while (iIndex > 0 || jIndex > 0)
            {
                if (iIndex > 0 && jIndex > 0)
                {
                    int diagScore = score[iIndex - 1, jIndex - 1] + ((sequence1[iIndex - 1] == sequence2[jIndex - 1]) ? match : mismatch);
                    if (score[iIndex, jIndex] == diagScore)
                    {
                        aligned1.Insert(0, sequence1[iIndex - 1]);
                        aligned2.Insert(0, sequence2[jIndex - 1]);
                        iIndex--;
                        jIndex--;
                        continue;
                    }
                }

                if (iIndex > 0)
                {
                    int upScore = score[iIndex - 1, jIndex] + gap;
                    if (score[iIndex, jIndex] == upScore)
                    {
                        aligned1.Insert(0, sequence1[iIndex - 1]);
                        aligned2.Insert(0, '-');
                        iIndex--;
                        continue;
                    }
                }

                if (jIndex > 0)
                {
                    aligned1.Insert(0, '-');
                    aligned2.Insert(0, sequence2[jIndex - 1]);
                    jIndex--;
                }
            }

            return new AlignmentResult(aligned1.ToString(), aligned2.ToString(), score[length1, length2]);
        }
    }

    public class AlignmentResult
    {
        public string AlignedSequence1 { get; }
        public string AlignedSequence2 { get; }
        public int Score { get; }

        public AlignmentResult(string alignedSequence1, string alignedSequence2, int score)
        {
            AlignedSequence1 = alignedSequence1;
            AlignedSequence2 = alignedSequence2;
            Score = score;
        }
    }
}