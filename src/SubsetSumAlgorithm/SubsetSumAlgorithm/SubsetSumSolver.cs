using System;

namespace SubsetSumAlgorithm
{
    public class SubsetSumSolver
    {
        public static bool HasSubsetSum(int[] numbers, int target)
        {
            if (numbers == null)
            {
                return false;
            }

            return CheckSubset(numbers, target, 0, 0);
        }

        private static bool CheckSubset(int[] numbers, int target, int index, int current)
        {
            if (current == target)
            {
                return true;
            }

            if (index == numbers.Length)
            {
                return false;
            }

            bool include = CheckSubset(numbers, target, index + 1, current + numbers[index]);
            bool exclude = CheckSubset(numbers, target, index + 1, current);
            return include || exclude;
        }
    }
}