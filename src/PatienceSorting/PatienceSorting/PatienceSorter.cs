using System;
using System.Collections.Generic;

namespace PatienceSorting
{
    public static class PatienceSorter
    {
        public static IList<T> Sort<T>(IEnumerable<T> source)
            where T : IComparable<T>
        {
            if (source == null)
            {
                return new List<T>();
            }

            List<List<T>> piles = new List<List<T>>();
            foreach (T value in source)
            {
                bool placed = false;
                for (int i = 0; i < piles.Count; i++)
                {
                    T top = piles[i][piles[i].Count - 1];
                    if (top.CompareTo(value) >= 0)
                    {
                        piles[i].Add(value);
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                {
                    List<T> newPile = new List<T>();
                    newPile.Add(value);
                    piles.Add(newPile);
                }
            }

            List<Queue<T>> sortedPiles = new List<Queue<T>>();
            foreach (List<T> pile in piles)
            {
                List<T> reversed = new List<T>(pile);
                reversed.Reverse();
                Queue<T> queue = new Queue<T>(reversed);
                sortedPiles.Add(queue);
            }

            List<T> result = new List<T>();
            while (true)
            {
                int minIndex = -1;
                bool found = false;
                T minElement = default(T)!;
                for (int i = 0; i < sortedPiles.Count; i++)
                {
                    if (sortedPiles[i].Count > 0)
                    {
                        T current = sortedPiles[i].Peek();
                        if (!found)
                        {
                            minElement = current;
                            minIndex = i;
                            found = true;
                        }
                        else if (current.CompareTo(minElement) < 0)
                        {
                            minElement = current;
                            minIndex = i;
                        }
                    }
                }

                if (!found)
                {
                    break;
                }

                result.Add(sortedPiles[minIndex].Dequeue());
            }

            return result;
        }
    }
}