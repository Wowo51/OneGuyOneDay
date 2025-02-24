using System;
using System.Collections.Generic;
using System.Linq;

namespace BullyAlgorithm
{
    public class BullyElection
    {
        public List<Process> Processes { get; set; }

        public BullyElection(List<Process> processes)
        {
            Processes = processes;
        }

        public Process? ElectCoordinator(Process initiator)
        {
            Console.WriteLine("Process {0} is starting the election.", initiator.ProcessId);
            List<Process> higherProcesses = Processes.FindAll(p => p.IsAlive && p.ProcessId > initiator.ProcessId);
            if (higherProcesses.Count > 0)
            {
                foreach (Process process in higherProcesses)
                {
                    Console.WriteLine("Process {0} received election message from Process {1}.", process.ProcessId, initiator.ProcessId);
                }
            }

            Process? coordinator = Processes.Where(p => p.IsAlive).OrderByDescending(p => p.ProcessId).FirstOrDefault();
            if (coordinator != null)
            {
                foreach (Process process in Processes)
                {
                    process.IsCoordinator = (process.ProcessId == coordinator.ProcessId);
                }

                Console.WriteLine("Process {0} is elected as coordinator.", coordinator.ProcessId);
            }
            else
            {
                Console.WriteLine("No available process to become coordinator.");
            }

            return coordinator;
        }
    }
}