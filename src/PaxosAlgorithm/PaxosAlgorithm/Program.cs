using System;
using System.Collections.Generic;

namespace PaxosAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<PaxosAcceptor> acceptors = new List<PaxosAcceptor>();
            acceptors.Add(new PaxosAcceptor());
            acceptors.Add(new PaxosAcceptor());
            acceptors.Add(new PaxosAcceptor());
            PaxosProposer proposer = new PaxosProposer(1, "ValueA", acceptors);
            bool success = proposer.RunProposal();
            if (success)
            {
                string learned = PaxosLearner.Learn(acceptors);
                Console.WriteLine("Consensus reached: " + learned);
            }
            else
            {
                Console.WriteLine("Consensus failed.");
            }
        }
    }
}