using System;
using System.Threading;

namespace PetersonAlgorithm
{
    public class Program
    {
        private static int sharedCounter = 0;
        private static PetersonLock peterson = new PetersonLock();
        public static void Main(string[] args)
        {
            try
            {
                Thread thread0 = new Thread(new ThreadStart(Process0));
                Thread thread1 = new Thread(new ThreadStart(Process1));
                thread0.Start();
                thread1.Start();
                thread0.Join();
                thread1.Join();
                Console.WriteLine("Final counter: " + sharedCounter);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unexpected error in Main: " + ex.Message);
            }
        }

        private static void Process0()
        {
            try
            {
                for (int i = 0; i < 1000; i++)
                {
                    peterson.Lock(0);
                    int temp = sharedCounter;
                    temp = temp + 1;
                    sharedCounter = temp;
                    peterson.Unlock(0);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error in Process0: " + ex.Message);
            }
        }

        private static void Process1()
        {
            try
            {
                for (int i = 0; i < 1000; i++)
                {
                    peterson.Lock(1);
                    int temp = sharedCounter;
                    temp = temp + 1;
                    sharedCounter = temp;
                    peterson.Unlock(1);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error in Process1: " + ex.Message);
            }
        }
    }
}