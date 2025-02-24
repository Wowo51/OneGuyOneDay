using System;
using System.Text;
using System.Collections.Generic;

namespace LongDivisionAlgorithm
{
    public class LongDivision
    {
        public static string FormatDivision(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                return "Division by zero is undefined";
            }

            string dividendStr = dividend.ToString();
            if (dividend < divisor)
            {
                // When no subtraction steps occur, simply display dividend and a 0 quotient.
                return dividendStr + "\n" + divisor.ToString() + "|0";
            }

            string quotientStr = (dividend / divisor).ToString();
            List<string> lines = new List<string>();
            int current = 0;
            int index = 0;
            // First line: show full dividend with a leading underscore.
            lines.Add("_" + dividendStr);
            // Find the smallest leftmost segment that is >= divisor.
            while (index < dividendStr.Length && current < divisor)
            {
                current = current * 10 + (dividendStr[index] - '0');
                index++;
            }

            int firstSub = (current / divisor) * divisor;
            string currentStr = current.ToString();
            int offset = index - currentStr.Length;
            string firstSubStr = firstSub.ToString();
            // Second line: first subtraction result aligned and the quotient printed to the right.
            string secondLine = new string (' ', offset) + firstSubStr + new string (' ', dividendStr.Length - (offset + firstSubStr.Length)) + "|" + quotientStr;
            lines.Add(secondLine);
            // Third line: dashes under the first subtraction and dashes for the quotient.
            string thirdLine = new string (' ', offset) + new string ('-', firstSubStr.Length) + new string (' ', dividendStr.Length - (offset + firstSubStr.Length)) + "|" + new string ('-', quotientStr.Length);
            lines.Add(thirdLine);
            int remainder = current - firstSub;
            // Process each subsequent digit.
            while (index < dividendStr.Length)
            {
                remainder = remainder * 10 + (dividendStr[index] - '0');
                string remStr = remainder.ToString();
                int pos = index - remStr.Length + 1;
                if (remainder < divisor)
                {
                    lines.Add(new string (' ', pos) + "_" + remStr);
                    index++;
                    continue;
                }

                int sub = (remainder / divisor) * divisor;
                string currentLine = new string (' ', pos) + "_" + remStr;
                string subLine = new string (' ', pos) + sub.ToString();
                string dashLine = new string (' ', pos) + new string ('-', sub.ToString().Length);
                lines.Add(currentLine);
                lines.Add(subLine);
                lines.Add(dashLine);
                remainder -= sub;
                index++;
            }

            // Final line: the last remainder aligned to the right.
            string remainderStr = remainder.ToString();
            string finalLine = new string (' ', dividendStr.Length - remainderStr.Length) + remainderStr;
            lines.Add(finalLine);
            return string.Join("\n", lines);
        }
    }
}