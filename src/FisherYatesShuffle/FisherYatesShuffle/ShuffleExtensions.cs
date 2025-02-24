using System;
using System.Collections.Generic;

namespace FisherYatesShuffle
{
    public static class ShuffleExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            if (list == null || list.Count <= 1)
            {
                return;
            }

            Random random = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}