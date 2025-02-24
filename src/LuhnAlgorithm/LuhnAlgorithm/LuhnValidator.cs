using System;

namespace LuhnAlgorithm
{
    public static class LuhnValidator
    {
        public static bool IsValid(string? number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }

            int sum = 0;
            bool alternate = false;
            for (int index = number.Length - 1; index >= 0; index--)
            {
                char digitChar = number[index];
                if (!char.IsDigit(digitChar))
                {
                    if (digitChar == ' ')
                    {
                        continue;
                    }

                    return false;
                }

                int digit = digitChar - '0';
                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }
    }
}