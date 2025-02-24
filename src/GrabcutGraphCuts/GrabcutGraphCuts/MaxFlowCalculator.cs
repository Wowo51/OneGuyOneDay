using System.Collections.Generic;

namespace GrabcutGraphCuts
{
    public class MaxFlowCalculator
    {
        public static double CalculateMaxFlow(int source, int sink, List<FlowEdge>[] graph)
        {
            int vertexCount = graph.Length;
            double maxFlow = 0.0;
            while (true)
            {
                FlowEdge[] edgeTo = new FlowEdge[vertexCount];
                bool[] visited = new bool[vertexCount];
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(source);
                visited[source] = true;
                while (queue.Count > 0 && !visited[sink])
                {
                    int v = queue.Dequeue();
                    foreach (FlowEdge edge in graph[v])
                    {
                        int w = edge.To;
                        if (!visited[w] && edge.ResidualCapacityTo(w) > 0)
                        {
                            visited[w] = true;
                            edgeTo[w] = edge;
                            queue.Enqueue(w);
                        }
                    }
                }

                if (!visited[sink])
                {
                    break;
                }

                double bottle = double.MaxValue;
                for (int v = sink; v != source;)
                {
                    FlowEdge edge = edgeTo[v];
                    bottle = System.Math.Min(bottle, edge.ResidualCapacityTo(v));
                    v = edge.From;
                }

                for (int v = sink; v != source;)
                {
                    FlowEdge edge = edgeTo[v];
                    edge.AddResidualFlowTo(v, bottle);
                    v = edge.From;
                }

                maxFlow += bottle;
            }

            return maxFlow;
        }
    }
}