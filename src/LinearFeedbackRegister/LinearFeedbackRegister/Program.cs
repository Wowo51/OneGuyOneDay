using System;

namespace LinearFeedbackRegister
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Lfsr lfsr = new Lfsr(0xACE1, 0xB400);
            for (int i = 0; i < 10; i++)
            {
                int nextValue = lfsr.Next();
                System.Console.WriteLine("LFSR next value: " + nextValue);
            }
        }
    }
}