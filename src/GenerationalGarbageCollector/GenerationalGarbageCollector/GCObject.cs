using System;

namespace GenerationalGarbageCollector
{
    public class GCObject
    {
        public string Name { get; set; }
        public bool IsReachable { get; set; }

        public GCObject(string name)
        {
            this.Name = name;
            this.IsReachable = true;
        }
    }
}