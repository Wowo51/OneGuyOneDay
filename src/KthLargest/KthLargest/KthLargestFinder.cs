namespace KthLargest
{
    public static class KthLargestFinder
    {
        public static int? FindKthLargest(int[] sequence, int k)
        {
            if (sequence == null || sequence.Length == 0 || k < 1 || k > sequence.Length)
            {
                return null;
            }

            int[] copy = (int[])sequence.Clone();
            int targetIndex = copy.Length - k;
            return QuickSelect(copy, 0, copy.Length - 1, targetIndex);
        }

        private static int QuickSelect(int[] arr, int left, int right, int kIndex)
        {
            if (left == right)
            {
                return arr[left];
            }

            int pivotIndex = Partition(arr, left, right);
            if (pivotIndex == kIndex)
            {
                return arr[pivotIndex];
            }
            else if (pivotIndex < kIndex)
            {
                return QuickSelect(arr, pivotIndex + 1, right, kIndex);
            }
            else
            {
                return QuickSelect(arr, left, pivotIndex - 1, kIndex);
            }
        }

        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int i = left;
            for (int j = left; j < right; j++)
            {
                if (arr[j] <= pivot)
                {
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    i++;
                }
            }

            int temp2 = arr[i];
            arr[i] = arr[right];
            arr[right] = temp2;
            return i;
        }
    }
}