using System.Collections.Generic;

namespace MarkAndSweep
{
    public class ObjectNode
    {
        public string Name { get; set; }
        public bool Marked { get; set; }
        public List<ObjectNode> Children { get; set; }

        public ObjectNode(string name)
        {
            Name = name;
            Marked = false;
            Children = new List<ObjectNode>();
        }

        public void AddChild(ObjectNode child)
        {
            if (child != null)
            {
                Children.Add(child);
            }
        }

        // Recursively mark this object and all objects reachable from it.
        public void MarkAll()
        {
            if (!Marked)
            {
                Marked = true;
                foreach (ObjectNode child in Children)
                {
                    child.MarkAll();
                }
            }
        }
    }
}