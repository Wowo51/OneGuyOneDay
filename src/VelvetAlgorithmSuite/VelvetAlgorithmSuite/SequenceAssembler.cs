using System;
using System.Collections.Generic;
using System.Linq;

namespace VelvetAlgorithmSuite
{
    public class SequenceAssembler
    {
        public string Assemble(IEnumerable<string> reads, int k)
        {
            if (reads == null)
            {
                return "";
            }

            DeBruijnGraph deBruijnGraph = new DeBruijnGraph(k);
            foreach (string read in reads)
            {
                if (!string.IsNullOrEmpty(read))
                {
                    deBruijnGraph.AddSequence(read);
                }
            }

            Dictionary<string, List<string>> graphCopy = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> originalGraph = deBruijnGraph.GetGraph();
            foreach (KeyValuePair<string, List<string>> kvp in originalGraph)
            {
                graphCopy[kvp.Key] = new List<string>(kvp.Value);
            }

            return FindEulerianPath(graphCopy, k);
        }

        private string FindEulerianPath(Dictionary<string, List<string>> graph, int k)
        {
            Dictionary<string, int> indegree = new Dictionary<string, int>();
            Dictionary<string, int> outdegree = new Dictionary<string, int>();
            foreach (KeyValuePair<string, List<string>> kvp in graph)
            {
                string node = kvp.Key;
                outdegree[node] = kvp.Value.Count;
                if (!indegree.ContainsKey(node))
                {
                    indegree[node] = 0;
                }

                foreach (string neighbor in kvp.Value)
                {
                    if (indegree.ContainsKey(neighbor))
                    {
                        indegree[neighbor] = indegree[neighbor] + 1;
                    }
                    else
                    {
                        indegree[neighbor] = 1;
                    }

                    if (!outdegree.ContainsKey(neighbor))
                    {
                        outdegree[neighbor] = 0;
                    }
                }
            }

            string? start = null;
            foreach (KeyValuePair<string, int> kvp in outdegree)
            {
                string node = kvp.Key;
                int outd = kvp.Value;
                int ind = (indegree.ContainsKey(node) ? indegree[node] : 0);
                if (outd - ind == 1)
                {
                    start = node;
                    break;
                }
            }

            if (start == null)
            {
                foreach (KeyValuePair<string, int> kvp in outdegree)
                {
                    if (kvp.Value > 0)
                    {
                        start = kvp.Key;
                        break;
                    }
                }
            }

            if (start == null)
            {
                return "";
            }

            Stack<string> stack = new Stack<string>();
            List<string> path = new List<string>();
            string current = start;
            while (stack.Count > 0 || (graph.ContainsKey(current) && graph[current].Count > 0))
            {
                if (graph.ContainsKey(current) && graph[current].Count > 0)
                {
                    stack.Push(current);
                    string next = graph[current][0];
                    graph[current].RemoveAt(0);
                    current = next;
                }
                else
                {
                    path.Add(current);
                    if (stack.Count > 0)
                    {
                        current = stack.Pop();
                    }
                }
            }

            path.Add(current);
            path.Reverse();
            if (path.Count == 0)
            {
                return "";
            }

            string assembled = path[0];
            for (int i = 1; i < path.Count; i++)
            {
                assembled += path[i][path[i].Length - 1];
            }

            return assembled;
        }
    }
}