namespace GnomeSort
{
    using System;

    public class Program
    {
        public static void Main()
        {
            int[] numbers = new int[]
            {
                34,
                2,
                78,
                1,
                56,
                9
            };
            Console.WriteLine("Before sorting: " + string.Join(", ", numbers));
            GnomeSorter.Sort(numbers);
            Console.WriteLine("After sorting: " + string.Join(", ", numbers));
        }
    }
}