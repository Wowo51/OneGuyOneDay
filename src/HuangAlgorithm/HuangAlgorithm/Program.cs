using System;

namespace HuangAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HuangTerminationDetector detector = new HuangTerminationDetector();
            detector.AddProcess(new DistributedProcess("P1"));
            detector.AddProcess(new DistributedProcess("P2"));
            detector.AddProcess(new DistributedProcess("P3"));
            detector.RunSimulation();
            bool terminationDetected = detector.CheckTermination();
            Console.WriteLine("Termination detected: " + terminationDetected.ToString());
            Console.WriteLine("Process statuses and message counts:");
            foreach (DistributedProcess proc in detector.Processes)
            {
                Console.WriteLine("Process " + proc.ProcessId + ": IsActive = " + proc.IsActive.ToString() + ", Sent = " + proc.SentMessages.ToString() + ", Received = " + proc.ReceivedMessages.ToString());
            }
        }
    }
}