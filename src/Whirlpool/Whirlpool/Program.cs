using System;
using System.Security.Cryptography;
using System.Text;

namespace Whirlpool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter input for Whirlpool hash: ");
            string input = Console.ReadLine() ?? "";
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            WhirlpoolHashAlgorithm whirlpool = new WhirlpoolHashAlgorithm();
            byte[] hashBytes = whirlpool.ComputeHash(inputBytes);
            string hashHex = BitConverter.ToString(hashBytes).Replace("-", "");
            Console.WriteLine("Whirlpool hash: " + hashHex);
        }
    }
}