using System;

namespace BitonicSorter
{
    public static class BitonicSorter
    {
        public static void Sort<T>(T[] array, bool ascending = true)
            where T : IComparable<T>
        {
            if (array == null)
            {
                return;
            }

            int length = array.Length;
            BitonicSort(array, 0, length, ascending);
        }

        private static void BitonicSort<T>(T[] array, int low, int count, bool ascending)
            where T : IComparable<T>
        {
            if (count > 1)
            {
                int k = count / 2;
                BitonicSort(array, low, k, true);
                BitonicSort(array, low + k, k, false);
                BitonicMerge(array, low, count, ascending);
            }
        }

        private static void BitonicMerge<T>(T[] array, int low, int count, bool ascending)
            where T : IComparable<T>
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = low; i < low + k; i++)
                {
                    if ((ascending && array[i].CompareTo(array[i + k]) > 0) || (!ascending && array[i].CompareTo(array[i + k]) < 0))
                    {
                        T temp = array[i];
                        array[i] = array[i + k];
                        array[i + k] = temp;
                    }
                }

                BitonicMerge(array, low, k, ascending);
                BitonicMerge(array, low + k, k, ascending);
            }
        }
    }
}