using System;
using System.Collections.Generic;

namespace InsertionSort
{
    public static class InsertionSorter
    {
        public static void Sort<T>(List<T> list)
            where T : IComparable<T>
        {
            if (list == null)
            {
                return;
            }

            int count = list.Count;
            for (int i = 1; i < count; i++)
            {
                T current = list[i];
                int j = i - 1;
                while (j >= 0 && list[j].CompareTo(current) > 0)
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = current;
            }
        }
    }
}