using System.Threading;

namespace LamportsBakeryAlgorithm
{
    public class BakeryLock
    {
        private int numberOfProcesses;
        private bool[] choosing;
        private int[] number;
        public BakeryLock(int processCount)
        {
            numberOfProcesses = processCount;
            choosing = new bool[processCount];
            number = new int[processCount];
            for (int i = 0; i < processCount; i++)
            {
                choosing[i] = false;
                number[i] = 0;
            }
        }

        public void Lock(int processId)
        {
            choosing[processId] = true;
            int max = 0;
            for (int i = 0; i < numberOfProcesses; i++)
            {
                if (number[i] > max)
                {
                    max = number[i];
                }
            }

            number[processId] = max + 1;
            choosing[processId] = false;
            for (int j = 0; j < numberOfProcesses; j++)
            {
                if (j == processId)
                {
                    continue;
                }

                while (choosing[j])
                {
                    Thread.Yield();
                }

                while (number[j] != 0 && (number[j] < number[processId] || (number[j] == number[processId] && j < processId)))
                {
                    Thread.Yield();
                }
            }
        }

        public void Unlock(int processId)
        {
            number[processId] = 0;
        }
    }
}