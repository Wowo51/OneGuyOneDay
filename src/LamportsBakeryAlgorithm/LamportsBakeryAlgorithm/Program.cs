using System;
using System.Threading;

namespace LamportsBakeryAlgorithm
{
    public class Program
    {
        private static int threadCount = 5;
        private static BakeryLock bakeryLock = new BakeryLock(threadCount);
        private static int CriticalCount = 0;
        public static void Main(string[] args)
        {
            Thread[] threads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                int processId = i;
                threads[i] = new Thread(() => DoWork(processId));
                threads[i].Start();
            }

            for (int i = 0; i < threadCount; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine("Final count: " + CriticalCount);
        }

        private static void DoWork(int processId)
        {
            for (int i = 0; i < 10; i++)
            {
                bakeryLock.Lock(processId);
                int temp = CriticalCount;
                temp = temp + 1;
                CriticalCount = temp;
                bakeryLock.Unlock(processId);
                Thread.Sleep(1);
            }
        }
    }
}