using System;
using System.Collections.Generic;
using System.Linq;

namespace BullyAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Process> processes = new List<Process>()
            {
                new Process(1),
                new Process(2),
                new Process(3),
                new Process(4),
                new Process(5)
            };
            // Simulate failure: Process 5 is down.
            Process? process5 = processes.Find(p => p.ProcessId == 5);
            if (process5 != null)
            {
                process5.IsAlive = false;
            }

            BullyElection election = new BullyElection(processes);
            Process? initiator = processes.Find(p => p.ProcessId == 2);
            if (initiator != null)
            {
                Console.WriteLine("Starting Bully election initiated by Process {0}.", initiator.ProcessId);
                Process? coordinator = initiator.StartElection(election);
                if (coordinator != null)
                {
                    Console.WriteLine("Coordinator elected: Process {0}.", coordinator.ProcessId);
                }
                else
                {
                    Console.WriteLine("No coordinator could be elected.");
                }
            }

            Console.WriteLine("Process states:");
            foreach (Process process in processes)
            {
                Console.WriteLine("Process {0}: IsAlive={1}, IsCoordinator={2}", process.ProcessId, process.IsAlive, process.IsCoordinator);
            }
        }
    }
}