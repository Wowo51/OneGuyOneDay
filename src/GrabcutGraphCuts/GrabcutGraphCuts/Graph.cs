using System.Collections.Generic;

namespace GrabcutGraphCuts
{
    public class Graph
    {
        public List<Node> Nodes { get; }
        public List<Edge> Edges { get; }

        public Graph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public void AddNode(Node node)
        {
            if (node != null)
            {
                Nodes.Add(node);
            }
        }

        public void AddEdge(Node from, Node to, double weight)
        {
            if (from != null && to != null)
            {
                Edges.Add(new Edge(from, to, weight));
            }
        }
    }

    public class Node
    {
        public int Id { get; }

        public Node(int id)
        {
            Id = id;
        }
    }

    public class Edge
    {
        public Node From { get; }
        public Node To { get; }
        public double Weight { get; }

        public Edge(Node from, Node to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}