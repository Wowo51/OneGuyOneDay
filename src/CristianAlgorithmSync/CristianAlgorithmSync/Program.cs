using System;

namespace CristianAlgorithmSync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ITimeServer timeServer = new TimeServer();
            CristianSynchronizer synchronizer = new CristianSynchronizer();
            DateTime syncTime = synchronizer.SynchronizeTime(timeServer);
            Console.WriteLine("Synchronized time: " + syncTime.ToString("O"));
        }
    }
}