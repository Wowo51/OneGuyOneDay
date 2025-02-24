using System;
using System.Collections.Generic;

namespace GenerationalGarbageCollector
{
    public class GarbageCollector
    {
        public List<GCGeneration> Generations { get; private set; }

        public GarbageCollector()
        {
            Generations = new List<GCGeneration>
            {
                new GCGeneration(0),
                new GCGeneration(1),
                new GCGeneration(2)
            };
        }

        public void AllocateObject(GCObject gcObject)
        {
            if (gcObject != null)
            {
                Generations[0].Objects.Add(gcObject);
            }
        }

        public void CollectGen0(Func<GCObject, bool> isAlive)
        {
            if (isAlive == null)
            {
                isAlive = delegate (GCObject o)
                {
                    return true;
                };
            }

            GCGeneration gen0 = Generations[0];
            List<GCObject> survivors = new List<GCObject>();
            foreach (GCObject obj in gen0.Objects)
            {
                if (isAlive(obj))
                {
                    survivors.Add(obj);
                }
            }

            Generations[1].Objects.AddRange(survivors);
            gen0.Objects.Clear();
        }

        public void CollectGen1(Func<GCObject, bool> isAlive)
        {
            if (isAlive == null)
            {
                isAlive = delegate (GCObject o)
                {
                    return true;
                };
            }

            GCGeneration gen1 = Generations[1];
            List<GCObject> survivors = new List<GCObject>();
            foreach (GCObject obj in gen1.Objects)
            {
                if (isAlive(obj))
                {
                    survivors.Add(obj);
                }
            }

            Generations[2].Objects.AddRange(survivors);
            gen1.Objects.Clear();
        }

        public void CollectGen2(Func<GCObject, bool> isAlive)
        {
            if (isAlive == null)
            {
                isAlive = delegate (GCObject o)
                {
                    return true;
                };
            }

            GCGeneration gen2 = Generations[2];
            List<GCObject> survivors = new List<GCObject>();
            foreach (GCObject obj in gen2.Objects)
            {
                if (isAlive(obj))
                {
                    survivors.Add(obj);
                }
            }

            gen2.Objects = survivors;
        }

        public void Collect(Func<GCObject, bool> isAlive)
        {
            CollectGen0(isAlive);
            CollectGen1(isAlive);
            CollectGen2(isAlive);
        }
    }
}