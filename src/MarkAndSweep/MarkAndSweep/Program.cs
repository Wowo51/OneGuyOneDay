using System;
using System.Collections.Generic;

namespace MarkAndSweep
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create a simulated heap of objects.
            List<ObjectNode> heap = new List<ObjectNode>();
            // Create objects and build a simple reference graph.
            ObjectNode root1 = new ObjectNode("Root1");
            ObjectNode child1 = new ObjectNode("Child1");
            ObjectNode child2 = new ObjectNode("Child2");
            ObjectNode orphan = new ObjectNode("Orphan");
            root1.AddChild(child1);
            child1.AddChild(child2);
            heap.Add(root1);
            heap.Add(child1);
            heap.Add(child2);
            heap.Add(orphan);
            // Mark phase: mark reachable objects starting from a root set.
            GarbageCollector.Mark(new List<ObjectNode> { root1 });
            // Sweep phase: remove unmarked objects and reset marks.
            List<ObjectNode> surviving = GarbageCollector.Sweep(heap);
            Console.WriteLine("Surviving objects after garbage collection:");
            foreach (ObjectNode node in surviving)
            {
                Console.WriteLine(node.Name);
            }
        }
    }
}