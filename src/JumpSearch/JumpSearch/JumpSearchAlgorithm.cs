using System;

namespace JumpSearch
{
    public static class JumpSearchAlgorithm
    {
        public static int Search<T>(T[] array, T target)
            where T : IComparable<T>
        {
            if (array == null)
            {
                return -1;
            }

            int length = array.Length;
            if (length == 0)
            {
                return -1;
            }

            int blockSize = (int)Math.Sqrt(length);
            int prev = 0;
            int step = blockSize;
            while (prev < length && array[Math.Min(step, length) - 1].CompareTo(target) < 0)
            {
                prev = step;
                step += blockSize;
                if (prev >= length)
                {
                    return -1;
                }
            }

            for (int index = prev; index < Math.Min(step, length); index++)
            {
                if (array[index].CompareTo(target) == 0)
                {
                    return index;
                }
            }

            return -1;
        }
    }
}