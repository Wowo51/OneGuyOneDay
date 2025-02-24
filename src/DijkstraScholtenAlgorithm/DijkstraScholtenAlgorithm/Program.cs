using System;

namespace DijkstraScholtenAlgorithm
{
    class Program
    {
        public static void Main(string[] args)
        {
            ProcessNode root = new ProcessNode("Root");
            root.Activate(null);
            ProcessNode child = new ProcessNode("Child");
            ProcessNode grandchild = new ProcessNode("Grandchild");
            // Simulate work propagation.
            root.SendWork(child);
            child.SendWork(grandchild);
            // Each node completes its work.
            grandchild.WorkDone();
            child.WorkDone();
            root.WorkDone();
            Console.WriteLine("Termination detected at Root: " + (!root.Active && root.Credit == 0));
        }
    }
}