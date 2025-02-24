using System;

namespace CycleSortInPlace
{
    public static class CycleSorter
    {
        public static void Sort<T>(T[] array)
            where T : IComparable<T>
        {
            if (array == null)
            {
                return;
            }

            int n = array.Length;
            for (int cycleStart = 0; cycleStart < n - 1; cycleStart++)
            {
                T item = array[cycleStart];
                int pos = cycleStart;
                for (int i = cycleStart + 1; i < n; i++)
                {
                    if (array[i].CompareTo(item) < 0)
                    {
                        pos++;
                    }
                }

                if (pos == cycleStart)
                {
                    continue;
                }

                while (item.CompareTo(array[pos]) == 0)
                {
                    pos++;
                }

                if (pos != cycleStart)
                {
                    T temp = array[pos];
                    array[pos] = item;
                    item = temp;
                }

                while (pos != cycleStart)
                {
                    pos = cycleStart;
                    for (int i = cycleStart + 1; i < n; i++)
                    {
                        if (array[i].CompareTo(item) < 0)
                        {
                            pos++;
                        }
                    }

                    while (item.CompareTo(array[pos]) == 0)
                    {
                        pos++;
                    }

                    if (item.CompareTo(array[pos]) != 0)
                    {
                        T temp = array[pos];
                        array[pos] = item;
                        item = temp;
                    }
                }
            }
        }
    }
}