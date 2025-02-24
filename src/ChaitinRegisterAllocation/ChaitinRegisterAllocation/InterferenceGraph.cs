using System;
using System.Collections.Generic;

namespace ChaitinRegisterAllocation
{
    public class InterferenceGraph
    {
        public List<GraphNode> Nodes { get; private set; }

        public InterferenceGraph()
        {
            this.Nodes = new List<GraphNode>();
        }

        public void AddNode(GraphNode node)
        {
            if (node != null && !this.Nodes.Contains(node))
            {
                this.Nodes.Add(node);
            }
        }

        public GraphNode? GetNode(string name)
        {
            foreach (GraphNode node in this.Nodes)
            {
                if (node.Name == name)
                {
                    return node;
                }
            }

            return null;
        }

        public void AddEdge(string name1, string name2)
        {
            GraphNode? node1 = GetNode(name1);
            GraphNode? node2 = GetNode(name2);
            if (node1 != null && node2 != null)
            {
                node1.AddNeighbor(node2);
            }
        }
    }
}