using System;
using System.Collections.Generic;

namespace PrimsAlgorithm
{
    public static class PrimAlgorithm
    {
        public static List<Edge> ComputeMST(Graph graph)
        {
            if (graph == null)
            {
                return new List<Edge>();
            }

            List<Edge> mstEdges = new List<Edge>();
            if (graph.Vertices.Count == 0)
            {
                return mstEdges;
            }

            HashSet<Vertex> treeVertices = new HashSet<Vertex>();
            // Start with the first vertex in the graph
            treeVertices.Add(graph.Vertices[0]);
            while (treeVertices.Count < graph.Vertices.Count)
            {
                Edge? minEdge = null;
                double minWeight = double.PositiveInfinity;
                foreach (Edge edge in graph.Edges)
                {
                    bool isFromInTree = treeVertices.Contains(edge.From);
                    bool isToInTree = treeVertices.Contains(edge.To);
                    if (isFromInTree && !isToInTree)
                    {
                        if (edge.Weight < minWeight)
                        {
                            minWeight = edge.Weight;
                            minEdge = edge;
                        }
                    }
                    else if (isToInTree && !isFromInTree)
                    {
                        if (edge.Weight < minWeight)
                        {
                            minWeight = edge.Weight;
                            minEdge = edge;
                        }
                    }
                }

                if (minEdge == null)
                {
                    break;
                }

                mstEdges.Add(minEdge);
                if (treeVertices.Contains(minEdge.From) && !treeVertices.Contains(minEdge.To))
                {
                    treeVertices.Add(minEdge.To);
                }
                else if (treeVertices.Contains(minEdge.To) && !treeVertices.Contains(minEdge.From))
                {
                    treeVertices.Add(minEdge.From);
                }
            }

            return mstEdges;
        }
    }
}