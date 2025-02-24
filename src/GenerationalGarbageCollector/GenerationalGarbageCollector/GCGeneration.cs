using System.Collections.Generic;

namespace GenerationalGarbageCollector
{
    public class GCGeneration
    {
        public int GenerationNumber { get; private set; }
        public List<GCObject> Objects { get; set; }

        public GCGeneration(int generationNumber)
        {
            GenerationNumber = generationNumber;
            Objects = new List<GCObject>();
        }
    }
}