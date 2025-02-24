using System;

namespace LuhnModN
{
    public static class LuhnModNAlgorithm
    {
        private const string DefaultAllowed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static char ComputeCheckCharacter(string input, string allowed)
        {
            if (input == null)
            {
                input = "";
            }

            if (string.IsNullOrEmpty(allowed))
            {
                allowed = DefaultAllowed;
            }

            int n = allowed.Length;
            int sum = 0;
            int factor = 2;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                int digit = allowed.IndexOf(input[i]);
                if (digit < 0)
                {
                    digit = 0;
                }

                int addend = digit * factor;
                if (addend >= n)
                {
                    addend = addend - n + 1;
                }

                sum += addend;
                factor = (factor == 2) ? 1 : 2;
            }

            int check = (n - (sum % n)) % n;
            return allowed[check];
        }

        public static string AppendCheckCharacter(string input, string allowed)
        {
            return input + ComputeCheckCharacter(input, allowed);
        }

        public static bool Validate(string fullInput, string allowed)
        {
            if (string.IsNullOrEmpty(fullInput))
            {
                return false;
            }

            if (string.IsNullOrEmpty(allowed))
            {
                allowed = DefaultAllowed;
            }

            string body = fullInput.Substring(0, fullInput.Length - 1);
            char expected = ComputeCheckCharacter(body, allowed);
            return expected == fullInput[fullInput.Length - 1];
        }

        public static char ComputeCheckCharacter(string input)
        {
            return ComputeCheckCharacter(input, DefaultAllowed);
        }

        public static string AppendCheckCharacter(string input)
        {
            return AppendCheckCharacter(input, DefaultAllowed);
        }

        public static bool Validate(string fullInput)
        {
            return Validate(fullInput, DefaultAllowed);
        }
    }
}