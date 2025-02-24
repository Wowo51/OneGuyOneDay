using System;
using System.Numerics;

namespace DiffieHellmanExchange
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DiffieHellmanParticipant alice = new DiffieHellmanParticipant();
            DiffieHellmanParticipant bob = new DiffieHellmanParticipant();
            BigInteger aliceSharedSecret = alice.ComputeSharedSecret(bob.PublicKey);
            BigInteger bobSharedSecret = bob.ComputeSharedSecret(alice.PublicKey);
            Console.WriteLine("Alice Public Key: " + alice.PublicKey);
            Console.WriteLine("Bob Public Key: " + bob.PublicKey);
            Console.WriteLine("Alice Shared Secret: " + aliceSharedSecret);
            Console.WriteLine("Bob Shared Secret: " + bobSharedSecret);
            Console.WriteLine("Exchange " + (aliceSharedSecret == bobSharedSecret ? "succeeded" : "failed"));
        }
    }
}