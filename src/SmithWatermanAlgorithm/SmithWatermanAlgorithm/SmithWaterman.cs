using System;

namespace SmithWatermanAlgorithm
{
    public class AlignmentResult
    {
        public int Score { get; set; }
        public string AlignedSequence1 { get; set; }
        public string AlignedSequence2 { get; set; }

        public AlignmentResult()
        {
            Score = 0;
            AlignedSequence1 = string.Empty;
            AlignedSequence2 = string.Empty;
        }
    }

    public class SmithWaterman
    {
        public AlignmentResult Align(string seq1, string seq2, int matchScore, int mismatchPenalty, int gapPenalty)
        {
            if (seq1 == null)
            {
                seq1 = string.Empty;
            }

            if (seq2 == null)
            {
                seq2 = string.Empty;
            }

            int m = seq1.Length;
            int n = seq2.Length;
            int[, ] scoreMatrix = new int[m + 1, n + 1];
            int maxScore = 0;
            int maxI = 0;
            int maxJ = 0;
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    int matchMismatch = (seq1[i - 1] == seq2[j - 1]) ? matchScore : mismatchPenalty;
                    int diag = scoreMatrix[i - 1, j - 1] + matchMismatch;
                    int delete = scoreMatrix[i - 1, j] - gapPenalty;
                    int insert = scoreMatrix[i, j - 1] - gapPenalty;
                    int cellScore = Math.Max(0, Math.Max(diag, Math.Max(delete, insert)));
                    scoreMatrix[i, j] = cellScore;
                    if (cellScore > maxScore)
                    {
                        maxScore = cellScore;
                        maxI = i;
                        maxJ = j;
                    }
                }
            }

            int iIndex = maxI;
            int jIndex = maxJ;
            string alignment1 = "";
            string alignment2 = "";
            while (iIndex > 0 && jIndex > 0 && scoreMatrix[iIndex, jIndex] > 0)
            {
                int currentScore = scoreMatrix[iIndex, jIndex];
                int scoreDiagonal = scoreMatrix[iIndex - 1, jIndex - 1];
                int matchMismatchScore = (seq1[iIndex - 1] == seq2[jIndex - 1]) ? matchScore : mismatchPenalty;
                if (currentScore == scoreDiagonal + matchMismatchScore)
                {
                    alignment1 = seq1[iIndex - 1] + alignment1;
                    alignment2 = seq2[jIndex - 1] + alignment2;
                    iIndex--;
                    jIndex--;
                }
                else if (iIndex > 0 && currentScore == scoreMatrix[iIndex - 1, jIndex] - gapPenalty)
                {
                    alignment1 = seq1[iIndex - 1] + alignment1;
                    alignment2 = "-" + alignment2;
                    iIndex--;
                }
                else if (jIndex > 0 && currentScore == scoreMatrix[iIndex, jIndex - 1] - gapPenalty)
                {
                    alignment1 = "-" + alignment1;
                    alignment2 = seq2[jIndex - 1] + alignment2;
                    jIndex--;
                }
                else
                {
                    break;
                }
            }

            AlignmentResult result = new AlignmentResult();
            result.Score = maxScore;
            result.AlignedSequence1 = alignment1;
            result.AlignedSequence2 = alignment2;
            return result;
        }
    }
}