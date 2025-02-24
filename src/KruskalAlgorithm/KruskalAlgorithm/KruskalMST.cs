using System;
using System.Collections.Generic;

namespace KruskalAlgorithm
{
    public static class KruskalMST
    {
        public static List<Edge> FindMinimumSpanningTree(Graph graph)
        {
            List<Edge> result = new List<Edge>();
            graph.Edges.Sort();
            DisjointSet disjointSet = new DisjointSet(graph.VerticesCount);
            int edgeCount = 0;
            int i = 0;
            while (edgeCount < graph.VerticesCount - 1 && i < graph.Edges.Count)
            {
                Edge nextEdge = graph.Edges[i];
                i++;
                int x = disjointSet.Find(nextEdge.Source);
                int y = disjointSet.Find(nextEdge.Destination);
                if (x != y)
                {
                    result.Add(nextEdge);
                    disjointSet.Union(x, y);
                    edgeCount++;
                }
            }

            return result;
        }
    }
}