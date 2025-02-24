using System;
using System.Collections.Generic;

namespace GirvanNewmanAlgorithm
{
    public static class CommunityDetection
    {
        public static List<List<string>> GirvanNewman(Graph graph)
        {
            List<List<string>> communities = graph.GetConnectedComponents();
            while (communities.Count == 1 && HasEdges(graph))
            {
                Dictionary<Tuple<string, string>, double> edgeBetweenness = ComputeEdgeBetweenness(graph);
                double maxBetweenness = -1;
                foreach (KeyValuePair<Tuple<string, string>, double> pair in edgeBetweenness)
                {
                    if (pair.Value > maxBetweenness)
                    {
                        maxBetweenness = pair.Value;
                    }
                }

                foreach (KeyValuePair<Tuple<string, string>, double> pair in edgeBetweenness)
                {
                    if (pair.Value == maxBetweenness)
                    {
                        graph.RemoveEdge(pair.Key.Item1, pair.Key.Item2);
                    }
                }

                communities = graph.GetConnectedComponents();
            }

            return communities;
        }

        private static bool HasEdges(Graph graph)
        {
            foreach (KeyValuePair<string, List<string>> pair in graph.AdjacencyList)
            {
                if (pair.Value.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static Dictionary<Tuple<string, string>, double> ComputeEdgeBetweenness(Graph graph)
        {
            Dictionary<Tuple<string, string>, double> edgeBetweenness = new Dictionary<Tuple<string, string>, double>();
            foreach (string s in graph.AdjacencyList.Keys)
            {
                Dictionary<string, int> dist = new Dictionary<string, int>();
                Dictionary<string, double> sigma = new Dictionary<string, double>();
                Dictionary<string, List<string>> pred = new Dictionary<string, List<string>>();
                foreach (string v in graph.AdjacencyList.Keys)
                {
                    dist[v] = -1;
                    sigma[v] = 0;
                    pred[v] = new List<string>();
                }

                dist[s] = 0;
                sigma[s] = 1;
                Queue<string> queue = new Queue<string>();
                List<string> stack = new List<string>();
                queue.Enqueue(s);
                while (queue.Count > 0)
                {
                    string v = queue.Dequeue();
                    stack.Add(v);
                    foreach (string w in graph.AdjacencyList[v])
                    {
                        if (dist[w] < 0)
                        {
                            dist[w] = dist[v] + 1;
                            queue.Enqueue(w);
                        }

                        if (dist[w] == dist[v] + 1)
                        {
                            sigma[w] += sigma[v];
                            pred[w].Add(v);
                        }
                    }
                }

                Dictionary<string, double> delta = new Dictionary<string, double>();
                foreach (string v in graph.AdjacencyList.Keys)
                {
                    delta[v] = 0;
                }

                for (int i = stack.Count - 1; i >= 0; i--)
                {
                    string w = stack[i];
                    foreach (string v in pred[w])
                    {
                        double contribution = (sigma[v] / sigma[w]) * (1 + delta[w]);
                        Tuple<string, string> edgeKey = GetEdgeKey(v, w);
                        if (!edgeBetweenness.ContainsKey(edgeKey))
                        {
                            edgeBetweenness[edgeKey] = 0;
                        }

                        edgeBetweenness[edgeKey] += contribution;
                        delta[v] += contribution;
                    }
                }
            }

            List<Tuple<string, string>> keys = new List<Tuple<string, string>>(edgeBetweenness.Keys);
            foreach (Tuple<string, string> key in keys)
            {
                edgeBetweenness[key] /= 2;
            }

            return edgeBetweenness;
        }

        private static Tuple<string, string> GetEdgeKey(string a, string b)
        {
            if (string.Compare(a, b) <= 0)
            {
                return new Tuple<string, string>(a, b);
            }
            else
            {
                return new Tuple<string, string>(b, a);
            }
        }
    }
}