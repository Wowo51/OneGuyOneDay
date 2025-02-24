using System;
using System.IO;
using System.Numerics;
using DiffieHellmanExchange;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiffieHellmanExchangeTest
{
    [TestClass]
    public class DiffieHellmanExchangeTests
    {
        [TestMethod]
        public void TestSharedSecret_WithDefaultParticipants()
        {
            DiffieHellmanParticipant alice = new DiffieHellmanParticipant();
            DiffieHellmanParticipant bob = new DiffieHellmanParticipant();
            System.Numerics.BigInteger aliceSecret = alice.ComputeSharedSecret(bob.PublicKey);
            System.Numerics.BigInteger bobSecret = bob.ComputeSharedSecret(alice.PublicKey);
            Assert.AreEqual(aliceSecret, bobSecret, "Shared secrets should match");
        }

        [TestMethod]
        public void TestCustomParameters_SmallPrime()
        {
            System.Numerics.BigInteger prime = new System.Numerics.BigInteger(3);
            System.Numerics.BigInteger generator = new System.Numerics.BigInteger(2);
            DiffieHellmanParticipant participant1 = new DiffieHellmanParticipant(prime, generator);
            DiffieHellmanParticipant participant2 = new DiffieHellmanParticipant(prime, generator);
            Assert.AreEqual(new System.Numerics.BigInteger(2), participant1.PublicKey, "Public key calculated correctly for small prime");
            System.Numerics.BigInteger secret1 = participant1.ComputeSharedSecret(participant2.PublicKey);
            System.Numerics.BigInteger secret2 = participant2.ComputeSharedSecret(participant1.PublicKey);
            Assert.AreEqual(secret1, secret2, "Shared secrets for small prime should match");
            Assert.AreEqual(new System.Numerics.BigInteger(2), secret1, "Shared secret should be 2 for small prime case");
        }

        [TestMethod]
        public void TestCustomParameters_LargerValues()
        {
            System.Numerics.BigInteger prime = new System.Numerics.BigInteger(97);
            System.Numerics.BigInteger generator = new System.Numerics.BigInteger(5);
            DiffieHellmanParticipant alice = new DiffieHellmanParticipant(prime, generator);
            DiffieHellmanParticipant bob = new DiffieHellmanParticipant(prime, generator);
            System.Numerics.BigInteger secret1 = alice.ComputeSharedSecret(bob.PublicKey);
            System.Numerics.BigInteger secret2 = bob.ComputeSharedSecret(alice.PublicKey);
            Assert.AreEqual(secret1, secret2, "Shared secrets should match for larger prime");
        }

        [TestMethod]
        public void TestProgramMain_Output()
        {
            using (StringWriter sw = new StringWriter())
            {
                TextWriter originalOut = Console.Out;
                Console.SetOut(sw);
                Program.Main(new string[] { });
                Console.SetOut(originalOut);
                string output = sw.ToString();
                StringAssert.Contains(output, "succeeded", "Program should indicate a successful exchange");
            }
        }

        [TestMethod]
        public void TestMultipleExchanges()
        {
            int iterations = 10;
            for (int i = 0; i < iterations; i++)
            {
                DiffieHellmanParticipant alice = new DiffieHellmanParticipant();
                DiffieHellmanParticipant bob = new DiffieHellmanParticipant();
                System.Numerics.BigInteger secret1 = alice.ComputeSharedSecret(bob.PublicKey);
                System.Numerics.BigInteger secret2 = bob.ComputeSharedSecret(alice.PublicKey);
                Assert.AreEqual(secret1, secret2, "Iteration " + i.ToString() + ": Shared secrets do not match");
            }
        }
    }
}