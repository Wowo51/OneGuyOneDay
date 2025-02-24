using System;

namespace FordFulkersonAlgorithm
{
    public class FordFulkerson
    {
        public int ComputeMaxFlow(Graph graph, int source, int sink)
        {
            int maxFlow = 0;
            while (true)
            {
                bool[] visited = new bool[graph.VertexCount];
                int flow = DepthFirstSearch(graph, source, sink, int.MaxValue, visited);
                if (flow == 0)
                {
                    break;
                }

                maxFlow += flow;
            }

            return maxFlow;
        }

        private int DepthFirstSearch(Graph graph, int current, int sink, int flow, bool[] visited)
        {
            if (current == sink)
            {
                return flow;
            }

            visited[current] = true;
            foreach (Edge edge in graph.AdjacencyList[current])
            {
                if (!visited[edge.Target] && edge.ResidualCapacity() > 0)
                {
                    int minFlow = flow < edge.ResidualCapacity() ? flow : edge.ResidualCapacity();
                    int result = DepthFirstSearch(graph, edge.Target, sink, minFlow, visited);
                    if (result > 0)
                    {
                        edge.Flow += result;
                        if (edge.Reverse != null)
                        {
                            edge.Reverse.Flow -= result;
                        }

                        return result;
                    }
                }
            }

            return 0;
        }
    }
}