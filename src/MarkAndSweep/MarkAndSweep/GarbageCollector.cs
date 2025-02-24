using System.Collections.Generic;

namespace MarkAndSweep
{
    public static class GarbageCollector
    {
        // Mark all objects reachable from the provided root nodes.
        public static void Mark(IEnumerable<ObjectNode> roots)
        {
            foreach (ObjectNode root in roots)
            {
                root.MarkAll();
            }
        }

        // Sweep through the heap and collect unmarked objects.
        public static List<ObjectNode> Sweep(List<ObjectNode> objects)
        {
            List<ObjectNode> alive = new List<ObjectNode>();
            foreach (ObjectNode obj in objects)
            {
                if (obj.Marked)
                {
                    alive.Add(obj);
                    // Reset mark for the next collection cycle.
                    obj.Marked = false;
                }
            }

            return alive;
        }
    }
}