using System;
using System.Collections.Generic;

namespace BankersAlgorithm
{
    public class Process
    {
        public string Id { get; }
        public int[] Allocation { get; }
        public int[] Maximum { get; }

        public Process(string id, int[] allocation, int[] maximum)
        {
            Id = id;
            Allocation = allocation;
            Maximum = maximum;
        }

        // Calculates the resource Need for the process.
        public int[] Need()
        {
            int length = Maximum.Length;
            int[] need = new int[length];
            for (int i = 0; i < length; i++)
            {
                need[i] = Maximum[i] - Allocation[i];
            }

            return need;
        }
    }

    public static class BankersAlgorithmSolver
    {
        // Finds and returns a safe sequence of process IDs if the system is in a safe state;
        // otherwise, returns an empty list.
        public static List<string> FindSafeSequence(int[] available, Process[] processes)
        {
            int processCount = processes.Length;
            int resourceCount = available.Length;
            bool[] finish = new bool[processCount];
            int[] work = new int[resourceCount];
            for (int i = 0; i < resourceCount; i++)
            {
                work[i] = available[i];
            }

            List<string> safeSequence = new List<string>();
            bool progress = true;
            while (progress)
            {
                progress = false;
                for (int i = 0; i < processCount; i++)
                {
                    if (!finish[i])
                    {
                        int[] need = processes[i].Need();
                        bool canProceed = true;
                        for (int j = 0; j < resourceCount; j++)
                        {
                            if (need[j] > work[j])
                            {
                                canProceed = false;
                                break;
                            }
                        }

                        if (canProceed)
                        {
                            for (int j = 0; j < resourceCount; j++)
                            {
                                work[j] += processes[i].Allocation[j];
                            }

                            finish[i] = true;
                            safeSequence.Add(processes[i].Id);
                            progress = true;
                        }
                    }
                }
            }

            // If any process could not finish, the safe sequence is invalid.
            for (int i = 0; i < processCount; i++)
            {
                if (!finish[i])
                {
                    safeSequence.Clear();
                    break;
                }
            }

            return safeSequence;
        }
    }
}