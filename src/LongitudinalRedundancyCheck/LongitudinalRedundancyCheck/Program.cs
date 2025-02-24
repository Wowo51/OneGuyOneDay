using System;
using System.Globalization;

namespace LongitudinalRedundancyCheck
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.Error.WriteLine("Provide hex values separated by space.");
                return 1;
            }

            byte[] data = new byte[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                byte value;
                if (byte.TryParse(args[i], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
                {
                    data[i] = value;
                }
                else
                {
                    Console.Error.WriteLine("Invalid input: " + args[i]);
                    return 1;
                }
            }

            byte lrc = LrcCalculator.ComputeLRC(data);
            Console.WriteLine("LRC: " + lrc.ToString("X2", CultureInfo.InvariantCulture));
            return 0;
        }
    }
}