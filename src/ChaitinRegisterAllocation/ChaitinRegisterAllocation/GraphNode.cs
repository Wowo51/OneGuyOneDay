using System;
using System.Collections.Generic;

namespace ChaitinRegisterAllocation
{
    public class GraphNode
    {
        public string Name { get; private set; }
        public double Cost { get; set; }
        public List<GraphNode> Neighbors { get; private set; }
        public int? AssignedRegister { get; set; }

        public GraphNode(string name, double cost)
        {
            this.Name = name;
            this.Cost = cost;
            this.Neighbors = new List<GraphNode>();
            this.AssignedRegister = null;
        }

        public int Degree
        {
            get
            {
                return this.Neighbors.Count;
            }
        }

        public void AddNeighbor(GraphNode neighbor)
        {
            if (neighbor != null && neighbor != this && !this.Neighbors.Contains(neighbor))
            {
                this.Neighbors.Add(neighbor);
                if (!neighbor.Neighbors.Contains(this))
                {
                    neighbor.Neighbors.Add(this);
                }
            }
        }
    }
}