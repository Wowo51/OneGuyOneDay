using System;

namespace PredictiveSearch
{
    public static class PredictiveSearchEngine
    {
        public static int InterpolatedSearch(int[] sortedArray, int target)
        {
            if (sortedArray == null)
            {
                return -1;
            }

            int low = 0;
            int high = sortedArray.Length - 1;
            while (low <= high && target >= sortedArray[low] && target <= sortedArray[high])
            {
                if (sortedArray[high] == sortedArray[low])
                {
                    if (sortedArray[low] == target)
                    {
                        return low;
                    }
                    else
                    {
                        break;
                    }
                }

                int mid = low + ((target - sortedArray[low]) * (high - low)) / (sortedArray[high] - sortedArray[low]);
                if (sortedArray[mid] == target)
                {
                    return mid;
                }

                if (sortedArray[mid] < target)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return -1;
        }
    }
}