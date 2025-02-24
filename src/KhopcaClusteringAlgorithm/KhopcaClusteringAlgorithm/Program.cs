using System;
using System.Threading;

namespace KhopcaClusteringAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int numNodes = 20;
            double communicationRange = 30.0;
            NetworkSimulator simulator = new NetworkSimulator(numNodes, communicationRange);
            simulator.UpdateNeighbors();
            KHOPCAAlgorithm algorithm = new KHOPCAAlgorithm(simulator.Nodes);
            for (int i = 0; i < 5; i++)
            {
                algorithm.RunIteration();
                Console.WriteLine("Iteration " + (i + 1) + ":");
                foreach (KhopcaClusteringAlgorithm.Node node in simulator.Nodes)
                {
                    Console.WriteLine(node.ToString());
                }

                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}