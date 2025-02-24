using System;

namespace BogosortTest
{
    public static class TestDataGenerator
    {
        private static readonly Random s_random = new Random();
        public static int[] GenerateRandomIntArray(int size, int min, int max)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = s_random.Next(min, max + 1);
            }

            return array;
        }

        public static string[] GenerateRandomStringArray(int size, int stringLength)
        {
            string[] array = new string[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = GenerateRandomString(stringLength);
            }

            return array;
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            char[] strChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                int index = s_random.Next(0, chars.Length);
                strChars[i] = chars[index];
            }

            return new string (strChars);
        }
    }
}