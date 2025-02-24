using System;
using System.Collections.Generic;
using BankersAlgorithm;

namespace BankersAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Define available resources; for example, two types of resources.
            int[] available = new int[]
            {
                3,
                3
            };
            // Define processes with their current allocation and maximum demand.
            // Process format: Process(id, allocation, maximum)
            Process[] processes = new Process[]
            {
                new Process("P0", new int[] { 0, 1 }, new int[] { 2, 2 }),
                new Process("P1", new int[] { 1, 0 }, new int[] { 2, 2 }),
                new Process("P2", new int[] { 1, 1 }, new int[] { 3, 3 })
            };
            // Compute the safe sequence using the Banker's algorithm.
            List<string> safeSequence = BankersAlgorithmSolver.FindSafeSequence(available, processes);
            if (safeSequence.Count > 0)
            {
                Console.WriteLine("The system is in a safe state. Safe sequence:");
                foreach (string process in safeSequence)
                {
                    Console.Write(process + " ");
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("The system is not in a safe state.");
            }
        }
    }
}