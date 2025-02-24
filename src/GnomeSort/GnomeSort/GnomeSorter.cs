namespace GnomeSort
{
    public static class GnomeSorter
    {
        public static void Sort(int[] array)
        {
            if (array == null)
            {
                return;
            }

            int index = 0;
            while (index < array.Length)
            {
                if (index == 0 || array[index] >= array[index - 1])
                {
                    index++;
                }
                else
                {
                    int temp = array[index];
                    array[index] = array[index - 1];
                    array[index - 1] = temp;
                    index--;
                }
            }
        }
    }
}