namespace BinarySearchAlgorithm
{
    public static class BinarySearch
    {
        public static int Search<T>(T[] array, T target)
            where T : System.IComparable<T>
        {
            if (array == null)
            {
                return -1;
            }

            int left = 0;
            int right = array.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int compareResult = array[mid].CompareTo(target);
                if (compareResult == 0)
                {
                    return mid;
                }
                else if (compareResult < 0)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return -1;
        }
    }
}