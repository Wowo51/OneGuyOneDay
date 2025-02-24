using System;
using System.Collections.Generic;

namespace GrabcutGraphCuts
{
    public static class GraphCutMinimizer
    {
        public static bool[, ] MinCut(ImageData image)
        {
            int height = image.Height;
            int width = image.Width;
            int pixelCount = height * width;
            int totalVertices = pixelCount + 2;
            int source = totalVertices - 2;
            int sink = totalVertices - 1;
            // Initialize graph as an adjacency list of FlowEdges.
            List<FlowEdge>[] graph = new List<FlowEdge>[totalVertices];
            for (int i = 0; i < totalVertices; i++)
            {
                graph[i] = new List<FlowEdge>();
            }

            // Local function to add an edge and its reverse edge.
            void AddEdge(int u, int v, double capacity)
            {
                FlowEdge forward = new FlowEdge(u, v, capacity);
                FlowEdge reverse = new FlowEdge(v, u, 0);
                forward.Reverse = reverse;
                reverse.Reverse = forward;
                graph[u].Add(forward);
                graph[v].Add(reverse);
            }

            // Build data term edges for each pixel.
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int index = i * width + j;
                    // Simple heuristic: left half likely foreground.
                    double sourceCapacity = (j < width / 2) ? 10.0 : 1.0;
                    double sinkCapacity = (j < width / 2) ? 1.0 : 10.0;
                    AddEdge(source, index, sourceCapacity);
                    AddEdge(index, sink, sinkCapacity);
                }
            }

            // Build smoothness term with 4-neighborhood connectivity.
            double smoothness = 3.0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int index = i * width + j;
                    // Right neighbor.
                    if (j + 1 < width)
                    {
                        int neighbor = i * width + (j + 1);
                        AddEdge(index, neighbor, smoothness);
                        AddEdge(neighbor, index, smoothness);
                    }

                    // Down neighbor.
                    if (i + 1 < height)
                    {
                        int neighbor = (i + 1) * width + j;
                        AddEdge(index, neighbor, smoothness);
                        AddEdge(neighbor, index, smoothness);
                    }
                }
            }

            // Compute max flow on the graph.
            double maxFlow = MaxFlowCalculator.CalculateMaxFlow(source, sink, graph);
            // Identify the min-cut: perform DFS from source on the residual graph.
            bool[] reachable = new bool[totalVertices];
            DFS(source, graph, reachable);
            // Build segmentation: pixels reachable from the source are marked foreground.
            bool[, ] segmentation = new bool[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int index = i * width + j;
                    segmentation[i, j] = reachable[index];
                }
            }

            return segmentation;
        }

        private static void DFS(int v, List<FlowEdge>[] graph, bool[] visited)
        {
            visited[v] = true;
            foreach (FlowEdge edge in graph[v])
            {
                int w = edge.To;
                if (!visited[w] && edge.ResidualCapacityTo(w) > 0)
                {
                    DFS(w, graph, visited);
                }
            }
        }
    }
}