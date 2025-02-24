using System;
using System.Collections.Generic;

namespace SortMergeJoin
{
    public class Program
    {
        public static void Main()
        {
            List<int> left = new List<int>
            {
                1,
                2,
                2,
                3,
                5,
                7,
                7
            };
            List<int> right = new List<int>
            {
                2,
                2,
                4,
                7,
                8
            };
            List<Tuple<int, int>> joinResult = SortMergeJoiner.Join<int, int, int>(left, right, new Func<int, int>(x => x), new Func<int, int>(x => x), Comparer<int>.Default);
            foreach (Tuple<int, int> pair in joinResult)
            {
                Console.WriteLine("Join: " + pair.Item1 + " and " + pair.Item2);
            }
        }
    }
}