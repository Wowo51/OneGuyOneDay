using System;
using System.Collections.Generic;

namespace PrimsAlgorithm
{
    public class Vertex
    {
        public int Id { get; }

        public Vertex(int id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Vertex other)
            {
                return Id == other.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    public class Edge
    {
        public Vertex From { get; }
        public Vertex To { get; }
        public double Weight { get; }

        public Edge(Vertex from, Vertex to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }

    public class Graph
    {
        public List<Vertex> Vertices { get; }
        public List<Edge> Edges { get; }

        public Graph()
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
        }

        public void AddVertex(Vertex vertex)
        {
            if (vertex != null && !Vertices.Contains(vertex))
            {
                Vertices.Add(vertex);
            }
        }

        public void AddEdge(Edge edge)
        {
            if (edge != null)
            {
                Edges.Add(edge);
                if (!Vertices.Contains(edge.From))
                {
                    Vertices.Add(edge.From);
                }

                if (!Vertices.Contains(edge.To))
                {
                    Vertices.Add(edge.To);
                }
            }
        }
    }
}