using System;
using System.Numerics;

namespace Rsa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BigInteger p = new BigInteger(61);
            BigInteger q = new BigInteger(53);
            BigInteger e = new BigInteger(17);
            RsaAlgorithm rsa = new RsaAlgorithm(p, q, e);
            BigInteger message = new BigInteger(65);
            BigInteger cipher = rsa.Encrypt(message);
            BigInteger decrypted = rsa.Decrypt(cipher);
            Console.WriteLine("Original message: " + message);
            Console.WriteLine("Encrypted message: " + cipher);
            Console.WriteLine("Decrypted message: " + decrypted);
        }
    }
}