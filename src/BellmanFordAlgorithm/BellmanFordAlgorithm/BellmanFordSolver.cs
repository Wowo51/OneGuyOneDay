using System;

namespace BellmanFordAlgorithm
{
    public class BellmanFordSolver
    {
        public static bool Solve(Graph graph, int source, out int[] distances, out int[] predecessors)
        {
            if (graph == null)
            {
                distances = new int[0];
                predecessors = new int[0];
                return false;
            }

            distances = new int[graph.VertexCount];
            predecessors = new int[graph.VertexCount];
            for (int i = 0; i < graph.VertexCount; i++)
            {
                distances[i] = int.MaxValue;
                predecessors[i] = -1;
            }

            distances[source] = 0;
            for (int i = 1; i <= graph.VertexCount - 1; i++)
            {
                bool changed = false;
                for (int j = 0; j < graph.Edges.Count; j++)
                {
                    Edge edge = graph.Edges[j];
                    if (distances[edge.Source] != int.MaxValue && distances[edge.Source] + edge.Weight < distances[edge.Target])
                    {
                        distances[edge.Target] = distances[edge.Source] + edge.Weight;
                        predecessors[edge.Target] = edge.Source;
                        changed = true;
                    }
                }

                if (!changed)
                {
                    break;
                }
            }

            for (int j = 0; j < graph.Edges.Count; j++)
            {
                Edge edge = graph.Edges[j];
                if (distances[edge.Source] != int.MaxValue && distances[edge.Source] + edge.Weight < distances[edge.Target])
                {
                    return false;
                }
            }

            return true;
        }
    }
}