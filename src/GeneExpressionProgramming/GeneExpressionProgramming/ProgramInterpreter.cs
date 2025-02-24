using System;

namespace GeneExpressionProgramming
{
    public static class ProgramInterpreter
    {
        public static double Execute(string gene)
        {
            double accumulator = 0.0;
            foreach (char instruction in gene)
            {
                int value = instruction - 'a' + 1;
                if (instruction <= 'm')
                {
                    accumulator += value;
                }
                else
                {
                    accumulator -= value;
                }
            }

            return accumulator;
        }
    }
}