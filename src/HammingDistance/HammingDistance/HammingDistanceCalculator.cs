using System;

namespace HammingDistance
{
    public static class HammingDistanceCalculator
    {
        public static int ComputeHammingDistance(string first, string second)
        {
            if (first == null)
            {
                first = "";
            }

            if (second == null)
            {
                second = "";
            }

            if (first.Length != second.Length)
            {
                return -1;
            }

            int distance = 0;
            for (int index = 0; index < first.Length; index++)
            {
                if (first[index] != second[index])
                {
                    distance++;
                }
            }

            return distance;
        }
    }
}