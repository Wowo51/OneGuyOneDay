using System;
using System.Collections.Generic;

namespace KargersAlgorithm
{
    public static class KargerAlgorithm
    {
        public static int FindMinCut(Graph graph)
        {
            Random random = new Random();
            Graph copyGraph = graph.Clone();
            while (copyGraph.Vertices.Count > 2)
            {
                int index = random.Next(copyGraph.Edges.Count);
                Edge edge = copyGraph.Edges[index];
                ContractEdge(copyGraph, edge);
            }

            return copyGraph.Edges.Count;
        }

        public static int FindMinCut(Graph graph, int iterations)
        {
            int bestCut = int.MaxValue;
            for (int i = 0; i < iterations; i++)
            {
                int cut = FindMinCut(graph);
                if (cut < bestCut)
                {
                    bestCut = cut;
                }
            }

            return bestCut;
        }

        private static void ContractEdge(Graph graph, Edge edge)
        {
            int u = edge.Vertex1;
            int v = edge.Vertex2;
            for (int i = 0; i < graph.Edges.Count; i++)
            {
                Edge currentEdge = graph.Edges[i];
                if (currentEdge.Vertex1 == v)
                {
                    currentEdge.Vertex1 = u;
                }

                if (currentEdge.Vertex2 == v)
                {
                    currentEdge.Vertex2 = u;
                }
            }

            graph.Edges.RemoveAll(e => e.Vertex1 == e.Vertex2);
            graph.Vertices.Remove(v);
        }
    }
}