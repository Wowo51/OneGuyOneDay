using System;

namespace GenerationalGarbageCollector
{
    public static class Program
    {
        public static void Main()
        {
            GarbageCollector gc = new GarbageCollector();
            GCObject obj1 = new GCObject("Object1");
            GCObject obj2 = new GCObject("Object2");
            GCObject obj3 = new GCObject("Object3");
            gc.AllocateObject(obj1);
            gc.AllocateObject(obj2);
            gc.AllocateObject(obj3);
            obj2.IsReachable = false;
            gc.Collect(o => o.IsReachable);
            for (int i = 0; i < gc.Generations.Count; i++)
            {
                Console.WriteLine("Generation " + i + " count: " + gc.Generations[i].Objects.Count);
            }
        }
    }
}