using System;
using System.Collections.Generic;

namespace RuzzoTompaAlgorithm
{
    public class RuzzoTompaAlgorithm
    {
        public static List<ScoreSegment> FindMaximalSubsequences(double[] scores)
        {
            List<ScoreSegment> segments = new List<ScoreSegment>();
            if (scores == null)
            {
                return segments;
            }

            int n = scores.Length;
            int index = 0;
            while (index < n)
            {
                while (index < n && scores[index] <= 0)
                {
                    index++;
                }

                if (index >= n)
                {
                    break;
                }

                int start = index;
                double currentSum = 0;
                double bestSum = double.NegativeInfinity;
                int bestEnd = start;
                int j = start;
                while (j < n && (currentSum + scores[j]) > 0)
                {
                    currentSum += scores[j];
                    if (currentSum > bestSum)
                    {
                        bestSum = currentSum;
                        bestEnd = j;
                    }

                    j++;
                }

                if (bestSum > 0)
                {
                    ScoreSegment segment = new ScoreSegment
                    {
                        Start = start,
                        End = bestEnd,
                        Score = bestSum
                    };
                    segments.Add(segment);
                }

                index = j;
            }

            return segments;
        }
    }
}